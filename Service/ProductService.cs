using AutoMapper;
using Ecommerce.API.DTOs;
using Ecommerce.API.Models;
using Ecommerce.API.Repositories;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync()
    {
        var products = await _repository.GetAllAsync();

        var result = _mapper.Map<List<ProductResponseDto>>(products);
        return result;
    }

    public async Task<ProductResponseDto> GetProductByIdAsync(int id)
    {
        var product = await _repository.GetByIdAsync(id);
        if (product == null) return null;
        var result = _mapper.Map<ProductResponseDto>(product);
        return result;
    }

    public async Task<ProductResponseDto> CreateProductAsync(ProductCreateDto dto)
    {

        var product = _mapper.Map<Product>(dto);
        product.CreatedDate = DateTime.UtcNow;

        var created = await _repository.CreateAsync(product);

        var result = _mapper.Map<ProductResponseDto>(created);

        return result;
    }

    public async Task<ProductResponseDto> UpdateProductAsync(int id, ProductUpdateDto dto)
    {
        var product = _mapper.Map<Product>(await _repository.GetByIdAsync(id));
        product.Name = dto.Name;
        product.Price = dto.Price;
        product.Description = dto.Description;
        if (product == null) return null;

        await _repository.UpdateAsync(product);
        var result = _mapper.Map<ProductResponseDto>(product);
        return result;
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<ProductResponseDto>> GetFilteredProductsAsync(string? name, decimal? minPrice, decimal? maxprice, string? sortBy, int PageNumber, int PageSize)
    {
        var products = await _repository.GetFilterAsync(name, minPrice, maxprice,sortBy,PageNumber,PageSize);

        var result = _mapper.Map<List<ProductResponseDto>>(products);
        return result;
    }
}