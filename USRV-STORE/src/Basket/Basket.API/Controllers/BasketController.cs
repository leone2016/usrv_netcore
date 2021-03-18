using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Basket.API.Entities;
using Basket.API.Repositories.Interfaces;
using EventBusRabbitMq.Common;
using EventBusRabbitMq.Events;
using EventBusRabbitMq.Producer;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;
        private readonly IMapper _mapper;
        private readonly EventBusRabbitMqProducer _eventBus;
        public BasketController(IBasketRepository repository, IMapper mapper, EventBusRabbitMqProducer eventBus)
        {
            _repository = repository;
            _mapper = mapper;
            _eventBus = eventBus;
        }

        [HttpGet]
        [ProducesResponseType(typeof(BasketCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<BasketCart>> GetBasketCart(string userName)
        {
            var basket =await _repository.GetBasket(userName);

            return Ok(basket ?? new BasketCart(userName));
        }
        
        [HttpPost]
        [ProducesResponseType(typeof(BasketCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<BasketCart>> UpdateBasketCart([FromBody] BasketCart basketCart)
        {
            var basket =await _repository.UpdateBasket(basketCart);

            return Ok(basket);
        }   
        
        [HttpDelete("{userName}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<BasketCart>> DeleteBasketCart(string userName)
        {
            return Ok(await _repository.DeleteBasket(userName));
        }
        
        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<BasketCart>> Checkout( [FromBody] BasketCheckout basketCheckout)
        {
           // get total price of basket
           // remove the basket
           // send checkout event to rabbitMQ

           var basket = await _repository.GetBasket(basketCheckout.UserName);
           if(basket == null)
               return BadRequest();

           var basketRemoved = await _repository.DeleteBasket(basket.UserName);
           
           if (!basketRemoved)
               return BadRequest();

           var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
           eventMessage.RequestId = Guid.NewGuid();
           eventMessage.TotalPrice = basket.TotalPrice;

           try
           {
                _eventBus.PublishBasketCheckout(EventBusConstants.BasketCheckoutQueue, eventMessage);
           }
           catch (Exception e)
           {
               Console.WriteLine(e);
               throw;
           }

           return Accepted();
        }
    }
}