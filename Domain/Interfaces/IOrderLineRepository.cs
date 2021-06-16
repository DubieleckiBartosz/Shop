using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IOrderLineRepository:IBaseRepository<OrderLine>
    {
        Task<OrderLine> GetOrderLineAsync(int orderLineId);
        Task UpdateOrderLineAsync(OrderLine orderLine);
    }
}
