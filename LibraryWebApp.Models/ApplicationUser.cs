using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWebApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Numele complet al utilizatorului.
        [Required]
        public string Name { get; set; }

        // Adresa stradală a utilizatorului.
        public string? StreetAddress { get; set; }

        // Orașul în care locuiește utilizatorul.
        public string? City { get; set; }

        // Statul în care locuiește utilizatorul.
        public string? State { get; set; }

        // Codul poștal al utilizatorului.
        public string? PostalCode { get; set; }

        // ID-ul companiei asociate utilizatorului, dacă este cazul.
        public int? CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        [ValidateNever]
        public Company? Company { get; set; }

        // Rolul utilizatorului în aplicație (nu este mapat în baza de date).
        [NotMapped]
        public string Role { get; set; }
    }
}
