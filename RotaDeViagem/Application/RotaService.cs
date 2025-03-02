﻿using Microsoft.EntityFrameworkCore;
using RotaDeViagem.Application.Interfaces;
using RotaDeViagem.Domain.Entities;
using RotaDeViagem.Domain.MensageResponse;
using RotaDeViagem.Infrastructure.Interfaces;

namespace RotaDeViagem.Application
{
    /// <summary>
    /// Serviço responsável pelo cálculo e gerenciamento de rotas.
    /// </summary>
    public class RotaService : IRotaService
    {
        private readonly IRotaRepository _rotaRepository;

        public RotaService(IRotaRepository rotaRepository)
        {
            _rotaRepository = rotaRepository;
        }

        /// <summary>
        /// Obtém a melhor rota entre uma origem e um destino.
        /// </summary>
        /// <param name="origem">Origem da viagem.</param>
        /// <param name="destino">Destino da viagem.</param>
        /// <returns>Uma string contendo a melhor rota e seu custo total.</returns>
        public async Task<ServiceResponse<string>> MelhorRotaAsync(string origem, string destino)
        {
            var rotas = await _rotaRepository.GetAllRotasAsync();

            if (rotas == null || !rotas.Any())
            {
                return new ServiceResponse<string>
                {
                    Data = null,
                    Success = false,
                    Message = "Nenhuma rota disponível no momento."
                };
            }

            var caminhos = EncontrarCaminhos(rotas, origem, destino);

            if (caminhos == null || !caminhos.Any())
            {
                return new ServiceResponse<string>
                {
                    Data = null,
                    Success = false,
                    Message = "Rota não encontrada."
                };
            }

            var melhoresRotas = caminhos
                .OrderBy(c => c.CustoTotal)
                .Select((c, index) => $"{index + 1}. {string.Join(" - ", c.Caminho)} ao custo de ${c.CustoTotal}")
                .ToList();

            return new ServiceResponse<string>
            {
                Data = string.Join("\n", melhoresRotas),
                Success = true,
                Message = "Rotas encontradas com sucesso."
            };
        }

        private List<(List<string> Caminho, decimal CustoTotal)> EncontrarCaminhos(List<Rota> rotas, string origem, string destino)
        {
            var caminhos = new List<(List<string>, decimal)>();
            Percorrer(rotas, origem, destino, new List<string>(), 0, caminhos);
            return caminhos;
        }

        private void Percorrer(List<Rota> rotas, string atual, string destino, List<string> caminhoAtual, decimal custoAtual, List<(List<string>, decimal)> caminhos)
        {
            caminhoAtual.Add(atual);
            if (atual == destino)
            {
                caminhos.Add((new List<string>(caminhoAtual), custoAtual));
            }
            else
            {
                foreach (var rota in rotas.Where(r => r.Origem == atual))
                {
                    if (!caminhoAtual.Contains(rota.Destino))
                    {
                        Percorrer(rotas, rota.Destino, destino, caminhoAtual, custoAtual + rota.Custo, caminhos);
                    }
                }
            }
            caminhoAtual.RemoveAt(caminhoAtual.Count - 1);
        }

        /// <summary>
        /// Adiciona uma nova rota ao sistema.
        /// </summary>
        /// <param name="rota"> rota.</param>
        public async Task<ServiceResponse<Rota>> InseriRota(Rota rota)
        {
            try
            {
                var rotaExistente = await _rotaRepository.GetQuery()
                    .FirstOrDefaultAsync(r => r.Origem == rota.Origem && r.Destino == rota.Destino);
                if (rotaExistente != null)
                {
                    return new ServiceResponse<Rota>
                    {
                        Data = null,
                        Success = false,
                        Message = "A rota já existe no banco de dados."
                    };
                }

                await _rotaRepository.AddRotaAsync(rota);
                return new ServiceResponse<Rota>
                {
                    Data = rota,
                    Success = true,
                    Message = "Rota inserida com sucesso."
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Rota>
                {
                    Data = null,
                    Success = false,
                    Message = $"Erro ao inserir a rota: {ex.Message}"
                };
            }
        }

        public async Task<ServiceResponse<List<Rota>>> BuscaRotasAsync()
        {
            try
            {
                var listaRotas = await _rotaRepository.GetAllRotasAsync();
                return new ServiceResponse<List<Rota>> { Data = listaRotas.ToList(), Success = true, Message = "Rotas encontradas com sucesso." };
            }
            catch (Exception)
            {

                return new ServiceResponse<List<Rota>> { Data = null, Success = false, Message = "Erro ao buscar rotas." };
            }
        }


        public async Task<ServiceResponse<Rota>> BuscaRota(int id)
        {
            try
            {
                var rota = await _rotaRepository.GetByIdAsync(id);
                if (rota == null)
                {
                    return new ServiceResponse<Rota> { Data = null, Success = false, Message = "Rota não encontrada." };
                }
                return new ServiceResponse<Rota> { Data = rota, Success = true, Message = "Rota encontrada com sucesso." };
            }
            catch (Exception)
            {

                return new ServiceResponse<Rota> { Data = null, Success = false, Message = "Erro ao buscar a rota." };
            }
        }

        public async Task<ServiceResponse<Rota>> AtualizaRota(Rota rota)
        {
            try
            {
                var rotaExistente = await _rotaRepository.GetByIdAsync(rota.Id);
                if (rotaExistente == null)
                {
                    return new ServiceResponse<Rota> { Data = null, Success = false, Message = "Rota não encontrada." };
                }
                
                rotaExistente.Origem = !string.IsNullOrWhiteSpace(rota.Origem) ? rota.Origem : rotaExistente.Origem;
                rotaExistente.Destino = !string.IsNullOrWhiteSpace(rota.Destino) ? rota.Destino : rotaExistente.Destino;
                rotaExistente.Custo = rota.Custo > 0 ? rota.Custo : rotaExistente.Custo;

                await _rotaRepository.UpdateAsync(rotaExistente);

                return new ServiceResponse<Rota>
                {
                    Data = rotaExistente,
                    Success = true,
                    Message = "Rota atualizada com sucesso."
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Rota>
                {
                    Data = null,
                    Success = false,
                    Message = $"Erro ao atualizar a rota: {ex.Message}"
                };
            }
        }


        public async Task<ServiceResponse<Rota>> DeletaRota(Rota rota)
        {
            try
            {
                await _rotaRepository.DeleteAsync(rota);
                return new ServiceResponse<Rota> { Data = null, Success = true, Message = "Rota deletada com sucesso." };
            }
            catch (Exception)
            {

                return new ServiceResponse<Rota> { Data = null, Success = false, Message = "Erro ao deletar a rota." };
            }
        }

        public async Task<ServiceResponse<List<Rota>>> InserirRotasAsync(List<Rota> rotas)
        {
            var rotasInseridas = new List<Rota>();
            try
            {
                foreach (var rota in rotas)
                {
                    var rotaExistente = await _rotaRepository.GetQuery()
                        .FirstOrDefaultAsync(r => r.Origem == rota.Origem && r.Destino == rota.Destino);

                    if (rotaExistente == null)
                    {
                        rotasInseridas.Add(rota);
                    }
                }

                if (rotasInseridas.Any())
                {
                    await _rotaRepository.AddRangeAsync(rotasInseridas);

                    return new ServiceResponse<List<Rota>>
                    {
                        Data = rotasInseridas,
                        Success = true,
                        Message = "Rotas inseridas com sucesso."
                    };
                }
                else
                {
                    return new ServiceResponse<List<Rota>>
                    {
                        Data = null,
                        Success = false,
                        Message = "Nenhuma nova rota foi inserida. Todas as rotas já existiam no banco."
                    };
                }
            }
            catch (Exception ex)
            {
                return new ServiceResponse<List<Rota>>
                {
                    Data = null,
                    Success = false,
                    Message = $"Erro ao inserir rotas: {ex.Message}"
                };
            }
        }
    }
}
