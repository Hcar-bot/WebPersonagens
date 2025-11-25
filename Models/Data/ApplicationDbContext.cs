using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebPersonagens.Models;

namespace WebPersonagens.Data
{
    // Tem que ser PUBLIC e herdar de IdentityDbContext
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Universo> Universos { get; set; }
        public DbSet<Profissao> Profissoes { get; set; }
        public DbSet<Personagem> Personagens { get; set; }
        public DbSet<Item> Itens { get; set; }
    }
}