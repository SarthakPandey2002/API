using System.ComponentModel.DataAnnotations;

namespace TaskManagementAPI.Models.DTOs
{
    public class CreateUserDto
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
