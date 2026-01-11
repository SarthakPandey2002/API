using TaskManagementAPI.Models.DTOs;
using TaskManagementAPI.Models.Enums;
using TaskStatus = TaskManagementAPI.Models.Enums.TaskStatus;

namespace TaskManagementAPI.Services
{
    public interface ITaskService
    {
        Task<TaskResponseDto> CreateTaskAsync(CreateTaskDto createTaskDto);
        Task<(List<TaskResponseDto> Tasks, int TotalCount)> GetTasksAsync(
            int page,
            int pageSize,
            TaskStatus? status,
            TaskPriority? priority,
            int? assignedToId);
        Task<TaskResponseDto> GetTaskByIdAsync(int id);
        Task<TaskResponseDto> UpdateTaskAsync(int id, UpdateTaskDto updateTaskDto);
        Task<TaskResponseDto> UpdateTaskStatusAsync(int id, UpdateTaskStatusDto updateTaskStatusDto);
        Task DeleteTaskAsync(int id);
    }
}
