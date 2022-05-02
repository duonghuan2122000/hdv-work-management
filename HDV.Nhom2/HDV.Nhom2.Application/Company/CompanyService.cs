using HDV.Nhom2.Application.Contracts;
using HDV.Nhom2.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.Application
{
    public class CompanyService: ICompanyService
    {
        #region Khởi tạo
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        #endregion

        #region Hàm
        /// <summary>
        /// tạo mới công ty
        /// </summary>
        /// <param name="createCompanyReq"></param>
        /// <returns></returns>
        public async Task<CreateCompanyRes> CreateAsync(CreateCompanyReq createCompanyReq)
        {
            var company = new Company
            {
                Name = createCompanyReq.Name
            };

            var newCompany = await _companyRepository.CreateAsync(company);
            return new CreateCompanyRes
            {
                Code = newCompany.Code,
                Name = newCompany.Name
            };
        }
        #endregion
    }
}
