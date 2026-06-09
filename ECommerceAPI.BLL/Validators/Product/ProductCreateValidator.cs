using FluentValidation;

namespace ECommerceAPI.BLL
{
    public class ProductCreateValidator : AbstractValidator<ProductCreateDTo>
    {
        public ProductCreateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(30)
                .WithErrorCode("ERR-01");
                
            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithErrorCode("ERR-02");
                
            RuleFor(x => x.Stock)
                .GreaterThan(0)
                .WithErrorCode("ERR-03");

            RuleFor(x => x.Description)
                .NotEmpty()
                .MinimumLength(15)
                .WithErrorCode("ERR-04");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0)
                .WithErrorCode("ERR-05");
                
        }
    }
}
