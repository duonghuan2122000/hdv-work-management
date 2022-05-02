using HDV.Nhom2.Application.Contracts;
using HDV.Nhom2.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.Application
{
    public class EmployeeService: IEmployeeService
    {
        #region Khởi tạo

        private readonly IEmployeeRepository _employeeRepository;

        private readonly ICommonUtility _commonUtility;

        public EmployeeService(IEmployeeRepository employeeRepository, ICommonUtility commonUtility)
        {
            _employeeRepository = employeeRepository;
            _commonUtility = commonUtility;
        }
        #endregion

        #region Hàm

        /// <summary>
        /// Tạo mới nhân viên
        /// </summary>
        /// CreatedBy: dbhuan 02/05/2022
        /// <param name="createEmployeeReq"></param>
        /// <returns></returns>
        public async Task<CreateEmployeeRes> CreateAsync(CreateEmployeeReq createEmployeeReq)
        {
            var employee = new Employee
            {
                FirstName = createEmployeeReq.FirstName,
                LastName = createEmployeeReq.LastName,
                Email = createEmployeeReq.Email,
                Username = createEmployeeReq.Username,
                PasswordSalt = Guid.NewGuid().ToString()
            };

            employee.PasswordHash = _commonUtility.Md5(createEmployeeReq.Password + employee.PasswordSalt);

            var newEmployee = await _employeeRepository.CreateAsync(employee);

            return new CreateEmployeeRes
            {
                FirstName = newEmployee.FirstName,
                LastName = newEmployee.LastName,
                Email = newEmployee.Email,
                Username = newEmployee.Username
            };
        }
        #endregion
    }
}
