using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.Domain
{
    public interface IProjectRepository
    {
        Task<Project> CreateAsync(Project project, string companyCode);

        Task<Project> GetAsync(Guid id);
    }
}
