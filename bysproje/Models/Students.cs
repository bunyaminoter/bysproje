namespace bysproje.Models
{
    public class Students
    {
        public int StudentID { get; set; } // Öğrenci kimliği
        public string FirstName { get; set; } = null!; // Öğrencinin adı
        public string LastName { get; set; } = null!; // Öğrencinin soyadı
        public string Email { get; set; } = null!; // E-posta adresi
        public int AdvisorID { get; set; } // Danışman kimliği

        // Navigasyon Özellikleri
        public Advisor Advisor { get; set; } = null!; // Öğrencinin danışmanı

        public ICollection<Transcripts> Transcripts { get; set; } = new List<Transcripts>();
        public ICollection<StudentCourseSelections> StudentCourseSelections { get; set; } = new List<StudentCourseSelections>(); // Öğrencinin ders seçimleri
    }
}

