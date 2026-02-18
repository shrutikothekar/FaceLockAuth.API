using System.ComponentModel.DataAnnotations;

namespace FaceLockAuth.API.DTOs
{
    public class FaceLoginRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        // 128-d face descriptor from face-api.js
        [Required]
        public float[] FaceDescriptor { get; set; }
    }
}
