using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Dynasy.Data;
using Dynasy.DTOs;
using Dynasy.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Dynasy.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderService> _logger;

        public OrderService(AppDbContext context, IMapper mapper, ILogger<OrderService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            try
            {
                var orders = await _context.Orders
                    .Include(o => o.User)
                    .Include(o => o.Products)
                    .ToListAsync();

                return _mapper.Map<IEnumerable<OrderDto>>(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении списка заказов");
                throw new ApplicationException("Не удалось получить список заказов", ex);
            }
        }

        public async Task<OrderDto> GetOrderByIdAsync(int id)
        {
            var order = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.Products)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                throw new NotFoundException($"Заказ с ID {id} не найден");

            return _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> CreateOrderAsync(CreateOrderDto createOrderDto)
        {
            try
            {
                var products = await _context.Products
                    .Where(p => createOrderDto.ProductIds.Contains(p.Id))
                    .ToListAsync();

                if (products.Count != createOrderDto.ProductIds.Count)
                    throw new ValidationException("Некоторые продукты не найдены");

                var order = new Order
                {
                    UserId = createOrderDto.UserId,
                    Products = products,
                    Status = OrderStatus.Created,
                    CreatedAt = DateTime.UtcNow,
                    TotalAmount = products.Sum(p => p.Price)
                };

                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();

                return _mapper.Map<OrderDto>(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при создании заказа");
                throw;
            }
        }

        public async Task<OrderDto> UpdateOrderAsync(int id, UpdateOrderDto updateOrderDto)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
                throw new NotFoundException($"Заказ с ID {id} не найден");

            order.Status = updateOrderDto.Status;
            await _context.SaveChangesAsync();

            return _mapper.Map<OrderDto>(order);
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
                throw new NotFoundException($"Заказ с ID {id} не найден");

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
    }
}