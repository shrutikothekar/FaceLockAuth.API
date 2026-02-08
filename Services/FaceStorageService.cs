namespace FaceLockAuth.API.Services
{
    public class FaceStorageService : IFaceStorageService
    {
        private readonly string _faceImageFolder = "FaceImages";

        public async Task<string> SaveBase64ImageAsync(string base64Image)
        {
            if (!Directory.Exists(_faceImageFolder))
                Directory.CreateDirectory(_faceImageFolder);

            var imageBytes = Convert.FromBase64String(
                base64Image.Split(',')[1]);

            var fileName = $"{Guid.NewGuid()}.jpg";
            var filePath = Path.Combine(_faceImageFolder, fileName);

            await File.WriteAllBytesAsync(filePath, imageBytes);
            return filePath; // store ONLY path in DB
        }
    }
}
