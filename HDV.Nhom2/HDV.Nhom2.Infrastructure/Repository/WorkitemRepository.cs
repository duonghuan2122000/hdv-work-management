using HDV.Nhom2.Domain;
using HDV.Nhom2.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.Infrastructure
{
    public class WorkitemRepository: IWorkitemRepository
    {
        private readonly Nhom2DbContext _dbContext;

        private readonly IProjectRepository _projectRepository;

        private readonly IEmployeeRepository _employeeRepository;

        public WorkitemRepository(Nhom2DbContext dbContext, IProjectRepository projectRepository, IEmployeeRepository employeeRepository)
        {
            _dbContext = dbContext;
            _projectRepository = projectRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<Workitem> CreateAsync(Workitem workitem)
        {
            var project = await _projectRepository.GetAsync(workitem.ProjectId);

            if(project == null)
            {
                throw new Nhom2Exception(CreateWorkitemErrorInfo.Code.ProjectNotFound, CreateWorkitemErrorInfo.Message.ProjectNotFound);
            }

            var employee = await _employeeRepository.GetAsync(workitem.EmployeeId);

            if(employee == null)
            {
                throw new Nhom2Exception(CreateWorkitemErrorInfo.Code.EmployeeNotFound, CreateWorkitemErrorInfo.Message.EmployeeNotFound);
            }

            workitem.CompanyId = project.CompanyId;

            var totalWorkitemInCompany = await _dbContext.Workitem.AsNoTracking()
                .Where(w => w.CompanyId == workitem.CompanyId)
                .CountAsync();

            workitem.Code = CreateNewWorkitemCode(totalWorkitemInCompany + 1);
            await _dbContext.Workitem.AddAsync(workitem);
            await _dbContext.SaveChangesAsync();

            return workitem;
        }

        private string CreateNewWorkitemCode(int index)
        {
            string indexStr = index.ToString();
            return "W" + indexStr.PadLeft(11, '0');
        }
    }
}
