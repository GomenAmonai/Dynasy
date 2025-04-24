using System;
using System.Collections.Generic;

namespace Dynasy.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public ICollection<Order> Orders { get; set; } = new List<Order>();  // Инициализация коллекции
    }
}