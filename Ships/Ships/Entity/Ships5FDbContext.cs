using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ships.Entity
{
    internal class ShipsDbContext : DbContext
    {
        public ShipsDbContext() { }

        public ShipsDbContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Board> LastGame { get; set; }
        public DbSet<Cords> PlayerShips { get; set; }
        public DbSet<Cords2> EnemyShips { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;Database=Ships5F;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
