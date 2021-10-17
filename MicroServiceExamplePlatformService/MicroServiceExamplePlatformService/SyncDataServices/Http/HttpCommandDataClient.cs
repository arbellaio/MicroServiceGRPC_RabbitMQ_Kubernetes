using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MicroServiceExamplePlatformService.Dtos;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace MicroServiceExamplePlatformService.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClientService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task SendPlatformToCommand(PlatformReadDto platformReadDto)
        {
            var httpContent = new StringContent(
                JsonConvert.SerializeObject(platformReadDto), 
                Encoding.UTF8,
                "application/json");
            Console.WriteLine(await httpContent.ReadAsStringAsync());
            var response = await _httpClient.PostAsync($"{_configuration["CommandService"]}",httpContent);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(" ----> Sync POST to Command Service is OK");
            }
            else
            {
                Console.WriteLine(" ----> Sync POST to Command Service is Not OK");
            }
        }
    }
}