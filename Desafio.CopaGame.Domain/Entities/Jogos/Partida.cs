using Desafio.CopaGame.Domain.Entities.Games;
using Desafio.CopaGame.Domain.Extensions;
using Desafio.CopaGame.Domain.Validators;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace Desafio.CopaGame.Domain.Entities.Jogos
{
    public class Partida : EntityBase
    {
        public ICollection<Jogo> Jogos { get; private set; }
        public Jogo Primeiro { get; private set; }
        public Jogo Segundo { get; private set; }

        public Partida(ICollection<Jogo> jogos)
        {
            Jogos = jogos;
        }

        public void OrdenarJogosPorNome()
        {
            Jogos = Jogos.OrderBy(p => p.Titulo).ToList();
        }

        private ICollection<Jogo> InciarRodada(ICollection<Jogo> jogos)
        {
            var novaPartida = new List<Jogo>();
            var ultimoIndice = jogos.Count - 1;
            for (int i = 0; i < jogos.Count / 2; i++)
            {
                var primeiroJogo = jogos.ElementAt(i);
                var segundoJogo = jogos.ElementAt(ultimoIndice - i);
                novaPartida.Add(SelecionarVencedorPartida(primeiroJogo, segundoJogo));
            }

            return novaPartida;
        }

        private Jogo SelecionarVencedorPartida(Jogo primeiroJogo, Jogo segundoJogo)
        {
            if (primeiroJogo.Nota > segundoJogo.Nota)
                return primeiroJogo;
            else if (segundoJogo.Nota > primeiroJogo.Nota)
                return segundoJogo;
            else
                return DefinirDesempate(primeiroJogo, segundoJogo);
        }

        public ICollection<Jogo> SelecionarVencedores()
        {
            ICollection<Jogo> vencedores = new List<Jogo>(Jogos);
            while (vencedores.Count > 2)
                vencedores = InciarRodada(vencedores);

            PreencherCampeoes(vencedores);
            return vencedores;
        }

        private void PreencherCampeoes(ICollection<Jogo> vencedores)
        {
            Primeiro = SelecionarVencedorPartida(vencedores.First(), vencedores.Last());
            Segundo = vencedores.Single(p => p != Primeiro);
        }

        private Jogo DefinirDesempate(Jogo primeiroJogo, Jogo segundoJogo)
        {
            if (primeiroJogo.Ano > segundoJogo.Ano)
                return primeiroJogo;
            if (segundoJogo.Ano > primeiroJogo.Ano)
                return segundoJogo;
            else
                throw new System.Exception("Empate sem regra definida");
        }

        public override bool IsValid()
        {
            var validador = new PartidaValidator();
            ValidaJogos();

            ValidationResult validationResult = validador.Validate(this);

            if (!validationResult.IsValid)
                ValidationResult = validationResult;

            return ValidationResult.IsValid;
        }


        private void ValidaJogos()
        {
            foreach (var item in Jogos)
            {
                if (!item.IsValid())
                    ValidationResult.AddErrors(item.ValidationResult);
            }
        }
    }
}
