using HDV.Nhom2.Gateway.BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDV.Nhom2.Gateway.HttpApi.Controllers
{
    /// <summary>
    /// Controller công ty
    /// </summary>
    [Route("companies")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        #region Khởi tạo
        /// <summary>
        /// Service công ty
        /// </summary>
        /// CreatedBy: dbhuan 25/06/2022
        private readonly ICompanyService _companyService;
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }
        #endregion

        #region Hàm
        [HttpGet]
        public async Task<List<CompanyDto>> GetListAsync()
        {
            var res = await _companyService.GetListAsync();
            Log.Logger.Debug("CompanyController-GetListAsync-Res: {@res}", res);
            return res;
        }
        #endregion
    }
}
