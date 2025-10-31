using Dominio.Entidades.Usuarios;
using Infra.Data;
using LinqKit;

namespace Infra.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly LocalDbContext _context;
        public UsuarioRepository(LocalDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> Atualizar(int id, Usuario usuario)
        {
            Usuario? _usuarioBanco = await _context.Usuarios.FindAsync(id);

            if (_usuarioBanco == null)
                return null;

            _usuarioBanco.AtualizarUsuario(usuario);
            _usuarioBanco.SetSenha(usuario.SenhaHash, usuario.SenhaSalt);
            _context.Usuarios.Update(_usuarioBanco);
            await _context.SaveChangesAsync();
            return _usuarioBanco;
        }

        public Task<Usuario> Criar(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return Task.FromResult(usuario);
        }

        public Task<bool> Excluir(int id)
        {
            Usuario? _usuarioBanco = _context.Usuarios.Find(id);
            if (_usuarioBanco == null)
                return Task.FromResult(false);
            _context.Usuarios.Remove(_usuarioBanco);
            _context.SaveChanges();
            return Task.FromResult(true);
        }

        public Task<Usuario?> Obter(ExpressionStarter<Usuario> filter)
        {
            return Task.FromResult(_context.Usuarios.Where(filter).FirstOrDefault());
        }

        public Task<List<Usuario>> Listar(ExpressionStarter<Usuario> filter, int pageNumber, int pageSize)
        {
            return Task.FromResult(_context.Usuarios.Where(filter)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .OrderBy(u => u.Nome)
                .ToList());
        }

        public Task<int> QuantidadeCadastrado()
        {
            return Task.FromResult(_context.Usuarios.Count());
        }
    }
}
