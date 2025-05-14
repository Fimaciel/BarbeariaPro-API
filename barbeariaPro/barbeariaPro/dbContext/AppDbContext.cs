using Microsoft.EntityFrameworkCore;

namespace barbeariaPro.dbContext;
using barbeariaPro.Models;

public class AppDbContext :  Microsoft.EntityFrameworkCore.DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    
    public DbSet<Cliente> Clientes { get; set; }
}