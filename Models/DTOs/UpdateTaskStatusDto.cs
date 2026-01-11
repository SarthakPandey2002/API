using System.ComponentModel.DataAnnotations;
using TaskManagementAPI.Models.Enums;
using TaskStatus = TaskManagementAPI.Models.Enums.TaskStatus;

namespace TaskManagementAPI.Models.DTOs
{
    public class UpdateTaskStatusDto
    {
        [Required]
        public TaskStatus Status { get; set; }
    }
}
