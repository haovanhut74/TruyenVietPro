using Microsoft.EntityFrameworkCore;

namespace TruyenHayPro.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    
}