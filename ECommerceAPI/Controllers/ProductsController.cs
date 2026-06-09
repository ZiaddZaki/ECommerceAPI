using ECommerceAPI.BLL;
using ECommerceAPI.Common;
using ECommerceAPI.DAL;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "AdminOnly")]

    public class ProductsController : ControllerBase
    {
        private readonly IProductManager _productManager;
        private readonly IErrorMapper _errorMapper;
        private readonly IValidator<ProductCreateDTo> _Validator;
        private readonly IValidator<ProductEditDTo> _EditValidator;

        public ProductsController(IProductManager productManager, IValidator<ProductEditDTo> EditValidator, IValidator<ProductCreateDTo> validator, IErrorMapper errorMapper)
        {
            _productManager = productManager;
            _Validator = validator;
            _errorMapper = errorMapper;
            _EditValidator = EditValidator;

        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<GeneralResult<IEnumerable<ProductReadDTo>>>> GetAllProducts()
        {
            var products = await _productManager.GetProductsAsync();
            return Ok(GeneralResult<IEnumerable<ProductReadDTo>>.SuccessResult(products));
        }
        [HttpGet]
        [Route("Pagination")]
        [AllowAnonymous]
        public async Task<ActionResult<GeneralResult<IEnumerable<Product>>>>
            GetAllPaginationAsync(
            [FromQuery] PaginationParameters paginationParameters,
            [FromQuery] PrdouctFilterParameters prdouctFilterParameters)
        {
            var result = await _productManager.GetProductsPagenationAsync(paginationParameters, prdouctFilterParameters);
            return Ok(GeneralResult<PagedResult<Product>>.SuccessResult(result));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<GeneralResult<ProductReadDTo>>> GetProductById([FromRoute] int id)
        {
            var product = await _productManager.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound(GeneralResult<ProductReadDTo>.NotFound());
            }
            return Ok(GeneralResult<ProductReadDTo>.SuccessResult(product));
        }
        [HttpPost]
        public async Task<ActionResult<GeneralResult<ProductReadDTo>>> CreateProduct([FromBody] ProductCreateDTo newProduct)
        {
            var validationResult = await _Validator.ValidateAsync(newProduct);
            if (!validationResult.IsValid)
            {
                return BadRequest(GeneralResult<ProductReadDTo>.FailResult(_errorMapper.MapError(validationResult)));
            }

            var product = await _productManager.CreateProduct(newProduct);
            if (product == null)
            {
                return BadRequest(GeneralResult<ProductReadDTo>.FailResult());
            }
            return Ok(GeneralResult<ProductReadDTo>.SuccessResult(product));
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResult<ProductEditDTo>>> EditProduct([FromRoute] int id, [FromBody] ProductEditDTo newEditedProduct)
        {
            var validationResult = await _EditValidator.ValidateAsync(newEditedProduct);
            if (!validationResult.IsValid)
            {
                return BadRequest(GeneralResult<ProductReadDTo>.FailResult(_errorMapper.MapError(validationResult)));
            }
            var EditedProduct = await _productManager.EditProduct(id, newEditedProduct);
            if (EditedProduct == null)
            {
                return BadRequest(GeneralResult<ProductEditDTo>.FailResult());
            }
            return Ok(GeneralResult<ProductEditDTo>.SuccessResult(EditedProduct));

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResult>> DeleteProduct([FromRoute] int id)
        {
            var result = await _productManager.DeleteProduct(id);
            if (result == false) return NotFound(GeneralResult.NotFound("can't find product with this id!"));
            return Ok(GeneralResult.SuccessResult($"Product with id {id} deleted successfully"));
        }

        [HttpPost("{id}/image")]
        public async Task<ActionResult<GeneralResult>> SetProductImage([FromRoute] int id, [FromForm] ImageUploadResultDTo imageUploadResultDTo)
        {
           var result = await _productManager.SetProductImage(id, imageUploadResultDTo);
            if (result == false) return NotFound(GeneralResult.NotFound());
            return Ok(GeneralResult.SuccessResult());
        }
    }
}
