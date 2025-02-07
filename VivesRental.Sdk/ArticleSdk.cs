using System.Net.Http;
using System.Net.Http.Json;
using VivesRental.Enums;
using VivesRental.Services.Model.Filters;
using VivesRental.Services.Model.Requests;
using VivesRental.Services.Model.Results;

namespace VivesRental.Sdk
{
    public class ArticleSdk
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ArticleSdk(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ArticleResult?> Get(Guid id)
        {
            var httpClient = _httpClientFactory.CreateClient("VivesRentalApi");
            var response = await httpClient.GetAsync($"api/Article/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ArticleResult>();
        }

        public async Task<IList<ArticleResult>> Find(Guid? productId = null, IList<Guid>? articleIds = null)
        {
            var httpClient = _httpClientFactory.CreateClient("VivesRentalApi");

            // Bouw de querystring op
            var queryParameters = new List<string>();

            if (productId.HasValue)
            {
                queryParameters.Add($"productId={productId.Value}");
            }

            if (articleIds != null && articleIds.Any())
            {
                queryParameters.Add($"articleIds={string.Join(",", articleIds)}");
            }

            var queryString = queryParameters.Count > 0 ? $"?{string.Join("&", queryParameters)}" : string.Empty;

            // Stuur het verzoek
            var response = await httpClient.GetAsync($"api/Article{queryString}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IList<ArticleResult>>() ?? new List<ArticleResult>();
            }

            return new List<ArticleResult>();
        }


        public async Task<ArticleResult?> Create(ArticleRequest entity)
        {
            var httpClient = _httpClientFactory.CreateClient("VivesRentalApi");
            var response = await httpClient.PostAsJsonAsync("api/Article", entity);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ArticleResult>();
        }

        public async Task<bool> UpdateStatus(Guid id, ArticleStatus status)
        {
            var httpClient = _httpClientFactory.CreateClient("VivesRentalApi");
            var response = await httpClient.PutAsJsonAsync($"api/Article/{id}/status", (int)status);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Remove(Guid id)
        {
            var httpClient = _httpClientFactory.CreateClient("VivesRentalApi");
            var response = await httpClient.DeleteAsync($"api/Article/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}








































