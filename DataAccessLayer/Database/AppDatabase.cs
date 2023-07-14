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

            builder.Entity<Phone>()
                .Ignore(p => p.Photo);

            base.OnModelCreating(builder);
        }
    }
}
