using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class OrderRepository:BaseRepository<Order>,IOrderRepository
    {
        public OrderRepository(ApplicationDbContext db):base(db)
        {
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            order.Amount = order.Line.TotalPrice(_db.Products);

            var createdOrder = await _db.Orders.AddAsync(order);          
            await _db.SaveChangesAsync();
            return createdOrder.Entity;
        }

        public async Task DeleteOrderAsync(Order order)
        {
            await Delete(order);
        }
     
        public async Task<Order> GetOrderAsync(int id)
        {
            return await FindByCondition(c => c.Id.Equals(id))
            .Include(s => s.Line)
            .FirstOrDefaultAsync();

        }
        public async Task<Order> CheckOrderExistAsync(int id)
        {
            return await FindByCondition(c => c.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Order>> GetOrderByDateAsync(DateTime startDate,DateTime endDate)
        {
            return await FindByCondition(c => c.CreatedDate>=startDate && c.CreatedDate <= endDate/*.AddDays(1)*/).Include(s=>s.Address).ToListAsync();
        }
        public async Task<IEnumerable<Order>> GetOrderByDateWithDetailsAsync(DateTime startDate, DateTime endDate)
        {
            return await FindByCondition(c => c.CreatedDate >= startDate && c.CreatedDate <= endDate)
                .Include(s => s.Address).Include(c=>c.Line).ThenInclude(c=>c.Product).ToListAsync();
        }
        public async Task<IEnumerable<Order>> GetOrdersByUserAsync(string userId)
        {
            return  await FindByCondition(c => c.UserId == userId && c.CreatedDate.AddDays(3) >= DateTime.Now)
                .Include(c=>c.Address)
                .Include(c => c.Line)
                .ThenInclude(c=>c.Product).ToListAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            order.Amount = order.Line.TotalPrice(_db.Products);
            await Update(order);
        }
    }
}
