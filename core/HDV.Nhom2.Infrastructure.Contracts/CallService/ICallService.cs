using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.Infrastructure.Contracts.CallService
{
    public interface ICallService
    {
        Task<CallServiceRes> CallRestApiAsync(string url, string method, object objReq, string contentType = "application/json", Dictionary<string, string> headers = null);
    }

    public class CallServiceRes
    {
        public HttpStatusCode StatusCode { get; set; }

        public string JsonObject { get; set; }
    }
}
