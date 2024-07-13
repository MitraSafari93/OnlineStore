using Application;
using Integration;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace OnlineStore.API
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService prodcutService)
        {
            _productService = prodcutService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductRequestModel product)
        {
            try 
            {
                await _productService.CreateProduct(product);
                return Ok();
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> IncreaseInventoryCount(int productId, int inventoryCount) 
        {
            try 
            {
                await _productService.UpdateInventoryCount(productId, inventoryCount);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct(int id)
        {
            try 
            {
                var product = await _productService.GetProductById(id);
                return Ok(product);
            }
            catch (Exception ex) 
            {
                return NotFound(ex.Message);
            }

        }
    }
}
