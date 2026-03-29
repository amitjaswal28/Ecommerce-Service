using Microsoft.AspNetCore.Mvc;
using Ecommerce.API.DTOs;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _service;

    public ProductController(IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetProducts()
    {
        return Ok(await _service.GetAllProductsAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductResponseDto>> GetProduct(int id)
    {
        var product = await _service.GetProductByIdAsync(id);
        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<ProductResponseDto>> CreateProduct(ProductCreateDto dto)
    {
        var product = await _service.CreateProductAsync(dto);
        return Ok(product);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProductResponseDto>> UpdateProduct(int id, ProductUpdateDto dto)
    {
        var product = await _service.UpdateProductAsync(id, dto);
        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var deleted = await _service.DeleteProductAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }

    [HttpGet("filter")]
    public async Task<ActionResult<IEnumerable<ProductResponseDto>>> FilterProducts(
     string? name,
     decimal? minPrice,
     decimal? maxPrice,
     string? sortBy,
     int pageNumber=1,
     int pageSize=5)
    {
        var products = await _service.GetFilteredProductsAsync(name, minPrice, maxPrice,sortBy,pageNumber,pageSize);
        return Ok(products);
    }

}