using Confluent.Kafka;
using HDV.Nhom2.Infrastructure.Contracts.CallService;
using HDV.Nhom2.Infrastructure.Contracts.Queue;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.Gateway.BL
{
    public class EmployeeService: IEmployeeService
    {
        private readonly ICallService _callService;

        private readonly IOptions<AuthServiceOption> _authServiceOptions;

        private readonly IKafkaProducer<Null, EmailContentDto> _producer;

        private readonly ICompanyService _companyService;

        private readonly IOptions<CompanyServiceOption> _companyServiceOptions;

        private readonly IOptions<ProjectServiceOption> _projectServiceOptions;

        private readonly IOptions<ProjectNetServiceOption> _projectNetServiceOptions;

        public EmployeeService(ICallService callService, 
            IOptions<AuthServiceOption> authServiceOptions,
            IKafkaProducer<Null, EmailContentDto> producer,
            ICompanyService companyService,
            IOptions<CompanyServiceOption> companyServiceOptions,
            IOptions<ProjectServiceOption> projectServiceOptions,
            IOptions<ProjectNetServiceOption> projectNetServiceOptions)
        {
            _callService = callService;
            _authServiceOptions = authServiceOptions;
            _producer = producer;
            _companyService = companyService;
            _companyServiceOptions = companyServiceOptions;
            _projectServiceOptions = projectServiceOptions;
            _projectNetServiceOptions = projectNetServiceOptions;
        }

        /// <summary>
        /// Thêm mới nhân viên
        /// 
        /// Exception:
        /// - E1000: Email đã tồn tại
        /// - E1001: Công ty không hợp lệ (id không chính xác)
        /// 
        /// - E3000: Lỗi hệ thống
        /// </summary>
        /// CreatedBy: dbhuan 11/06/2022
        /// <param name="createEmployeeReqDto"></param>
        /// <returns></returns>
        public async Task<CreateEmployeeResDto> CreateAsync(CreateEmployeeReqDto createEmployeeReqDto)
        {
            var createEmployeeResDto = new CreateEmployeeResDto();

            #region Kiểm tra thông tin công ty
            // kiểm tra thông tin công ty
            var companyDto = await _companyService.GetById(createEmployeeReqDto.CompanyId);
            #endregion

            #region Tạo tài khoản user cho nhân viên
            // gọi api tạo user mới (tạo tài khoản)
            var createUserReqDto = new CreateUserReqDto
            {
                Email = createEmployeeReqDto.Email,
                Password = createEmployeeReqDto.Password,
                FirstName = createEmployeeReqDto.FirstName,
                LastName = createEmployeeReqDto.LastName
            };
            var user = await CreateUser(createUserReqDto);

            createEmployeeResDto.Email = user.Email;
            createEmployeeResDto.FirstName = user.FirstName;
            createEmployeeResDto.LastName = user.LastName;
            #endregion

            #region Tạo nhân viên mới (gọi service go)
            // tạo nhân viên mới
            var createEmployeeGoReqDto = new CreateEmployeeGoReqDto
            {
                CompanyId = createEmployeeReqDto.CompanyId,
                Name = $"{createEmployeeReqDto.LastName} {createEmployeeReqDto.FirstName}",
                Email = createEmployeeReqDto.Email,
                DateOfBirth = createEmployeeReqDto.DateOfBirth,
                Gender = MapGender(createEmployeeReqDto.Gender),
                Role = createEmployeeReqDto.Role
            };

            await CreateEmployeeGoService(createEmployeeGoReqDto);
            #endregion

            #region Đẩy message gửi mail vào kafka
            var mailWhenRegisterEmployeeSuccessPath = Directory.GetCurrentDirectory() + "/FileTemplate/Mail/MailWhenRegisterEmployeeSuccess.html";
            var mailWhenRegisterEmployeeSuccess = File.ReadAllText(mailWhenRegisterEmployeeSuccessPath);
            mailWhenRegisterEmployeeSuccess = mailWhenRegisterEmployeeSuccess.Replace("##EMPLOYEE_NAME##", $"{createEmployeeResDto.LastName} {createEmployeeResDto.FirstName}")
                .Replace("##COMPANY_NAME##", companyDto.Name);

            var emailContentDto = new EmailContentDto
            {
                Email = createEmployeeResDto.Email,
                Subject = "[HDV.Nhom2] Đăng ký thành công nhân viên",
                Body = mailWhenRegisterEmployeeSuccess
            };

            _ = AddSendMailToQueue(emailContentDto);
            #endregion

            return createEmployeeResDto;
        }

        private string MapGender(int gender)
        {
            return gender switch
            {
                1 => "male",
                2 => "female",
                _ => "not_specified",
            };
        }

        /// <summary>
        /// Gọi service tạo user
        /// </summary>
        /// <param name="createUserReqDto"></param>
        /// <returns></returns>
        private async Task<CreateUserResDto> CreateUser(CreateUserReqDto createUserReqDto)
        {
            var createUserUrl = $"{_authServiceOptions.Value.BaseUrl}/users";

            Log.Logger.Debug("EmployeeController-CreateUser-CallService: url={createUserUrl} - Req:{@createUserReqDto}", createUserUrl, createUserReqDto);
            var createUserResponse = await _callService.CallRestApiAsync(createUserUrl, "post", createUserReqDto);
            Log.Logger.Debug("EmployeeController-CreateUser-CallService: url={createUserUrl} - Res:{@createUserResponse}", createUserUrl, createUserResponse);

            if (createUserResponse.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var errorInfo = JsonConvert.DeserializeObject<ErrorInfo>(createUserResponse.JsonObject);
                throw new Nhom2Exception(errorInfo.ErrorCode, errorInfo.ErrorMessage, createUserResponse.StatusCode);
            }

            var createUserResDto = JsonConvert.DeserializeObject<CreateUserResDto>(createUserResponse.JsonObject);

            return createUserResDto;
        }

        private async Task CreateEmployeeGoService(CreateEmployeeGoReqDto createEmployeeGoReqDto)
        {
            try
            {
                var createEmployeeGoServiceUrl = $"{_companyServiceOptions.Value.BaseUrl}/company/{createEmployeeGoReqDto.CompanyId}/employee";
                var createEmployeeGoServiceResponse = await _callService.CallRestApiAsync(createEmployeeGoServiceUrl, "POST", createEmployeeGoReqDto);
                Log.Logger.Debug("EmployeeService-CreateEmployeeGoService: url={createEmployeeGoServiceUrl}, Res={@createEmployeeGoServiceResponse}", createEmployeeGoServiceUrl, createEmployeeGoServiceResponse);
                if(createEmployeeGoServiceResponse.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Nhom2Exception("E3000", "Lỗi hệ thống", System.Net.HttpStatusCode.InternalServerError);
                }
            }
            catch(Nhom2Exception ex)
            {
                Log.Logger.Error("EmployeeService-CreateEmployeeGoService-Exception: {ex}", ex);
                throw;
            }
            catch (Exception ex)
            {
                Log.Logger.Error("EmployeeService-CreateEmployeeGoService-Exception: {ex}", ex);
                throw new Nhom2Exception("E3000", "Lỗi hệ thống", System.Net.HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Thêm một msg gửi mail tạo nhân viên vào queue
        /// </summary>
        /// <returns></returns>
        private async Task AddSendMailToQueue(EmailContentDto emailContentDto)
        {
            var emailContentDtoStr = JsonConvert.SerializeObject(emailContentDto);
            Log.Logger.Debug("EmployeeService-AddSendMailCreateUserToQueue: {emailContentDtoStr}", emailContentDtoStr);
            await _producer.ProduceAsync("HDV_EmailService", null, emailContentDto);
        }

        /// <summary>
        /// Tạo và giao task cho nhân viên
        /// </summary>
        /// CreatedBy: dbhuan 25/06/2022
        /// <param name="createAndAssignTaskForEmployeeReqDto"></param>
        /// <returns></returns>
        public async Task<bool> CreateAndAssignTaskForEmployee(CreateAndAssignTaskForEmployeeReqDto createAndAssignTaskForEmployeeReqDto)
        {
            var employee = await GetEmployee(createAndAssignTaskForEmployeeReqDto.EmployeeId);

            var createTaskGoReqDto = new CreateTaskGoReqDto
            {
                ProjectId = createAndAssignTaskForEmployeeReqDto.ProjectId,
                Name = createAndAssignTaskForEmployeeReqDto.Name,
                Description = createAndAssignTaskForEmployeeReqDto.Description
            };
            var createTaskGoResDto = await CreateTask(createTaskGoReqDto);

            var assignTaskGoReqDto = new AssignTaskGoReqDto
            {
                TaskId = createTaskGoResDto.Id,
                EmployeeId = createAndAssignTaskForEmployeeReqDto.EmployeeId
            };

            await AssignTask(assignTaskGoReqDto);

            var mailWhenAssignTaskPath = Directory.GetCurrentDirectory() + "/FileTemplate/Mail/MailWhenAssignTask.html";
            var mailWhenAssignTask = File.ReadAllText(mailWhenAssignTaskPath);
            mailWhenAssignTask = mailWhenAssignTask.Replace("##EMPLOYEE_NAME##", employee.Name)
                .Replace("##TASK_NAME##", createAndAssignTaskForEmployeeReqDto.Name);

            var emailAssignTaskDto = new EmailContentDto
            {
                Email = employee.Email,
                Subject = "[HDV.Nhom2] Thông báo công việc mới",
                Body = mailWhenAssignTask
            };
            _ = AddSendMailToQueue(emailAssignTaskDto);

            return true;
        }

        private async Task<EmployeeGoDto> GetEmployee(string employeeId)
        {
            try
            {
                var url = $"{_companyServiceOptions.Value.BaseUrl}/employee/{employeeId}";
                var getEmployeeResponse = await _callService.CallRestApiAsync(url, "GET", null);
                if(getEmployeeResponse.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Nhom2Exception("E3000", "Lỗi hệ thống", System.Net.HttpStatusCode.InternalServerError);
                }

                var employeeGoDto = JsonConvert.DeserializeObject<EmployeeGoDto>(getEmployeeResponse.JsonObject);
                return employeeGoDto;
            }
            catch (Exception ex)
            {
                throw new Nhom2Exception("E3000", "Lỗi hệ thống", System.Net.HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Tạo mới công việc
        /// </summary>
        /// <returns></returns>
        private async Task<CreateTaskGoResDto> CreateTask(CreateTaskGoReqDto createTaskGoReqDto)
        {
            try
            {
                var url = $"{_projectServiceOptions.Value.BaseUrl}/project/{createTaskGoReqDto.ProjectId}/task";
                var createTaskGoResponse = await _callService.CallRestApiAsync(url, "POST", createTaskGoReqDto);
                if(createTaskGoResponse.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Nhom2Exception("E3000", "Lỗi hệ thống", System.Net.HttpStatusCode.InternalServerError);
                }

                var createTaskGoResDto = JsonConvert.DeserializeObject<CreateTaskGoResDto>(createTaskGoResponse.JsonObject);
                return createTaskGoResDto;
            }
            catch (Exception ex)
            {
                Log.Logger.Error("EmployeeService-CreateTask-Exception: {ex}", ex);
                throw new Nhom2Exception("E3000", "Lỗi hệ thống", System.Net.HttpStatusCode.InternalServerError);
            }
        }

        private async Task AssignTask(AssignTaskGoReqDto assignTaskGoReqDto)
        {
            try
            {
                var url = $"{_projectServiceOptions.Value.BaseUrl}/task-assign";

                var assignTaskGoResponse = await _callService.CallRestApiAsync(url, "POST", assignTaskGoReqDto);

                if (assignTaskGoResponse.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Nhom2Exception("E3000", "Lỗi hệ thống", System.Net.HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Error("EmployeeService-AssignTask-Exception: {ex}", ex);
                throw new Nhom2Exception("E3000", "Lỗi hệ thống", System.Net.HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Lấy danh sách nhân viên bằng email hoặc tên
        /// </summary>
        /// <param name="emailOrName"></param>
        /// <returns></returns>
        public async Task<GetListEmployeeDto<EmployeeDto>> GetListEmployee(string emailOrName, int companyId)
        {
            //var url = $"{_companyServiceOptions.Value.BaseUrl}/employee/search";
            var url = $"{_projectNetServiceOptions.Value.BaseUrl}/employees/list?keyword={emailOrName}&companyId={companyId}";

            var getListEmployeeGoResponse = await _callService.CallRestApiAsync(url, "GET", null);

            Log.Logger.Debug("EmployeeService-GetListEmployee-CallService-Req: {@getListEmployeeGoResponse}", getListEmployeeGoResponse);

            if(getListEmployeeGoResponse.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Nhom2Exception("E3000", "Lỗi hệ thống", System.Net.HttpStatusCode.InternalServerError);
            }

            var getListEmployeeGoRes = JsonConvert.DeserializeObject<List<EmployeeGoDto>>(getListEmployeeGoResponse.JsonObject);

            var getListEmployeeRes = new GetListEmployeeDto<EmployeeDto>();

            if(getListEmployeeGoRes.Count > 0)
            {
                foreach(var employee in getListEmployeeGoRes)
                {
                    getListEmployeeRes.Items.Add(new EmployeeDto
                    {
                        Id = employee.Id,
                        Name = employee.Name,
                        Email = employee.Email,
                        DateOfBirth = employee.DateOfBirth,
                        Gender = employee.Gender,
                        Role = employee.Role
                    });
                }


                getListEmployeeRes.TotalCount = getListEmployeeRes.Items.Count;
            }

            return getListEmployeeRes;
        }


        public async Task<AuthResDto> LoginAsync(AuthReqDto authReqDto)
        {
            var res = new AuthResDto();
            try
            {
                var urlLogin = $"{_authServiceOptions.Value.BaseUrl}/users/authenticate";

                var authUserReqDto = new AuthUserReqDto
                {
                    Email = authReqDto.Email,
                    Password = authReqDto.Password
                };

                var authUserResponse = await _callService.CallRestApiAsync(urlLogin, "POST", authUserReqDto);

                if(authUserResponse.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    var errorObj = JsonConvert.DeserializeObject<ErrorObj>(authUserResponse.JsonObject);
                    throw new Nhom2Exception(errorObj.ErrorCode, errorObj.ErrorMessage, authUserResponse.StatusCode);
                }
                var authUserResDto = JsonConvert.DeserializeObject<AuthUserResDto>(authUserResponse.JsonObject);

                res.Email = authUserResDto.Email;
                res.Token = authUserResDto.Token;
                //var listEmployee = await GetListEmployee(res.Email);
                //var employee = listEmployee.Items[0];

                var employeeNetUrl = $"{_projectNetServiceOptions.Value.BaseUrl}/employees/email?email={res.Email}";
                var employeeNetResponse = await _callService.CallRestApiAsync(employeeNetUrl, "GET", null);

                Log.Logger.Debug("EmployeeService-GetEmployee-CallService: {@employeeNetResponse}", employeeNetResponse);

                var employeeNet = JsonConvert.DeserializeObject<EmployeeNetDto>(employeeNetResponse.JsonObject);

                Log.Logger.Debug("EmployeeService-GetEmployee-CallService: {@employeeNet}", employeeNet);
                res.Id = employeeNet.Id.ToString();
                //res.Gender = employeeNet.Gender;
                res.Role = employeeNet.Role;
                res.CompanyId = employeeNet.CompanyId;
            }
            catch (Exception ex)
            {
                Log.Logger.Debug("EmployeeService-GetEmployee-Exception: {ex}", ex);
                if (ex is Nhom2Exception)
                    throw;
                throw new Nhom2Exception("E3000", "Lỗi hệ thống", System.Net.HttpStatusCode.InternalServerError);
            }

            return res;
        }
    }
}
