using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.IRepositories;
using Talabat.Core.Specifications;

namespace Talabat.APIs.Controllers
{
    public class EmployeeController : BaseAPIController
    {
        private readonly IGenericRepository<Employee> _employeesRepo;

        public EmployeeController(IGenericRepository<Employee> employeesRepo)
        {
            _employeesRepo = employeesRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            var spec = new EmployeeWithDepartmentSpecification();
            var employees = await _employeesRepo.GetAllWithSpecAsync(spec);
            return Ok(employees);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var spec = new EmployeeWithDepartmentSpecification(id);
            var employee = await _employeesRepo.GetEntityWithSpecAsync(spec);
            return Ok(employee);
        }
    }
}
