using System.ComponentModel.DataAnnotations;

namespace FaceLockAuth.API.DTOs
{
    public class FaceLoginRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Base64Image { get; set; }

    }
}
