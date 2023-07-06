﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication5;
using WebApplication5.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        

        // inject ContactsAPIDbContext into the controller
        public EmployeeController(IEmployeeService employeeService, ILogger _logger)
        {
            _employeeService = employeeService;
            
        }

        // get method to get all the contacts
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var res = await _employeeService.GetAllEmployees();
            if (res != null)
            {
                return Ok(res);
            }

            return BadRequest("Error while retrieving data");
        }

        // get method to get a specific employee using 'id' as a parameter in route
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var employee = await _employeeService.GetEmployeeById(id);
            if (employee != null)
            {

                return Ok(employee);

            }

            
            return NotFound();
        }

        // add a new employee to the databse
        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            var res = await _employeeService.AddEmployee(employee);
            if (res != null)
            {
                return Ok(res);
            }

            return BadRequest("Error while inserting employee");

        }

        // update a particular contact in the database
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, Employee employee)
        {

            if (employee == null)
            {
                return BadRequest("Null input passed");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Object input format incorrect");
            }

            int res = await _employeeService.UpdateEmployee(id, employee);
            if (res != 0)
            {
                return Ok();
            }

            return NotFound("Contact not found");

        }

        // delete a contact from database
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            int res = await _employeeService.DeleteEmployee(id);

            if (res == 1)
            {
                return Ok();
            }

            return NotFound();
        }
    }
}

        



    
