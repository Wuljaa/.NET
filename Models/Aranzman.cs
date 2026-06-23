using System;
using System.ComponentModel.DataAnnotations;

namespace TuristickaAgencija.Mvc.Models
{
    public class Aranzman
    {
        public int Id { get; set; }

        [Required]
        public string Naziv { get; set; }

        public string Opis { get; set; }

        public decimal Cijena { get; set; }

        public DateTime DatumPolaska { get; set; }

        public DateTime DatumPovratka { get; set; }

        public int DestinacijaId { get; set; }
        public Destinacija? Destinacija { get; set; }
    }
}