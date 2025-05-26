using Microsoft.EntityFrameworkCore;

namespace CW_9_s29441.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
}