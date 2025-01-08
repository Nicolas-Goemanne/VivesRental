using Blazored.LocalStorage;
using VivesRental.Authentication;

namespace VivesRental.Blazor.Stores
{
    public class TokenStore(ISyncLocalStorageService localStorageService) : IBearerTokenStore
    {
        private readonly ISyncLocalStorageService _localStorageService = localStorageService;

        private const string TokenName = "BearerToken";

        public string GetToken()
        {
            var token = _localStorageService.GetItem<string>(TokenName) ?? string.Empty;
            return token;
        }

        public void SetToken(string token)
        {
            _localStorageService.SetItem(TokenName, token);
        }
    }
}
