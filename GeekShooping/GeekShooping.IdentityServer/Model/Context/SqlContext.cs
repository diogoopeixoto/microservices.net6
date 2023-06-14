using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GeekShooping.IdentityServer.Model.Context
{
    public class SqlContext : IdentityDbContext<ApplicationUser>
    {


        public SqlContext(DbContextOptions<SqlContext> options)
            : base(options) { }

    }
          
}
