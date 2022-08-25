namespace Kabylan.BLL.DataTransferObjects {
    public class SaleDTO {
        public int Id { get; set; }
        public CustomerDTO Customer { get; set; }
        public ApartmentDTO Apartment { get; set; }
        public List<PaymentDTO> Payments { get; set; } = new List<PaymentDTO>();
        public int PaydMonths { get; set; }
        public DateTime SaleDate { get; set; }
    }
}
