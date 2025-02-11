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
    public class ShoppingCart
    {
        // Identificator unic pentru coșul de cumpărături.
        public int Id { get; set; }

        // ID-ul produsului adăugat în coș.
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        [ValidateNever]
        public Product Product { get; set; }

        // Cantitatea produsului în coș.
        [Range(1, 1000, ErrorMessage = "Please enter a value between 1 and 1000")]
        public int Count { get; set; }

        // ID-ul utilizatorului care deține coșul.
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        // Prețul total pentru produsele din coș (calculat).
        [NotMapped]
        public double Price { get; set; }
    }
}
