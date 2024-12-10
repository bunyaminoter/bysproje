
namespace bysproje.Models
{
    public class Users
    {
        public int UserID { get; set; }  // Kullanıcı kimliği
        public string Username { get; set; } = null!;  // Kullanıcı adı
        public string PasswordHash { get; set; } = null!; // Şifre hash'i
        public string Role { get; set; } = null!; // Kullanıcı rolü (örneğin, "Advisor" veya "Student")
        public int RelatedID { get; set; } // İlişkili kimlik (örneğin, danışman veya öğrenci ID'si)
        public string Email { get; set; } = null!; // Kullanıcının e-posta adresi

        // İlişkili tablolar için navigasyon özellikleri (isteğe bağlı)
        public Advisor Advisor { get; set; } = null!; // Eğer rol "Advisor" ise, ilişkili danışman
        public Students Students { get; set; } = null!; // Eğer rol "Student" ise, ilişkili öğrenci
    }
}
