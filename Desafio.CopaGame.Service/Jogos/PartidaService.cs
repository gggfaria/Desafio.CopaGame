using AutoMapper;
using Desafio.CopaGame.Domain.Entities.Games;
using Desafio.CopaGame.Domain.Entities.Jogos;
using Desafio.CopaGame.Domain.Interfaces;
using Desafio.CopaGame.Service.DTOs;
using Desafio.CopaGame.Service.Interfaces;
using Desafio.CopaGame.Service.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Desafio.CopaGame.Service.Jogos
{
    public class PartidaService : IPartidaService
    {
        private readonly IJogoRepository _jogoRepository;
        private readonly IMapper _mapper;
        public PartidaService(IJogoRepository jogoRepository, IMapper mapper)
        {
            _jogoRepository = jogoRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<IEnumerable<JogoViewDTO>>> GetJogos()
        {
            IEnumerable<Jogo> jogos = await _jogoRepository.GetAll();
            var dto = _mapper.Map<IEnumerable<JogoViewDTO>>(jogos);

            return ServiceResultFactory<IEnumerable<JogoViewDTO>>.Ok(dto);
        }

        public ServiceResult IniciarPartidas(ICollection<JogoCreateDTO> dto)
        {
            var jogos = _mapper.Map<ICollection<Jogo>>(dto);
            Partida partida = new Partida(jogos);

            if (!partida.IsValid())
                return ServiceResultFactory.BadRequest(_mapper.Map<ICollection<ValidacaoResponseDTO>>(partida.GetInvalidDataError()), "Request inválido");

            partida.OrdenarJogosPorNome();
            partida.SelecionarVencedores();

            var partidaDTO = _mapper.Map<PartidaViewDTO>(partida);

            return ServiceResultFactory<PartidaViewDTO>.Ok(partidaDTO);
        }
    }
}
