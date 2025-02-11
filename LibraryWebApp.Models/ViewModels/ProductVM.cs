using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWebApp.Models.ViewModels
{
    public class ProductVM
    {
        // Reprezintă produsul curent.
        public Product Product { get; set; }

        // Lista de categorii pentru selectare în interfața utilizator.
        // [ValidateNever] indică faptul că această proprietate nu trebuie validată.
        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}
