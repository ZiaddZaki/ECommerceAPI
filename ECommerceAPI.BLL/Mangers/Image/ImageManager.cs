using System.Threading.Tasks;

namespace ECommerceAPI.BLL
{
    public class ImageManager : IImageManager
    {
        public async Task<ImageUploadResultDTo> UploadAsync(ImageUploadDTo imageUploadDTo, string basePase, string? schema, string? host)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
           

            if (string.IsNullOrWhiteSpace(schema) || string.IsNullOrWhiteSpace(host))
            {
                return null;
            }

            var file = imageUploadDTo.File;
            var extention = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(extention))
                return null;

            var cleanName = Path.GetFileNameWithoutExtension(file.FileName).Replace(" ", "-").ToLower();
            var newFileName = $"{cleanName}-{Guid.NewGuid()}{extention}";

            var directoryPath = Path.Combine(basePase, "Files");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            var fullFilePath = Path.Combine(directoryPath, newFileName);

            using (var stream = new FileStream(fullFilePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            var url = $"{schema}://{host}/Files/{newFileName}";
            var imageUploadResultDTo = new ImageUploadResultDTo(url);
            return imageUploadResultDTo;
        }
    }
}
