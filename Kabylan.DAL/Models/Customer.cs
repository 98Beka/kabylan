namespace Kabylan.DAL.Models {
    public class Customer : Person {
        public int Id { get; set; }
        public override string FirstName { get; set; } = String.Empty;
        public override string MiddleName { get; set; } = String.Empty;
        public override string LastName { get; set; } = String.Empty;
        public int SaleId { get; set; }
        public Sale Sale { get; set; } = null!;
    }
}
