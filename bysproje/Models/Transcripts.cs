
namespace bysproje.Models
{
    public class Transcripts
    {
        public int TranscriptID { get; set; }  // Not kimliği
        public int StudentID { get; set; } // Öğrenci kimliği (foreign key)
        public int CourseID { get; set; } // Ders kimliği (foreign key)
        public string Grade { get; set; } = null!; // Not (örneğin, A, B, C, vs.)
        public string Semester { get; set; } = null!; // Dönem (örneğin, "2023-2024 Bahar")

        // İlişkili tablolar için navigasyon özellikleri
        public Students Student { get; set; } = null!;
        public Courses Course { get; set; } = null!;
    }
}
