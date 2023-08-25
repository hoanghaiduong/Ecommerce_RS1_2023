using Microsoft.EntityFrameworkCore;

namespace Ecommerce_2023.Models.Role
{
    public class RoleContext : DbContext
    {
        public RoleContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<RoleEntity> Roles { get; set; }                    
    }
}
