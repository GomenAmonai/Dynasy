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
    private readonly IMapper _mapper;
    private readonly ILogger<ProductService> _logger;

    public ProductService(AppDbContext context, IMapper mapper, ILogger<ProductService> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        try
        {
            var products = await _context.Products
                .Include(p => p.Seller)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении списка продуктов");
            throw new ApplicationException("Не удалось получить список продуктов", ex);
        }
    }

    public async Task<ProductDto> GetProductByIdAsync(int id)
    {
        var product = await _context.Products
            .Include(p => p.Seller)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
            throw new NotFoundException($"Продукт с ID {id} не найден");

        return _mapper.Map<ProductDto>(product);
    }

    public async Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto)
    {
        try
        {
            var product = _mapper.Map<Product>(createProductDto);
            product.CreatedAt = DateTime.UtcNow;

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProductDto>(product);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при создании продукта");
            throw;
        }
    }

    public async Task<ProductDto> UpdateProductAsync(int id, UpdateProductDto updateProductDto)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
            throw new NotFoundException($"Продукт с ID {id} не найден");

        _mapper.Map(updateProductDto, product);
        await _context.SaveChangesAsync();

        return _mapper.Map<ProductDto>(product);
    }

    public async Task DeleteProductAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
            throw new NotFoundException($"Продукт с ID {id} не найден");

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
}