using Dynasy.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dynasy.Services;

public interface IProductService
{
    Task<Product> CreateProductAsync(string name, decimal price, string description);
    Task<List<Product>> GetAllProductsAsync();
    Task<Product> GetProductByIdAsync(int id);
    Task<Product> UpdateProductAsync(int id, string name, decimal price, string description);
    Task<bool> DeleteProductAsync(int id);
}