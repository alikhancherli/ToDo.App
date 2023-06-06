using System.ComponentModel.DataAnnotations;

namespace ToDo.App.Api.Models
{
    public class UpdateUserRequest
    {
        //int UserId, string Email, string FirstName, string LastName

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public string Email { get; set; }
    }
}
