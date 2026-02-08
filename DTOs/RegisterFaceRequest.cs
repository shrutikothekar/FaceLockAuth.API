using System.ComponentModel.DataAnnotations;

namespace FaceLockAuth.API.DTOs
{
    public class RegisterFaceRequest
    {
        [Required]
        public string FullName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        // Face image uploaded from frontend
        [Required]
        public string Base64Image { get; set; }

    }
}
