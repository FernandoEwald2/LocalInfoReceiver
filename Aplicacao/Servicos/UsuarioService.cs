using Aplicacao.Exceptions;
using Aplicacao.Modelos;
using Aplicacao.Modelos.Query;
using Aplicacao.Services.Interfaces;
using Aplicacao.Util;
using AutoMapper;
using Dominio.Entidades.Usuarios;
using Infra.Repositories;
using LinqKit;

namespace Aplicacao.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        public UsuarioService(IMapper mapper, IUsuarioRepository usuarioRepository)
        {
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
        }
        public async Task<UsuarioResponse> Atualizar(int id, UsuarioRequest usuarioRequest)
        {
            ExpressionStarter<Usuario> filter = PredicateBuilder.New<Usuario>(a => true);

            filter.And(a => a.Id == id);
            Usuario? _usuarioBanco = await _usuarioRepository.Obter(filter);
            if (_usuarioBanco == null)
                throw new LocalException(ExceptionEnum.NotFound, "Usuário não encontrado");

            Usuario ususarioReq = _mapper.Map<Usuario>(usuarioRequest);

            if (!string.IsNullOrEmpty(usuarioRequest.senha))
            {
                byte[] hash;
                byte[] salt;
                Criptografia.CriarHashSalt(usuarioRequest.senha, out hash, out salt);


                ususarioReq.SetSenha(hash, salt);
            }

            Usuario usuarioAttualizado = _usuarioRepository.Atualizar(id, ususarioReq).Result;

            return _mapper.Map<UsuarioResponse>(usuarioAttualizado);

        }
        public async Task<UsuarioResponse> Criar(UsuarioRequest usuarioRequest)
        {
            usuarioRequest.nome = usuarioRequest.nome.ToUpper().Trim();
            bool ValidarLoginUsuarioExiste(string login)
            {
                ExpressionStarter<Usuario> filter = PredicateBuilder.New<Usuario>(a => true);
                filter.And(a => a.Login == login);
                Usuario? _usuarioBanco = _usuarioRepository.Obter(filter).Result;
                if (_usuarioBanco != null)
                    return true;
                return false;
            }
            if (ValidarLoginUsuarioExiste(usuarioRequest.login))
                throw new Exception("Esse login não esta disponível");

            byte[] hash;
            byte[] salt;
            Criptografia.CriarHashSalt(usuarioRequest.senha, out hash, out salt);

            //Usuario usuario = new Usuario(usuarioRequest.nome.ToUpper(), usuarioRequest.login, hash, salt, usuarioRequest.cep
            //    , usuarioRequest.logradouro, usuarioRequest.bairro, usuarioRequest.numero
            //    , usuarioRequest.cidade, usuarioRequest.estado, usuarioRequest.perfil
            //    , usuarioRequest.cpf, usuarioRequest.telefone, usuarioRequest.email);

            Usuario usuario = _mapper.Map<Usuario>(usuarioRequest);
            usuario.SetSenha(hash, salt);
            await _usuarioRepository.Criar(usuario);

            return _mapper.Map<UsuarioResponse>(usuario);
        }
        public async Task<bool> Excluir(int id)
        {
            await _usuarioRepository.Excluir(id);
            return true;
        }
        public async Task<UsuarioResponse> Obter(int id)
        {
            ExpressionStarter<Usuario> filter = PredicateBuilder.New<Usuario>(a => true);

            filter.And(a => a.Id == id);
            Usuario? _usuarioBanco = await _usuarioRepository.Obter(filter);

            return _mapper.Map<UsuarioResponse>(_usuarioBanco);
        }
        public async Task<Paginacao<UsuarioResponse>> Listar(UsuarioQuery? query)
        {

            ExpressionStarter<Usuario> filter = PredicateBuilder.New<Usuario>(a => true);

            if (!string.IsNullOrEmpty(query.busca))
                filter.And(a => a.Nome.ToUpper().Contains(query.busca.ToUpper())).Or(a => a.Login.ToUpper().Contains(query.busca.ToUpper()));

            List<Usuario> listaUsuarios = await _usuarioRepository.Listar(filter, query.pagina, query.quantidade_por_pagina);

            var response = _mapper.Map<List<UsuarioResponse>>(listaUsuarios);

            var totalRecords = await _usuarioRepository.QuantidadeCadastrado();

            return new Paginacao<UsuarioResponse>(response, totalRecords, query.pagina, query.quantidade_por_pagina);
        }

    }
}
