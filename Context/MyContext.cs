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
        public DbSet<Account> Account { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Account)
                .WithOne(a => a.Employee)
                .HasForeignKey<Account>(a=>a.NIK); //foreign key sesuaikan dengan yang di erd
        }
    }
}
