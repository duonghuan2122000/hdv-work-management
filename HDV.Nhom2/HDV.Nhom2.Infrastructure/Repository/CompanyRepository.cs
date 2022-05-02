using HDV.Nhom2.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.Infrastructure
{
    public class CompanyRepository: ICompanyRepository
    {
        #region Khởi tạo
        private readonly Nhom2DbContext _dbContext;

        public CompanyRepository(Nhom2DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Hàm
        /// <summary>
        /// tạo mới công ty
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public async Task<Company> CreateAsync(Company company)
        {
            var totalCompany = await _dbContext.Company.AsNoTracking()
                .CountAsync();

            var newCompanyCode = CreateNewCompanyCode(totalCompany + 1);

            company.Code = newCompanyCode;

            await _dbContext.Company.AddAsync(company);
            await _dbContext.SaveChangesAsync();
            return company;
        }

        private string CreateNewCompanyCode(int index)
        {
            string indexStr = index.ToString();
            return "C" + indexStr.PadLeft(11, '0');
        }
        #endregion
    }
}
