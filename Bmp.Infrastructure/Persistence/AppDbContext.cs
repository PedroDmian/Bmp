using Microsoft.EntityFrameworkCore;
using Bmp.Domain.Entities;
using Bmp.Domain.Common;
using System.Linq.Expressions;

namespace Bmp.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
            {
                modelBuilder.Entity(entityType.ClrType)
                    .HasQueryFilter(GetIsDeletedRestriction(entityType.ClrType));
            }
        }

        /*
        * Configure relationships and constraints here
        */
        /*modelBuilder.Entity<User>()
            .HasMany(u => u.Archives)
            .WithOne(a => a.User)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);*/
    }

    private static LambdaExpression GetIsDeletedRestriction(Type type)
    {
        var param = Expression.Parameter(type, "e");
        var prop = Expression.Property(param, nameof(ISoftDelete.DeletedAt));
        var condition = Expression.Equal(prop, Expression.Constant(null, typeof(DateTime?)));
        var lambda = Expression.Lambda(condition, param);

        return lambda;
    }
}