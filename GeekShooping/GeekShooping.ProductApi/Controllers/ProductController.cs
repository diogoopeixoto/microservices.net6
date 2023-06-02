using GeekShooping.ProductApi.Data.ValueObjects;
using GeekShooping.ProductApi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeekShooping.ProductApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductVO>>> FindAll()
        {
            var products = await _repository.FindAll();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductVO>> FindById(long id)
        {
            var product = await _repository.FindById(id);
            if (product == null) return NotFound();
            return Ok(product);
        }


        [HttpPost]
        public async Task<ActionResult<ProductVO>> Create(ProductVO vO)
        {
            if (vO == null) return BadRequest();
            var product = await _repository.Create(vO);
            
            return Ok(vO);
        }
        
        [HttpPut]
        public async Task<ActionResult<ProductVO>> Update(ProductVO vO)
        {
            if (vO == null) return BadRequest();
            var product = await _repository.Update(vO);
            
            return Ok(vO);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            var status = await _repository.DeleteById(id);
            if (!status) return BadRequest();
            

            return Ok(status);
        }

    }
}
