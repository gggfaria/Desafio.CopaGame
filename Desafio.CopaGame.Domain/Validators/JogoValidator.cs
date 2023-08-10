using Desafio.CopaGame.Domain.Entities.Games;
using FluentValidation;

namespace Desafio.CopaGame.Domain.Validators
{
    class JogoValidator : AbstractValidator<Jogo>
    {
        public JogoValidator()
        {
            ValidaNota();
            ValidaAno();
        }

        private void ValidaNota()
        {
            RuleFor(p => p.Nota)
                .NotNull()
                .InclusiveBetween(0, 100)
                .NotEmpty();
        }

        private void ValidaAno()
        {
            RuleFor(p => p.Ano)
                .NotNull()
                .GreaterThan(0)
                .NotEmpty();
        }

    }
}
