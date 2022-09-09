namespace Kabylan.DAL.Models {
    public class Sale {
        public int Id { get; set; }
        public Customer? Customer { get; set; } = null!;
        public Apartment? Apartment { get; set; } = null!;
        public List<Payment>? Payments { get; set; } = new List<Payment>();
        public int PaydMonths { get; set; }
        public DateTime SaleDate { get; set; }
    }
}
