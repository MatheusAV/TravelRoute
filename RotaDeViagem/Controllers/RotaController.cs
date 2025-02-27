using Microsoft.AspNetCore.Mvc;
using RotaDeViagem.Application.Interfaces;
using RotaDeViagem.Domain.Entities;

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
    /// Adiciona uma nova rota ao sistema
    /// </summary>
    /// <param name="rota">Dados da rota a ser adicionada</param>
    /// <returns>Mensagem de sucesso</returns>
    [HttpPost]
    public async Task<IActionResult> AdicionarRota([FromBody] Rota rota)
    {
        var response = await _rotaService.InseriRota(rota);
        return response.Success ? Ok(response.Message) : BadRequest(response.Message);
    }

    /// <summary>
    /// Busca a melhor rota entre dois pontos
    /// </summary>
    /// <param name="origem">Ponto de origem</param>
    /// <param name="destino">Ponto de destino</param>
    /// <returns>Melhor rota encontrada</returns>
    [HttpGet("melhor-rota")]
    public async Task<IActionResult> MelhorRota([FromQuery] string origem, [FromQuery] string destino)
    {
        var resultado = await _rotaService.MelhorRotaAsync(origem, destino);
        return Ok(resultado);
    }

    /// <summary>
    /// Busca todas as rotas disponíveis
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> BuscarRotas()
    {
        var response = await _rotaService.BuscaRotasAsync();
        return response.Success ? Ok(response.Data) : BadRequest(response.Message);
    }

    /// <summary>
    /// Busca uma rota específica pelo ID
    /// </summary>
    /// <param name="id">ID da rota</param>
    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarRota(int id)
    {
        var response = await _rotaService.BuscaRota(id);
        return response.Success ? Ok(response.Data) : NotFound(response.Message);
    }

    /// <summary>
    /// Atualiza os dados de uma rota existente
    /// </summary>
    /// <param name="rota">Dados da rota a serem atualizados</param>
    [HttpPut]
    public async Task<IActionResult> AtualizarRota([FromBody] Rota rota)
    {
        var response = await _rotaService.AtualizaRota(rota);
        return response.Success ? Ok(response.Message) : BadRequest(response.Message);
    }

    /// <summary>
    /// Remove uma rota do sistema
    /// </summary>
    /// <param name="id">ID da rota a ser removida</param>
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
    [HttpPost("lote")]
    public async Task<IActionResult> InserirRotas([FromBody] List<Rota> rotas)
    {
        var response = await _rotaService.InserirRotasAsync(rotas);
        return response.Success ? Ok(response.Message) : BadRequest(response.Message);
    }
}
