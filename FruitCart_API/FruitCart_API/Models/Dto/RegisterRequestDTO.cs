using System.ComponentModel.DataAnnotations;

namespace FruitCart_API.Models.Dto
{
    public class RegisterRequestDTO
    {
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string Name { get; set; } = string.Empty;
        //public string Role { get; set; } = string.Empty; not needed as dont want the user to choose their role, we will assign it in the controller as "User" by default.
    }
}
