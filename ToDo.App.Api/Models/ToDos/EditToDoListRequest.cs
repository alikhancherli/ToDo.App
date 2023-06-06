using System.ComponentModel.DataAnnotations;
using ToDo.App.Domain.Enums;

namespace ToDo.App.Api.Models.ToDos
{
    public class EditToDoListRequest
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public Tag Tag { get; set; }
        [Required]
        public IList<EditToDoItemRequest> ToDoItemRequests { get; set; }
    }
}
