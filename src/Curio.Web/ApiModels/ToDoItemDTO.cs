using System;
using System.ComponentModel.DataAnnotations;
using Curio.Domain.Entities;

namespace Curio.Web.ApiModels
{
    // Note: doesn't expose events or behavior
    public class ToDoItemDTO
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; private set; }

        public static ToDoItemDTO FromToDoItem(ToDoItem item)
        {
            return new ToDoItemDTO()
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                IsDone = item.IsDone
            };
        }
    }
}
