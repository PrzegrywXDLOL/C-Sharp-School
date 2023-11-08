using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crud5FNet7.Entity
{
    internal class crud5FNet7DbContext : DbContext
    {
        public crud5FNet7DbContext() { }

        public crud5FNet7DbContext(DbContextOptions options) 
            : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;Database=crud5FNet7;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
