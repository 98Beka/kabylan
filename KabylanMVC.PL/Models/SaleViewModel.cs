using Kabylan.DAL.Models;

namespace KabylanMVC.PL.Models {
    public class SaleViewModel {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerMiddleName { get; set; }
        public string CustomerLastName { get; set; }
        public int Price { get; set; }
        public int Square { get; set; }
        public int RoomCount { get; set; }
        public int PayingMonths { get; set; }
        public int TotalPayment { get; set; }
        public int HaveToPay { get; set; }
        public int MonthPayment { get; set; }
        public DateTime SaleDate { get; set; }
        public List<Payment> Payments { get; set; }
        public int Entrance { get; set; }
        public int Block { get; set; }
        public int Floor { get; set; }
        public int Number { get; set; }
    }
}
