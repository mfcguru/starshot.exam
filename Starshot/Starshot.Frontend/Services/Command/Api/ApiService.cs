using Microsoft.Extensions.Options;
using Starshot.Frontend.Services.Command;
using System.Net.Http.Headers;

namespace Starshot.Frontend.Services.Api
{
    public class ApiService : IDispatchService
    {
        private readonly string apiBaseUri;
        
        public ApiService(IOptions<AppSettings> appSettings)
        {
            apiBaseUri = appSettings.Value.ApiBaseUri;
        }

        public async Task<ServiceResult> AddUser(string token, string firstName, string lastName, DateTime timeIn, DateTime timeOut)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                try
                {
                    var endpoint = $"{apiBaseUri}/AddUser";
                    var payload = new { firstName, lastName, timeIn, timeOut };                    
                    var response = await client.PostAsJsonAsync(endpoint, payload);
                    response.EnsureSuccessStatusCode();

                    return new ServiceResult 
                    { 
                        StatusCode = response.StatusCode 
                    };
                }
                catch(HttpRequestException e)
                {
                    return new ServiceResult
                    {
                        StatusCode = e.StatusCode,
                        ErrorMessage = e.Message,
                        Success = false
                    };
                }
            }   
        }

        public async Task<ServiceResult> DeleteUser(string token, int userId)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                try
                {
                    var endpoint = $"{apiBaseUri}/DeleteUser/{userId}";
                    var response = await client.DeleteAsync(endpoint);
                    response.EnsureSuccessStatusCode();

                    return new ServiceResult 
                    { 
                        StatusCode = response.StatusCode 
                    };
                }
                catch (HttpRequestException e)
                {
                    return new ServiceResult
                    {
                        StatusCode = e.StatusCode,
                        ErrorMessage = e.Message,
                        Success = false
                    };
                }
            }
        }

        public async Task<ServiceResult> EditUser(string token, int userId, string firstName, string lastName, DateTime timeIn, DateTime timeOut, bool active)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                try
                {
                    var endpoint = $"{apiBaseUri}/EditUser/{userId}";
                    var payload = new { firstName, lastName, timeIn, timeOut, active };
                    var response = await client.PutAsJsonAsync(endpoint, payload);
                    response.EnsureSuccessStatusCode();

                    return new ServiceResult 
                    { 
                        StatusCode = response.StatusCode 
                    };
                }
                catch (HttpRequestException e)
                {
                    return new ServiceResult
                    {
                        StatusCode = e.StatusCode,
                        ErrorMessage = e.Message,
                        Success = false
                    };
                }
            }
        }

        public async Task<ServiceResult> GetUser(string token, int userId)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                try
                {
                    var endpoint = $"{apiBaseUri}/GetUser/{userId}";
                    var response = await client.GetAsync(endpoint);
                    response.EnsureSuccessStatusCode();

                    return new ServiceResult 
                    { 
                        StatusCode = response.StatusCode,
                        JsonData = await response.Content.ReadAsStringAsync()
                    };
                }
                catch (HttpRequestException e)
                {
                    return new ServiceResult
                    {
                        StatusCode = e.StatusCode,
                        ErrorMessage = e.Message,
                        Success = false
                    };
                }
            }
        }

        public async Task<ServiceResult> GetUsers(string token, string searchKey = "", int activeFilter = 0)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                try
                {
                    var endpoint = $"{apiBaseUri}/GetUsers?filterName={searchKey}&filterActive={activeFilter}";
                    var response = await client.GetAsync(endpoint);
                    response.EnsureSuccessStatusCode();

                    return new ServiceResult
                    {
                        StatusCode = response.StatusCode,
                        JsonData = await response.Content.ReadAsStringAsync()
                    };
                }
                catch (HttpRequestException e)
                {
                    return new ServiceResult
                    {
                        StatusCode = e.StatusCode,
                        ErrorMessage = e.Message,
                        Success = false
                    };
                }
            }
        }

        public async Task<ServiceResult> Login(string username, string password)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var endpoint = $"{apiBaseUri}/login";
                    var payload = new { username, password };
                    var response = await client.PostAsJsonAsync(endpoint, payload);
                    response.EnsureSuccessStatusCode();

                    return new ServiceResult
                    {
                        StatusCode = response.StatusCode,
                        JsonData = await response.Content.ReadAsStringAsync()
                    };
                }
                catch (HttpRequestException e)
                {
                    return new ServiceResult
                    {
                        StatusCode = e.StatusCode,
                        ErrorMessage = "Username or password is incorrect",
                        Success = false
                    };
                }
            }
        }
    }
}
