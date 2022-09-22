namespace Kabylan.DAL.Models {
    public class Customer {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int SaleId { get; set; }
        public Sale Sale { get; set; } = null!;
    }
}
