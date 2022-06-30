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
    [Route("tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public TaskController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("list")]
        public async Task<GetListTaskDto> GetList(GetListTaskFilterDto getListTaskFilterDto)
        {
            var conn = new MySqlConnection(_configuration.GetConnectionString("Default"));
            string sql = @"SELECT t.id AS Id, t.name AS Name, t.status AS Status, t.description AS Description, t.created_at AS CreatedDate, et.employee_id AS EmployeeId, t.project_id as ProjectId
FROM tasks t
JOIN employee_tasks et ON t.id = et.task_id;";

            if (getListTaskFilterDto.EmployeeId != null)
            {
                sql = $@"SELECT t.id AS Id, t.name AS Name, t.status AS Status, t.description AS Description, t.created_at AS CreatedDate, et.employee_id AS EmployeeId, t.project_id as ProjectId
FROM tasks t
JOIN employee_tasks et ON t.id = et.task_id
WHERE et.employee_id = '{getListTaskFilterDto.EmployeeId}';";
            }

            var tasks = await conn.QueryAsync<TaskDto>(sql);

            var res = new GetListTaskDto
            {
                TotalCount = tasks.Count(),
                Items = tasks.ToList()
            };

            return res;
        }
    }
}
