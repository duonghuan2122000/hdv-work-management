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
    public class ProjectRepository: IProjectRepository
    {
        private readonly Nhom2DbContext _dbContext;

        public ProjectRepository(Nhom2DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Project> CreateAsync(Project project, string companyCode)
        {
            var company = await _dbContext.Company.AsNoTracking()
                .Where(c => c.Code == companyCode)
                .FirstOrDefaultAsync();

            if(company == null)
            {
                throw new Nhom2Exception(CreateProjectErrorInfo.Code.CompanyNotFound, CreateProjectErrorInfo.Message.CompanyNotFound);
            }

            project.CompanyId = company.Id;

            var totalProjectsInCompany = await _dbContext.Project.AsNoTracking()
                .Where(p => p.CompanyId == company.Id)
                .CountAsync();

            project.Code = CreateNewProjectCode(totalProjectsInCompany + 1);
            await _dbContext.Project.AddAsync(project);
            await _dbContext.SaveChangesAsync();
            return project;
        }

        private string CreateNewProjectCode(int index)
        {
            string indexStr = index.ToString();
            return "DA" + indexStr.PadLeft(11, '0');
        }

        public async Task<Project> GetAsync(Guid id)
        {
            var project = await _dbContext.Project.AsNoTracking()
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();

            return project;
        }
    }
}
