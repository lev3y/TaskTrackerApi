using System.ComponentModel.DataAnnotations;

namespace TaskTrackerApi.DTOs
{
    public class ProjectRequestDto
    {
        [Required]
        [RegularExpression(@".*\S.*", ErrorMessage = "Name cannot be blank or whitespace.")]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}