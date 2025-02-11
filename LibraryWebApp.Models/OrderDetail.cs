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
	public class OrderDetail
	{
		// Identificator unic pentru detaliul comenzii.
		public int Id { get; set; }

		// ID-ul antetului comenzii la care se referă acest detaliu.
		[Required]
		public int OrderHeaderId { get; set; }
		[ForeignKey(nameof(OrderHeaderId))]
		[ValidateNever]
		public OrderHeader OrderHeader { get; set; }

		// ID-ul produsului comandat.
		[Required]
		public int ProductId { get; set; }
		[ForeignKey(nameof(ProductId))]
		[ValidateNever] 
		public Product Product { get; set; }

		// Cantitatea produsului comandat.
		public int Count { get; set; }

		// Prețul produsului la momentul comenzii.
		public double Price { get; set; }
	}
}
