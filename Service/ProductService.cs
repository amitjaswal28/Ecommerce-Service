using Ecommerce.API.DTOs;
using Ecommerce.API.Models;
using Ecommerce.API.Repositories;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync()
    {
        var products = await _repository.GetAllAsync();

        return products.Select(p => new ProductResponseDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            CreatedDate = p.CreatedDate
        });
    }

    public async Task<ProductResponseDto> GetProductByIdAsync(int id)
    {
        var product = await _repository.GetByIdAsync(id);
        if (product == null) return null;

        return new ProductResponseDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            CreatedDate = product.CreatedDate
        };
    }

    public async Task<ProductResponseDto> CreateProductAsync(ProductCreateDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            CreatedDate = DateTime.UtcNow
        };

        var created = await _repository.CreateAsync(product);

        return new ProductResponseDto
        {
            Id = created.Id,
            Name = created.Name,
            Description = created.Description,
            Price = created.Price,
            CreatedDate = created.CreatedDate
        };
    }

    public async Task<ProductResponseDto> UpdateProductAsync(int id, ProductUpdateDto dto)
    {
        var product = await _repository.GetByIdAsync(id);
        if (product == null) return null;

        product.Name = dto.Name;
        product.Description = dto.Description;
        product.Price = dto.Price;

        await _repository.UpdateAsync(product);

        return new ProductResponseDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            CreatedDate = product.CreatedDate
        };
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<ProductResponseDto>> GetFilteredProductsAsync(string? name, decimal? minPrice, decimal? maxprice, string? sortBy, int PageNumber, int PageSize)
    {
        var products = await _repository.GetFilterAsync(name, minPrice, maxprice,sortBy,PageNumber,PageSize);
        return products.Select(p => new ProductResponseDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            CreatedDate = p.CreatedDate
        });
    }
}