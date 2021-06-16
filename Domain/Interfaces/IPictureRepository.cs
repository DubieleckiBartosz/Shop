using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPictureRepository:IBaseRepository<Picture>
    {
        Task<IEnumerable<Picture>> GetByIdProductAsync(int id);
        Task<Picture> AddPictureAsync(Picture picture);
        Task<Picture> GetByIdAsync(int id);
        Task SetMainPictureAsync(int productId,int id);
        Task DeleteAsync(Picture picture);
    }
}
