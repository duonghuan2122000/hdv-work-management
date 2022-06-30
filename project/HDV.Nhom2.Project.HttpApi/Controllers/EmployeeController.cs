using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDV.Nhom2.Project.HttpApi.Controllers
{
    [Route("employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("email")]
        public async Task<object> GetEmployeeById([FromQuery] string email)
        {
            var conn = new MySqlConnection(_configuration.GetConnectionString("Default"));
            var employee = await conn.QueryFirstOrDefaultAsync("select id, email, role, company_id as companyId from employees where email = @Email limit 1;", new
            {
                Email = email
            });

            return employee;
        }

        [HttpGet("list")]
        public async Task<List<object>> GetListEmployee([FromQuery] string keyword, [FromQuery] int companyId)
        {
            var conn = new MySqlConnection(_configuration.GetConnectionString("Default"));
            var employee = await conn.QueryAsync("select id, email, name, role from employees where email like @Email and company_id = @CompanyId limit 1;", new
            {
                Email = $"%{keyword}%",
                CompanyId = companyId
            });

            return employee.ToList();
        }
    }
}
