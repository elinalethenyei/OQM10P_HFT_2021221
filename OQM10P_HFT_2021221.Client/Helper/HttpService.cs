using OQM10P_HFT_2021221.Models.ResponseObjects;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace OQM10P_HFT_2021221.Client.Helper
{
    public class HttpService<TEntity, TKey>
    {
        string controllerName;

        Uri baseAddress;

        JsonSerializerOptions serializerOptions;

        public HttpService(string controllerName, string baseAddress)
        {
            this.controllerName = controllerName;
            this.baseAddress = new Uri(baseAddress);
            serializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        }

        public List<TEntity> GetAll(string actionName = null)
        {
            using (var client = new HttpClient())
            {
                InitClient(client);

                var response = client.GetAsync(GetActionName(actionName ?? nameof(GetAll))).GetAwaiter().GetResult(); // Block here
                return JsonSerializer.Deserialize<List<TEntity>>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult(), serializerOptions);
            }
        }

        public TEntity Get(TKey id, string actionName = null)
        {
            using (var client = new HttpClient())
            {
                InitClient(client);

                var response = client.GetAsync($"{GetActionName(actionName ?? nameof(Get))}/{id}").GetAwaiter().GetResult(); // Block here
                return JsonSerializer.Deserialize<TEntity>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult(), serializerOptions);
            }
        }

        public ApiResult Create(TEntity entity, string actionName = null)
        {
            using (var client = new HttpClient())
            {
                InitClient(client);

                var response = client.PostAsJsonAsync(GetActionName(actionName ?? nameof(Create)), entity).GetAwaiter().GetResult(); // Block here
                return JsonSerializer.Deserialize<ApiResult>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult(), serializerOptions);
            }
        }

        public ApiResult Update(TEntity entity, string actionName = null)
        {
            using (var client = new HttpClient())
            {
                InitClient(client);

                var response = client.PutAsJsonAsync(GetActionName(actionName ?? nameof(Update)), entity).GetAwaiter().GetResult(); // Block here
                return JsonSerializer.Deserialize<ApiResult>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult(), serializerOptions);
            }
        }

        public ApiResult Delete(TKey id, string actionName = null)
        {
            using (var client = new HttpClient())
            {
                InitClient(client);

                var response = client.DeleteAsync($"{GetActionName(actionName ?? nameof(Delete))}/{id}").GetAwaiter().GetResult(); // Block here
                return JsonSerializer.Deserialize<ApiResult>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult(), serializerOptions);
            }
        }

        private string GetActionName(string actionName) => $"{controllerName}/{actionName}";

        private void InitClient(HttpClient client)
        {
            client.BaseAddress = baseAddress;
        }
    }
}
