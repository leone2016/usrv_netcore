using System.Net;
using System.Threading.Tasks;
using Basket.API.Entities;
using Basket.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;

        public BasketController(IBasketRepository repository)
        {
            _repository = repository;
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
    }
}