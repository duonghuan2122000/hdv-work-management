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
    [Route("projects")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ProjectController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("list")]
        public async Task<GetListProjectDto> GetList(GetListFilterProjectDto getListFilterProjectDto)
        {
            var conn = new MySqlConnection(_configuration.GetConnectionString("Default"));
            var sql = "select * from projects p ORDER BY p.created_at desc;";
            if (getListFilterProjectDto.CompanyId != null)
            {
                sql = $"select * from projects p WHERE p.company_id = '{getListFilterProjectDto.CompanyId}' ORDER BY p.created_at desc;";
            }
            var projects = await conn.QueryAsync<ProjectDto>(sql);
            var res = new GetListProjectDto
            {
                TotalCount = projects.Count(),
                Items = projects.ToList()
            };
            return res;
        }


    }
}
