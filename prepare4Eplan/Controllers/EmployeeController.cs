using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prepare4Eplan.Models;
using prepare4Eplan.VM;

namespace prepare4Eplan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDBContext applicationDBContext;

        public EmployeeController(ApplicationDBContext _applicationDBContext) {
           applicationDBContext = _applicationDBContext;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(applicationDBContext.Employees.ToList());
        }



        [HttpPost(Name = "FindByIdRoute")]

        public IActionResult Add([FromBody] EmployeeVM employee)
        {
            Employee employee1 = new Employee();
            employee1.Name= employee.Name;
            employee1.Email= employee.Email;

            applicationDBContext.Employees.Add(employee1);
            var cre= applicationDBContext.SaveChanges();
            string url = Url.Link("FindByIdRoute", employee1.Id);

            return Created(url, cre);
        }
        [HttpPut]
        public IActionResult EditOpt([FromHeader] int id, [FromBody] EmployeeVM employee)
        {
            Employee employee1 = applicationDBContext.Employees.FirstOrDefault(c => c.Id == id);
            employee1.Name = employee.Name;
            employee1.Email = employee.Email;
            try
            {
                applicationDBContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {


                ex.Entries.Single().Reload();
                var entry = ex.Entries.Single();
                var clientValues = entry.Entity;
                var databaseValues = entry.GetDatabaseValues();
                entry.OriginalValues.SetValues(databaseValues);
                entry.CurrentValues.SetValues(clientValues);
                applicationDBContext.SaveChanges();

            }
            return StatusCode(StatusCodes.Status200OK, employee1);
        }
            [HttpPut("EditPess")]
            public IActionResult EditPess([FromHeader] int id, [FromBody] EmployeeVM employee)
            {
            using var transaction = applicationDBContext.Database.BeginTransaction();
            Employee employee1 = applicationDBContext.Employees.FirstOrDefault(c => c.Id == id);
            applicationDBContext.Database.ExecuteSqlRaw("SELECT * FROM Employees WITH (UPDLOCK) WHERE Id = 1", 1);
            try
            {
                employee1.Name = employee.Name;
                employee1.Email = employee.Email;
                applicationDBContext.SaveChanges();
                transaction.Commit();
            }
            catch(Exception ex)
            {

                transaction.Rollback();
            }
            return StatusCode(StatusCodes.Status200OK, employee1);
        }
        }
}
