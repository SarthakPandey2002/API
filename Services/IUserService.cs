using TaskManagementAPI.Models.DTOs;

namespace TaskManagementAPI.Services
{
    public interface IUserService
    {
        Task<UserResponseDto> CreateUserAsync(CreateUserDto createUserDto);
        Task<(List<UserResponseDto> Users, int TotalCount)> GetUsersAsync(int page, int pageSize);
        Task<UserResponseDto> GetUserByIdAsync(int id);
    }
}
