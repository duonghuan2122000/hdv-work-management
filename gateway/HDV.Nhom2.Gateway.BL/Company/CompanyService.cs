using HDV.Nhom2.Infrastructure.Contracts.CallService;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.Gateway.BL
{
    /// <summary>
    /// Service công ty
    /// </summary>
    /// CreatedBy: dbhuan 19/6/2022
    public class CompanyService: ICompanyService
    {
        #region Khởi tạo

        private readonly ICallService _callService;

        private readonly IOptions<CompanyServiceOption> _companyServiceOptions;

        public CompanyService(ICallService callService,
            IOptions<CompanyServiceOption> companyServiceOptions)
        {
            _callService = callService;
            _companyServiceOptions = companyServiceOptions;
        }
        #endregion

        #region Hàm
        /// <summary>
        /// Lấy thông tin công ty bằng id
        /// </summary>
        /// CreatedBy: dbhuan 19/06/2022
        public async Task<CompanyDto> GetById(string id)
        {
            var companyDto = new CompanyDto();
            
            try
            {
                var getCompanyByIdUrl = $"{_companyServiceOptions.Value.BaseUrl}/company/{id}";

                Log.Logger.Debug("CompanyService-GetById-CallService: url={getCompanyByIdUrl}", getCompanyByIdUrl);
                var getCompanyByIdResponse = await _callService.CallRestApiAsync(getCompanyByIdUrl, "GET", null);
                Log.Logger.Debug("CompanyService-GetById-CallService: Res: {@getCompanyByIdResponse}", getCompanyByIdResponse);

                if(getCompanyByIdResponse.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Nhom2Exception("E1001", "Công ty không hợp lệ");
                }

                companyDto = JsonConvert.DeserializeObject<CompanyDto>(getCompanyByIdResponse.JsonObject);
            }
            catch (Nhom2Exception ex)
            {
                Log.Logger.Error("CompanyService-GetById-Exception: {ex}", ex);
                throw;
            }
            catch (Exception ex)
            {
                Log.Logger.Error("CompanyService-GetById-Exception: {ex}", ex);
                throw new Nhom2Exception("E3000", "Lỗi hệ thống");
            }

            return companyDto;
        }

        /// <summary>
        /// Lấy tất cả danh sách công ty
        /// </summary>
        /// CreatedBy: dbhuan 25/06/2022
        public async Task<List<CompanyDto>> GetListAsync()
        {
            var getListCompanyGoUrl = $"{_companyServiceOptions.Value.BaseUrl}/companies?page=1&limit=100000";

            var getListCompanyGoResponse = await _callService.CallRestApiAsync(getListCompanyGoUrl, "GET", null);

            if(getListCompanyGoResponse.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Nhom2Exception("E3000", "Lỗi hệ thống", System.Net.HttpStatusCode.InternalServerError);
            }

            var getListCompanyRes = JsonConvert.DeserializeObject<GetListCompanyGoResDto>(getListCompanyGoResponse.JsonObject);
            return getListCompanyRes.Items;
        }
        #endregion
    }
}
