using DogsHouseService.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsHouseService.DAL
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Dog> Dogs { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Dog>().HasKey(d => d.Name);

            builder.Entity<Dog>().Property(d => d.Name).HasColumnName("name");
            builder.Entity<Dog>().Property(d => d.Color).HasColumnName("color");
            builder.Entity<Dog>().Property(d => d.TailLength).HasColumnName("tail_length");
            builder.Entity<Dog>().Property(d => d.Weight).HasColumnName("weight");
        }
    }
}
