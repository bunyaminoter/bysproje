using bysproje.Data;
using bysproje.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace bysproje.Pages
{
    public class LoginModel : PageModel
    {

        private readonly ApplicationDbContext _context;

        // DbContext'i dependency injection ile alýyoruz
        public LoginModel(ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }


        public string? Message { get; set; }

        public string Email { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                Message = "E-posta ve þifre gereklidir.";
                return Page();
            }

            // Kullanýcýyý veritabanýnda e-posta ile arama
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == Email);

            if (user != null)
            {
                // Þifreyi doðrulama
                var passwordHasher = new PasswordHasher<Users>();
                if (passwordHasher.VerifyHashedPassword(user, user.PasswordHash, Password) == PasswordVerificationResult.Success)
                {
                    // Kullanýcý doðrulandý, rolüne göre yönlendirme yapma
                    if (user.Role == "Advisor")
                    {
                        // Personel giriþ yaptý
                        return RedirectToPage("/Personnel/PersonnelDashboard");
                    }
                    else if (user.Role == "Student")
                    {
                        // Öðrenci giriþ yaptý
                        return RedirectToPage("/Student/StudentDashboard");
                    }
                }
            }

            // Geçersiz e-posta veya þifre
            Message = "Geçersiz e-posta veya þifre.";
            return Page();
        }


    }
}
