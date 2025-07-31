using Microsoft.AspNetCore.Mvc;
using EmployeeApi.Models;
using EmployeeApi.Services;

namespace EmployeeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeService _service;

        public EmployeesController(EmployeeService service)
        {
            _service = service;
        }

        // GET /api/employees - get all employees
        [HttpGet]
        public ActionResult<List<Employee>> GetAll() => _service.GetAll();

        // GET /api/employees/{id} - get employee by ID
        [HttpGet("{id}")]
        public ActionResult<Employee> GetById(int id)
        {
            var employee = _service.GetById(id);
            if (employee == null) return NotFound();
            return Ok(employee);
        }

        // POST /api/employees - create a new employee
        [HttpPost]
        public ActionResult<Employee> Create(Employee employee)
        {
            var created = _service.Add(employee);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT /api/employees/{id} - update an existing employee
        [HttpPut("{id}")]
        public IActionResult Update(int id, Employee employee)
        {
            var success = _service.Update(id, employee);
            if (!success) return NotFound();
            return Ok();
        }

        // DELETE /api/employees/{id} - delete an employee
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var success = _service.Delete(id);
            if (!success) return NotFound();
            return Ok();
        }
    }
}
