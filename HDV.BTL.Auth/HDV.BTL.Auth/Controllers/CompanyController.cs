using HDV.BTL.Auth.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDV.BTL.Auth.Controllers
{
    [Route("companies")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ILogger<CompanyController> _logger;
        public CompanyController(ILogger<CompanyController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<List<CompanyDto>> GetListAsync()
        {
            var listCompanyClient = new RestClient("http://dbhuan.tk:5012/companies?page=1&limit=1000000");
            var listCompanyRequest = new RestRequest("", Method.Get);

            var listCompanyResponse = await listCompanyClient.ExecuteAsync(listCompanyRequest);
            _logger.LogWarning($"{JsonConvert.SerializeObject(listCompanyResponse)}");
            if (!listCompanyResponse.IsSuccessful)
            {
                throw new Exception("Lỗi hệ thống");
            }

            var listCompanyDto = JsonConvert.DeserializeObject<PagedResultDto<CompanyDto>>(listCompanyResponse.Content);
            return listCompanyDto.Items;
        }
    }
}
