namespace Kabylan.DAL.Models {
    public class Sale {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; } = null!;
        public virtual Apartment? Apartment { get; set; } = null!;
        public virtual List<Payment>? Payments { get; set; } = new List<Payment>();
        public int PaydMonths { get; set; }
        public DateTime SaleDate { get; set; }
    }
}
