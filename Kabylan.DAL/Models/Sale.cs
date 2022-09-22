namespace Kabylan.DAL.Models {
    public class Sale {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public Apartment Apartment { get; set; }
        public List<Payment> Payments { get; set; } = new List<Payment>();
        public int PayingMonths { get; set; }
        public DateTime SaleDate { get; set; }
    }
}
