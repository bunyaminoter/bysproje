using bysproje.Data;
using bysproje.Models; // Student ve Courses s�n�flar�n� kullanabilmek i�in
using bysproje.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore; // Entity Framework i�in gerekli
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aspdenemesi01.Pages.Student
{
    public class StudentDashboardModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public StudentDashboardModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string StudentName { get; set; } = "Ali Veli";
        public List<Courses> CoursesList { get; set; } = new List<Courses>();


        public async Task OnGetAsync()
        {
            // Courses tablosundan t�m verileri �ekme
            CoursesList = await _context.Courses.ToListAsync();

            if (CoursesList == null || CoursesList.Count == 0)
            {
                Console.WriteLine("Ders listesi bo� veya y�klenemedi.");
            }
        }
    }
}
