using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bysproje.Data; // DbContext'i kullanmak için
using bysproje.Models;

namespace bysproje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        // DbContext'i dependency injection ile alıyoruz
        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }



        // GET api/Courses
        [HttpGet("courses")]
        public async Task<IActionResult> GetCourses()
        {
            var courses = await _context.Courses.ToListAsync();
            return Ok(courses);
        }







        /// GET api/students
        [HttpGet("students")] // api/Students/students
        public async Task<IActionResult> GetStudents()
        {
            var students = await _context.Students.ToListAsync();
            return Ok(students);
        }




        // GET api/students/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound("Öğrenci bulunamadı.");
            }
            return Ok(student);
        }

        // POST api/students
        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] Students student)
        {
            if (student == null)
            {
                return BadRequest("Geçersiz öğrenci bilgisi.");
            }

            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetStudent), new { id = student.StudentID }, student);
        }

        // PUT api/students/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] Students student)
        {
            if (id != student.StudentID)
            {
                return BadRequest("ID uyumsuzluğu.");
            }

            _context.Entry(student).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound("Güncellenecek öğrenci bulunamadı.");
                }
                throw;
            }
            return NoContent();
        }

        // DELETE api/students/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound("Silinecek öğrenci bulunamadı.");
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.StudentID == id);
        }
    }
}
