using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Domain.Catalog;
using Microsoft.EntityFrameworkCore;

namespace EShop.MsSql
{
    public class CatalogItemsStorage : ICatalogItemsStorage
    {
        private readonly MsSqlContext _context;
        
        public CatalogItemsStorage(MsSqlContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<CatalogItems>, long)> GetItemsAsync(int skip, int take)
        {
            var totalCount = await _context.CatalogItems.CountAsync();

            var entity = await _context.CatalogItems.OrderBy(x => x.Name).Skip(skip).Take(take).ToListAsync();

            return (entity.Select(ToDomain), totalCount);
        }

        public async Task<CatalogItems> FindItemByIdAsync(Guid id)
        {
            var item = await _context.CatalogItems.SingleOrDefaultAsync(x => x.Id == id);

            return item == null ? null : ToDomain(item);
        }

        public async Task<Guid> InsertItemAsync(CatalogItems item)
        {
            var entity = new CatalogItemsEntity
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                PictureFileName = item.PictureFileName,
                PictureUri = item.PictureUri,
                AvailableStock = item.AvailableStock
            };

            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task UpdateItemAsync(Guid id, CatalogItems updatedItem)
        {
            var item = await _context.CatalogItems.SingleOrDefaultAsync(x => x.Id == id);

            if (item == null)
            {
                return;
            }

            item.Name = updatedItem.Name;
            item.Description = updatedItem.Description;
            item.Price = updatedItem.Price;
            item.PictureFileName = updatedItem.PictureFileName;
            item.AvailableStock = updatedItem.AvailableStock;

            _context.CatalogItems.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var item = await _context.CatalogItems.SingleOrDefaultAsync(x => x.Id == id);
            
            if (item == null)
            {
                return;
            }

            _context.CatalogItems.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePictureUriAsync(Guid id, string pictureUri)
        {
            var item = await _context.CatalogItems.SingleOrDefaultAsync(x => x.Id == id);

            if (item == null)
            {
                return;
            }

            item.PictureUri = pictureUri;

            _context.CatalogItems.Update(item);
            await _context.SaveChangesAsync();

        }

        public async Task DeletePictureUriAsync(Guid id)
        {
            var item = await _context.CatalogItems.SingleOrDefaultAsync(x => x.Id == id);

            if (item == null)
            {
                return;
            }

            item.PictureUri = null;

            _context.CatalogItems.Update(item);
            await _context.SaveChangesAsync();
        }

        private static CatalogItems ToDomain(CatalogItemsEntity entity)
        {
            return new CatalogItems(
                entity.Id,
                entity.Name,
                entity.Description,
                entity.Price,
                entity.PictureFileName,
                entity.PictureUri,
                entity.AvailableStock
                );
        }
    }
}