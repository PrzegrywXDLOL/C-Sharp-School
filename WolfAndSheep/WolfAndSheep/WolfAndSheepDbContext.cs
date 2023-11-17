using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolfAndSheep {
    internal class WolfAndSheepDbContext : DbContext 
    {
        public WolfAndSheepDbContext() { }
        public WolfAndSheepDbContext(DbContextOptions<DbContext> options) : base(options) { }
        public DbSet<Move> Moves { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;Database=WolfAndSheep5F;Trusted_Connection=True;"
                );
            base.OnConfiguring(optionsBuilder);
        }
    }
}
