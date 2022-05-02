using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.Application.Contracts
{
    public interface IEmployeeService
    {
        /// <summary>
        /// Tạo mới nhân viên
        /// </summary>
        /// CreatedBy: dbhuan 02/05/2022
        /// <param name="createEmployeeReq"></param>
        /// <returns></returns>
        Task<CreateEmployeeRes> CreateAsync(CreateEmployeeReq createEmployeeReq);

        /// <summary>
        /// Xác thực nhân viên
        /// </summary>
        /// <param name="authEmployeeReq"></param>
        /// <returns></returns>
        Task<AuthEmployeeRes> AuthEmployee(AuthEmployeeReq authEmployeeReq);
    }
}
