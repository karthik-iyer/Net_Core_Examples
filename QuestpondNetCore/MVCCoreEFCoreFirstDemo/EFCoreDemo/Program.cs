using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace EFCoreDemo
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true).Build();

            var meowData = await ReadFunData(configuration);

            var dbContext = new CustomerDbContext(configuration);

            dbContext.Database.EnsureCreated();

            var customer = new Customer
            {
                Name = "Raju",
                YourMeowData = meowData[0]
            };
            dbContext.Add(customer);
            dbContext.SaveChanges();

            var cust= dbContext.Customers.FirstOrDefault(x => x.Id == 1);

            Console.WriteLine($"Customer name : {cust?.YourMeowData}");

        }

        public static async Task<string[]> ReadFunData(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var obj = await client.GetStringAsync(configuration["MeowUri"]);
            var catData = JsonConvert.DeserializeObject<RandomCatData>(obj);
            return catData.data;
        }
    }
}