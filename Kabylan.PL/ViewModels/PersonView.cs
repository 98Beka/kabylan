namespace Kabylan.PL.ViewModels {
    public abstract class PersonView {
        public abstract string FirstName { get; set; }
        public abstract string? MiddleName { get; set; }
        public abstract string? LastName { get; set; }
    }
}
