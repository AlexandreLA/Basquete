using System;
using System.ComponentModel;

namespace TemporadaDeBasquete.Models
{
    public class RegistroJogo
    {
        public Guid Id { get; set; }

        [DisplayName("Número do Jogo")]
        public int NumeroJogo { get; set; }

        [DisplayName("Placar")]
        public int Placar { get; set; }

        [DisplayName("Min. Temporada")]
        public int? MinimoTemporada {get;set;}

        [DisplayName("Max. Temporada")]
        public int? MaximoTemporada { get; set; }

        [DisplayName("Quebra de Recorde Máximo")]
        public int? QuebraRecordeMaximo { get; set; }

        [DisplayName("Quebra de Recorde Mínimo")]
        public int? QuebraRecordeMinimo { get; set; }

        [DisplayName("Temporada")]
        public Guid TemporadaId { get; set; }

        public Temporada Temporada { get; set; }
    }
}
