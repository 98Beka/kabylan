namespace Kabylan.DAL.Models {
    public class Apartment {
        public int Id { get; set; }
        public int Square { get; set; }
        public int RoomCount { get; set; }
        public int Price { get; set; }
        public int SaleId { get; set; }
        public Sale Sale { get; set; }
    }
}
