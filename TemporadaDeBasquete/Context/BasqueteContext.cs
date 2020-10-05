using Microsoft.EntityFrameworkCore;
using TemporadaDeBasquete.Models;

namespace WebAPI.Models
{
    public class BasqueteContext : DbContext
    {
        public BasqueteContext(DbContextOptions<BasqueteContext> options) : base(options)
        {

        }

        public DbSet<RegistroJogo> RegistroJogo { get; set; }

        public DbSet<Temporada> Temporada { get; set; }
    }
}
