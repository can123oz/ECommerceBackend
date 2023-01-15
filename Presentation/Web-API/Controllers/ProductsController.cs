using Application.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductReadRepository _productRead;
        private readonly IProductWriteRepository _productWrite;


        public ProductsController(IProductReadRepository productRead, IProductWriteRepository productWrite)
        {
            _productRead = productRead;
            _productWrite = productWrite;
        }

        [HttpGet("Test")]
        public async Task<IActionResult> Test()
        {
            //await _productWrite.AddRangeAsync(new()
            //{
            //    new() { Id = Guid.NewGuid(), Name = "Product1", Price = 10, Stock = 100, CreatedDate = DateTime.UtcNow },
            //    new() { Id = Guid.NewGuid(), Name = "Product2", Price = 20, Stock = 200, CreatedDate = DateTime.UtcNow },
            //    new() { Id = Guid.NewGuid(), Name = "Product3", Price = 30, Stock = 300, CreatedDate = DateTime.UtcNow },
            //});
            //await _productWrite.SaveAsync();
            var products = _productRead.GetAll();
            return Ok();
        }


        [HttpGet("Test/{id}")]
        public async Task<IActionResult> Test(string id)
        {
            if (id == "1")
            {
                Product product = await _productRead.GetByIdAsync("26530898-ce7b-4721-aaef-5afd0a58201d");
                return Ok(product);
            }
            else
            {
                Product product = await _productRead.GetByIdAsync("26530898-ce7b-4721-aaef-5afd0a58201d", false);
                return Ok(product);
            }
        }
    }
}