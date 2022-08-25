﻿namespace Kabylan.BLL.DataTransferObjects {
    public class CustomerDTO : PersonDTO {
        public int Id { get; set; }
        public override string? FirstName { get; set; } = null!;
        public override string? MiddleName { get; set; } = null!;
        public override string? LastName { get; set; } = null!;
    }
}