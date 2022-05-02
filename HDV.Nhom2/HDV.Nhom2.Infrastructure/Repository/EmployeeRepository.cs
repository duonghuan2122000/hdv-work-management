using HDV.Nhom2.Domain;
using HDV.Nhom2.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace HDV.Nhom2.Infrastructure
{
    public class EmployeeRepository: IEmployeeRepository
    {
        #region Khởi tạo
        private readonly Nhom2DbContext _dbContext;

        public EmployeeRepository(Nhom2DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Hàm
        /// <summary>
        /// Thêm mới nhân viên
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task<Employee> CreateAsync(Employee employee)
        {
            var existEmployee = await _dbContext.Employee.AsNoTracking()
                .Where(e => e.Email.Equals(employee.Email) || e.Username.Equals(employee.Username))
                .AnyAsync();

            if (existEmployee)
            {
                throw new Nhom2Exception(CreateEmployeeErrorInfo.Code.EmailOrUserNameExist, CreateEmployeeErrorInfo.Message.EmailOrUsernameExist, HttpStatusCode.BadRequest);
            }

            await _dbContext.Employee.AddAsync(employee);
            await _dbContext.SaveChangesAsync();
            return employee;
        }

        /// <summary>
        /// Lấy thông tin bằng email hoặc username
        /// </summary>
        /// <param name="email"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<Employee> GetAsync(string email, string username)
        {
            var employee = await _dbContext.Employee.AsNoTracking()
                .Where(e => e.Email.Equals(email) || e.Username.Equals(username))
                .FirstOrDefaultAsync();
            return employee;
        }
        #endregion
    }
}
