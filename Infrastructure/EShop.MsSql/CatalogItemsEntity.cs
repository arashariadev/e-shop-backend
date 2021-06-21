using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShop.MsSql
{
    public class CatalogItemsEntity
    {
        [Key]
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        [Column(TypeName = "decimal")]
        public decimal Price { get; set; }
        
        public string PictureFileName { get; set; }
        
        public string PictureUri { get; set; }
        
        public int AvailableStock { get; set; }
    }
}