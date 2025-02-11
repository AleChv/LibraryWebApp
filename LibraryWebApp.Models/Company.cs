using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWebApp.Models
{
    public class Company
    {
        // Identificator unic pentru companie.
        public int Id { get; set; }

        // Numele companiei.
        [Required]
        public string Name { get; set; }

        // Adresa stradală a companiei.
        public string? StreetAddress { get; set; }

        // Orașul în care se află compania.
        public string? City { get; set; }

        // Statusul în care se află compania.
        public string? State { get; set; }

        // Codul poștal al companiei.
        public string? PostalCode { get; set; }

        // Numărul de telefon al companiei.
        public string? PhoneNumber { get; set; }
    }
}
