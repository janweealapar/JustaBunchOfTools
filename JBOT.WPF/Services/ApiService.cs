using JBOT.Application.Dtos;
using JBOT.Infrastructure.Services;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBOT.WPF.Services
{
    public interface IApiService
    {
        Task<List<DatabaseDto>> GetDatabaseDtos();
    }
    public class ApiService : BaseApiService, IApiService
    {
        public ApiService(): base(ConfigurationManager.AppSettings["baseUrl"]?.ToString() ?? string.Empty)
        {
        }

        public async Task<List<DatabaseDto>> GetDatabaseDtos()
        {
            var response = await SendRequest<List<DatabaseDto>>(new RestRequest("api/Database"));
            
            return response.Data ?? new List<DatabaseDto>();
        }
    }
}
