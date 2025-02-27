using Microsoft.EntityFrameworkCore;
using RotaDeViagem.Domain.Entities;
using RotaDeViagem.Infrastructure.Context;
using RotaDeViagem.Infrastructure.Interfaces;

namespace RotaDeViagem.Infrastructure
{  

    /// <summary>
    /// Repositório responsável pelo gerenciamento de rotas no banco de dados.
    /// </summary>
    public class RotaRepository : IRotaRepository
    {
        private readonly RotaDbContext _context;

        public RotaRepository(RotaDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém todas as rotas disponíveis no banco de dados.
        /// </summary>
        public async Task<List<Rota>> GetAllRotasAsync() => await _context.Rotas.ToListAsync();

        /// <summary>
        /// Adiciona uma nova rota ao banco de dados.
        /// </summary>
        public async Task AddRotaAsync(Rota rota)
        {
            _context.Rotas.Add(rota);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Obtém uma rota pelo seu identificador único.
        /// </summary>
        public async Task<Rota> GetByIdAsync(int id) => await _context.Rotas.FindAsync(id);

        /// <summary>
        /// Atualiza uma rota existente no banco de dados.
        /// </summary>
        public async Task UpdateAsync(Rota rota)
        {
            _context.Rotas.Update(rota);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Remove uma rota do banco de dados.
        /// </summary>
        public async Task DeleteAsync(Rota rota)
        {
            _context.Rotas.Remove(rota);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Insere uma lista de rotas no banco de dados.
        /// </summary>
        public async Task AddRangeAsync(List<Rota> rotas)
        {
            await _context.Rotas.AddRangeAsync(rotas);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Obtém uma query para operações avançadas sobre as rotas.
        /// </summary>
        public IQueryable<Rota> GetQuery() => _context.Rotas.AsQueryable();
    }

}