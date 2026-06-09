using ECommerceAPI.DAL;

namespace ECommerceAPI.BLL
{
    public class CategoryManager : ICategoryManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CategoryReadDTo>> GetCatrgoriesAsync()
        {

            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
            return categories.Select(c => new CategoryReadDTo
            {
                Id = c.Id,
                Name = c.Name,
            });

        }
        public async Task<CategoryReadDTo?> GetCategoryByIdAsync(int id)
        {

            var catrgory = await _unitOfWork.CategoryRepository.GetByIdAsync(id);

            if (catrgory == null)
            {
                return null;
            }

            var categoryDto = new CategoryReadDTo
            {
                Id = catrgory.Id,
                Name = catrgory.Name,
            };

            return categoryDto;

        }
        public async Task<CategoryReadDTo?> CreateCategory(CategoryCreateDTo NewCategory)
        {

            if (NewCategory == null)
            {
                return null;
            }
            var category = new Category
            {
                Name = NewCategory.Name
            };
            _unitOfWork.CategoryRepository.Add(category);
            await _unitOfWork.SaveChangesAsync();
            var categoryDto = new CategoryReadDTo
            {
                Id = category.Id,
                Name = category.Name,
            };
            return categoryDto;
        }
        public async Task<CategoryEditDTo?> EditCategory(int id, CategoryEditDTo EditedCategory)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);

            if (EditedCategory == null || category == null)
            {
                return null;
            }

            category.Name = EditedCategory.Name;


            await _unitOfWork.SaveChangesAsync();
            return EditedCategory;
        }
        public async Task<bool> DeleteCategory(int id)
        {
            var Category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (Category == null)
            {
                return false;
            }
            _unitOfWork.CategoryRepository.Delete(Category);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        public async Task<bool> SetCategoryImage(int CategoryId, ImageUploadResultDTo imageUploadResultDTo)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(CategoryId);
            if (category == null)
            {
                return false;
            }
            category.ImageUrl = imageUploadResultDTo.ImageUrl;
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

    }
}
