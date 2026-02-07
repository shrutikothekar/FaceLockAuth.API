namespace FaceLockAuth.API.Services
{
    public interface IFaceStorageService
    {
        Task<string> SaveFaceImageAsync(IFormFile faceImage);

    }
}
