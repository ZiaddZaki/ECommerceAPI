using FluentValidation;

namespace ECommerceAPI.BLL
{
    public class CategoryCreateValidator : AbstractValidator<CategoryCreateDTo>
    {
        public CategoryCreateValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                .MaximumLength(30)
                .MinimumLength(3)
                .WithErrorCode("ERR-01");
        }
    }
}
