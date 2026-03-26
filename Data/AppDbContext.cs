using Ecommerce.API.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

    }
}
