using Aplicacao.Modelos;
using Aplicacao.Modelos.Request;
using Aplicacao.Modelos.Response;
using AutoMapper;
using Dominio.Entidades.Agendas;
using Dominio.Entidades.Usuarios;


namespace Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UsuarioRequest, Usuario>();
            CreateMap<Usuario, UsuarioResponse>();
            CreateMap<AgendaRequest, Agenda>();
            CreateMap<Agenda, AgendaResponse>();
        }
    }
}
