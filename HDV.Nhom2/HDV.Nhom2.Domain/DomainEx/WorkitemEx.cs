using HDV.Nhom2.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.Domain
{
    public partial class Workitem
    {
        public Workitem()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.Now;
            Status = (int)WorkitemStatusEnum.CREATE;
        }
    }
}
