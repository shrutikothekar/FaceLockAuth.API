namespace FaceLockAuth.API.Services
{
    public class FaceAuthService : IFaceAuthService
    {
        //public async Task<bool> VerifyFaceAsync(string storedImagePath, string uploadedImage)
        //{
        //    if (!File.Exists(storedImagePath))
        //        return false;

        //    if (uploadedImage == null || uploadedImage.Length == 0)
        //        return false;

        //    return true;
        //}
        public Task<bool> VerifyFaceAsync(float[] storedDescriptor, float[] incomingDescriptor)
        {
            if (storedDescriptor == null || incomingDescriptor == null)
                return Task.FromResult(false);

            if (storedDescriptor.Length != incomingDescriptor.Length)
                return Task.FromResult(false);

            double distance = 0;

            for (int i = 0; i < storedDescriptor.Length; i++)
            {
                distance += Math.Pow(storedDescriptor[i] - incomingDescriptor[i], 2);
            }

            distance = Math.Sqrt(distance);

            return Task.FromResult(distance < 0.6);
        }


    }
}
