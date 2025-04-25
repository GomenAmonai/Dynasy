using Dynasy.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dynasy.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int productId);
        Task<Product> CreateProductAsync(string name, decimal price, string description);
        Task<Product> UpdateProductAsync(int productId, string name, decimal price, string description);
        Task<bool> DeleteProductAsync(int productId);
    }
}