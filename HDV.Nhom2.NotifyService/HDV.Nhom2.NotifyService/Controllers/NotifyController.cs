using Dapper;
using HDV.Nhom2.NotifyService.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Configuration;
using System.Linq;

namespace HDV.Nhom2.NotifyService.Controllers
{
    [Route("notify")]
    [ApiController]
    public class NotifyController : ControllerBase
    {
        private readonly NotifyDbContext _dbContext;

        public NotifyController(IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionStrings:Default"];
            _dbContext = new NotifyDbContext(connectionString); 
        }

        [HttpPost("employee/register")]
        public async Task EmployeeRegisterNotify(EmployeeRegisterNotifyDto employeeRegisterNotifyDto)
        {
            var urlGetEmployee = $"http://dbhuan.tk:5012/go/employee/{employeeRegisterNotifyDto.EmployeeId}";

            var getEmployeeClient = new RestClient(urlGetEmployee);
            var getEmployeeRequest = new RestRequest("", Method.Get);
            var getEmployeeResponse = await getEmployeeClient.ExecuteAsync(getEmployeeRequest);
            if (!getEmployeeResponse.IsSuccessful)
            {
                throw new Nhom2Exception("E1000", "Nhân viên không tồn tại");
            }

            var getEmployeeRes = JsonConvert.DeserializeObject<GetEmployeeGoRes>(getEmployeeResponse.Content);

            if(!getEmployeeRes.CompanyId.Equals(employeeRegisterNotifyDto.CompanyId.ToString()))
            {
                throw new Nhom2Exception("E1001", "Công ty không tồn tại");
            }

            var conn = _dbContext.GetConnection();
            await conn.OpenAsync();

            string getEmployeeNotifySql = "select * from employeeNotify where EmployeeId = @EmployeeId and CompanyId = @CompanyId";
            var employeeNotify = await conn.QueryFirstOrDefaultAsync<EmployeeNotify>(getEmployeeNotifySql, new
            {
                EmployeeId = employeeRegisterNotifyDto.EmployeeId,
                CompanyId = employeeRegisterNotifyDto.CompanyId
            });

            if(employeeNotify != null)
            {
                string[] emailCCOld = employeeNotify.EmailCc.Split(",");
                string[] emailCCNew = employeeRegisterNotifyDto.EmailCc.Split(",");
                List<string> emailCC = new List<string>(emailCCOld);
                for(int i = 0; i < emailCCNew.Length; i++)
                {
                    if (!emailCCOld.Contains(emailCCNew[i]))
                    {
                        emailCC.Add(emailCCNew[i]);
                    }
                }
                string updateEmployeeNotifySql = "update employeeNotify set EmailCc = @EmailCc where Id = @Id";
                await conn.ExecuteAsync(updateEmployeeNotifySql, new
                {
                    EmailCc = string.Join(",", emailCC.ToArray()),
                    Id = employeeNotify.Id
                });
            } 
            else
            {
                string createEmployeeNotifySql = "insert into employeeNotify (Id, EmployeeId, CompanyId, EmailCc) values (@Id, @EmployeeId, @CompanyId, @EmailCc)";

                await conn.ExecuteAsync(createEmployeeNotifySql, new
                {
                    Id = Guid.NewGuid(),
                    EmployeeId = employeeRegisterNotifyDto.EmployeeId,
                    CompanyId = employeeRegisterNotifyDto.CompanyId,
                    EmailCc = employeeRegisterNotifyDto.EmailCc
                });
            }

           
        }
    }
}
