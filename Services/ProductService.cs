using Dynasy.Data;
using Dynasy.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dynasy.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        // Получить все продукты
        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        // Получить продукт по ID
        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _context.Products
                                 .FirstOrDefaultAsync(p => p.Id == productId);
        }

        // Создать новый продукт
        public async Task<Product> CreateProductAsync(string name, decimal price, string description)
        {
            var product = new Product
            {
                Name = name,
                Price = price,
                Description = description
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        // Обновить продукт
        public async Task<Product> UpdateProductAsync(int productId, string name, decimal price, string description)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return null;

            product.Name = name;
            product.Price = price;
            product.Description = description;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        // Удалить продукт
        public async Task<bool> DeleteProductAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}