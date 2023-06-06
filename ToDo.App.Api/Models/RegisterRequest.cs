using System.ComponentModel.DataAnnotations;

namespace ToDo.App.Api.Models
{
    public class RegisterRequest
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; }
        [Required]
        public string Username { get; set; }
    }
}
