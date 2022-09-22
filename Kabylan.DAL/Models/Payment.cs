using System.Text.Json.Serialization;

namespace Kabylan.DAL.Models {
    public class Payment {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int MoneyCount { get; set; }
        public int SaleId { get; set; }
        [JsonIgnore]
        public Sale Sale { get; set; } = null!;
    }
}
