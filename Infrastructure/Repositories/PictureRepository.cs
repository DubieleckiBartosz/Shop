using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PictureRepository : BaseRepository<Picture>, IPictureRepository
    {
        public PictureRepository(ApplicationDbContext db) : base(db)
        {

        }


        public async Task<Picture> AddPictureAsync(Picture picture)
        {
            var createdPicture = await _db.Pictures.AddAsync(picture);
            await _db.SaveChangesAsync();
            return createdPicture.Entity;
        }

        public async Task DeleteAsync(Picture picture)
        {
           await Delete(picture);
        }

        public async Task<Picture> GetByIdAsync(int id)
        {
            return await FindByCondition(c => c.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Picture>> GetByIdProductAsync(int id)
        {
            return await _db.Pictures.Include(c => c.Products).Where(c => c.Products.Select(x => x.Id).Contains(id)).ToListAsync();
        }

        public async Task SetMainPictureAsync(int productId, int id)
        {
            var currentMainPicture = await _db.Pictures.Include(c => c.Products)
                .Where(x => x.Products.Select(x => x.Id).Contains(productId)).FirstOrDefaultAsync();
            currentMainPicture.Main = false;

            var newMainPicture = await FindByCondition(c => c.Id == id).FirstOrDefaultAsync();
            newMainPicture.Main = true;
            await _db.SaveChangesAsync();
        }
    }
}
