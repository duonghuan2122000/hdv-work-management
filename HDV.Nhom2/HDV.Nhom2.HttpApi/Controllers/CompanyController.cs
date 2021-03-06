using HDV.Nhom2.Application.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDV.Nhom2.HttpApi.Controllers
{
    [Route("companies")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        #region Khởi tạo

        private readonly ICompanyService _companyService;

        private readonly IProjectService _projectService;

        public CompanyController(ICompanyService companyService, IProjectService projectService)
        {
            _companyService = companyService;
            _projectService = projectService;
        }
        #endregion

        #region Hàm
        /// <summary>
        /// tạo mới công ty
        /// </summary>
        /// <param name="createCompanyReq"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<CreateCompanyRes> CreateAsync(CreateCompanyReq createCompanyReq)
        {
            var createCompanyRes = await _companyService.CreateAsync(createCompanyReq);
            return createCompanyRes;
        }

        [HttpPost("{companyCode}/projects")]
        public async Task<CreateProjectRes> CreateProject(CreateProjectReq createProjectReq, string companyCode)
        {
            var createProjectRes = await _projectService.CreateAsync(createProjectReq, companyCode);
            return createProjectRes;
        }
        #endregion
    }
}
