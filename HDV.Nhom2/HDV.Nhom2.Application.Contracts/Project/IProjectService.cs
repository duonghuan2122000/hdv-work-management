using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.Application.Contracts
{
    public interface IProjectService
    {
        Task<CreateProjectRes> CreateAsync(CreateProjectReq createProjectReq, string companyCode);
    }
}
