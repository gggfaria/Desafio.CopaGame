using AutoMapper;
using Desafio.CopaGame.Domain.Entities.Games;
using Desafio.CopaGame.Domain.Entities.Jogos;
using Desafio.CopaGame.Service.DTOs;
using FluentValidation.Results;

namespace Desafio.CopaGame.Service.AutoMapperConfigs
{
    public class MapperConfigs : Profile
    {
        public MapperConfigs()
        {
            CreateMap<Jogo, JogoViewDTO>();
            CreateMap<Partida, PartidaViewDTO>();
         
            CreateMap<JogoCreateDTO, Jogo>();

            CreateMap<ValidationFailure, ValidacaoResponseDTO>();
        }
    }
}
