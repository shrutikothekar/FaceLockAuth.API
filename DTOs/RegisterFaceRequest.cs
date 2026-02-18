using System.ComponentModel.DataAnnotations;

namespace FaceLockAuth.API.DTOs
{
    public class RegisterFaceRequest
    {
        [Required]
        public string FullName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        // 128-d face descriptor from face-api.js
        [Required]
        public float[] FaceDescriptor { get; set; }
    }
}
