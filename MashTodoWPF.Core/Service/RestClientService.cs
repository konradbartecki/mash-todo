using MashTodo.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MashTodo.Service
{
    public class RestClientService
    {
        private static HttpClient _client;
        private readonly IMashAppConfig _MashAppConfig;

        private HttpClient Client => _client ?? (_client = CreateClient());

        private HttpClient CreateClient()
        {
            return new HttpClient()
            {
                BaseAddress = new Uri(_MashAppConfig.BaseAddress)
            };
        }

        public RestClientService(IMashAppConfig mashAppConfig)
        {
            _MashAppConfig = mashAppConfig;
        }

        public async Task<IEnumerable<TodoItem>> GetAllTodos()
        {
            var response = await Client.GetAsync(string.Empty); //api/TodoItems
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<TodoItem>>(json);
        }

        public async Task<Guid> CreateTask(TodoItem task)
        {
            var jsonPayload = JsonConvert.SerializeObject(task);
            var request = new HttpRequestMessage(HttpMethod.Post, "");
            request.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            var response = await Client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TodoItem>(result).Id;
        }

        public async Task UpdateTask(TodoItem task)
        {
            var jsonPayload = JsonConvert.SerializeObject(task);
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), "");
            request.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            var response = await Client.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteTask(Guid id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, id.ToString());
            var response = await Client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
        }
    }
}