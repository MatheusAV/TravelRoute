# Guia de Execução da Aplicação

## Como Executar a Aplicação

1. **Clonar o Repositório**
   ```sh
   git clone <URL_DO_REPOSITORIO>
   cd TravelRoute
   ```

2. **Configurar o Banco de Dados**
   - A aplicação utiliza SQL Server. Certifique-se de que um servidor SQL está disponível.
   - Ajuste a string de conexão em `appsettings.json`:
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=RotasDB;Trusted_Connection=True;"
     }
     ```

3. **Aplicar Migrações**
   ```sh
   dotnet ef database update
   ```

4. **Compilar e Executar a API**
   ```sh
   dotnet run --project API/RotaDeViagem.csproj
   ```
   - A API estará disponível em `http://localhost:5000` ou `https://localhost:5001`.

5. **Acessar a Documentação Swagger**
   - Acesse `http://localhost:5000/swagger` para testar os endpoints.

---

## Estrutura do Projeto

```
TravelRoute
│   appsettings.json
│   Program.cs
│
├───API
│   ├───Application
│   │   ├───Interfaces
│   │   │   ├── IotaService.cs
│   │   ├───Services
│   │       ├── RotaService.cs
│   │
│   ├───Controllers
│   │   ├── RotaController.cs
│
├───Domain
│   ├───Entities
│   │   ├── Rota.cs
│   ├───MessageResponse
│   │   ├── ServiceResponse.cs
│
├───Infrastructure
│   ├───Context
│   │   ├── RotaDbContext.cs
│   ├───Interfaces
│   │   ├── RotaRepository.cs
│
├───Migrations
│   ├── 20250227180503_InitialCreate.cs
│   ├── RotaDbContextModelSnapshot.cs
│
└───Test
    ├───RotaDeViagem.Tests
    │   ├── RotaRepositoryTests.cs
    │   ├── RotaServiceTests.cs
```

---

## Decisões de Design

1. **Arquitetura em Camadas**
   - **Application:** Contém a lógica de negócio e os serviços.
   - **Domain:** Contém as entidades e objetos de valor.
   - **Infrastructure:** Responsável pela comunicação com o banco de dados.
   - **Controllers:** Exposição dos endpoints da API.

2. **Uso do Entity Framework Core**
   - Utilizado para persistência de dados no SQL Server.
   - Implementação de migrações para controle de versão do banco.

3. **Swagger para Documentação**
   - Implementado para facilitar o consumo da API.

4. **Testes Unitários**
   - Criados para validar a funcionalidade dos repositórios e serviços.
   - Utilização do xUnit para automação de testes.

---

## Endpoints Disponíveis

- `POST /api/rotas` → Adiciona uma nova rota.
- `GET /api/rotas` → Lista todas as rotas disponíveis.
- `PUT /api/rotas` → Atualiza uma rota existente.
- `GET /api/rotas/melhor-rota` → Busca a rota mais barata entre dois pontos.
- `GET /api/rotas/{id}` → Busca detalhes de uma rota pelo ID.
- `DELETE /api/rotas/{id}` → Remove uma rota específica.
- `POST /api/rotas/lote` → Adiciona múltiplas rotas ao sistema.

---
