using Microsoft.AspNetCore.Mvc;
using JOB_PORTAL.Data;
using JOB_PORTAL.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace JOB_PORTAL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CompanyController(ApplicationDbContext context)
        {
            _context = context;
            

        }
        [HttpPost("create")]
        public IActionResult CreateCompany([FromBody] Company company)
        {
            if (ModelState.IsValid)
            {
                _context.Companies.Add(company); 
                _context.SaveChanges();          
                return Ok("Company created successfully!");
            }
            return BadRequest("Invalid company data");
        }
        [HttpGet]
        public IActionResult GetCompanies()
        {
            var companies = _context.Companies.ToList(); 
            return Ok(companies);
        }

        [HttpGet("{id}")]
        public IActionResult GetCompanyById(int id)
        {
            var company = _context.Companies.FirstOrDefault(c => c.Id == id);
            if (company == null)
            {
                return NotFound("Company not found");
            }
            return Ok(company);
        }

        [HttpPut("update/{id}")]
        public IActionResult UpdateCompany(int id, [FromBody] Company company)
        {
            var existingCompany = _context.Companies.FirstOrDefault(c => c.Id == id);
            if (existingCompany == null)
            {
                return NotFound("Company not found");
            }

            if (ModelState.IsValid)
            {
                existingCompany.CompanyName = company.CompanyName;
                existingCompany.Logo = company.Logo;
                existingCompany.Address = company.Address;

                _context.SaveChanges();
                return Ok("Company updated successfully!");
            }
            return BadRequest("Invalid company data");
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteCompany(int id)
        {
            var company = _context.Companies.FirstOrDefault(c => c.Id == id);
            if (company == null)
            {
                return NotFound("Company not found");
            }

            _context.Companies.Remove(company);
            _context.SaveChanges();
            return Ok("Company deleted successfully!");
        }
        [HttpPost("login")]
        public IActionResult CreateLogin([FromBody] Login login)
        {
            if (ModelState.IsValid)
            {
                // Check if the user exists in the Register table (i.e., the user is registered)
                var registeredUser = _context.Registers
                    .FirstOrDefault(u => u.Username == login.Username && u.Password == login.Password);

                if (registeredUser != null)
                {
                    // User is registered, proceed with login
                    var existingLogin = _context.Logins
                        .FirstOrDefault(u => u.Username == login.Username && u.Password == login.Password);

                    if (existingLogin != null)
                    {
                        // If the user is already in the Logins table, return successful login
                        return Ok("Login successful!");
                    }
                    else
                    {
                        // If the user is not in the Logins table, save the login data
                        _context.Logins.Add(login);
                        _context.SaveChanges();
                        return Ok("Login saved and successful!");
                    }
                }
                else
                {
                    // User is not registered, return an error message
                    return Unauthorized("User not registered. Please register first.");
                }
            }
            return BadRequest("Invalid login data");
        }


        ////[HttpPost("login")]
        ////public IActionResult CreateLogin([FromBody] Login login)
        ////{
        ////    if (ModelState.IsValid)
        ////    {
        ////        // Check if the user exists in the database
        ////        var existingUser = _context.Registers
        ////            .FirstOrDefault(u => u.Username == login.Username && u.Password == login.Password);

        ////        if (existingUser != null)
        ////        {
        ////            // If user exists, login is successful
        ////            return Ok("Login successful!");
        ////        }
        ////        else
        ////        {
        ////            // If user does not exist, return an error
        ////            return Unauthorized("Invalid login credentials or user not registered.");
        ////        }
        ////    }
        ////    return BadRequest("Invalid login data");
        ////}


        //[HttpPost("login")]
        //public IActionResult CreateLogin([FromBody] Login login)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Logins.Add(login);      
        //        _context.SaveChanges();          
        //        return Ok("Login saved successfully!");
        //    }
        //    return BadRequest("Invalid login data");
        //}

        [HttpPost("register")]
        public IActionResult CreateRegister([FromBody] Register register)
        {
            if (ModelState.IsValid)
            {
                _context.Registers.Add(register); 
                _context.SaveChanges();           
                return Ok("Register saved successfully!");
            }
            return BadRequest("Invalid register data");
        }
        [HttpPost("jobs/create")]
        public IActionResult CreateJob([FromBody] Job job)
        {
            if (ModelState.IsValid)
            {
                job.Id = 0; 
                _context.Jobs.Add(job); 
                _context.SaveChanges();  
                return Ok("Job created successfully!");
            }
            return BadRequest("Invalid job data");
        }
        [HttpGet("jobs")]
        public IActionResult GetJobs()
        {
            var jobs = _context.Jobs.ToList(); 
            return Ok(jobs);
        }
        [HttpGet("jobs/{id}")]
        public IActionResult GetJobById(int id)
        {
            var job = _context.Jobs.FirstOrDefault(j => j.Id == id);
            if (job == null)
            {
                return NotFound("Job not found");
            }
            return Ok(job);
        }
        [HttpPut("jobs/update/{id}")]
        public IActionResult UpdateJob(int id, [FromBody] Job job)
        {
            var existingJob = _context.Jobs.FirstOrDefault(j => j.Id == id);
            if (existingJob == null)
            {
                return NotFound("Job not found");
            }

            if (ModelState.IsValid)
            {
                existingJob.CompanyId = job.CompanyId;
                existingJob.Title = job.Title;
                existingJob.Description = job.Description;
                existingJob.StartDate = job.StartDate;
                existingJob.EndDate = job.EndDate;
                existingJob.Salary = job.Salary;
                //existingJob.Status = job.Status;

                _context.SaveChanges(); 
                return Ok("Job updated successfully!");
            }
            return BadRequest("Invalid job data");
        }

        [HttpDelete("jobs/delete/{id}")]
        public IActionResult DeleteJob(int id)
        {
            var job = _context.Jobs.FirstOrDefault(j => j.Id == id);
            if (job == null)
            {
                return NotFound("Job not found");
            }
            //job.Status = false;
            _context.Jobs.Remove(job);
            _context.SaveChanges(); 
            return Ok("Job deleted successfully!");
        }
    }
}
