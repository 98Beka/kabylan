namespace Kabylan.BLL.DataTransferObjects {
    public abstract class PersonDTO {
        public abstract string? FirstName { get; set; }
        public abstract string? MiddleName { get; set; }
        public abstract string? LastName { get; set; }
    }
}
