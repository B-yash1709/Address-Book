using System.ComponentModel.DataAnnotations;

namespace ModelLayer.Model
{
    public class ForgotPasswordDTO
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
    }
}
