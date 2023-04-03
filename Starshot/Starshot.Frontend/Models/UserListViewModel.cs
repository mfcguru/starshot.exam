namespace Starshot.Frontend.Models
{
    public class UserListViewModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }
        public bool Active { get; set; }
    }
}
