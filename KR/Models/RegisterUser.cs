using System.ComponentModel.DataAnnotations;

namespace KR.Models
{
    public class RegisterUser
    {
        [Required]
        public string RegUserName { get; set; }
        [Required]
        public string RegUserFirstName { get; set; }
        [Required]
        [EmailAddress]
        public string RegUserEmail { get; set; }

        [Required]
        public string RegUserPhone { get; set; }
    }
}
