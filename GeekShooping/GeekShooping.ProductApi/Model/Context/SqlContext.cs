using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace GeekShooping.ProductApi.Model.Context
{
    public class SqlContext : DbContext
    {
        public SqlContext() { }
        
        public SqlContext(DbContextOptions<SqlContext> options)
            : base(options) { }
        
       public System.Data.Entity.DbSet<Product> Products { get; set; } 
    }
}
