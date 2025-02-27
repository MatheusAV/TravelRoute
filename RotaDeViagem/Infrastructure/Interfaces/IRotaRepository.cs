using RotaDeViagem.Domain.Entities;
using RotaDeViagem.Domain.MensageResponse;
using System.Threading.Tasks;

namespace RotaDeViagem.Infrastructure.Interfaces
{
    /// <summary>
    /// Repositório para gerenciamento das rotas
    /// </summary>
    public interface IRotaRepository
    {
        Task<List<Rota>> GetAllRotasAsync();
        Task AddRotaAsync(Rota rota);
        Task<Rota> GetByIdAsync(int id);
        Task UpdateAsync(Rota rota);
        Task DeleteAsync(Rota rota);
        Task AddRangeAsync(List<Rota> rotas);
        IQueryable<Rota> GetQuery();       
    }
}