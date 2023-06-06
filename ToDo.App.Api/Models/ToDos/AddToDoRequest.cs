using System.ComponentModel.DataAnnotations;
using ToDo.App.Domain.Enums;

namespace ToDo.App.Api.Models.ToDos
{
    public sealed class AddToDoRequest
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public Tag Tag { get; set; }
        [Required]
        public IList<AddToDoItemRequest> ToDoItemRequests { get; set; }
    }
}
