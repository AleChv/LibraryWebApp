using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWebApp.Models.ViewModels
{
    public class RoleManagmentVM
    {
        // Utilizatorul aplicației pentru care se gestionează rolurile.
        public ApplicationUser ApplicationUser { get; set; }

        // Lista de roluri disponibile pentru selectare.
        public IEnumerable<SelectListItem> RoleList { get; set; }

        // Lista de companii disponibile pentru selectare.
        public IEnumerable<SelectListItem> CompanyList { get; set; }
    }
}
