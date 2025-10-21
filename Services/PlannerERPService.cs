using System.Net.Http;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Sanibell_ProductionModule.ViewModels;

namespace Sanibell_ProductionModule.Services
{
    public class PlannerErpService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;

        public PlannerErpService(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        public async Task<string> SendProductionOrderToErpAsync(PlanningViewModel planning)
        {
            // Building URL
            var baseUrl = _config["PlannerERPSettings:BaseUrl"]?.TrimEnd('/');
            var token = _config["PlannerERPSettings:Add_Order_Secret"];
            var relativePath = "Productieorder_Toevoegen";

            var url = new Uri(new Uri(baseUrl + "/"), relativePath);

            // Building Payload 
            var payload = new
            {
                ReceptCode = planning.ArticleDescription,
                AantalTeProduceren = planning.Amount,
                Status = "INGEPLAND",
                DefaultMagazijnComponenten = 1,
                DefaultMagazijnEindproduct = 1
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = content
            };

            request.Headers.TryAddWithoutValidation("ACCESS-TOKEN", token);

            // send en processing response 
            var response = await client.SendAsync(request);

            // Debugging: log request + response
            var responseBody = await response.Content.ReadAsStringAsync();
            var requestBody = await request.Content.ReadAsStringAsync();

            Console.WriteLine($"Request URL: {url}");
            Console.WriteLine($"Request Body: {requestBody}");
            Console.WriteLine($"Response Status: {response.StatusCode}");
            Console.WriteLine($"Response Body: {responseBody}");

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Fout bij aanmaken productieorder: {response.StatusCode} - {responseBody}");
            }

            using var doc = JsonDocument.Parse(responseBody);
            var root = doc.RootElement;

            if (root.ValueKind == JsonValueKind.Array && root.GetArrayLength() > 0)
            {
                var firstItem = root[0];
                if (firstItem.TryGetProperty("ProductieorderNummer", out var orderNumberElement))
                {
                    return orderNumberElement.GetRawText(); // Or GetString() if string
                }
            }

            throw new Exception("Response bevat geen geldig ProductieorderNummer");
        }

        public async Task UnlockProductionOrderAsync(string productieorderNummer)
        {
            // Building URL
            var baseUrl = _config["PlannerERPSettings:BaseUrl"]?.TrimEnd('/');
            var token = _config["PlannerERPSettings:Unlock_Order_Secret"];
            var relativePath = "Productieorder_LockVrijgeven";

            var url = new Uri(new Uri(baseUrl + "/"), relativePath);

            var payload = new
            {
                ProductieorderNummer = productieorderNummer
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = content
            };
            request.Headers.TryAddWithoutValidation("ACCESS-TOKEN", token);

            var response = await client.SendAsync(request);
            var responseBody = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"Unlock Order response: {response.StatusCode} - {responseBody}");

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Fout bij unlocken productieorder: {response.StatusCode} - {responseBody}");
        }

        public async Task ProductionOrderCreatedByAsync(string productieorderNummer, string gebruiker)
        {
            // Building URL
            var baseUrl = _config["PlannerERPSettings:BaseUrl"]?.TrimEnd('/');
            var token = _config["PlannerERPSettings:MadeBy_Order_Secret"];
            var relativePath = "Productieorder_VrijeRubriek_Wijzigen";

            var url = new Uri(new Uri(baseUrl + "/"), relativePath);

            var payload = new
            {
                ProductieorderNummer = productieorderNummer,
                RubriekOmschrijving = "CreatedBy",
                RubriekInhoud = gebruiker
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = content
            };
            request.Headers.TryAddWithoutValidation("ACCESS-TOKEN", token);

            var response = await client.SendAsync(request);
            var responseBody = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"Unlock Order response: {response.StatusCode} - {responseBody}");

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Fout bij aanpassen Creator: {response.StatusCode} - {responseBody}");
        }

        public async Task ProductionOrderUrgencyAsync(string productieorderNummer, bool Urgency)
        {
            // Building URL
            var baseUrl = _config["PlannerERPSettings:BaseUrl"]?.TrimEnd('/');
            var token = _config["PlannerERPSettings:MadeBy_Order_Secret"];
            var relativePath = "Productieorder_VrijeRubriek_Wijzigen";

            var url = new Uri(new Uri(baseUrl + "/"), relativePath);

            var payload = new
            {
                ProductieorderNummer = productieorderNummer,
                RubriekOmschrijving = "Urgency",
                RubriekInhoud = Urgency
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = content
            };
            request.Headers.TryAddWithoutValidation("ACCESS-TOKEN", token);

            var response = await client.SendAsync(request);
            var responseBody = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"Unlock Order response: {response.StatusCode} - {responseBody}");

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Fout bij aanpassen Urgentie: {response.StatusCode} - {responseBody}");
        }
    }
}