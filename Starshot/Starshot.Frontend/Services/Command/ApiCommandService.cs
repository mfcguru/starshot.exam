using Starshot.Frontend.Services.Api;

namespace Starshot.Frontend.Services.Command
{
    public class ApiCommandService : ICommandService
    {
        private readonly string token;
        private readonly IApiService apiService;

        public ApiCommandService(string token, IApiService apiService)
        {
            this.token = token;
            this.apiService = apiService;
        }

        public async Task<ServiceResult> AddUser(string firstName, string lastName, DateTime timeIn, DateTime timeOut)
        {
            return await apiService.AddUser(token, firstName, lastName, timeIn, timeOut);   
        }

        public async Task<ServiceResult> DeleteUser(int userId)
        {
            return await apiService.DeleteUser(token, userId);
        }

        public async Task<ServiceResult> EditUser(int userId, string firstName, string lastName, DateTime timeIn, DateTime timeOut, bool active)
        {
            return await apiService.EditUser(token, userId, firstName, lastName, timeIn, timeOut, active);
        }
    }
}
