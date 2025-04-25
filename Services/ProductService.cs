using System.Collections.Generic;
using System.Threading.Tasks;
using Dynasy.Data;
using Dynasy.Models;
using Dynasy.Services;
using Microsoft.EntityFrameworkCore;

namespace Dynasy.Services ;
public class ProductService : IProductService
{
    private readonly AppDbContext _context;

    public ProductService(AppDbContext context)
    {
        _context = context;
    }

    // Создание продукта
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

    // Получение всех продуктов
    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }

    // Получение продукта по ID
    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
    }

    // Обновление продукта
    public async Task<Product> UpdateProductAsync(int id, string name, decimal price, string description)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product == null)
        {
            return null;
        }

        product.Name = name;
        product.Price = price;
        product.Description = description;
        await _context.SaveChangesAsync();

        return product;
    }

    // Удаление продукта
    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product == null)
        {
            return false;
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }
}