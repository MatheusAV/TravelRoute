using Microsoft.EntityFrameworkCore;
using RotaDeViagem.Domain.Entities;
using RotaDeViagem.Infrastructure;
using RotaDeViagem.Infrastructure.Context;

namespace RotaDeViagem.Tests
{
    public class RotaRepositoryTests
    {
        private readonly DbContextOptions<RotaDbContext> _options;

        public RotaRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<RotaDbContext>()
                .UseInMemoryDatabase(databaseName: "RotaDbTest")
                .Options;
        }

        [Fact]
        public async Task AddRotaAsync_ShouldAddRota()
        {
            using var context = new RotaDbContext(_options);
            var repository = new RotaRepository(context);

            var rota = new Rota { Origem = "GRU", Destino = "BRC", Custo = 10 };
            await repository.AddRotaAsync(rota);

            var rotas = await repository.GetAllRotasAsync();
            Assert.Single(rotas);
        }
    }
}