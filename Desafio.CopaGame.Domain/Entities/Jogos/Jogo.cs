using Desafio.CopaGame.Domain.Validators;
using FluentValidation.Results;

namespace Desafio.CopaGame.Domain.Entities.Games
{
    public class Jogo : EntityBase
    {
        public Jogo(string id, string titulo, double nota, int ano, string urlImagem)
        {
            Id = id;
            Titulo = titulo;
            Nota = nota;
            Ano = ano;
            UrlImagem = urlImagem;
        }

        public string Id { get; private set; }
        public string Titulo { get; private set; }
        public double Nota { get; private set; }
        public int Ano { get; private set; }
        public string UrlImagem { get; private set; }

        public override bool IsValid()
        {
            var validador = new JogoValidator();
            ValidationResult validationResult = validador.Validate(this);

            if (!validationResult.IsValid)
                ValidationResult = validationResult;

            return ValidationResult.IsValid; 
        }
    }
}
