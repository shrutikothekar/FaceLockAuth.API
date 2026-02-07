namespace FaceLockAuth.API.Services
{
    public interface IFaceAuthService
    {
        Task<bool> VerifyFaceAsync(string storedImagePath, IFormFile uploadedImage);
    }
}
