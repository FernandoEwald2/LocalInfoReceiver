using Aplicacao.Modelos.Query;
using Aplicacao.Modelos.Request;
using Aplicacao.Modelos.Response;
using Aplicacao.Services.Interfaces;
using Aplicacao.Util;
using AutoMapper;
using Dominio.Entidades.Agendas;
using Infra.Repositories.Agendas;
using LinqKit;

namespace Aplicacao.Services
{
    public class AgendaService : IAgendaService
    {
        private readonly IAgendaRepository _agendaRepository;
        private readonly IMapper _mapper;
        public AgendaService(IAgendaRepository agendaRepository , IMapper mapper )
        {
            _agendaRepository = agendaRepository;
            _mapper = mapper;
        }

        public Task<AgendaResponse> Atualizar(int id, AgendaRequest request)
        {
            var _agendaBanco = _agendaRepository.Obter(PredicateBuilder.New<Agenda>(a => a.Id == id)).Result;
            if (_agendaBanco == null)
                throw new Exception("Agenda não encontrada");

            Agenda agenda = _mapper.Map<Agenda>(request);

            var agendaAtualizada = _agendaRepository.Atualizar(id, agenda).Result;

            var agendaResponse = _mapper.Map<AgendaResponse>(agendaAtualizada);
            return Task.FromResult(agendaResponse);
        }

        public async Task<AgendaResponse> Criar(AgendaRequest request)
        {
            request.Nome = request.Nome.ToUpper().Trim();

            bool ValidarAgendaExiste(Agenda agenda)
            {
                ExpressionStarter<Agenda> filter = PredicateBuilder.New<Agenda>(a => true);
                filter.And(a => a.Nome.ToUpper().Equals( agenda.Nome.ToUpper()));
                filter.And(a => a.Email.ToUpper().Equals(agenda.Email.ToUpper()));
                filter.And(a => a.Telefone.Equals(agenda.Telefone));
                Agenda? _agendaBanco = _agendaRepository.Obter(filter).Result;
                if (_agendaBanco != null)
                    return true;
                return false;
            }

            Agenda agenda = _mapper.Map<Agenda>(request);

            if (ValidarAgendaExiste(agenda))
                throw new Exception("Essa agenda já está cadastrada");

            await _agendaRepository.Criar(agenda);

            return _mapper.Map<AgendaResponse>(agenda);

        }

        public async Task<bool> Excluir(int id)
        {
            await _agendaRepository.Excluir(id);
            return true;
        }

        public async Task<AgendaResponse?> Obter(int id)
        {
            ExpressionStarter<Agenda> filter = PredicateBuilder.New<Agenda>(a => true);
            filter.And(a => a.Id == id);
            return _mapper.Map< AgendaResponse >(await _agendaRepository.Obter(filter));
        }

        public async Task<Paginacao<AgendaResponse>> Listar(AgendaQuery agendaQuery)
        {
            ExpressionStarter<Agenda> filter = PredicateBuilder.New<Agenda>(a => true);
            if (!string.IsNullOrEmpty(agendaQuery.nome))
            {
                filter.And(a => a.Nome.ToUpper().Contains(agendaQuery.nome.ToUpper()));
            }
            if (agendaQuery.data_nascimento != null && agendaQuery.data_nascimento != DateTime.MinValue) 
            {
                filter.And(a => a.DataNascimento == agendaQuery.data_nascimento);
            }

            var totalRecords = await _agendaRepository.QuantidadeCadastrado();

            var dados = await _agendaRepository.Listar(filter, agendaQuery.pagina, agendaQuery.quantidade_por_pagina);

            var response = _mapper.Map<List<AgendaResponse>>(dados);

            return new Paginacao<AgendaResponse>(response, totalRecords, agendaQuery.pagina, agendaQuery.quantidade_por_pagina);

        }
    }
}
