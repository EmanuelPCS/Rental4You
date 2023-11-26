using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TPRental4You.Models;

namespace TPRental4You.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<TiposDeVeiculo> TiposDeVeiculos { get; set; }
        public DbSet<Localizacao> Localizacoes { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Avaliacao> Avaliacoes { get; set; }
        public DbSet<Lucro> Lucro { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}