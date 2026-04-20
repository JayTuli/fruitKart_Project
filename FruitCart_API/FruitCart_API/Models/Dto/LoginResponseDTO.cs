using System.ComponentModel.DataAnnotations;

namespace FruitCart_API.Models.Dto
{
    public class LoginResponseDTO
    {
        [Required]
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string Role { get; set; }= string.Empty;
    }
}
