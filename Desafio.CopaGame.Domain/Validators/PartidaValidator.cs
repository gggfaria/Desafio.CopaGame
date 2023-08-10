using Desafio.CopaGame.Domain.Entities.Jogos;
using FluentValidation;

namespace Desafio.CopaGame.Domain.Validators
{
    class PartidaValidator : AbstractValidator<Partida>
    {
        public PartidaValidator()
        {
            ValidaJogosPartida();
        }

        private void ValidaJogosPartida()
        {
            RuleFor(p => p.Jogos)
               .Must(x => x.Count == 8).WithMessage("Número de jogos participantes deve ser igual a 8");
        }
    }
}
