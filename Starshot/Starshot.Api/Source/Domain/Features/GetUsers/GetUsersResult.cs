
namespace Starshot.Api.Source.Domain.Features.GetUsers
{
    public class GetUsersResult
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime? TimeOut { get; set; }
        public bool Active { get; set; }
    }
}
