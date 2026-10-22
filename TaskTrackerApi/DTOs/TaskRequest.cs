using TaskTrackerApi.Models;
using System.ComponentModel.DataAnnotations;

namespace TaskTrackerApi.DTOs
{
    public class TaskRequestDto
    {
        [Required]
        [RegularExpression(@".*\S.*", ErrorMessage = "Title cannot be blank or whitespace.")]
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public TaskItemStatus Status { get; set; } = TaskItemStatus.Todo;
        public DateTime? DueDate { get; set; }
    }
}