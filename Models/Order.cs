using System;
using System.Collections.Generic;

namespace Dynasy.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = new User();  // Инициализация связи с User
        public ICollection<Product> Products { get; set; } = new List<Product>();  // Инициализация коллекции
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  // Инициализация значением по умолчанию
        public OrderStatus Status { get; set; } // Инициализация значением по умолчанию
    }
}