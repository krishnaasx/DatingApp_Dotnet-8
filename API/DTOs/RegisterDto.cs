using System.ComponentModel.DataAnnotations;

namespace API.DTOs {
    public class RegisterDto {
        public string Username { get; set; } = string.Empty;

        [StringLength(8, MinimumLength = 4)]
        public string Password { get; set; } = string.Empty;
    }
}