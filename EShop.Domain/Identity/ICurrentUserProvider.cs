namespace EShop.Domain.Identity
{
    public interface ICurrentUserProvider
    {
        public string UserId { get; }
    }
}