using bysproje.Data;
using bysproje.Models; // Student ve Courses sýnýflarýný kullanabilmek için
using bysproje.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore; // Entity Framework için gerekli
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
            // Courses tablosundan tüm verileri çekme
            CoursesList = await _context.Courses.ToListAsync();

            if (CoursesList == null || CoursesList.Count == 0)
            {
                Console.WriteLine("Ders listesi boþ veya yüklenemedi.");
            }
        }
    }
}
