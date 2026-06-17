using exampleProject.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exampleProject.Data.Context
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        //veritabanında yer almasını isteediğimiz tabloları DbSet olarak tanımlıyoruz.
        public DbSet<Category> Categories { get; set; }
        public DbSet<Store> Stores { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Bu satır Identity tablolarının haritalanması için şarttır!

            modelBuilder.Entity<Category>()
                .Property(c => c.CreatedDate)
                .HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Store>()
                .Property(s => s.CreatedDate)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
