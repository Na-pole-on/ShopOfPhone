using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Database
{
    public class AppDatabase: IdentityDbContext<User>
    {
        public DbSet<Phone> Phones { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;

        public AppDatabase(DbContextOptions<AppDatabase> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
             builder.Entity<User>()
                .HasMany(u => u.Phones)
                .WithOne(p => p.User)
                .HasForeignKey(u => u.UserName)
                .HasPrincipalKey(u => u.UserName);

            builder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .HasPrincipalKey(u => u.Id);

            builder.Entity<Phone>()
                .Ignore(p => p.Photo);

            builder.Entity<Phone>()
                .HasOne(p => p.Order)
                .WithOne(o => o.Phone)
                .HasForeignKey<Order>(o => o.PhoneId)
                .HasPrincipalKey<Phone>(p => p.Id);

            base.OnModelCreating(builder);
        }
    }
}
