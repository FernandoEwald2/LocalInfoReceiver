using Aplicacao.Modelos.Query;
using Aplicacao.Modelos.Request;
using Aplicacao.Modelos.Response;
using Aplicacao.Util;

namespace Aplicacao.Services.Interfaces
{
    public interface IAgendaService
    {
        Task<Paginacao<AgendaResponse>> Listar(AgendaQuery agendaQuery);
        Task<AgendaResponse?> Obter(int id);
        Task<AgendaResponse> Criar(AgendaRequest agenda);
        Task<AgendaResponse> Atualizar(int id, AgendaRequest agenda);
        Task<bool> Excluir(int id);
    }
}
