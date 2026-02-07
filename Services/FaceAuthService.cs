namespace FaceLockAuth.API.Services
{
    public class FaceAuthService : IFaceAuthService
    {
        public async Task<bool> VerifyFaceAsync(string storedImagePath, IFormFile uploadedImage)
        {
            // v1 logic:
            // 1. Check stored image exists
            // 2. Check uploaded image exists
            // 3. Accept as valid (placeholder)

            if (!File.Exists(storedImagePath))
                return false;

            if (uploadedImage == null || uploadedImage.Length == 0)
                return false;

            // v1 assumption: email + face upload is enough
            return true;
        }
    }
}
