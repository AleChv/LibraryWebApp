using System;
using System.Linq;
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
                // Poți loga sau gestiona excepțiile aici
            }

            // Verifică dacă rolul "Customer" există în baza de date
            if (!_roleManager.RoleExistsAsync(SD.Role_Customer).GetAwaiter().GetResult())
            {
                // Creează rolurile de bază pentru utilizatori
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Company)).GetAwaiter().GetResult();

                // Creează un utilizator admin cu detaliile specifice
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "admin@admin.com", // Email-ul utilizatorului
                    Email = "admin@admin.com", // Email-ul utilizatorului
                    Name = "Admin User", // Numele utilizatorului
                    PhoneNumber = "1234567890", // Număr de telefon
                    StreetAddress = "Admin Street 123", // Adresa utilizatorului
                    State = "Admin State", // Statul
                    PostalCode = "12345", // Codul poștal
                    City = "Admin City" // Orașul
                }, "Admin12345@").GetAwaiter().GetResult();

                // Căutăm utilizatorul creat și îl adăugăm la rolul Admin
                ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@admin.com");
                if (user != null)
                {
                    _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
                }
            }
            return;
        }
    }
}
