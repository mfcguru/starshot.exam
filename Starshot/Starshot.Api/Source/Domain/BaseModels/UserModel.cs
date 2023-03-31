using System.ComponentModel.DataAnnotations;

namespace Starshot.Api.Source.Domain.BaseModels
{
    public class UserModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime TimeIn { get; set; }

        public DateTime? TimeOut { get; set; }
    }
}
