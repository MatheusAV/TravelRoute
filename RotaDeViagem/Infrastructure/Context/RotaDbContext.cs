using Microsoft.EntityFrameworkCore;
using RotaDeViagem.Domain.Entities;

namespace RotaDeViagem.Infrastructure.Context
{
    /// <summary>
    /// Contexto do Banco de Dados
    /// </summary>
    public class RotaDbContext : DbContext
    {
        public DbSet<Rota> Rotas { get; set; }
        public RotaDbContext(DbContextOptions<RotaDbContext> options) : base(options) { }
    }
}
