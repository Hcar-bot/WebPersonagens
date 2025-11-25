using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebPersonagens.Models;

namespace WebPersonagens.Data
{
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