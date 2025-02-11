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
	public class OrderHeader
	{
        // Identificator unic pentru antetul comenzii.
        public int Id { get; set; }

        // ID-ul utilizatorului care a plasat comanda.
        public string ApplicationUserId { get; set; }
        [ForeignKey(nameof(ApplicationUserId))]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        // Data la care a fost plasată comanda.
        public DateTime OrderDate { get; set; }

        // Data estimată pentru livrarea comenzii.
        public DateTime ShippingDate { get; set; }

        // Totalul comenzii.
        public double OrderTotal { get; set; }

        // Statusul actual al comenzii.
        public string? OrderStatus { get; set; }

        // Statusul plății pentru comandă.
        public string? PaymentStatus { get; set; }

        // Numărul de urmărire pentru livrare.
        public string? TrackingNumber { get; set; }

        // Transportatorul care livrează comanda.
        public string? Carrier { get; set; }

        // Data la care a fost efectuată plata.
        public DateTime PaymentDate { get; set; }

        // Data scadentă pentru plată.
        public DateTime PaymentDueDate { get; set; }

        // ID-ul sesiunii de plată.
        public string? SessionId { get; set; }

        // ID-ul intenției de plată.
        public string? PaymentIntentId { get; set; }

        // Informații de contact și adresă pentru livrare.
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string? StreetAddress { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
		[Required]
		public string PostalCode { get; set; }
		[Required]
		public string Name { get; set; }
	}
}
