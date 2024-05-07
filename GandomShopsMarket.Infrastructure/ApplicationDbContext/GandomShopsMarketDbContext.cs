#region properties

using Microsoft.EntityFrameworkCore;
using GandomShopsMarket.Domain.Entities.Account;
using GandomShopsMarket.Domain.Entities.Role;
namespace GandomShopsMarket.Infrastructure.ApplicationDbContext;

#endregion

public class GandomShopsMarketDbContext : DbContext
{
    #region Ctor

    public GandomShopsMarketDbContext(DbContextOptions<GandomShopsMarketDbContext> options)
           : base(options)
    {

    }

    #endregion

    #region Entity

    #region User

    public DbSet<User> Users { get; set; }

    public DbSet<SmsCode> SmsCodes { get; set; }

    public DbSet<UserToken> UserTokens { get; set; }

    #endregion

    #region Role 

    public DbSet<Role> Roles { get; set; }

    public DbSet<Permission> Permissions{ get; set; }

    public DbSet<RolePermission> RolePermissions { get; set; }

    public DbSet<UserRole> UserRoles { get; set; }

    #endregion

    #endregion

    #region OnConfiguring

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        base.OnModelCreating(modelBuilder);
    }

    #endregion
}
