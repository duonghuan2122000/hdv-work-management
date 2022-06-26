using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.Gateway.BL
{
    /// <summary>
    /// interface app service của công ty
    /// </summary>
    /// CreatedBy: dbhuan 19/06/2022
    public interface ICompanyService
    {
        /// <summary>
        /// Lấy thông tin công ty bằng id
        /// </summary>
        /// CreatedBy: dbhuan 19/06/2022
        Task<CompanyDto> GetById(string id);

        /// <summary>
        /// Lấy tất cả danh sách công ty
        /// </summary>
        /// CreatedBy: dbhuan 25/06/2022
        Task<List<CompanyDto>> GetListAsync();
    }
}
