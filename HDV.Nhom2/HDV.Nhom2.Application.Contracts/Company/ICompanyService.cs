using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.Application.Contracts
{
    public interface ICompanyService
    {
        /// <summary>
        /// tạo mới công ty
        /// </summary>
        /// <param name="createCompanyReq"></param>
        /// <returns></returns>
        Task<CreateCompanyRes> CreateAsync(CreateCompanyReq createCompanyReq);
    }
}
