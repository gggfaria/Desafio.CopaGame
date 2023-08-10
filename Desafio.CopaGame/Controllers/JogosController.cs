using Desafio.CopaGame.Domain.Entities.Games;
using Desafio.CopaGame.Service.DTOs;
using Desafio.CopaGame.Service.Interfaces;
using Desafio.CopaGame.Service.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Desafio.CopaGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JogosController : ControllerBase
    {
        readonly IPartidaService _partidaService;

        public JogosController(IPartidaService partidaService)
        {
            _partidaService = partidaService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<JogoViewDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult> Get()
        {
            ServiceResult<IEnumerable<JogoViewDTO>> result =
                await _partidaService.GetJogos();

            return StatusCode(result.StatusCode, result);
        }


        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<PartidaViewDTO>), StatusCodes.Status200OK)]
        public ActionResult PostIniciarPartida([FromBody] ICollection<JogoCreateDTO> jogos)
        {
            var result = _partidaService.IniciarPartidas(jogos);

            return StatusCode(result.StatusCode, result);
        }
    }
}
