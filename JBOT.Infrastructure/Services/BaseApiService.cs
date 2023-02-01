using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JBOT.Infrastructure.Services
{
    public abstract class BaseApiService
    {
        public string BaseUrl { get; private set; }
        private readonly RestClient _restClient;
        public BaseApiService(string baseUrl)
        {
            BaseUrl = baseUrl;
            _restClient = new RestClient(baseUrl);
        }

        public virtual async Task<RestResponse<T>> SendRequest<T>(RestRequest request, Method method = Method.Get)
        {
            switch (method)
            {
                case Method.Get:
                    return await _restClient.ExecuteGetAsync<T>(request);
                case Method.Post:
                    return await _restClient.ExecutePostAsync<T>(request);
                case Method.Put:
                    return await _restClient.ExecuteAsync<T>(request,Method.Put);
                case Method.Delete:
                    return await _restClient.ExecuteAsync<T>(request, Method.Delete);
                case Method.Head:
                case Method.Options:
                case Method.Patch:
                case Method.Merge:
                case Method.Copy:
                case Method.Search:
                default:
                    return await _restClient.ExecuteGetAsync<T>(request);
            }
            
        }
    }
}
