using System.ComponentModel.DataAnnotations;

namespace KR.Models
{
    public class LoginUser
    {
        [Required]
        [EmailAddress]
        public string LogUserEmail { get; set; }
    }
}
