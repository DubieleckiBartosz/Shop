using Application.DtoModels.PictureModels;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Service.Interfaces
{
    public interface IPictureService
    {
        Task<PictureDto> AddPictureToProductAsync(int productId, IFormFile file);
        Task<IEnumerable<PictureDto>> GetPicturesByProductIdAsync(int productId);
        Task<PictureDto> GetPictureByIdAsync(int id);
        Task DeletePictureAsync(int id);
        Task SetMainPicture(int productId, int id);
    }
}
