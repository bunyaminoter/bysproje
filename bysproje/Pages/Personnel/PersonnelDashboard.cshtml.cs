using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using bysproje.Data; // ApplicationDbContext'i kullanmak i�in
using bysproje.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; // ToListAsync() i�in


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

        public string PersonnelName { get; set; } = "Mehmet Y�lmaz"; // Varsay�lan de�er

        //Sayfa y�klendi�inde t�m ��rencileri listeleme
        public async Task<IActionResult> OnGetAsync()
        {
            // Veritaban�ndan t�m ��rencileri �ekiyoruz
            StudentsList = await _context.Students.ToListAsync();
            return Page();
        }

        // Yeni ��renci ekleme i�lemi
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Yeni ��renci verisini veritaban�na ekleme
                    _context.Students.Add(NewStudent);
                    await _context.SaveChangesAsync();

                    // Ba�ar�yla eklendikten sonra sayfay� yeniden y�kleme
                    return RedirectToPage("/Personnel/PersonnelDashboard");
                }
                catch (Exception ex)
                {
                    // Hata mesaj� g�sterme
                    ModelState.AddModelError(string.Empty, $"��renci eklenirken bir hata olu�tu: {ex.Message}");
                }
            }
            StudentsList = await _context.Students.ToListAsync();
            // Ekleme i�leminde hata olduysa tekrar sayfay� d�nd�r
            return Page();
        }

        // ��renci g�ncelleme
        public async Task<IActionResult> OnPostUpdateAsync()
        {
            if (ModelState.IsValid)
            {
                var existingStudent = await _context.Students.FindAsync(NewStudent.StudentID);
                if (existingStudent == null)
                {
                    ModelState.AddModelError(string.Empty, "G�ncellenecek ��renci bulunamad�.");
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
                    ModelState.AddModelError(string.Empty, $"Veritaban� g�ncelleme hatas�: {dbEx.Message}");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"G�ncelleme s�ras�nda hata olu�tu: {ex.Message}");
                }
            }

            // Listeyi g�ncel verilerle y�kle
            StudentsList = await _context.Students.ToListAsync();
            return Page();
        }
        public async Task<IActionResult> OnGetUpdateAsync(int id)
        {
            // Belirli bir ��renciyi ID ile bulma
            NewStudent = await _context.Students.FindAsync(id);
            if (NewStudent == null)
            {
                return NotFound("G�ncellenecek ��renci bulunamad�.");
            }

            // G�ncelleme formunu g�stermek i�in sayfay� d�nd�r
            return Page();
        }


        // ��renci silme i�lemi
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                ModelState.AddModelError(string.Empty, "Silinecek ��renci bulunamad�.");
                StudentsList = await _context.Students.ToListAsync();
                return Page();
            }

            try
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
                // Silme i�leminden sonra listeyi g�ncelleme
                StudentsList = await _context.Students.ToListAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"��renci silinirken bir hata olu�tu: {ex.Message}");
            }
            return Page();
        }
    }
}
