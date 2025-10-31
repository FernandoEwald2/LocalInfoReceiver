

using LinqKit;
using Dominio.Entidades.Usuarios;

namespace Infra.Repositories
{
    public interface IUsuarioRepository
    {
        Task<List<Usuario>> Listar(ExpressionStarter<Usuario> filter, int pageNumber, int pageSize);
        Task<Usuario?> Obter(ExpressionStarter<Usuario> filter);
        Task<Usuario> Criar(Usuario usuario);
        Task<Usuario> Atualizar(int id, Usuario usuario);
        Task<bool> Excluir(int id);
        Task<int> QuantidadeCadastrado();
    }
}
