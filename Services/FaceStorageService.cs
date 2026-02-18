namespace FaceLockAuth.API.Services
{
    public class FaceStorageService : IFaceStorageService
    {
        private readonly string _faceImageFolder = "FaceImages";

        public async Task<string> SaveBase64ImageAsync(string base64Image)
        {
            if (string.IsNullOrWhiteSpace(base64Image))
                throw new ArgumentException("Image data is empty");

            if (!Directory.Exists(_faceImageFolder))
                Directory.CreateDirectory(_faceImageFolder);

            string cleanBase64 = base64Image;

            // If string contains metadata like "data:image/jpeg;base64,"
            if (base64Image.Contains(","))
            {
                cleanBase64 = base64Image.Split(',')[1];
            }

            var imageBytes = Convert.FromBase64String(cleanBase64);

            var fileName = $"{Guid.NewGuid()}.jpg";
            var filePath = Path.Combine(_faceImageFolder, fileName);

            await File.WriteAllBytesAsync(filePath, imageBytes);

            return filePath;
        }

    }
}
