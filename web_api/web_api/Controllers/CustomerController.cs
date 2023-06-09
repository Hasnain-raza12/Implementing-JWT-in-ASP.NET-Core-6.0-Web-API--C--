﻿using Api.Common;
using Api.Common.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System.ComponentModel.DataAnnotations;
using web_api.DTos;
using web_api.Middleware;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace web_api.Controllers
{
   
    [Authorize]
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
       // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("CustomerList")]
        public IEnumerable<Customer> GetAll()
        {
            return _customerRepo.GetAll();
        }
        [HttpGet("{id}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public Customer GetId(string id)
        {
            return _customerRepo.GetbyId( id);
        }

        [HttpPost]
        [Route("insert")]
        public void Create(CreatCustomer c)
        {
            try
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
            catch (Exception)
            {

                throw;
            }
        
        }
        [HttpPut]
       // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("{id}")]
        public void Update(UpdateCustomer c)
        {
            try
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
            catch (Exception)
            {

                throw;
            }

        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
       //[Route("delete")]
        public void Delete(string id) {
            _customerRepo.Delete(id);
        }
    }
}
