using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWebApp.Models
{
	public class ProductImage
	{
		// Identificator unic pentru imaginea produsului.
		public int Id { get; set; }

		// URL-ul imaginii.
		[Required]
		public string ImageUrl { get; set; }

		// ID-ul produsului asociat cu această imagine.
		public int ProductId { get; set; }
		[ForeignKey("ProductId")]
		public Product Product { get; set; }
	}
}
