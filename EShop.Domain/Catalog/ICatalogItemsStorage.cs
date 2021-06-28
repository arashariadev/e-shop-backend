using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EShop.Domain.Catalog
{
    public interface ICatalogItemsStorage
    {
        Task<(IEnumerable<CatalogItems>, long)> GetItemsAsync(int skip, int take);
        
        Task<CatalogItems> FindItemByIdAsync(Guid id);

        Task<Guid> InsertItemAsync(CatalogItems item);

        Task UpdateItemAsync(Guid id, CatalogItems updatedItem);

        Task DeleteItemAsync(Guid id);

        Task UpdatePictureUriAsync(Guid id, string pictureUri);

        Task DeletePictureUriAsync(Guid id);
    }
}