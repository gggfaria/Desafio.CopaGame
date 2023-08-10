using Desafio.CopaGame.Domain.Entities.Games;
using Desafio.CopaGame.Service.DTOs;
using Desafio.CopaGame.Service.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Desafio.CopaGame.Service.Interfaces
{
    public interface IPartidaService
    {
        Task<ServiceResult<IEnumerable<JogoViewDTO>>> GetJogos();
        ServiceResult IniciarPartidas(ICollection<JogoCreateDTO> dto);
    }
}
