namespace Kabylan.PL.ViewModels {
    public class CustomerView : PersonView {
        public override string FirstName { get; set; } = null!;
        public override string? MiddleName { get; set; } = null!;
        public override string? LastName { get; set; } = null!;
    }
}
