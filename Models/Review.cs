using System;

namespace Dynasy.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } = new Product();  // Инициализация связи с Product
        public int UserId { get; set; }
        public User User { get; set; } = new User();  // Инициализация связи с User
        public string Content { get; set; } = string.Empty;  // Инициализация значением по умолчанию
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  // Инициализация значением по умолчанию
    }
}