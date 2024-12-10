namespace bysproje.Models
{
    public class Courses
    {
        public int CourseID { get; set; } // Ders kimliği
        public string CourseCode { get; set; } = null!; // Ders kodu
        public string CourseName { get; set; } = null!; // Ders adı
        public bool IsMandatory { get; set; } // Zorunlu ders mi?
        public int Credit { get; set; } // Kredi sayısı
        public string Department { get; set; } = null!; // Bölüm

        // Navigasyon Özellikleri
        public ICollection<StudentCourseSelections> StudentCourseSelections { get; set; } = new List<StudentCourseSelections>(); // Dersin öğrenci seçimleri
        public ICollection<Transcripts> Transcripts { get; set; } = new List<Transcripts>(); // Dersin transkriptleri
    }
}
