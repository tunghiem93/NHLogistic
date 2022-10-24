using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NHLogistic.RestAPI
{
    public interface IRestApiServices
    {
        Task<ResponseModels> PostAsJson<TViewModel>(string ApiURL, TViewModel model);
        Task<ResponseModels> GetAsJson<TViewModel>(string ApiURL, string search = null);
        Task<ResponseModels> DeleteJson<TViewModel>(string ApiURL);
    }
}