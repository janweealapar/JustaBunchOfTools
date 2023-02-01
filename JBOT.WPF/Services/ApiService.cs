using Azure;
using JBOT.Application.Dtos;
using JBOT.Domain.Entities;
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
        Task<List<DatabaseDto>> GetDatabaseDtos(string server);
        Task<List<TestableObjectDto>> TestableObjectDtos(string server,int databaseId);
        Task<TestableObjectDetailsDto> GetTestableObjectDetails(string server, int databaseId, int objectId);
        Task<TestableObjectDetailsDto> GetUnitTestById(int id);
        Task<TestableObjectDetailsDto> QuickRun(TestableObjectDetailsDto unitTest, string server);
        Task<TestableObjectDetailsDto> Add(TestableObjectDetailsDto unitTest);
        Task<TestableObjectDetailsDto> Update(TestableObjectDetailsDto unitTest);
        Task<List<UnitTestDto>> GetUnitTests(string server, string database);
        Task<int> RemoveUnitTest(int id);
        Task<List<string>> GetServers();
        Task<List<UnitTestDto>> RunTest(string server, string database);
    }
    public class ApiService : BaseApiService, IApiService
    {
        public ApiService(): base(ConfigurationManager.AppSettings["baseUrl"]?.ToString() ?? string.Empty)
        {
        }

        public async Task<List<DatabaseDto>> GetDatabaseDtos(string server)
        {
            var request = new RestRequest("api/Database");
            request.AddParameter("Server", server);
            var response = await SendRequest<List<DatabaseDto>>(request);
            
            return response.Data ?? new List<DatabaseDto>();
        }

        public async Task<List<TestableObjectDto>> TestableObjectDtos(string server,int databaseId)
        {
            string resource = $"api/Database/{databaseId}/TestableObjects";
            var request = new RestRequest(resource);
            request.AddParameter("Server", server);

            var response = await SendRequest<List<TestableObjectDto>>(request);

            return response.Data ?? new List<TestableObjectDto>();
        }

        public async Task<TestableObjectDetailsDto> GetTestableObjectDetails(string server, int databaseId, int objectId)
        {
            string resource = $"api/Database/{databaseId}/TestableObjects/details";
            var request = new RestRequest(resource);
            request.AddParameter("Server", server);
            request.AddParameter("objId", objectId);


            var response = await SendRequest<TestableObjectDetailsDto>(request);

            return response.Data ?? new TestableObjectDetailsDto();
        }

        public async Task<TestableObjectDetailsDto> QuickRun(TestableObjectDetailsDto unitTest, string server)
        {
            string resource = $"api/UnitTest/Run";
            var request = new RestRequest(resource);
            request.AddJsonBody(unitTest);
            request.AddQueryParameter("server",server);

            var response = await SendRequest<TestableObjectDetailsDto>(request,Method.Post);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                unitTest.Status = StatusEnums.Failed;

            return response.Data ?? unitTest;
        }

        public async Task<TestableObjectDetailsDto> Add(TestableObjectDetailsDto unitTest)
        {
            string resource = $"api/UnitTest";
            var request = new RestRequest(resource);
            //string json = JsonConvert.SerializeObject(unitTest);
            request.AddJsonBody(unitTest);

            var response = await SendRequest<TestableObjectDetailsDto>(request, Method.Post);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                unitTest.Status = StatusEnums.Failed;

            return response.Data ?? unitTest;
        }

        public async Task<List<UnitTestDto>> GetUnitTests(string server, string database)
        {
            var request = new RestRequest("api/UnitTest");
            request.AddParameter("server", server);
            request.AddParameter("database", database);
            var response = await SendRequest<List<UnitTestDto>>(request);

            return response.Data ?? new List<UnitTestDto>();
        }

        public async Task<int> RemoveUnitTest(int id)
        {
            string resource = $"api/UnitTest";
            var request = new RestRequest(resource);
            request.AddQueryParameter("id", id);

            var response = await SendRequest<int>(request,Method.Delete);  
            return (int)response.StatusCode;
        }

        public async Task<TestableObjectDetailsDto> GetUnitTestById(int id)
        {
            string resource = $"api/UnitTest";
            var request = new RestRequest(resource);
            request.AddQueryParameter("id", id);

            var response = await SendRequest<TestableObjectDetailsDto>(request);

            return response.Data ?? new TestableObjectDetailsDto();
        }

        public async Task<TestableObjectDetailsDto> Update(TestableObjectDetailsDto unitTest)
        {
            string resource = $"api/UnitTest";
            var request = new RestRequest(resource);
            request.AddJsonBody(unitTest);

            var response = await SendRequest<int>(request, Method.Put);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                unitTest.Status = StatusEnums.Failed;

            return unitTest;
        }

        public async Task<List<string>> GetServers()
        {
            var response = await SendRequest<List<string>>(new RestRequest("api/Database/Servers"));

            return response.Data ?? new List<string>();
        }

        public async Task<List<UnitTestDto>> RunTest(string server, string database)
        {
            var request = new RestRequest("api/UnitTest/Run/All");
            request.AddQueryParameter("server", server);
            request.AddQueryParameter("database", database);

            var response = await SendRequest<List<UnitTestDto>>(request,Method.Post);
            

            return response.Data ?? new List<UnitTestDto>();
        }
    }
}
