using System.ComponentModel.DataAnnotations;

namespace ToDo.App.Api.Models.ToDos
{
    public class EditToDoItemRequest
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Note { get; set; }
        public DateTimeOffset? Reminder { get; set; }
        [Required]
        public byte PriorityLevel { get; set; }
    }
}
