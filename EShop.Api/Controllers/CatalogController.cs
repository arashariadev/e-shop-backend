using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using EShop.Api.Helpers;
using EShop.Api.Models;
using EShop.Api.Models.CatalogItem;
using EShop.Domain.Catalog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace EShop.Api.Controllers
{
    [Route("[controller]/item")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogItemsService _catalogItemsService;
        
        public CatalogController(ICatalogItemsService catalogItemsService)
        {
            _catalogItemsService = catalogItemsService;
        }
        
        /// <summary>
        /// Get one item by id
        /// </summary>
        /// <param name="id">Item id</param>
        /// <response code="200">Returns item</response>
        /// <response code="404">Item not found</response>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CatalogItems>> GetByIdAsync([FromRoute] Guid id)
        {
            var item = await _catalogItemsService.FindItemByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }
        
        /// <summary>
        /// Search items from catalog
        /// </summary>
        /// <param name="skip">Skip items up to a specified position</param>
        /// <param name="take">Take items up to a specified position</param>
        /// <response code="200">Returns items</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerResponseHeader(StatusCodes.Status200OK, "X-TotalCount", "Number", "Total count of publications with applied filters")]
        public async Task<ActionResult<IEnumerable<CatalogItems>>> SearchAsync(
            [FromQuery, Range(0, int.MaxValue)] int skip,
            [FromQuery, Range(1, 100)] int take)
        {
            var (list, totalCount) = await _catalogItemsService.GetItemsAsync(skip, take);
            Response.Headers.Add("X-TotalCount", totalCount.ToString());

            return Ok(list);
        }
        
        /// <summary>
        /// Create one item
        /// </summary>
        /// <param name="model">CatalogItem</param>
        /// <response code="201">Item successfully created</response>
        /// <response code="400">Validation failed</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateAsync([FromBody] CreateItemViewModel model)
        {
            var (result, item) = await _catalogItemsService.InsertItemAsync(model.Name, model.Description, model.Price,
                model.PictureFileName, model.PictureUri, model.AvailableStock);

            if (result.Successed)
            {
                //need fix Created
                return Created(string.Empty, await _catalogItemsService.FindItemByIdAsync(item));
            }

            return BadRequest(result.ToProblemDetails());
        }
        
        /// <summary>
        /// Update item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <response code="204">Item successfully updated</response>
        /// <response code="400">Validation failed</response>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateItemViewModel model)
        {
            var item = await _catalogItemsService.FindItemByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }
            
            var result = await _catalogItemsService.UpdateItemAsync(id, model.Name, model.Description, model.Price,
                model.PictureFileName, model.PictureUri, model.AvailableStock);

            if (result.Successed)
            {
                return NoContent();
            }

            return BadRequest(result.ToProblemDetails());
        }
        
        /// <summary>
        /// Delete item
        /// </summary>
        /// <param name="id">Item id</param>
        /// <response code="204">Item successfully deleted</response>
        /// <response code="400">Item not found</response>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var item = await _catalogItemsService.FindItemByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            
            await _catalogItemsService.DeleteItemAsync(id);
            return NoContent();
        }
        
        /// <summary>
        /// Upload image
        /// </summary>
        /// <param name="file">file</param>
        /// <param name="id">item id</param>
        /// <response code="200">Return item with image url</response>
        /// <response code="400">Item not found or file is null</response>
        [HttpPost("{id:guid}/image/upload")]
        [RequestSizeLimit(31457280)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status413PayloadTooLarge)]
        public async Task<ActionResult<CatalogItems>> UploadImageAsync(IFormFile file, [FromRoute] Guid id)
        {
            if (file != null)
            {
                await using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    var extension = Path.GetExtension(file.FileName);
                    var result = await _catalogItemsService.UpdateImageAsync(id, memoryStream, extension);
                    if (result.Successed)
                    {
                        return Ok(await _catalogItemsService.FindItemByIdAsync(id));
                    }

                    return BadRequest(result.ToProblemDetails());
                }
            }

            return BadRequest("File is null");
        }

        /// <summary>
        /// Delete image url
        /// </summary>
        /// <param name="id">Publication id</param>
        /// <response code="200">Image url now is null</response>
        /// <response code="404">Publication not found or images url is null</response>
        [HttpDelete("{id:guid}/image/delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteImageAsync([FromRoute] Guid id)
        {
            var item = await _catalogItemsService.FindItemByIdAsync(id);

            if (item?.PictureUri == null)
            {
                return NotFound();
            }

            await _catalogItemsService.DeleteImageAsync(id);

            return Ok();
        }
    }
}