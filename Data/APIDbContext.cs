using Microsoft.EntityFrameworkCore;

namespace APIMySQL.Data
{
    public class APIDbContext : DbContext
    {
        public APIDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
