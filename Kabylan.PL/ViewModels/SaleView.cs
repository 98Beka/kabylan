namespace Kabylan.PL.ViewModels {
    public class SaleView {
        public int Id { get; set; }
        public CustomerView? Customer { get; set; }
        public ApartmentView? Apartment { get; set; }
        public List<PaymentView>? Payments { get; set; }
        public int PaydMonths { get; set; }
        public DateTime SaleDate { get; set; }
    }
}
