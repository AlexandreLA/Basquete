using System;
using System.ComponentModel;

namespace TemporadaDeBasquete.Models
{
    public class Temporada
    {
        public Guid Id { get; set; }

        [DisplayName("Temporada")]
        public string TemporadaDescricao { get; set; }
    }
}
