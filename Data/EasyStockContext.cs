using Microsoft.EntityFrameworkCore;
using EasyStock.Models;

namespace EasyStock.Data
{
    public class EasyStockContext : DbContext
    {
        public EasyStockContext(DbContextOptions<EasyStockContext> options)
            : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; } = null!;
    }
}