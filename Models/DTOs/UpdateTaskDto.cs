using System.ComponentModel.DataAnnotations;
using TaskManagementAPI.Models.Enums;
using TaskStatus = TaskManagementAPI.Models.Enums.TaskStatus;

namespace TaskManagementAPI.Models.DTOs
{
    public class UpdateTaskDto
    {
        [MinLength(3)]
        [MaxLength(100)]
        public string? Title { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public TaskStatus? Status { get; set; }

        public TaskPriority? Priority { get; set; }

        public DateTime? DueDate { get; set; }

        public int? AssignedToId { get; set; }
    }
}
