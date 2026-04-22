using ECommerceAPI.BLL;
using ECommerceAPI.Common;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "AdminOnly")]

    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryManager _categoryManager;
        private readonly IValidator<CategoryCreateDTo> _createValidator;
        private readonly IValidator<CategoryEditDTo> _editValidator;
        private readonly IErrorMapper _errorMapper;

        public CategoriesController(ICategoryManager categoryManager, IValidator<CategoryCreateDTo> createValidator, IValidator<CategoryEditDTo> editValidator, IErrorMapper errorMapper)
        {
            _categoryManager = categoryManager;
            _createValidator = createValidator;
            _editValidator = editValidator;
            _errorMapper = errorMapper;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResult<IEnumerable<CategoryReadDTo>>>> GetAllCatrgories()
        {
            var categories = await _categoryManager.GetCatrgoriesAsync();
            return Ok(GeneralResult<IEnumerable<CategoryReadDTo>>.SuccessResult(categories));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResult<CategoryReadDTo>>> GetCategoryById([FromRoute] int id)
        {
            var category = await _categoryManager.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound(GeneralResult<CategoryReadDTo>.NotFound());
            }
            return Ok(GeneralResult<CategoryReadDTo>.SuccessResult(category));
        }
        [HttpPost]
        public async Task<ActionResult<GeneralResult<CategoryReadDTo>>> CreateCategory([FromBody] CategoryCreateDTo newCategory)
        {
            var validationResult = await _createValidator.ValidateAsync(newCategory);
            if (!validationResult.IsValid)
            {
                return BadRequest(GeneralResult<ProductReadDTo>.FailResult(_errorMapper.MapError(validationResult)));
            }

            var catrgory = await _categoryManager.CreateCategory(newCategory);
            if (catrgory == null)
            {
                return BadRequest(GeneralResult<CategoryReadDTo>.FailResult());
            }
            return Ok(GeneralResult<CategoryReadDTo>.SuccessResult(catrgory));
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResult<CategoryEditDTo>>> EditCategory([FromRoute] int id, [FromBody] CategoryEditDTo newEditedCategory)
        {
            var validationResult = await _editValidator.ValidateAsync(newEditedCategory);
            if (!validationResult.IsValid)
            {
                return BadRequest(GeneralResult<ProductReadDTo>.FailResult(_errorMapper.MapError(validationResult)));
            }

            var EditedCategory = await _categoryManager.EditCategory(id, newEditedCategory);
            if (EditedCategory == null)
            {
                return BadRequest(GeneralResult<CategoryEditDTo>.FailResult());
            }
            return Ok(GeneralResult<CategoryEditDTo>.SuccessResult(EditedCategory));

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResult>> DeleteCategory([FromRoute] int id)
        {
            var result = await _categoryManager.DeleteCategory(id);
            if (result == false) return NotFound(GeneralResult.NotFound("can't find product with this id!"));
            return Ok(GeneralResult.SuccessResult($"Category with id {id} deleted successfully"));
        }

        [HttpPost("{id}/image")]
        public async Task<ActionResult<GeneralResult>> SetCategoryImage([FromRoute] int id, [FromForm] ImageUploadResultDTo imageUploadResultDTo)
        {
            var result = await _categoryManager.SetCategoryImage(id, imageUploadResultDTo);
            if (result == false) return NotFound(GeneralResult.NotFound());
            return Ok(GeneralResult.SuccessResult());
        }
    }
}
