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

        // DbContext'i dependency injection ile al�yoruz
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
                Message = "E-posta ve �ifre gereklidir.";
                return Page();
            }

            // Kullan�c�y� veritaban�nda e-posta ile arama
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == Email);

            if (user != null)
            {
                // �ifreyi do�rulama
                var passwordHasher = new PasswordHasher<Users>();
                if (passwordHasher.VerifyHashedPassword(user, user.PasswordHash, Password) == PasswordVerificationResult.Success)
                {
                    // Kullan�c� do�ruland�, rol�ne g�re y�nlendirme yapma
                    if (user.Role == "Advisor")
                    {
                        // Personel giri� yapt�
                        return RedirectToPage("/Personnel/PersonnelDashboard");
                    }
                    else if (user.Role == "Student")
                    {
                        // ��renci giri� yapt�
                        return RedirectToPage("/Student/StudentDashboard");
                    }
                }
            }

            // Ge�ersiz e-posta veya �ifre
            Message = "Ge�ersiz e-posta veya �ifre.";
            return Page();
        }


    }
}
