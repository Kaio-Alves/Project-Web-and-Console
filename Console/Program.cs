using ShoppingCart.Model;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ProjectCMD
{
    public class Product
    {
        public string ClientName { get; set; }
        public string ProductName { get; set; }
        public string InfoProduct { get; set; }
    }
    class Program
    {
        static HttpClient client = new HttpClient();
        static async Task<Uri> CreateProductAsync(Product product)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "InsertData", product);
            response.EnsureSuccessStatusCode();
            return response.Headers.Location;
        }
        static async Task RunAsync()
        {
            client.BaseAddress = new Uri("http://localhost:44372/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                Product product = new Product
                {
                    ClientName = "ViaHub",
                    ProductName = "Carro6",
                    InfoProduct = "vendido"
                };

                var url = await CreateProductAsync(product);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
        static void Main(string[] args)
        {
            //Executando Part. 1 do desafio proposto:
            RunAsync();
            //Executando Part. 2 do desafio proposto:
            Cars[] car = new Cars[] { new Cars { name = "Carro1", time = 10}, new Cars { name = "Carro2", time = 5},
                new Cars { name = "Carro3", time = 1}, new Cars { name = "Carro4", time = 1}, 
                new Cars { name = "Carro5", time = 1}};
            int gasolinePump = 3;
            GasStationVia.GasStation(car, gasolinePump);
        }
    }
}
