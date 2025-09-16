using Api.Domain.Models.Request;
using Api.Domain.Models.Response;
using Api.Util;

namespace Api.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<Paginacao<UsuarioResponse>> ObterTodos(int pageNumber, int pageSize);
        Task<UsuarioResponse> ObterPorId(int id);
        Task<UsuarioResponse> Criar(UsuarioRequest usuario);
        Task<UsuarioResponse> Atualizar(int id, UsuarioRequest usuario);
        Task<bool> Excluir(int id);
    }
}
