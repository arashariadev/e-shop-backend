using Domain;

namespace EShop.Domain
{
    public interface IValidator<in T>
    {
        DomainResult Validate(T param);
    }
}