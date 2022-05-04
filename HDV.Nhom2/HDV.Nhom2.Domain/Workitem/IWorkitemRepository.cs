using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.Domain
{
    public interface IWorkitemRepository
    {
        Task<Workitem> CreateAsync(Workitem workitem);
    }
}
