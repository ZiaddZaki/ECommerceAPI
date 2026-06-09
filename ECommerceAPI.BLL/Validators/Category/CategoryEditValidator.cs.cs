using FluentValidation;

namespace ECommerceAPI.BLL
{
    public class CategoryEditValidator : AbstractValidator<CategoryEditDTo>
    {
        public CategoryEditValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(30)
                .MinimumLength(3)
                .WithErrorCode("ERR-01");

        }
    }
}
