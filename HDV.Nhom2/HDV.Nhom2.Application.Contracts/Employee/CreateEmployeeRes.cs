using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.Application.Contracts
{
    public class CreateEmployeeRes
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public int Role { get; set; }
    }
}
