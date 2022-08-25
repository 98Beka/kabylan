using System.ComponentModel.DataAnnotations;

namespace Kabylan.PL.ViewModels {
    public class PaymentView {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int MoneyCount { get; set; }
    }
}
