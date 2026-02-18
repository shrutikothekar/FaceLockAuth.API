namespace FaceLockAuth.API.Services
{
    public interface IFaceAuthService
    {
        //Task<bool> VerifyFaceAsync(string storedImagePath, string uploadedImage);
        Task<bool> VerifyFaceAsync(float[] storedDescriptor, float[] incomingDescriptor);

    }
}
