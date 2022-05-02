using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.Domain
{
    public interface IEmployeeRepository
    {
        /// <summary>
        /// Thêm mới nhân viên
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        Task<Employee> CreateAsync(Employee employee);

        /// <summary>
        /// Lấy thông tin bằng email hoặc username
        /// </summary>
        /// <param name="email"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<Employee> GetAsync(string email, string username);
    }
}
