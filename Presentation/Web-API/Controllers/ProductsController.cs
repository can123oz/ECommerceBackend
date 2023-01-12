using Application.Repositories;
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
        public async void Test() 
        {
            await _productWrite.AddRangeAsync(new()
            {
                new() { Id = Guid.NewGuid(), Name = "Product1", Price = 10, Stock = 100, CreatedDate = DateTime.UtcNow },
                new() { Id = Guid.NewGuid(), Name = "Product2", Price = 20, Stock = 200, CreatedDate = DateTime.UtcNow },
                new() { Id = Guid.NewGuid(), Name = "Product3", Price = 30, Stock = 300, CreatedDate = DateTime.UtcNow },
            });
            await _productWrite.SaveAsync();
        }

    }
}