using HDV.Nhom2.Infrastructure.Contracts.CallService;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.Infrastructure.CallService
{
    public class CallService: ICallService
    {
        public CallService()
        {

        }

        public async Task<CallServiceRes> CallRestApiAsync(string url, string method, object objReq = null, string contentType = "application/json", Dictionary<string, string> headers = null)
        {
            var callServiceRes = new CallServiceRes();

            try
            {
                var options = new RestClientOptions(url)
                {
                    MaxTimeout = 30000
                };

                var client = new RestClient(options);

                var request = new RestRequest();

                if(headers != null)
                {
                    foreach (var header in headers)
                    {
                        request.AddHeader(header.Key, header.Value);
                    }
                }

                request.Method = method.ToLower() switch
                {
                    "get" => Method.Get,
                    "post" => Method.Post,
                    "put" => Method.Put,
                    "patch" => Method.Patch,
                    "delete" => Method.Delete,
                    _ => Method.Get,
                };

                switch (contentType)
                {
                    case "application/json":
                        if (objReq != null)
                        {
                            var objReqStr = JsonConvert.SerializeObject(objReq);
                            request.AddStringBody(objReqStr, ContentType.Json);
                        }
                        break;

                    default:
                        if(objReq != null)
                        {
                            var objReqStr = JsonConvert.SerializeObject(objReq);
                            request.AddStringBody(objReqStr, ContentType.Json);
                        }
                        break;
                }

                Log.Logger.Debug("CallService-CallRestApiAsync: Url={url} - Method={method} - Request={@request}", url, method, request);
                var response = await client.ExecuteAsync(request);
                Log.Logger.Debug("CallService-CallRestApiAsync: Url={url} - Method={method} - Response={@response}", url, method, response);

                callServiceRes.StatusCode = response.StatusCode;
                callServiceRes.JsonObject = response.Content;
            }
            catch (Exception ex)
            {
                Log.Logger.Error("CallService-CallRestApiAsync-Exception: {ex}", ex);
            }

            return callServiceRes;
        }
    }
}
