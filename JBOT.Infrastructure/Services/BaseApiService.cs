using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public virtual async Task<RestResponse<T>> SendRequest<T>(RestRequest request)
        {
            return await _restClient.ExecuteGetAsync<T>(request);
        }
    }
}
