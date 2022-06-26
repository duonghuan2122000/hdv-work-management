using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.Gateway.BL
{
    public interface IEmployeeService
    {
        Task<CreateEmployeeResDto> CreateAsync(CreateEmployeeReqDto createEmployeeReqDto);

        /// <summary>
        /// Tạo và giao task cho nhân viên
        /// </summary>
        /// CreatedBy: dbhuan 25/06/2022
        /// <param name="createAndAssignTaskForEmployeeReqDto"></param>
        /// <returns></returns>
        Task<bool> CreateAndAssignTaskForEmployee(CreateAndAssignTaskForEmployeeReqDto createAndAssignTaskForEmployeeReqDto);

        /// <summary>
        /// Lấy danh sách nhân viên bằng email hoặc tên
        /// </summary>
        /// <param name="emailOrName"></param>
        /// <returns></returns>
        Task<GetListEmployeeDto<EmployeeDto>> GetListEmployee(string emailOrName);
    }
}
