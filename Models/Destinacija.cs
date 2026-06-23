using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TuristickaAgencija.Mvc.Models
{
    public class Destinacija
    {
        public int Id { get; set; }

        [Required]
        public string Naziv { get; set; }

        public string Drzava { get; set; }

        public string Opis { get; set; }

        public bool Popularna { get; set; }

        public List<Aranzman>? Aranzmani { get; set; }
    }
}