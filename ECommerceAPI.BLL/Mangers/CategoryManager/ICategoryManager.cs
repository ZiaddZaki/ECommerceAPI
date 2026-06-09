
namespace ECommerceAPI.BLL
{
    public interface ICategoryManager
    {
        Task<CategoryReadDTo?> CreateCategory(CategoryCreateDTo NewCategory);
        Task<bool> DeleteCategory(int id);
        Task<CategoryEditDTo?> EditCategory(int id, CategoryEditDTo EditedCategory);
        Task<CategoryReadDTo?> GetCategoryByIdAsync(int id);
        Task<IEnumerable<CategoryReadDTo>> GetCatrgoriesAsync();
        Task<bool> SetCategoryImage(int CategoryId, ImageUploadResultDTo imageUploadResultDTo);

    }
}