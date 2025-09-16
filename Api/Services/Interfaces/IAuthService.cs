using Api.Services.Models;

namespace Api.Services.Interfaces
{
    public interface IAuthService
    {
        Autenticacao Autenticar(Auth auth);
    }
}
