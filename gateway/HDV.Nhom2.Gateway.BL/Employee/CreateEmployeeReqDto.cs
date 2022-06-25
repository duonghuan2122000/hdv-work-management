using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDV.Nhom2.Gateway.BL
{
    /// <summary>
    /// Req tạo mới nhân viên
    /// </summary>
    /// CreatedBy: dbhuan 12/06/2022
    public class CreateEmployeeReqDto
    {
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Tên
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Họ và tên đệm
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Mật khẩu
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public string DateOfBirth { get; set; }

        /// <summary>
        /// Giới tính
        /// 1: Nam
        /// 2: Nữ
        /// 3: Khác
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// Quyền hạn: "manager", "staff", "intern"
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Id công ty
        /// </summary>
        public string CompanyId { get; set; }
    }
}
