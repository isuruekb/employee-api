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

        [HttpGet]
        public ActionResult<List<Employee>> GetAll() => _service.GetAll();

        [HttpGet("{id}")]
        public ActionResult<Employee> GetById(int id)
        {
            var employee = _service.GetById(id);
            if (employee == null)
                return NotFound(new { Message = $"Employee with ID {id} not found" });

            return Ok(employee); // 200 OK
        }

        [HttpPost]
        public ActionResult<Employee> Create([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // 400

            var created = _service.Add(employee);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created); // 201
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // 400

            var success = _service.Update(id, employee);
            if (!success)
                return NotFound(new { Message = $"Employee with ID {id} not found" }); // 404

            return NoContent(); // 204
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var success = _service.Delete(id);
            if (!success)
                return NotFound(new { Message = $"Employee with ID {id} not found" }); // 404

            return NoContent(); // 204
        }

    }
}
