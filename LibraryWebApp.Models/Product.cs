using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace LibraryWebApp.Models
{
    public class Product
    {
        // Identificator unic pentru produs.
        public int Id { get; set; }

        // Titlul produsului.
        [Required]
        [DisplayName("Category Name")]
        [MaxLength(30)]
        public string Title { get; set; }

        // Descrierea produsului.
        public string Description { get; set; }

        // Codul ISBN al produsului.
        [Required]
        public string ISBN { get; set; }

        // Autorul produsului.
        [Required]
        public string Author { get; set; }

        // Prețul de listă al produsului.
        [Required]
        [Display(Name = "List Price")]
        [Range(1, 1000)]
        public double ListPrice { get; set; }

        // Prețul pentru cantități între 1 și 50.
        [Required]
        [Display(Name = "Price for 1-50")]
        [Range(1, 1000)]
        public double Price { get; set; }

        // Prețul pentru cantități de peste 50.
        [Required]
        [Display(Name = "Price for 50+")]
        [Range(1, 1000)]
        public double Price50 { get; set; }

        // Prețul pentru cantități de peste 100.
        [Required]
        [Display(Name = "Price for 100+")]
        [Range(1, 1000)]
        public double Price100 { get; set; }

        // ID-ul categoriei din care face parte produsul.
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }

        // Lista de imagini asociate produsului.
        [ValidateNever] 
        public List<ProductImage> ProductImages { get; set; }
    }
}
