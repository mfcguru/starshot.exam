
namespace Starshot.Frontend.Services.Command
{
    public interface ICommandService
    {
        Task<ServiceResult> AddUser(string firstName, string lastName, DateTime timeIn, DateTime timeOut);
        Task<ServiceResult> EditUser(int userId, string firstName, string lastName, DateTime timeIn, DateTime timeOut, bool active);
        Task<ServiceResult> DeleteUser(int userId);
    }
}
