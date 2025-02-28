using Microsoft.AspNetCore.Mvc;
using RotaDeViagem.Application.Interfaces;
using RotaDeViagem.Domain.Entities;
using RotaDeViagem.Domain.MensageResponse;
using Swashbuckle.AspNetCore.Annotations;

namespace RotaDeViagem.Controllers;

/// <summary>
/// Controlador da API de Rotas
/// </summary>
[ApiController]
[Route("api/rotas")]
public class RotaController : ControllerBase
{
    private readonly IRotaService _rotaService;

    public RotaController(IRotaService rotaService)
    {
        _rotaService = rotaService;
    }

    /// <summary>
    /// Insere uma nova rota no sistema.
    /// </summary>
    /// <param name="rota">Dados da nova rota.</param>
    /// <response code="201">Rota criada com sucesso.</response>
    /// <response code="400">Erro ao inserir a rota.</response>
    [SwaggerResponse(201, "Rota inserida com sucesso", typeof(ServiceResponse<Rota>))]
    [SwaggerResponse(400, "Erro ao inserir a rota")]
    [HttpPost]
    public async Task<IActionResult> AdicionarRota([FromBody] Rota rota)
    {
        var response = await _rotaService.InseriRota(rota);
        return response.Success ? Ok(response.Message) : BadRequest(response.Message);
    }

    /// <summary>
    /// Busca a rota mais barata entre duas localidades.
    /// </summary>
    /// <param name="origem">A localidade de origem da rota.</param>
    /// <param name="destino">A localidade de destino da rota.</param>
    /// <response code="200">Rota mais barata encontrada com sucesso.</response>
    /// <response code="400">Erro ao buscar a rota mais barata.</response>
    [HttpGet("BuscaMelhorRota")]
    [SwaggerResponse(200, "Rota mais barata encontrada com sucesso", typeof(ServiceResponse<string>))]
    [SwaggerResponse(400, "Erro ao buscar a rota mais barata")]
    [HttpGet("melhor-rota")]
    public async Task<IActionResult> MelhorRota([FromQuery] string origem, [FromQuery] string destino)
    {
        var resultado = await _rotaService.MelhorRotaAsync(origem, destino);
        return Ok(resultado);
    }
    /// <summary>
    /// Lista todas as rotas disponíveis.
    /// </summary>
    /// <response code="200">Retorna a lista de rotas.</response>
    /// <response code="404">Não foi possível encontrar rotas.</response>
    [SwaggerResponse(200, "Lista de rotas encontradas", typeof(ServiceResponse<List<Rota>>))]
    [SwaggerResponse(404, "Rotas não encontradas")]
    [HttpGet]
    public async Task<IActionResult> BuscarRotas()
    {
        var response = await _rotaService.BuscaRotasAsync();
        return response.Success ? Ok(response.Data) : BadRequest(response.Message);
    }

    /// <summary>
    /// Busca detalhes de uma rota específica pelo ID.
    /// </summary>
    /// <param name="id">O ID da rota.</param>
    /// <response code="200">Retorna os detalhes da rota.</response>
    /// <response code="404">Rota não encontrada.</response>
    [SwaggerResponse(200, "Detalhes da rota", typeof(ServiceResponse<Rota>))]
    [SwaggerResponse(404, "Rota não encontrada")]
    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarRota(int id)
    {
        var response = await _rotaService.BuscaRota(id);
        return response.Success ? Ok(response.Data) : NotFound(response.Message);
    }

    /// <summary>
    /// Atualiza os dados de uma rota existente.
    /// </summary>
    /// <param name="rota">Dados atualizados da rota.</param>
    /// <response code="200">Rota atualizada com sucesso.</response>
    /// <response code="404">Rota não encontrada.</response>
    [SwaggerResponse(200, "Rota atualizada com sucesso", typeof(ServiceResponse<Rota>))]
    [SwaggerResponse(404, "Rota não encontrada")]
    [HttpPut]
    public async Task<IActionResult> AtualizarRota([FromBody] Rota rota)
    {
        var response = await _rotaService.AtualizaRota(rota);
        return response.Success ? Ok(response.Message) : BadRequest(response.Message);
    }

    /// <summary>
    /// Deleta uma rota específica pelo ID.
    /// </summary>
    /// <param name="id">O ID da rota a ser deletada.</param>
    /// <response code="200">Rota deletada com sucesso.</response>
    /// <response code="400">Erro ao deletar a rota.</response>
    [SwaggerResponse(200, "Rota deletada com sucesso", typeof(ServiceResponse<Rota>))]
    [SwaggerResponse(400, "Erro ao deletar a rota")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletarRota(int id)
    {
        var rota = await _rotaService.BuscaRota(id);
        if (!rota.Success) return NotFound(rota.Message);

        var response = await _rotaService.DeletaRota(rota.Data);
        return response.Success ? Ok(response.Message) : BadRequest(response.Message);
    }

    /// <summary>
    /// Adiciona múltiplas rotas ao sistema
    /// </summary>
    /// <param name="rotas">Lista de rotas a serem inseridas</param>
    [SwaggerResponse(201, "Rotas inseridas com sucesso", typeof(ServiceResponse<List<Rota>>))]
    [SwaggerResponse(400, "Erro ao inserir rotas")]
    [HttpPost("lote")]
    public async Task<IActionResult> InserirRotas([FromBody] List<Rota> rotas)
    {
        var response = await _rotaService.InserirRotasAsync(rotas);
        return response.Success ? Ok(response.Message) : BadRequest(response.Message);
    }
}
