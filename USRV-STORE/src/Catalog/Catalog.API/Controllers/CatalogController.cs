using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Catalog.API.Entities;
using Catalog.API.Repositories.Interfaces;
using DnsClient.Internal;
using Microsoft.Extensions.Logging;

namespace Catalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepository repository, ILogger<CatalogController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Product>), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _repository.GetProducts();
            return Ok(products);
        }

        //validate that the para id should be 24 characters length 
        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            var product = await _repository.GetProduct(id);
            if (product == null)
            {
                _logger.LogError($"Product with id: {id}, not found");
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Product>), (int) HttpStatusCode.OK)]
        [Route("[action]/{category}")]
        public async Task<ActionResult<List<Product>>> GetProductByCategory(string category)
        {
            var products = await _repository.GetProductsByCategory(category);
            return Ok(products);
        }


        [HttpPost]
        [ProducesResponseType(typeof(Product), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            await _repository.Create(product);
            // after creating a product,we are re-routing these GetProduct function and it's again retrieving products
            // with id and returning our products 
            return CreatedAtRoute("GetProduct", new {id = product.Id}, product);
        }
        
        [HttpPut]
        [ProducesResponseType(typeof(Product), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> UpdateProduct([FromBody] Product product)
        {
            return Ok(await _repository.Update(product));
        }
        
        [HttpDelete("id:length(24)")]
        [ProducesResponseType(typeof(Product), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> DeleteProduct(string id)
        {
            return Ok(await _repository.Delete(id));
        }
    }
}