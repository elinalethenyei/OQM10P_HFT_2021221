using OQM10P_HFT_2021221.Models;
using OQM10P_HFT_2021221.Models.ResponseObjects;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace OQM10P_HFT_2021221.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Waiting for server...");
            Console.ReadLine();

            var serializerOption = new JsonSerializerOptions(JsonSerializerDefaults.Web);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:51332/api/");
                var response = client.GetAsync("Issue/GetAll").GetAwaiter().GetResult();
                Console.WriteLine(response);
                //Console.WriteLine(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
                var issueString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var issues = JsonSerializer.Deserialize<List<Issue>>(issueString, serializerOption);
                Console.ReadLine();

                var newIssue = new Issue("Új feladat alapértelmezett beállításokkal");
                var stringResponse = client.PostAsJsonAsync<Issue>("Issue/Create", newIssue).GetAwaiter().GetResult();
                var issueCreateResult = JsonSerializer.Deserialize<ApiResult>(stringResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult());
                Console.ReadLine();


            }

        }
    }
}
