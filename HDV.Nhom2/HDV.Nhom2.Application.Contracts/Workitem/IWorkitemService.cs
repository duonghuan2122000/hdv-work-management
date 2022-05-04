using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.Application.Contracts
{
    public interface IWorkitemService
    {
        Task<CreateWorkitemRes> CreateAsync(CreateWorkitemReq createWorkitemReq, string token);
    }
}
