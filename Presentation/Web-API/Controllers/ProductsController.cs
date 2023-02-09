using Application.Repositories;
using Application.RequestParameters;
using Application.ViewModels.Products;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Caching.Memory;
using System.Net;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductReadRepository _productRead;
        private readonly IProductWriteRepository _productWrite;



        public ProductsController(IProductReadRepository productRead,
            IProductWriteRepository productWrite)
        {
            _productRead = productRead;
            _productWrite = productWrite;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] Pagination pagination)
        {
            Thread.Sleep(1000);
            var allProducts = _productRead.GetAll(false);
            var totalCount = allProducts.Count();
            var products = allProducts.Skip(pagination.Page * pagination.Size)
                .Take(pagination.Size)
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Price,
                    p.Stock,
                    p.CreatedDate,
                    p.UpdatedDate
                });
            return Ok(new { products, totalCount }); //no need to track, only for reading...
        }
            
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var product = await _productRead.GetByIdAsync(id, false);
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Post(VM_Create_Product model)
        {
            if (ModelState.IsValid)
            {
                var result = await _productWrite.AddAsync(new Product
                {
                    Name = model.Name,
                    Stock = model.Stock,
                    Price = model.Price,
                });
                await _productWrite.SaveAsync();
                return StatusCode((int)HttpStatusCode.Created, result);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ModelState);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(VM_Update_Product model)
        {
            var product = await _productRead.GetByIdAsync(model.Id);
            product.Name = model.Name;
            product.Stock = model.Stock;
            product.Price = model.Price;
            await _productWrite.SaveAsync();
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWrite.RemoveAsync(id);
            await _productWrite.SaveAsync();
            return Ok(new { message = "Successfuly deleted" });
        }
    }
}