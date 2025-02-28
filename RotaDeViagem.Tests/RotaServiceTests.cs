using Microsoft.EntityFrameworkCore;
using Moq;
using RotaDeViagem.Application;
using RotaDeViagem.Domain.Entities;
using RotaDeViagem.Infrastructure;
using RotaDeViagem.Infrastructure.Context;
using RotaDeViagem.Infrastructure.Interfaces;

namespace RotaDeViagem.Tests
{
    public class RotaServiceTests
    {
        private readonly Mock<IRotaRepository> _rotaRepositoryMock;
        private readonly RotaService _rotaService;

        public RotaServiceTests()
        {
            _rotaRepositoryMock = new Mock<IRotaRepository>();
            _rotaService = new RotaService(_rotaRepositoryMock.Object);
        }

        [Fact]
        public async Task MelhorRotaAsync_ShouldReturnBestRoute()
        {
            var rotas = new List<Rota>
            {
                new Rota { Origem = "GRU", Destino = "BRC", Custo = 10 },
                new Rota { Origem = "BRC", Destino = "SCL", Custo = 5 },
                new Rota { Origem = "SCL", Destino = "ORL", Custo = 20 },
                new Rota { Origem = "ORL", Destino = "CDG", Custo = 5 }
            };

            _rotaRepositoryMock.Setup(repo => repo.GetAllRotasAsync()).ReturnsAsync(rotas);

            var result = await _rotaService.MelhorRotaAsync("GRU", "CDG");
            Assert.Contains("GRU - BRC - SCL - ORL - CDG ao custo de $40", result);
        }
    }
}