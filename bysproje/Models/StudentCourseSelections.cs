
namespace bysproje.Models
{
    public class StudentCourseSelections
    {
        public int SelectionID { get; set; }
        public int StudentID { get; set; }
        public int CourseID { get; set; }
        public DateTime SelectionDate { get; set; }
        public bool IsApproved { get; set; }

        // İlişkili tablolar için navigasyon özellikleri
        public Students Student { get; set; } = null!;
        public Courses Course { get; set; } = null!;
    }
}
