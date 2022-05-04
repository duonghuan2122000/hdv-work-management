using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.Application.Contracts
{
    public class CreateWorkitemReq
    {
        public Guid ProjectId { get; set; }

        public Guid EmployeeId { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }
    }
}
