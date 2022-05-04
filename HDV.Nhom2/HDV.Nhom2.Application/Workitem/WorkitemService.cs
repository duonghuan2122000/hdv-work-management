using HDV.Nhom2.Application.Contracts;
using HDV.Nhom2.Domain;
using HDV.Nhom2.Domain.Shared;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.Application
{
    public class WorkitemService: IWorkitemService
    {
        private readonly IWorkitemRepository _workitemRepository;

        private readonly IEmployeeRepository _employeeRepository;

        public WorkitemService(IWorkitemRepository workitemRepository, IEmployeeRepository employeeRepository)
        {
            _workitemRepository = workitemRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<CreateWorkitemRes> CreateAsync(CreateWorkitemReq createWorkitemReq, string token)
        {
            // kiểm tra quyền hạn tạo workitem của employee
            var accessToken = token.Substring("Bearer ".Length);

            var handleToken = new JwtSecurityTokenHandler();
            var jsonToken = handleToken.ReadJwtToken(accessToken);
            var employeeId = Guid.Parse(jsonToken.Claims.FirstOrDefault(t => t.Type == "EmployeeId").Value);
            var employee = await _employeeRepository.GetAsync(employeeId);

            if(employee.Role == (int)EmployeeRoleEnum.Intern)
            {
                throw new Nhom2Exception(ErrorInfo.Code.UnAuthorized, ErrorInfo.Message.UnAuthorized, System.Net.HttpStatusCode.Unauthorized);
            }

            var workitem = new Workitem
            {
                Name = createWorkitemReq.Name,
                Content = createWorkitemReq.Content,
                EmployeeId = createWorkitemReq.EmployeeId,
                ProjectId = createWorkitemReq.ProjectId
            };

            var newWorkitem = await _workitemRepository.CreateAsync(workitem);

            return new CreateWorkitemRes
            {
                Code = newWorkitem.Code,
                Name = newWorkitem.Name,
                Content = newWorkitem.Content
            };
        }
    }
}
