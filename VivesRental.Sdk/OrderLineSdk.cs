using System.Net.Http;
using System.Net.Http.Json;
using VivesRental.Services.Model.Filters;
using VivesRental.Services.Model.Results;

namespace VivesRental.Sdk
{
    public class OrderLineSdk
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public OrderLineSdk(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<OrderLineResult>> Find(OrderLineFilter? filter = null)
        {
            var httpClient = _httpClientFactory.CreateClient("VivesRentalApi");
            var queryString = filter != null ? $"?orderId={filter.OrderId}" : string.Empty;
            var response = await httpClient.GetAsync($"api/OrderLine{queryString}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<OrderLineResult>>() ?? new List<OrderLineResult>();
        }

        public async Task<bool> Rent(Guid orderId, Guid articleId)
        {
            var httpClient = _httpClientFactory.CreateClient("VivesRentalApi");
            var queryParams = $"?orderId={orderId}&articleId={articleId}";
            var response = await httpClient.PostAsync($"api/OrderLine/Rent{queryParams}", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Return(Guid orderLineId, DateTime returnedAt)
        {
            var httpClient = _httpClientFactory.CreateClient("VivesRentalApi");
            var response = await httpClient.PostAsJsonAsync($"api/OrderLine/{orderLineId}/Return", returnedAt);
            return response.IsSuccessStatusCode;
        }
    }
}





























