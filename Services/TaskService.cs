using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Data;
using TaskManagementAPI.Exceptions;
using TaskManagementAPI.Models.DTOs;
using TaskManagementAPI.Models.Entities;
using TaskManagementAPI.Models.Enums;
using TaskStatus = TaskManagementAPI.Models.Enums.TaskStatus;

namespace TaskManagementAPI.Services
{
    public class TaskService : ITaskService
    {
        private readonly ApplicationDbContext _context;

        public TaskService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TaskResponseDto> CreateTaskAsync(CreateTaskDto createTaskDto)
        {
            // Validate assigned user if provided
            if (createTaskDto.AssignedToId.HasValue)
            {
                var userExists = await _context.Users.AnyAsync(u => u.Id == createTaskDto.AssignedToId.Value);
                if (!userExists)
                {
                    throw new NotFoundException($"User with ID {createTaskDto.AssignedToId.Value} not found.");
                }
            }

            var task = new TaskItem
            {
                Title = createTaskDto.Title,
                Description = createTaskDto.Description,
                Status = createTaskDto.Status,
                Priority = createTaskDto.Priority,
                DueDate = createTaskDto.DueDate,
                AssignedToId = createTaskDto.AssignedToId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return await GetTaskByIdAsync(task.Id);
        }

        public async Task<(List<TaskResponseDto> Tasks, int TotalCount)> GetTasksAsync(
            int page,
            int pageSize,
            TaskStatus? status,
            TaskPriority? priority,
            int? assignedToId)
        {
            var query = _context.Tasks.AsQueryable();

            // Apply filters
            if (status.HasValue)
            {
                query = query.Where(t => t.Status == status.Value);
            }

            if (priority.HasValue)
            {
                query = query.Where(t => t.Priority == priority.Value);
            }

            if (assignedToId.HasValue)
            {
                query = query.Where(t => t.AssignedToId == assignedToId.Value);
            }

            var totalCount = await query.CountAsync();

            var tasks = await query
                .Include(t => t.AssignedTo)
                .OrderByDescending(t => t.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(t => new TaskResponseDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Status = t.Status,
                    Priority = t.Priority,
                    DueDate = t.DueDate,
                    CreatedAt = t.CreatedAt,
                    UpdatedAt = t.UpdatedAt,
                    AssignedTo = t.AssignedTo != null ? new UserResponseDto
                    {
                        Id = t.AssignedTo.Id,
                        Name = t.AssignedTo.Name,
                        Email = t.AssignedTo.Email
                    } : null
                })
                .ToListAsync();

            return (tasks, totalCount);
        }

        public async Task<TaskResponseDto> GetTaskByIdAsync(int id)
        {
            var task = await _context.Tasks
                .Include(t => t.AssignedTo)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (task == null)
            {
                throw new NotFoundException($"Task with ID {id} not found.");
            }

            return new TaskResponseDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                Priority = task.Priority,
                DueDate = task.DueDate,
                CreatedAt = task.CreatedAt,
                UpdatedAt = task.UpdatedAt,
                AssignedTo = task.AssignedTo != null ? new UserResponseDto
                {
                    Id = task.AssignedTo.Id,
                    Name = task.AssignedTo.Name,
                    Email = task.AssignedTo.Email
                } : null
            };
        }

        public async Task<TaskResponseDto> UpdateTaskAsync(int id, UpdateTaskDto updateTaskDto)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                throw new NotFoundException($"Task with ID {id} not found.");
            }

            // Validate assigned user if being updated
            if (updateTaskDto.AssignedToId.HasValue)
            {
                var userExists = await _context.Users.AnyAsync(u => u.Id == updateTaskDto.AssignedToId.Value);
                if (!userExists)
                {
                    throw new NotFoundException($"User with ID {updateTaskDto.AssignedToId.Value} not found.");
                }
            }

            // Update only provided fields
            if (updateTaskDto.Title != null)
                task.Title = updateTaskDto.Title;

            if (updateTaskDto.Description != null)
                task.Description = updateTaskDto.Description;

            if (updateTaskDto.Status.HasValue)
                task.Status = updateTaskDto.Status.Value;

            if (updateTaskDto.Priority.HasValue)
                task.Priority = updateTaskDto.Priority.Value;

            if (updateTaskDto.DueDate.HasValue)
                task.DueDate = updateTaskDto.DueDate;

            if (updateTaskDto.AssignedToId.HasValue)
                task.AssignedToId = updateTaskDto.AssignedToId;

            task.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return await GetTaskByIdAsync(id);
        }

        public async Task<TaskResponseDto> UpdateTaskStatusAsync(int id, UpdateTaskStatusDto updateTaskStatusDto)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                throw new NotFoundException($"Task with ID {id} not found.");
            }

            task.Status = updateTaskStatusDto.Status;
            task.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return await GetTaskByIdAsync(id);
        }

        public async Task DeleteTaskAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                throw new NotFoundException($"Task with ID {id} not found.");
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}
