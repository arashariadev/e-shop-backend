namespace EShop.Api.Models
{
    public class UpdateItemViewModel
    {
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public decimal Price { get; set; }
        
        public string PictureFileName { get; set; }
        
        public string PictureUri { get; set; }
        
        public int AvailableStock { get; set; }
    }
}