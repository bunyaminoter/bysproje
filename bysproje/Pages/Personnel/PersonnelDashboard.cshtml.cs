using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using bysproje.Data; // ApplicationDbContext'i kullanmak için
using bysproje.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; // ToListAsync() için


namespace bysproje.Pages.Personnel
{
    public class PersonnelDashboardModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        // DbContext'i enjekte etme
        public PersonnelDashboardModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Students NewStudent { get; set; } = new Students();

        public List<Students> StudentsList { get; set; } = new List<Students>();

        public string PersonnelName { get; set; } = "Mehmet Yýlmaz"; // Varsayýlan deðer

        //Sayfa yüklendiðinde tüm öðrencileri listeleme
        public async Task<IActionResult> OnGetAsync()
        {
            // Veritabanýndan tüm öðrencileri çekiyoruz
            StudentsList = await _context.Students.ToListAsync();
            return Page();
        }

        // Yeni öðrenci ekleme iþlemi
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Yeni öðrenci verisini veritabanýna ekleme
                    _context.Students.Add(NewStudent);
                    await _context.SaveChangesAsync();

                    // Baþarýyla eklendikten sonra sayfayý yeniden yükleme
                    return RedirectToPage("/Personnel/PersonnelDashboard");
                }
                catch (Exception ex)
                {
                    // Hata mesajý gösterme
                    ModelState.AddModelError(string.Empty, $"Öðrenci eklenirken bir hata oluþtu: {ex.Message}");
                }
            }
            StudentsList = await _context.Students.ToListAsync();
            // Ekleme iþleminde hata olduysa tekrar sayfayý döndür
            return Page();
        }

        // Öðrenci güncelleme
        public async Task<IActionResult> OnPostUpdateAsync()
        {
            if (ModelState.IsValid)
            {
                var existingStudent = await _context.Students.FindAsync(NewStudent.StudentID);
                if (existingStudent == null)
                {
                    ModelState.AddModelError(string.Empty, "Güncellenecek öðrenci bulunamadý.");
                    StudentsList = await _context.Students.ToListAsync();
                    return Page();
                }

                existingStudent.FirstName = NewStudent.FirstName;
                existingStudent.LastName = NewStudent.LastName;
                existingStudent.Email = NewStudent.Email;
                existingStudent.AdvisorID = NewStudent.AdvisorID;

                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToPage("/Personnel/PersonnelDashboard");
                }
                catch (DbUpdateException dbEx)
                {
                    ModelState.AddModelError(string.Empty, $"Veritabaný güncelleme hatasý: {dbEx.Message}");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Güncelleme sýrasýnda hata oluþtu: {ex.Message}");
                }
            }

            // Listeyi güncel verilerle yükle
            StudentsList = await _context.Students.ToListAsync();
            return Page();
        }
        public async Task<IActionResult> OnGetUpdateAsync(int id)
        {
            // Belirli bir öðrenciyi ID ile bulma
            NewStudent = await _context.Students.FindAsync(id);
            if (NewStudent == null)
            {
                return NotFound("Güncellenecek öðrenci bulunamadý.");
            }

            // Güncelleme formunu göstermek için sayfayý döndür
            return Page();
        }


        // Öðrenci silme iþlemi
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                ModelState.AddModelError(string.Empty, "Silinecek öðrenci bulunamadý.");
                StudentsList = await _context.Students.ToListAsync();
                return Page();
            }

            try
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
                // Silme iþleminden sonra listeyi güncelleme
                StudentsList = await _context.Students.ToListAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Öðrenci silinirken bir hata oluþtu: {ex.Message}");
            }
            return Page();
        }
    }
}
