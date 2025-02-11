using LibraryWebApp.DataAccess.Data;
using LibraryWebApp.DataAccess.DbInitializer;
using LibraryWebApp.DataAccess.Repository;
using LibraryWebApp.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using LibraryWebApp.Utility;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Adaugă serviciile necesare aplicației în containerul de dependențe
builder.Services.AddControllersWithViews();

// Configurează conexiunea la baza de date
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurează setările pentru Stripe
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

// Adaugă Identity pentru gestionarea utilizatorilor și rolurilor
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

// Configurează rutele pentru login/logout și acces restricționat
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.LogoutPath = "/Identity/Account/Logout";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});

// Configurează autentificarea prin Facebook (opțional)
builder.Services.AddAuthentication().AddFacebook(option =>
{
    option.AppId = "1050376639719992"; // ID-ul aplicației Facebook
    option.AppSecret = "63768e3cc24c59203bb7caabc19dbd70"; // Secretul aplicației Facebook
});

// Activează caching-ul și sesiuni de utilizator
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(100); // Setează timeout-ul pentru sesiuni
    options.Cookie.HttpOnly = true; // Asigură că cookie-urile sunt sigure
    options.Cookie.IsEssential = true; // Cookie-urile sunt esențiale pentru funcționarea aplicației
});

// Adaugă serviciile pentru inițializarea bazei de date și pentru unitatea de lucru
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddRazorPages();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>(); // Serviciul pentru trimiterea email-urilor

var app = builder.Build();

// Configurează pipeline-ul de request-uri HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Handle error page
    app.UseHsts(); // Security headers
}

app.UseHttpsRedirection(); // Forțează folosirea HTTPS
app.UseStaticFiles(); // Permite accesul la fișierele statice (CSS, JS, imagini)

// Configurăm Stripe cu cheia secretă
StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();

app.UseRouting();
app.UseAuthentication(); // Activează autentificarea utilizatorilor
app.UseAuthorization(); // Permite autorizația pe baza rolurilor utilizatorilor

app.UseSession(); // Activează sesiunea utilizatorului

// Inițializează baza de date cu datele implicite (crearea contului de admin)
SeedDatabase();

app.MapRazorPages(); // Rutează către Razor Pages
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}"); // Rutează către controlerele standard

app.Run(); // Rulează aplicația

// Metoda care inițializează datele din baza de date, creând conturi și roluri necesare
void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize(); // Apelăm metoda de inițializare pentru a crea roluri și utilizatori
    }
}
