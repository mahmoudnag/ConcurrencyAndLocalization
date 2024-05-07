using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace prepare4Eplan.Models
{
   
        public class ApplicationDBContext : DbContext
        {

            public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }
            public DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>()
                .Property(p => p.RowVersion)
                
                .ValueGeneratedOnAddOrUpdate()
                .IsConcurrencyToken();

        }
         
        }
    }

