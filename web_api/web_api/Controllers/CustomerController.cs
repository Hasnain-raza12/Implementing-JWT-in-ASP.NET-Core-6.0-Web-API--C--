using Api.Common;
using Api.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using web_api.DTos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace web_api.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IRepositry<Customer> _customerRepo;

        public CustomerController(IRepositry<Customer> customerRepo)
        {
            _customerRepo = customerRepo;
        }
        [HttpGet]
        [Authorize]
        public IEnumerable<Customer> GetAll()
        {
            return _customerRepo.GetAll();
        }
        [HttpGet("{id}")]
        public Customer GetId(string id)
        {
            return _customerRepo.GetbyId( id);
        }

        [HttpPost]
        public void Create(CreatCustomer c)
        {
            var customer = new Customer
            {
                
                //Id = Guid.NewGuid(),
                Name = c.name,
                Email = c.email,
                ContactNumber = c.contactNumber,
                IsActive = true,


            };
            _customerRepo.Create(customer);
        
        }
        [HttpPut]
        public void Update(UpdateCustomer c)
        {
            var customer = new Customer
            {
                Id = c.id,
                Name = c.name,
                Email = c.email,
                ContactNumber = c.contactNumer,
                IsActive = true,


            };
            _customerRepo.Update(customer);

        }

        [HttpDelete("{id}")]
        public void Delete(string id) {
            _customerRepo.Delete(id);
        }
    }
}
