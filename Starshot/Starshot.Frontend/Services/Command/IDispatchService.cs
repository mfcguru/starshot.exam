using Starshot.Frontend.Services.Api;

namespace Starshot.Frontend.Services.Command
{
    public interface IDispatchService
    {
        Task<ServiceResult> AddUser(string token, string firstName, string lastName, DateTime timeIn, DateTime timeOut);
        Task<ServiceResult> EditUser(string token, int userId, string firstName, string lastName, DateTime timeIn, DateTime timeOut, bool active);
        Task<ServiceResult> DeleteUser(string token, int userId);
        Task<ServiceResult> GetUser(string token, int userId);
        Task<ServiceResult> GetUsers(string token, string searchKey = "", int activeFilter = 0);
        Task<ServiceResult> Login(string username, string password);
    }
}
