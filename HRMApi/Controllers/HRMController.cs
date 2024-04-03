using HRMApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace HRMApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HRMController : ControllerBase
    {
        private readonly List<Employee> _employees;
        public HRMController()
        {
            _employees = new List<Employee>
            {
                new Employee { Id = 1, Name = "Vanh", PhoneNumber = "12345678", Email="name1@gmail.com" },
                new Employee { Id = 2, Name = "Nguyen Van A", PhoneNumber = "12345678", Email="name1@gmail.com" }
            };
        }

        // GET: api/Employees
        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return _employees;
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var employee = _employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        // POST: api/Employees
        [HttpPost]
        public IActionResult Post([FromBody] Employee employee)
        {
            // Generate new ID for employee
            employee.Id = _employees.Count + 1;
            // Thêm nhân viên mới vào danh sách
            _employees.Add(employee);
            // Trả về mã trạng thái 201 Created và chi tiết của nhân viên đã tạo
            return CreatedAtAction(nameof(Get), new { id = employee.Id }, employee);
        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Employee employee)
        {
            var existingEmployee = _employees.FirstOrDefault(e => e.Id == id);
            if (existingEmployee == null)
            {
                return NotFound();
            }
            // Cập nhật thông tin của nhân viên
            existingEmployee.Name = employee.Name;
            existingEmployee.PhoneNumber = employee.PhoneNumber;
            existingEmployee.Email = employee.Email;
            return NoContent();
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var employeeToRemove = _employees.FirstOrDefault(e => e.Id == id);
            if (employeeToRemove == null)
            {
                return NotFound();
            }
            // Xóa nhân viên khỏi danh sách
            _employees.Remove(employeeToRemove);
            return NoContent();
        }

        // GET: api/Employees/search?name=John
        [HttpGet("search")]
        public IActionResult Search(string name)
        {
            var employees = _employees.Where(e => e.Name.ToLower().Contains(name.ToLower()));
            return Ok(employees);
        }
    }
}