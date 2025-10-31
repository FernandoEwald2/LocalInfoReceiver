using Aplicacao.Modelos;
using Aplicacao.Modelos.Query;
using Aplicacao.Modelos.Request;
using Aplicacao.Modelos.Response;
using Aplicacao.Services.Interfaces;
using Aplicacao.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AgendaController : ControllerBase
    {
        private IAgendaService _service;

        public AgendaController(IAgendaService agendaService)
        {
            _service = agendaService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<UsuarioResponse>> Criar(AgendaRequest request)
        {
            var dado = await _service.Criar(request);
            return Ok(dado);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Paginacao<AgendaResponse>>> Listar([FromQuery] AgendaQuery query)
        {
            var dados = await _service.Listar(query);

            return Ok(dados);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<AgendaResponse>> Obter(int id)
        {
            var dado = await _service.Obter(id);
            if (dado == null)
                return NotFound();

            return Ok(dado);
        }


        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<UsuarioResponse>> Atualizar(int id, AgendaRequest request)
        {
            var dado = await _service.Atualizar(id, request);
            if (dado == null)
                return NotFound();

            return Ok(dado);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Excluir(int id)
        {
            var removido = await _service.Excluir(id);
            if (!removido)
                return NotFound();

            return Ok(removido);
        }

    }
}
