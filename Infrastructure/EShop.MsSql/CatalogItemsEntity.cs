using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShop.MsSql
{
    public class CatalogItemsEntity
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        [Column(TypeName = "decimal")]
        [Required]
        public decimal Price { get; set; }
        
        public string PictureFileName { get; set; }
        
        public string PictureUri { get; set; }
        
        public int AvailableStock { get; set; }
    }
}