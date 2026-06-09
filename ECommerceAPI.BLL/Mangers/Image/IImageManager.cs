
namespace ECommerceAPI.BLL
{
    public interface IImageManager
    {
        Task<ImageUploadResultDTo> UploadAsync(ImageUploadDTo imageUploadDTo, string basePase, string? schema, string? host);
    }
}