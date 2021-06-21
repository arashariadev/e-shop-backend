namespace EShop.Domain.Catalog
{
    public class CatalogItemContext
    {
        public CatalogItemContext(string name, string description, decimal price)
        {
            Name = name;
            Description = description;
            Price = price;
        }
        
        public string Name { get; }
        
        public string Description { get; }
        
        public decimal Price { get; }
    }
}