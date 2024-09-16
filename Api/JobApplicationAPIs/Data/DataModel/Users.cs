using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace JobApplicationAPIs.Data.DataModel
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        [DefaultValue("User")]
        public string Role { get; set; }
        public string? RefreshToken { get; set; }
        public string? AccessToken { get; set; }
    }
}
