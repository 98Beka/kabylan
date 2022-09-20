namespace Kabylan.DAL.Models {
    public class User {
        public int Id { get; set; }
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int Status { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
