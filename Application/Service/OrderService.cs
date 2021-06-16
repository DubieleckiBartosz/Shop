using Application.DtoModels.OrderLineModels;
using Application.DtoModels.OrderModels;
using Application.Exceptions;
using Application.Service.Interfaces;
using Application.Wrapper;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Service
{
    public class OrderService:IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderService> _logger;
        private readonly IUserContextService _userContextService;
        private readonly IOrderLineRepository _orderLineRepository;
        public OrderService(IProductRepository productRepository,IOrderRepository orderRepository,IUserContextService userContextService,
            IMapper mapper,ILogger<OrderService> logger,IOrderLineRepository orderLineRepository)
        {
            _orderLineRepository = orderLineRepository;
            _userContextService = userContextService;
            _orderRepository = orderRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<OrderDto> CreateOrderAsync(CreateOrderDto createOrderDto,string userId)
        {
            if (createOrderDto is null) throw new BadRequestException("Order is null");
            var orderDb = _mapper.Map<Order>(createOrderDto);
            orderDb.UserId = userId;          

            var order=await _orderRepository.CreateOrderAsync(orderDb);
            _logger.LogInformation("New order created");
             return _mapper.Map<OrderDto>(order);
        }

        public async Task<ServiceResponse> DeleteAsync(int id,string userId)
        {
            if(await UserOrder(id, userId)||GetRole())
            {

                var orderEntity = await _orderRepository.GetOrderAsync(id);
                if (orderEntity is null) throw new NotFoundException("Not found order, try again");
                if ((DateTime.Now - orderEntity.CreatedDate).TotalDays < 3)
                {
                    await _orderRepository.DeleteOrderAsync(orderEntity);
                    _logger.LogInformation($"Order with id {id} is deleted");
                    return new ServiceResponse("Order has been deleted");
                }
                return new ServiceResponse("More than three days have passed", false);
            }
            else
            {
                return new ServiceResponse("Not found order", false);
            }


        }

        public async Task<IEnumerable<OrderDto>> GetActiveOrdersAsync(string userId)
        {
            var order = await _orderRepository.GetOrdersByUserAsync(userId);
            if (order is null) throw new NotFoundException("There is no pending order");
            return _mapper.Map<IEnumerable<OrderDto>>(order);
        }


        public async Task<IEnumerable<OrderDto>> GetOrdersByDateAsync(DateTime minDate, DateTime maxDate)
        {
            var orders =await _orderRepository.GetOrderByDateWithDetailsAsync(minDate, maxDate);
            if (orders is null) throw new NotFoundException($"Order not found between {minDate} and {maxDate}");

            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<ServiceResponse> UpdateOrderAsync(int orderId, string userId, UpdateOrderLineDto updatelineDto)
        {
            ServiceResponse response = new ServiceResponse();
            if (!await UserOrder(orderId, userId))
            {
                response.Success = false;
                response.Message = "Order not found";
                return response;
            }    
                var line = await _orderLineRepository.GetOrderLineAsync(updatelineDto.PositionId);

                if (line is null) throw new NotFoundException("Position not found");
                _mapper.Map(updatelineDto, line);
                await _orderLineRepository.UpdateOrderLineAsync(line);
                var order=await _orderRepository.GetOrderAsync(orderId);

                 await _orderRepository.UpdateOrderAsync(order);
                 response.Message="Order has been changed";
                return response;    
        }

        private async Task<bool> UserOrder(int orderId, string userId)
        {
            var order = await _orderRepository.CheckOrderExistAsync(orderId);
            if (order is null) return false;
            if (order.UserId != userId) return false;
            return true;
        }
        private bool GetRole()
        {
            var role = _userContextService.GetUser;
            if ((role=="Admin") || (role=="Manager")) return true;
            return false;       
        }


    }
}
