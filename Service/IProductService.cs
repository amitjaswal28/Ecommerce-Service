using Ecommerce.API.DTOs;

public interface IProductService
{
    Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync();
    Task<ProductResponseDto> GetProductByIdAsync(int id);
    Task<ProductResponseDto> CreateProductAsync(ProductCreateDto dto);
    Task<ProductResponseDto> UpdateProductAsync(int id, ProductUpdateDto dto);
    Task<bool> DeleteProductAsync(int id);
    Task<IEnumerable<ProductResponseDto>> GetFilteredProductsAsync(string? name, decimal? minPrice, decimal? maxprice);
}