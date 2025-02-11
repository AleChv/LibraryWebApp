using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryWebApp.DataAccess.Data;
using LibraryWebApp.Models;
using LibraryWebApp.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApp.DataAccess.DbInitializer
{
    // Clasa DbInitializer implementează IDbInitializer pentru a configura datele inițiale ale aplicației
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager; // Gestionează utilizatorii (creare, autentificare etc.)
        private readonly RoleManager<IdentityRole> _roleManager; // Gestionează rolurile utilizatorilor
        private readonly ApplicationDbContext _db; // Contextul bazei de date pentru aplicație

        // Constructor pentru injectarea dependențelor necesare
        public DbInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }

        // Metodă care inițializează baza de date unde se creează roluri și utilizatori
        public void Initialize()
        {
            try
            {
                // Verifică dacă există migrări care nu au fost aplicate și le aplică
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception x)
            {
                // Adaugă log pentru a înregistra eroarea
                Console.WriteLine("Eroare la aplicarea migrărilor: " + x.Message);
            }

            // Verifică dacă rolul "Customer" există în baza de date
            if (!_roleManager.RoleExistsAsync(SD.Role_Customer).GetAwaiter().GetResult())
            {
                Console.WriteLine("Roluri nu există. Creăm roluri...");

                // Creează rolurile de bază
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Company)).GetAwaiter().GetResult();

                Console.WriteLine("Roluri create.");

                // Creează un utilizator admin cu detaliile specifice
                var result = _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "admin@admin.com", // Schimbă emailul pentru a fi același
                    Email = "admin@admin.com",    // Schimbă emailul pentru a fi același
                    Name = "Admin User",
                    PhoneNumber = "1112223333",
                    StreetAddress = "test 123 Ave",
                    State = "IL",
                    PostalCode = "23422",
                    City = "Chicago"
                }, "Admin12345@").GetAwaiter().GetResult();

                if (result.Succeeded)
                {
                    Console.WriteLine("Contul admin a fost creat cu succes.");

                    // Asigură-te că utilizatorul corect este selectat
                    ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@admin.com");
                    if (user != null)
                    {
                        // Atribuie utilizatorului rolul de Admin
                        _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
                        Console.WriteLine("Rolul de Admin a fost atribuit utilizatorului.");
                    }
                    else
                    {
                        Console.WriteLine("Utilizatorul nu a fost găsit pentru atribuirea rolului.");
                    }
                }
                else
                {
                    // Dacă există erori la crearea utilizatorului, le afișăm
                    Console.WriteLine("Crearea contului admin a eșuat. Detalii eroare: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}

  