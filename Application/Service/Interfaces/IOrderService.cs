using Application.DtoModels.OrderLineModels;
using Application.DtoModels.OrderModels;
using Application.Wrapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Service.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrderAsync(CreateOrderDto createOrderDto,string userId);
        Task<ServiceResponse> DeleteAsync(int id, string userId);
        Task<IEnumerable<OrderDto>> GetActiveOrdersAsync(string userId);
        Task<IEnumerable<OrderDto>> GetOrdersByDateAsync(DateTime minDate, DateTime maxDate);
        Task<ServiceResponse> UpdateOrderAsync(int orderId,string userId, UpdateOrderLineDto updatelineDto);
    }
}
