using Application.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //private readonly IProductService _productService;

        //public ProductsController(IProductService productService) 
        //{
        //    _productService= productService;
        //}

        [HttpGet("GetProducts")]
        public IActionResult GetProducts() 
        {
            return Ok("ok 123");
            //var result = _productService.GetProducts();
            //return Ok(result);
        }
    }
}