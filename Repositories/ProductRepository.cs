using Ecommerce.API.Data;
using Ecommerce.API.Models;
using Ecommerce.API.Repositories;
using Microsoft.EntityFrameworkCore;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task<Product> CreateAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
            return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<IEnumerable<Product>> GetFilterAsync(string? name, decimal? minPrice, decimal? maxPrice, string? sortBy, int PageNumber, int PageSize)
    {
        var query = _context.Products.AsQueryable();

        if(!string.IsNullOrEmpty(name))
        {
            query=query.Where(x=> x.Name.Contains(name));
        }
        if(minPrice != null)
        {
            query=query.Where(x=>x.Price >= minPrice.Value);
        }
        if (maxPrice != null)
        {
            query=query.Where(x=>x.Price <= maxPrice.Value);
        }
        if (!string.IsNullOrEmpty(sortBy))
        {
            if (sortBy == "name")
            {
                query = query.OrderBy(x => x.Name);
            }
            else if (sortBy == "price")
            {
                query = query.OrderBy(x => x.Price);
            }
            else if (sortBy == "priceDsc")
            {
                query = query.OrderByDescending(x => x.Price);
            }
        }
        if (PageNumber < 1)
            PageNumber = 1;

        query = query.Skip((PageNumber - 1) * PageSize).Take(PageSize);

        return await query.ToListAsync();
    }
}