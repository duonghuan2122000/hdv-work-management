using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.Domain
{
    public interface ICompanyRepository
    {
        /// <summary>
        /// tạo mới công ty
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        Task<Company> CreateAsync(Company company);
    }
}
