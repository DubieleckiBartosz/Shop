using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class OrderLineRepository : BaseRepository<OrderLine>, IOrderLineRepository
    {
        public OrderLineRepository(ApplicationDbContext db):base(db)
        {

        }
        public async Task<OrderLine> GetOrderLineAsync(int orderLineId)
        {
            return await FindByCondition(c => c.Id == orderLineId).FirstOrDefaultAsync();
        }

        public async Task UpdateOrderLineAsync(OrderLine orderLine)
        {
            await Update(orderLine);
        }
    }
}
