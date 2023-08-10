using Bogus;
using Desafio.CopaGame.Domain.Entities.Games;
using Desafio.CopaGame.Domain.Entities.Jogos;
using System;
using Xunit;

namespace Desafio.CopaGame.UnitTest.DomainTest
{
    public class PartidaTest
    {
        //AAA Arrange, Act, Assert


        [Fact]
        [Trait("Categoria", "Partida")]
        public void SelecionarVencedores_EliminarJogos_ReturnJogosVencedores()
        {
            //Arrange
            var partida = GerarNewPartida();

            //Act
            partida.OrdenarJogosPorNome();
            var vencedores = partida.SelecionarVencedores();

            Assert.True(vencedores.Count == 2, "Número de vencedores maior do que 2");
        }

        [Fact]
        [Trait("Categoria", "Partida")]
        public void PartidaIsValid_Validar_ReturnTrue()
        {
            //Arrange
            var partida = GerarNewPartida();
        
            //Act
            bool isValid = partida.IsValid();

            Assert.True(isValid, "Partida inválida");
        }

        [Fact]
        [Trait("Categoria", "Partida")]
        public void Partida_ValidarPrimeiroLugar_PrimeiroLugarCorreto()
        {
            //Arrange
            var vencedor = new Jogo(Guid.NewGuid().ToString(), "Primeiro", 10, 2025, "");
            var partida = GerarNewPartidaAddJogoFixo(vencedor);

            //Act
            partida.SelecionarVencedores();

            Assert.True(partida.Primeiro == vencedor, "Partida inválida");
        }


        private Partida GerarNewPartida()
        {
            var jogoFaker = new Faker<Jogo>();
            var jogos = jogoFaker.CustomInstantiator(j => new Jogo
                (
                     id: Guid.NewGuid().ToString(),
                     titulo: j.Lorem.Text(),
                     nota: j.Random.Double(1, 10),
                     ano: j.Random.Number(1999, 2010),
                     urlImagem: j.Image.PicsumUrl()
                )).Generate(8);

            return new Partida(jogos);
        }

        private Partida GerarNewPartidaAddJogoFixo(Jogo jogo)
        {
            var jogoFaker = new Faker<Jogo>();
            var jogos = jogoFaker.CustomInstantiator(j => new Jogo
                (
                     id: Guid.NewGuid().ToString(),
                     titulo: j.Lorem.Text(),
                     nota: j.Random.Double(1, 9),
                     ano: j.Random.Number(1999, 2010),
                     urlImagem: j.Image.PicsumUrl()
                )).Generate(7);
            jogos.Add(jogo);

            return new Partida(jogos);
        }
    }
}
