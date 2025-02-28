using RotaDeViagem.Domain.Entities;
using RotaDeViagem.Domain.MensageResponse;

namespace RotaDeViagem.Application.Interfaces
{
    /// <summary>
    /// Serviço de rotas que gerencia a lógica de negócios
    /// </summary>
    public interface IRotaService
    {
        Task<ServiceResponse<string>> MelhorRotaAsync(string origem, string destino);        
        Task<ServiceResponse<Rota>> InseriRota(Rota rota);
        Task<ServiceResponse<List<Rota>>> BuscaRotasAsync();
        Task<ServiceResponse<Rota>> BuscaRota(int id);
        Task<ServiceResponse<Rota>> AtualizaRota(Rota rota);
        Task<ServiceResponse<Rota>> DeletaRota(Rota rota);
        Task<ServiceResponse<List<Rota>>> InserirRotasAsync(List<Rota> rotas);
    }
}
