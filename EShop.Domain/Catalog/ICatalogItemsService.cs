using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace EShop.Domain.Catalog
{
    public interface ICatalogItemsService
    {
        Task<(IEnumerable<CatalogItems>, long)> GetItemsAsync(int skip, int take);
        
        Task<CatalogItems> FindItemByIdAsync(Guid id);

        Task<(DomainResult, Guid)> InsertItemAsync(string name, string description, decimal price, string pictureFileName, string pictureUri, int availableStock);

        Task<DomainResult> UpdateItemAsync(Guid id, string name, string description, decimal price, string pictureFileName, string pictureUri, int availableStock);

        Task DeleteItemAsync(Guid id);
    }

    public class CatalogItemsService : ICatalogItemsService
    {
        private readonly ICatalogItemsStorage _catalogItemsStorage;
        private readonly IValidator<CatalogItemContext> _validator;
        
        public CatalogItemsService(ICatalogItemsStorage catalogItemsStorage, IValidator<CatalogItemContext> validator)
        {
            _catalogItemsStorage = catalogItemsStorage;
            _validator = validator;
        }
        
        public async Task<(IEnumerable<CatalogItems>, long)> GetItemsAsync(int skip, int take)
        {
            return await _catalogItemsStorage.GetItemsAsync(skip, take);
        }

        public async Task<CatalogItems> FindItemByIdAsync(Guid id)
        {
            return await _catalogItemsStorage.FindItemByIdAsync(id);
        }

        public async Task<(DomainResult, Guid)> InsertItemAsync(string name, string description, decimal price, string pictureFileName, string pictureUri, int availableStock)
        {
            var result = _validator.Validate(new CatalogItemContext(name, description, price));

            if (!result.Successed)
            {
                return (result, default);
            }

            var item = new CatalogItems(Guid.NewGuid(), name, description, price, pictureFileName, pictureUri,
                availableStock);

            await _catalogItemsStorage.InsertItemAsync(item);

            return (DomainResult.Success(), item.Id);
        }

        public async Task<DomainResult> UpdateItemAsync(Guid id, string name, string description, decimal price, string pictureFileName, string pictureUri, int availableStock)
        {
            var result = _validator.Validate(new CatalogItemContext(name, description, price));

            if (!result.Successed)
            {
                return (result);
            }

            var item = await _catalogItemsStorage.FindItemByIdAsync(id);
            if (item == null)
            {
                return DomainResult.Error("item not found");
            }

            item.Update(name, description, price, pictureFileName, pictureUri, availableStock);
            await _catalogItemsStorage.UpdateItemAsync(id, item);

            return DomainResult.Success();
        }

        public async Task DeleteItemAsync(Guid id)
        {
            await _catalogItemsStorage.DeleteItemAsync(id);
        }
    }
}