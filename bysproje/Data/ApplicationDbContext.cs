using Microsoft.EntityFrameworkCore;
using bysproje.Models; // Model sınıflarını kullanmak için
using bysproje.Controllers;

namespace bysproje.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Veritabanı tablolarını temsil eden DbSet'ler
        public DbSet<Users> Users { get; set; } = null!;
        public DbSet<Advisor> Advisors { get; set; } = null!;
        public DbSet<Students> Students { get; set; } = null!;
        public DbSet<Courses> Courses { get; set; } = null!;
        public DbSet<Transcripts> Transcripts { get; set; } = null!;
        public DbSet<StudentCourseSelections> StudentCourseSelections { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Kullanıcılar tablosu için birincil anahtar
            modelBuilder.Entity<Users>().HasKey(u => u.UserID);

            // Danışmanlar (Advisors) tablosu için birincil anahtar
            modelBuilder.Entity<Advisor>().HasKey(a => a.AdvisorID);

            // Öğrenciler (Students) tablosu
            modelBuilder.Entity<Students>()
                .HasKey(s => s.StudentID);

            modelBuilder.Entity<Students>()
                .HasOne(s => s.Advisor) // Bir öğrenci bir danışmana bağlı
                .WithMany(a => a.Students) // Bir danışmanın birden fazla öğrencisi olabilir
                .HasForeignKey(s => s.AdvisorID)
                .OnDelete(DeleteBehavior.Restrict); // Danışman silindiğinde öğrenciler korunur

            // Dersler (Courses) tablosu için birincil anahtar
            modelBuilder.Entity<Courses>().HasKey(c => c.CourseID);

            // Ders Seçimleri (StudentCourseSelections) tablosu
            modelBuilder.Entity<StudentCourseSelections>()
                .HasKey(scs => scs.SelectionID);

            modelBuilder.Entity<StudentCourseSelections>()
                .HasOne(scs => scs.Student) // Ders seçimi bir öğrenciye bağlı
                .WithMany(s => s.StudentCourseSelections) // Bir öğrencinin birden fazla ders seçimi olabilir
                .HasForeignKey(scs => scs.StudentID)
                .OnDelete(DeleteBehavior.Cascade); // Öğrenci silinirse ilgili seçimler de silinir

            modelBuilder.Entity<StudentCourseSelections>()
                .HasOne(scs => scs.Course) // Ders seçimi bir derse bağlı
                .WithMany(c => c.StudentCourseSelections) // Bir ders birden fazla seçime sahip olabilir
                .HasForeignKey(scs => scs.CourseID)
                .OnDelete(DeleteBehavior.Cascade);

            // Transkriptler (Transcripts) tablosu
            modelBuilder.Entity<Transcripts>()
                .HasKey(t => t.TranscriptID);

            modelBuilder.Entity<Transcripts>()
                .HasOne(t => t.Student) // Transkript bir öğrenciye bağlı
                .WithMany(s => s.Transcripts) // Bir öğrencinin birden fazla transkripti olabilir
                .HasForeignKey(t => t.StudentID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Transcripts>()
                .HasOne(t => t.Course) // Transkript bir derse bağlı
                .WithMany(c => c.Transcripts) // Bir ders birden fazla transkripte sahip olabilir
                .HasForeignKey(t => t.CourseID)
                .OnDelete(DeleteBehavior.Cascade);

            // Öğrenciler tablosu için zorunlu alanlar
            modelBuilder.Entity<Students>()
                .Property(s => s.FirstName)
                .IsRequired();

            modelBuilder.Entity<Students>()
                .Property(s => s.LastName)
                .IsRequired();

            modelBuilder.Entity<Students>()
                .Property(s => s.Email)
                .IsRequired();

            // Dersler tablosu için zorunlu alanlar
            modelBuilder.Entity<Courses>()
                .Property(c => c.CourseName)
                .IsRequired();

            modelBuilder.Entity<Courses>()
                .Property(c => c.CourseCode)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
