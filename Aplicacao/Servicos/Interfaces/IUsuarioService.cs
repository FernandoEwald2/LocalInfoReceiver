using Aplicacao.Modelos;
using Aplicacao.Modelos.Query;
using Aplicacao.Util;

namespace Aplicacao.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<Paginacao<UsuarioResponse>> Listar(UsuarioQuery? query);
        Task<UsuarioResponse> Obter(int id);
        Task<UsuarioResponse> Criar(UsuarioRequest usuario);
        Task<UsuarioResponse> Atualizar(int id, UsuarioRequest usuario);
        Task<bool> Excluir(int id);
    }
}
