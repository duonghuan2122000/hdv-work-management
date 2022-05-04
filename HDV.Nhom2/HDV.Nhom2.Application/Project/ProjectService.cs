using HDV.Nhom2.Application.Contracts;
using HDV.Nhom2.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.Application
{
    public class ProjectService: IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<CreateProjectRes> CreateAsync(CreateProjectReq createProjectReq, string companyCode)
        {
            var project = new Project
            {
                Name = createProjectReq.Name,
                Description = createProjectReq.Description
            };

            var newProject = await _projectRepository.CreateAsync(project, companyCode);
            return new CreateProjectRes
            {
                Code = newProject.Code,
                Name = newProject.Name,
                Description = newProject.Description
            };
        }
    }
}
