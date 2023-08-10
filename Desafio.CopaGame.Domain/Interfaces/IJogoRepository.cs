using Desafio.CopaGame.Domain.Entities.Games;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Desafio.CopaGame.Domain.Interfaces
{
    public interface IJogoRepository
    {
        Task<IEnumerable<Jogo>> GetAll();
    }
}
