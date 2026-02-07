using Microsoft.AspNetCore.Http;

namespace FaceLockAuth.API.Services
{
    public class FaceStorageService : IFaceStorageService
    {
        private readonly string _faceImageFolder = "FaceImages";

        public async Task<string> SaveFaceImageAsync(IFormFile faceImage)
        {
            if (!Directory.Exists(_faceImageFolder))
                Directory.CreateDirectory(_faceImageFolder);

            var fileName = $"{Guid.NewGuid()}_{faceImage.FileName}";
            var filePath = Path.Combine(_faceImageFolder, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await faceImage.CopyToAsync(stream);

            return filePath; // store ONLY path in DB
        }
    }
}
