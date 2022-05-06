using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDV.BTL.Auth.Entity
{
    /// <summary>
    /// Người dùng
    /// </summary>
    /// CreatedBy: dbhuan 31/03/2022
    public class User
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PasswordSalt { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public int EmployeeId { get; set; }
    }
}
