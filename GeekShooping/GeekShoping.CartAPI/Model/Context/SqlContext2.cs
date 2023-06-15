using GeekShopping.CartAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace GeekShooping.CartAPI.Model.Context
{
    public class SqlContext2 : DbContext
    {
        public SqlContext2() { }

        public SqlContext2(DbContextOptions<SqlContext2> options)
            : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<CartHeader> CartHeaders { get; set; }



    }
}

