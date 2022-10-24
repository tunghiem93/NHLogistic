using DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace NHLogistic.RestAPI
{
    public class RestApiServices : IRestApiServices
    {

        private static string ApiEndPoint = "http://localhost:44350/";
        private void InitHeaderRequest(HttpClient Client)
        {
            try
            {
                //ISession _session = _httpContextAccessor.HttpContext.Session;
                Client.BaseAddress = new Uri(ApiEndPoint);
                //var jwtToken = SessionHelpers.GetObject<string>(_session, "jwtToken");
                //if (!string.IsNullOrEmpty(jwtToken))
                //{
                //    Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtToken);
                //}
                Client.DefaultRequestHeaders.Accept.Clear();
                Client.DefaultRequestHeaders.ConnectionClose = true;
            }
            catch (Exception ex) { }
        }

        public async Task<ResponseModels> DeleteJson<TViewModel>(string ApiURL)
        {
            var response = new ResponseModels();
            HttpClient client = new HttpClient();
            InitHeaderRequest(client);
            try
            {
                HttpResponseMessage httpResponse = await client.GetAsync(ApiURL);
                var statusCode = (int)httpResponse.StatusCode;
                if (statusCode == (int)HttpStatusCode.Unauthorized || statusCode == (int)HttpStatusCode.Forbidden)
                {
                    response.Status = statusCode;
                    response.Message = "Tài khoản của bạn không có quyền truy cập tài nguyên";
                    response.Success = false;
                }
                else
                {
                    var result = await httpResponse.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(result))
                    {
                        response = JsonConvert.DeserializeObject<ResponseModels>(result);
                        if (response.Data != null)
                        {
                            response.Data = "";
                        }
                    }
                }
            }
            catch (Exception) { }
            return response;
        }

        public async Task<ResponseModels> GetAsJson<TViewModel>(string ApiURL, string search = null)
        {
            var response = new ResponseModels();
            HttpClient client = new HttpClient();
            InitHeaderRequest(client);
            try
            {
                if (search != null)
                {
                    var dataAsString = JsonConvert.SerializeObject(search);
                    var content = new StringContent(dataAsString);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                }
                HttpResponseMessage httpResponse = await client.GetAsync(ApiURL);
                var statusCode = (int)httpResponse.StatusCode;
                if (statusCode == (int)HttpStatusCode.Unauthorized || statusCode == (int)HttpStatusCode.Forbidden)
                {
                    response.Status = statusCode;
                    response.Message = "Tài khoản của bạn không có quyền truy cập tài nguyên";
                    response.Success = false;
                }
                else
                {
                    var result = await httpResponse.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(result))
                    {
                        response = JsonConvert.DeserializeObject<ResponseModels>(result);
                        if (response.Data != null)
                        {
                            response.Data = "";
                        }
                    }
                    response.Status = statusCode;
                }
            }
            catch (Exception ex) { }
            finally
            {
                client.CancelPendingRequests();
            }
            return response;
        }

        public async Task<ResponseModels> PostAsJson<TViewModel>(string ApiURL, TViewModel model)
        {
            HttpClient client = new HttpClient();
            var response = new ResponseModels();
            InitHeaderRequest(client);
            try
            {
                if (model != null)
                {
                    var dataAsString = JsonConvert.SerializeObject(model);
                    var content = new StringContent(dataAsString);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    HttpResponseMessage httpResponse = await client.PostAsync(ApiURL, content);
                    var statusCode = (int)httpResponse.StatusCode;
                    if (statusCode == (int)HttpStatusCode.Unauthorized || statusCode == (int)HttpStatusCode.Forbidden)
                    {
                        response.Status = statusCode;
                        response.Message = "Tài khoản của bạn không có quyền truy cập tài nguyên";
                        response.Success = false;
                    }
                    else
                    {
                        var result = await httpResponse.Content.ReadAsStringAsync();
                        if (!string.IsNullOrEmpty(result))
                        {
                            response = JsonConvert.DeserializeObject<ResponseModels>(result);
                            if (response.Data != null)
                            {
                                response.Data = "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { }
            finally
            {
                client.CancelPendingRequests();
            }
            return response;
        }
    }
}
