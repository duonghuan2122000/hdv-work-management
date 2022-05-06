using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDV.BTL.Auth.Dto
{
    public class CreateEmployeeReqDto
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime Dob { get; set; }

        public string Gender { get; set; }

        public string Role { get; set; }
    }
}
