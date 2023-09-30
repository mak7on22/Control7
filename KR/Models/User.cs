using System.ComponentModel.DataAnnotations;

namespace KR.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserFirstName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public List<Book>? Books { get; set; }
    }
}
