
using InternetServiceBack.Models;
using Microsoft.EntityFrameworkCore;

namespace InternetServiceBack.Models;

public partial class DatabaseInternetServiceContext : DbContext
{

    public DatabaseInternetServiceContext(DbContextOptions<DatabaseInternetServiceContext> options)
            : base(options) { }
    
    public DbSet<AttentionStatus> AttentionStatus { get; set; }
    public DbSet<AttentionType> AttentionType{ get; set; }
    public DbSet<Cash> Cash { get; set; }
    public DbSet<Client> Client { get; set; }
    public DbSet<Menu> Menu { get; set; }
    public DbSet<Turn> Turn { get; set; }
    public DbSet<User> User { get; set; }
    


}
