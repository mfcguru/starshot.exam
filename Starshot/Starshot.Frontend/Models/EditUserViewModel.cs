namespace Starshot.Frontend.Models
{
    public class EditUserViewModel : AddUserViewModel
    {
        public int UserId { get; set; }
        public bool Active { get; set; }
    }
}
