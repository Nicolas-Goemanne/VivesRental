using System.Net.Http;
using System.Net.Http.Json;
using VivesRental.Services.Model.Filters;
using VivesRental.Services.Model.Requests;
using VivesRental.Services.Model.Results;

namespace VivesRental.Sdk
{
    public class ArticleReservationSdk
    {
        private readonly HttpClient _httpClient;

        public ArticleReservationSdk(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ArticleReservationResult?> Get(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/articlereservations/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ArticleReservationResult>();
        }

        public async Task<List<ArticleReservationResult>> Find(ArticleReservationFilter? filter)
        {
            var response = await _httpClient.PostAsJsonAsync("api/articlereservations/find", filter);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<ArticleReservationResult>>();
        }

        public async Task<ArticleReservationResult?> Create(ArticleReservationRequest entity)
        {
            var response = await _httpClient.PostAsJsonAsync("api/articlereservations", entity);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ArticleReservationResult>();
        }

        public async Task<bool> Remove(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/articlereservations/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}

















