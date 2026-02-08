namespace FaceLockAuth.API.Services
{
    public interface IFaceStorageService
    {
        //Task<string> SaveFaceImageAsync(IFormFile faceImage);
        Task<string> SaveBase64ImageAsync(string base64Image);


    }
}
