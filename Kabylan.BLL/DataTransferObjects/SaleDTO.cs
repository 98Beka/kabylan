using Kabylan.DAL.Models;

namespace Kabylan.BLL.DataTransferObjects {
    public class SaleDTO {
        public int Id { get; set; }
        public string CustomerFirstName { get; set; } = String.Empty;
        public string CustomerMiddleName { get; set; } = String.Empty;
        public string CustomerLastName { get; set; } = String.Empty;
        public int Price { get; set; }
        public int Square { get; set; }
        public int RoomCount { get; set; }
        public int PayingMonths { get; set; }
        public DateTime SaleDate { get; set; }
        public List<Payment> Payments { get; set; }
        public int Entrance { get; set; }
        public int Block { get; set; }
        public int Floor { get; set; }
        public int Number { get; set; }
    }
}
