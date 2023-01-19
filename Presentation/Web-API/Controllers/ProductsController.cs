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
        private readonly IOrderWriteRepository _orderWrite;
        private readonly ICustomerWriteRepository _customerWrite;
        private readonly ICustomerReadRepository _customerRead;



        public ProductsController(IProductReadRepository productRead,
            IProductWriteRepository productWrite,
            IOrderWriteRepository orderWrite,
            ICustomerWriteRepository customerWrite,
            ICustomerReadRepository customerRead)
        {
            _productRead = productRead;
            _productWrite = productWrite;
            _orderWrite = orderWrite;
            _customerWrite = customerWrite;
            _customerRead = customerRead;
        }

        [HttpGet("Test1")]
        public async Task<IActionResult> Test1()
        {
            Customer customer = await _customerRead.GetByIdAsync("e9017731-5fa0-42e2-a2a1-ba94af7515ae");
            customer.Name = "update deneme";
            await _customerWrite.SaveAsync();
            return Ok();
        }


        [HttpGet("Test2")]
        public async Task<IActionResult> Test2()
        {
            var customerId = Guid.NewGuid();
            await _customerWrite.AddAsync(new() { Id = customerId, Name = "son deneme"});
            await _orderWrite.AddAsync(new() { Id = Guid.NewGuid(), Address = "artvin", Description = "order test12", CustomerId = customerId});
            await _orderWrite.SaveAsync();
            return Ok();
        }
    }
}