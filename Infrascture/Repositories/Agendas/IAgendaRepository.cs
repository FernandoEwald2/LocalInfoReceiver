using Dominio.Entidades.Agendas;
using LinqKit;

namespace Infra.Repositories.Agendas
{
    public interface IAgendaRepository
    {
        Task<Agenda> Atualizar(int id, Agenda agenda);
        Task<Agenda> Criar(Agenda agenda);
        Task<bool> Excluir(int id);
        Task<Agenda?> Obter(ExpressionStarter<Agenda> filter);
        Task<List<Agenda>> Listar(ExpressionStarter<Agenda> filter, int pageNumber, int pageSize);
        Task<int> QuantidadeCadastrado();


    }
}
