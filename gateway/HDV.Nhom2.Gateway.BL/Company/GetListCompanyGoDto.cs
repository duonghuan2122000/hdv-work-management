using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.Gateway.BL
{
    /// <summary>
    /// Req lấy danh sách công ty từ service go
    /// </summary>
    /// CreatedBy: dbhuan 25/06/2022
    //public class GetListCompanyGoReqDto
    //{
    //    public int MyProperty { get; set; }
    //}

    public class GetListCompanyGoResDto
    {
        [JsonProperty("items")]
        public List<CompanyDto> Items { get; set; }
    }
}
