using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using VivesRental.Services.Model.Requests;

namespace VivesRental.Sdk
{
    //public class IdentitySdk(IHttpClientFactory httpClientFactory)
    //{
    //    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

    //    public async Task<AuthenticationResult> SignIn(UserSignInRequest request)
    //    {
    //        var httpClient = _httpClientFactory.CreateClient("VivesRentalApi");

    //        var route = "Identity/sign-in";
    //        var response = await httpClient.PostAsJsonAsync(route, request);

    //        response.EnsureSuccessStatusCode();

    //        var result = await response.Content.ReadFromJsonAsync<AuthenticationResult>();
    //        if (result is null)
    //        {
    //            return new AuthenticationResult();
    //        }

    //        return result;
    //    }

    //    public async Task<AuthenticationResult> Register(UserRegisterRequest request)
    //    {
    //        var httpClient = _httpClientFactory.CreateClient("VivesRentalApi");

    //        var route = "Identity/register";
    //        var response = await httpClient.PostAsJsonAsync(route, request);

    //        response.EnsureSuccessStatusCode();

    //        var result = await response.Content.ReadFromJsonAsync<AuthenticationResult>();
    //        if (result is null)
    //        {
    //            return new AuthenticationResult();
    //        }

    //        return result;
    //    }
    //}
}
