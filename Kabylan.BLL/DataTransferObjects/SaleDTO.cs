using Kabylan.DAL.Models;

namespace Kabylan.BLL.DataTransferObjects {
    public class SaleDTO {
        public int Id { get; set; }
        public int Price { get; set; }
        public int Square { get; set; }
        public int RoomCount { get; set; }
        public int PayingMonths { get; set; }
        public DateTime SaleDate { get; set; }
        public List<Payment> Payments { get; set; }
    }
}
