using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.Gateway.BL
{
    public interface ITaskService
    {
        Task<GetListTaskDto<TaskDto>> GetList();
    }
}
