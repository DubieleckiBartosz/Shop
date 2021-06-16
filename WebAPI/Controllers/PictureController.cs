using Application.DtoModels.PictureModels;
using Application.Service.Interfaces;
using Application.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Manager")]
    public class PictureController : ControllerBase
    {
        private readonly IPictureService _pictureService;

        public PictureController(IPictureService pictureService)
        {
            _pictureService = pictureService;
        }

        [SwaggerOperation(Summary ="Add picture to product")]
        [HttpPost("{productId}")]
        public async Task<IActionResult> AddToProduct(int productId, IFormFile file)
        {
            var picture = await _pictureService.AddPictureToProductAsync(productId, file);
            return Created($"api/pictures/{picture.Id}", new ServiceResponse<PictureDto>(picture));
        }

        [SwaggerOperation(Summary = "Receiving pictures selected product")]
        [HttpGet("[action]/{productId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByProductId([FromRoute] int productId)
        {
            var pictures = await _pictureService.GetPicturesByProductIdAsync(productId);
            return Ok(new ServiceResponse<IEnumerable<PictureDto>>(pictures));
        }

        [SwaggerOperation(Summary = "Receiving picture")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPicture([FromRoute]int id)
        {
            var picture = await _pictureService.GetPictureByIdAsync(id);
            return Ok(new ServiceResponse<PictureDto>(picture));
        }

        [SwaggerOperation(Summary = "Set main picture")]
        [HttpPut("[action]/{productId}/{id}")]
        public async Task<IActionResult> SetMainPicture(int productId,int id)
        {
            await _pictureService.SetMainPicture(productId, id);
            return NoContent();
        }

        [SwaggerOperation(Summary = "Delete picture")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePicture([FromRoute]int id)
        {
            await _pictureService.DeletePictureAsync(id);
            return NoContent();
        }

    }
}
