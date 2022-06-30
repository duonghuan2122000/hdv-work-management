using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.Gateway.BL
{
    public interface IProjectService
    {
        Task<GetListProjectDto<ProjectDto>> GetList(int companyId);
    }
}
