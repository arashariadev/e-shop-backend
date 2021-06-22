using System;

namespace EShop.Domain.Catalog
{
    public class CatalogItems
    {
        public CatalogItems(
            Guid id, string name, string description, decimal price, string pictureFileName, string pictureUri, int availableStock)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            PictureFileName = pictureFileName;
            PictureUri = pictureUri;
            AvailableStock = availableStock;
        }
        
        public Guid Id { get; }
        
        public string Name { get; private set; }
        
        public string Description { get; private set; }
        
        public decimal Price { get; private set; }
        
        public string PictureFileName { get; private set; }
        
        public string PictureUri { get; private set; }
        
        public int AvailableStock { get; private set; }

        public CatalogItems Update(string name, string description, decimal price, string pictureFileName, string pictureUri, int availableStock)
        {
            Name = name;
            Description = description;
            Price = price;
            PictureFileName = pictureFileName;
            PictureUri = pictureUri;
            AvailableStock = availableStock;

            return this;
        }
    }
}