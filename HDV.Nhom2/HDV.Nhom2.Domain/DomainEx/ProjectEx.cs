using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.Domain
{
    public partial class Project
    {
        public Project()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.Now;
        }
    }
}
