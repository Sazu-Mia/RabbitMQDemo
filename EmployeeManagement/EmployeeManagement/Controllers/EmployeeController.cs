using EmployeeManagement.Models;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using Shared.Messages;
using System.Text;
using System.Text.Json;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IPublishEndpoint _publishEndpoint;
        public EmployeeController(AppDbContext context, IPublishEndpoint publishEndpoint)
        {
            _context = context;
           _publishEndpoint = publishEndpoint;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployee()
        {
            var employees = await _context.Employees.ToListAsync();
            if (employees == null) return NotFound();
            return employees;
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int Id)
        {
            var employees = await _context.Employees.Where(x=> x.Id == Id).FirstOrDefaultAsync();
            if (employees == null) return NotFound();
            return employees;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            // Send message to RabbitMQ
            await _publishEndpoint.Publish(new EmployeeUpdateDto
            {
                Id = employee.Id,
                Salary = employee.Salary
            });

            return CreatedAtAction(nameof(GetEmployeeById), new {Id = employee.Id}, employee);
        }
    }
}
