using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Domain;
using EShop.Domain.Azure;

namespace EShop.Domain.Catalog
{
    public interface ICatalogItemsService
    {
        Task<(IEnumerable<CatalogItems>, long)> GetItemsAsync(int skip, int take);
        
        Task<CatalogItems> FindItemByIdAsync(Guid id);

        Task<(DomainResult, Guid)> InsertItemAsync(string name, string description, decimal price, string pictureFileName, string pictureUri, int availableStock);

        Task<DomainResult> UpdateItemAsync(Guid id, string name, string description, decimal price, string pictureFileName, string pictureUri, int availableStock);

        Task DeleteItemAsync(Guid id);
        
        Task<DomainResult> UpdateImageAsync(Guid id, Stream stream, string extension);

        Task<DomainResult> DeleteImageAsync(Guid id);
    }

    public class CatalogItemsService : ICatalogItemsService
    {
        private readonly ICatalogItemsStorage _catalogItemsStorage;
        private readonly IImagesStorage _imagesStorage;
        private readonly IValidator<CatalogItemContext> _validator;
        
        public CatalogItemsService(ICatalogItemsStorage catalogItemsStorage, IValidator<CatalogItemContext> validator, IImagesStorage imagesStorage)
        {
            _catalogItemsStorage = catalogItemsStorage;
            _imagesStorage = imagesStorage;
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

        public async Task<DomainResult> UpdateImageAsync(Guid id, Stream stream, string extension)
        {
            var item = await _catalogItemsStorage.FindItemByIdAsync(id);
            if (item == null)
            {
                return DomainResult.Error("Item from catalog not found.");
            }

            var fileName = Guid.NewGuid() + extension;
            stream.Position = 0;

            var imagesUrl = await _imagesStorage.UploadImageAsync(stream, fileName);
            if (item.PictureUri != null)
            {
                return DomainResult.Error("Picture is exist");
            }

            await _catalogItemsStorage.UpdatePictureUriAsync(id, imagesUrl);
            return DomainResult.Success();
        }

        public async Task<DomainResult> DeleteImageAsync(Guid id)
        {
            var item = await _catalogItemsStorage.FindItemByIdAsync(id);

            await _imagesStorage.DeleteImageAsync(item.PictureUri);

            await _catalogItemsStorage.DeletePictureUriAsync(id);

            return DomainResult.Success();
        }
    }
}