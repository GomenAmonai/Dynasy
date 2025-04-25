using System.Collections.Generic;
using System.Threading.Tasks;
using Dynasy.Models;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(Order order);
    Task<Order> GetOrderByIdAsync(int orderId);
    Task<IEnumerable<Order>> GetAllOrdersAsync();
    Task UpdateOrderStatusAsync(int orderId, OrderStatus status);
    Task DeleteOrderAsync(int orderId);
}