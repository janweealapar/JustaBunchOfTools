using Azure;
using JBOT.Application.Dtos;
using JBOT.Domain.Entities.Enums;
using JBOT.Infrastructure.Services;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Common;

namespace JBOT.WPF.Services
{
    public interface IApiService
    {
        Task<List<DatabaseDto>> GetDatabaseDtos();
        Task<List<TestableObjectDto>> TestableObjectDtos(int databaseId);
        Task<TestableObjectDetailsDto> GetTestableObjectDetails(int databaseId, int objectId);
        Task<TestableObjectDetailsDto> QuickRun(TestableObjectDetailsDto unitTest);
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

        public async Task<List<TestableObjectDto>> TestableObjectDtos(int databaseId)
        {
            string resource = $"api/Database/{databaseId}/TestableObjects";
            var response = await SendRequest<List<TestableObjectDto>>(new RestRequest(resource));

            return response.Data ?? new List<TestableObjectDto>();
        }

        public async Task<TestableObjectDetailsDto> GetTestableObjectDetails(int databaseId, int objectId)
        {
            string resource = $"api/Database/{databaseId}/TestableObjects/details";
            var request = new RestRequest(resource);
            request.AddParameter("objId", objectId);

            var response = await SendRequest<TestableObjectDetailsDto>(request);

            return response.Data ?? new TestableObjectDetailsDto();
        }

        public async Task<TestableObjectDetailsDto> QuickRun(TestableObjectDetailsDto unitTest)
        {
            string resource = $"api/UnitTest/Run";
            var request = new RestRequest(resource);
            string json = JsonConvert.SerializeObject(unitTest);
            request.AddJsonBody(unitTest);

            var response = await SendRequest<TestableObjectDetailsDto>(request,Method.Post);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                unitTest.Status = StatusEnums.Failed;

            return response.Data ?? unitTest;
        }
    }
}
