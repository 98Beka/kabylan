namespace Kabylan.DAL.Models {
    public class Apartment {
        public int Id { get; set; }
        public int Square { get; set; }
        public int RoomCount { get; set; }
        public int Price { get; set; }
        public int SaleId { get; set; }
        public Sale Sale { get; set; }
        public int Entrance { get; set; }
        public int Block { get; set; }
        public int Floor { get; set; }
        public int Number { get; set; }
    }
}
