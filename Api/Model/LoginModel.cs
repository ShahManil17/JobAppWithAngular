using System.ComponentModel.DataAnnotations;

namespace JobApplicationAPIs.Model
{
    public class LoginModel
    {
        [Required(ErrorMessage = "This Field Must Be Filled Out")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "This Field Must Be Filled Out")]
        public string? Password { get; set; }
    }
}
