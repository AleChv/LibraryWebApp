using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWebApp.Models.ViewModels
{
	public class OrderVM
	{
        // Informațiile de bază ale comenzii.
        public OrderHeader OrderHeader { get; set; }

        // Detaliile individuale ale fiecărui produs din comandă.
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}
