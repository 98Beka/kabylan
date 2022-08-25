namespace Kabylan.DAL.Models {
    public abstract class Person {
        public abstract string? FirstName { get; set; }
        public abstract string? MiddleName { get; set; }
        public abstract string? LastName { get; set; }
    }
}
