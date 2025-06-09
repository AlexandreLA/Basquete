using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TemporadaDeBasquete.Controllers;
using TemporadaDeBasquete.Models;
using WebAPI.Models;
using Xunit;

namespace TemporadaDeBasquete.Tests
{
    public class RegistroJogoControllerTests
    {
        private static RegistroJogo InvokeCalcula(RegistroJogoController controller, RegistroJogo registro)
        {
            var method = typeof(RegistroJogoController).GetMethod("CalculaRegistrosTemporada", BindingFlags.NonPublic | BindingFlags.Instance);
            return (RegistroJogo)method.Invoke(controller, new object[] { registro });
        }

        private BasqueteContext CreateContext(string name)
        {
            var options = new DbContextOptionsBuilder<BasqueteContext>()
                .UseInMemoryDatabase(name)
                .Options;
            return new BasqueteContext(options);
        }

        [Fact]
        public void PrimeiroJogoDefineMinimosMaximosEContadores()
        {
            var context = CreateContext(nameof(PrimeiroJogoDefineMinimosMaximosEContadores));
            var temporada = new Temporada { Id = Guid.NewGuid(), TemporadaDescricao = "2024" };
            context.Temporada.Add(temporada);
            context.SaveChanges();
            var controller = new RegistroJogoController(context);
            var registro = new RegistroJogo { Placar = 30, TemporadaId = temporada.Id };

            var resultado = InvokeCalcula(controller, registro);

            Assert.Equal(1, resultado.NumeroJogo);
            Assert.Equal(30, resultado.MinimoTemporada);
            Assert.Equal(30, resultado.MaximoTemporada);
            Assert.Equal(1, resultado.QuebraRecordeMaximo);
            Assert.Equal(1, resultado.QuebraRecordeMinimo);
        }

        [Fact]
        public void NovoPlacarMenorQueMinimoAtualAtualizaMinimo()
        {
            var context = CreateContext(nameof(NovoPlacarMenorQueMinimoAtualAtualizaMinimo));
            var temporada = new Temporada { Id = Guid.NewGuid(), TemporadaDescricao = "2024" };
            context.Temporada.Add(temporada);
            context.RegistroJogo.Add(new RegistroJogo
            {
                Id = Guid.NewGuid(),
                NumeroJogo = 1,
                Placar = 30,
                MinimoTemporada = 30,
                MaximoTemporada = 30,
                QuebraRecordeMaximo = 1,
                QuebraRecordeMinimo = 1,
                TemporadaId = temporada.Id
            });
            context.SaveChanges();
            var controller = new RegistroJogoController(context);
            var registro = new RegistroJogo { Placar = 20, TemporadaId = temporada.Id };

            var resultado = InvokeCalcula(controller, registro);

            Assert.Equal(2, resultado.NumeroJogo);
            Assert.Equal(20, resultado.MinimoTemporada);
            Assert.Equal(30, resultado.MaximoTemporada);
            Assert.Equal(1, resultado.QuebraRecordeMaximo);
            Assert.Equal(2, resultado.QuebraRecordeMinimo);
        }

        [Fact]
        public void NovoPlacarMaiorQueMaximoAtualAtualizaMaximo()
        {
            var context = CreateContext(nameof(NovoPlacarMaiorQueMaximoAtualAtualizaMaximo));
            var temporada = new Temporada { Id = Guid.NewGuid(), TemporadaDescricao = "2024" };
            context.Temporada.Add(temporada);
            context.RegistroJogo.Add(new RegistroJogo
            {
                Id = Guid.NewGuid(),
                NumeroJogo = 1,
                Placar = 30,
                MinimoTemporada = 30,
                MaximoTemporada = 30,
                QuebraRecordeMaximo = 1,
                QuebraRecordeMinimo = 1,
                TemporadaId = temporada.Id
            });
            context.SaveChanges();
            var controller = new RegistroJogoController(context);
            var registro = new RegistroJogo { Placar = 40, TemporadaId = temporada.Id };

            var resultado = InvokeCalcula(controller, registro);

            Assert.Equal(2, resultado.NumeroJogo);
            Assert.Equal(30, resultado.MinimoTemporada);
            Assert.Equal(40, resultado.MaximoTemporada);
            Assert.Equal(2, resultado.QuebraRecordeMaximo);
            Assert.Equal(1, resultado.QuebraRecordeMinimo);
        }
    }
}
