namespace Kabylan.DAL.Models {
    public class Payment {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int MoneyCount { get; set; }
        public virtual Sale Sale { get; set; } = null!;
    }
}
