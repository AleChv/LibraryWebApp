using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWebApp.Models.ViewModels
{
    public class ShoppingCartVM
    {
        // Lista de articole din coșul de cumpărături.
        public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }

        // Informațiile antetului comenzii asociate cu coșul de cumpărături.
        public OrderHeader OrderHeader { get; set; }
    }
}
