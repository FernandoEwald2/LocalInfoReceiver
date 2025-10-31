
using Dominio.Entidades.Agendas;
using Infra.Data;
using Infra.Repositories.Agendas;
using LinqKit;

namespace Infra.Repositories
{
    public class AgendaRepository : IAgendaRepository
    {
        private readonly LocalDbContext _context;
        public AgendaRepository(LocalDbContext context)
        {
            _context = context;
        }
        public Task<Agenda> Atualizar(int id, Agenda agenda)
        {
            Agenda? _agendaBanco = _context.Agendas.Find(id);
            if (_agendaBanco == null)
                return Task.FromResult<Agenda>(null!);

            _agendaBanco.AtualizarAgenda(agenda);
            _context.Agendas.Update(_agendaBanco);
            _context.SaveChanges();
            return Task.FromResult(_agendaBanco);

        }

        public Task<Agenda> Criar(Agenda agenda)
        {
            _context.Agendas.Add(agenda);
            _context.SaveChanges();
            return Task.FromResult(agenda);
        }

        public Task<bool> Excluir(int id)
        {
            Agenda? _agendaBanco = _context.Agendas.Find(id);
            if (_agendaBanco == null)
                return Task.FromResult(false);
            _context.Agendas.Remove(_agendaBanco);
            _context.SaveChanges();
            return Task.FromResult(true);
        }

        public Task<Agenda?> Obter(ExpressionStarter<Agenda> filter)
        {
            return Task.FromResult(_context.Agendas.Where(filter).FirstOrDefault());
        }

        public Task<List<Agenda>> Listar(ExpressionStarter<Agenda> filter, int pageNumber, int pageSize)
        {
            return Task.FromResult(_context.Agendas.Where(filter)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .OrderBy(a => a.Nome)
                .ToList());
        }
        public Task<int> QuantidadeCadastrado()
        {
            return Task.FromResult(_context.Agendas.Count());
        }
    }
}
