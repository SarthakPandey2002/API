using System.ComponentModel.DataAnnotations;
using TaskManagementAPI.Models.Enums;
using TaskStatus = TaskManagementAPI.Models.Enums.TaskStatus;

namespace TaskManagementAPI.Models.DTOs
{
    public class CreateTaskDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public TaskStatus Status { get; set; } = TaskStatus.TODO;

        [Required]
        public TaskPriority Priority { get; set; } = TaskPriority.MEDIUM;

        public DateTime? DueDate { get; set; }

        public int? AssignedToId { get; set; }
    }
}
