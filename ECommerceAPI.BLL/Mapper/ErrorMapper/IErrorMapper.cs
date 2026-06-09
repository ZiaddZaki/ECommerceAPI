using ECommerceAPI.Common;
using FluentValidation.Results;

namespace ECommerceAPI.BLL
{
    public interface IErrorMapper
    {
        Dictionary<string, List<Errors>> MapError(ValidationResult validationResult);
    }
}