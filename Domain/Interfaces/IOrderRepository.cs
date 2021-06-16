using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IOrderRepository:IBaseRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrderByDateAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Order>> GetOrderByDateWithDetailsAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Order>> GetOrdersByUserAsync(string userId);
        Task UpdateOrderAsync(Order order);
        Task<Order> CheckOrderExistAsync(int id);
        Task<Order> CreateOrderAsync(Order order);
        Task DeleteOrderAsync(Order order);
        Task<Order> GetOrderAsync(int id);
    }
}
