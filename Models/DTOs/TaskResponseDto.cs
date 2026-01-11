using TaskManagementAPI.Models.Enums;
using TaskStatus = TaskManagementAPI.Models.Enums.TaskStatus;

namespace TaskManagementAPI.Models.DTOs
{
    public class TaskResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public TaskStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public UserResponseDto? AssignedTo { get; set; }
    }
}
