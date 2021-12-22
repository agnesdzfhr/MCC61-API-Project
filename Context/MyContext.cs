using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MCC61_API_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace MCC61_API_Project.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; } //menghubungi table ke database di sql server
        public DbSet<Account> Accounts { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Profiling> Profilings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<University>()
                .HasMany(u => u.Educations)
                .WithOne(ed => ed.University);

            modelBuilder.Entity<Education>()
                .HasMany(ed => ed.Profilings)
                .WithOne(p => p.Education);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Account)
                .WithOne(a => a.Employee)
                .HasForeignKey<Account>(a=>a.NIK); //foreign key sesuaikan dengan yang di erd

            modelBuilder.Entity<Account>()
                .HasOne(a => a.Profiling)
                .WithOne(p => p.Account)
                .HasForeignKey<Profiling>(p => p.NIK);
        }
    }
}
