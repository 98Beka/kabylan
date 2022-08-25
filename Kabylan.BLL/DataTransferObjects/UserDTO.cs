﻿namespace Kabylan.BLL.DataTransferObjects {
    public class UserDTO : PersonDTO {
        public int Id { get; set; }
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int Status { get; set; }
        public override string? FirstName { get; set; } = null!;
        public override string? MiddleName { get; set; } = null!;
        public override string? LastName { get; set; } = null!;
    }
}
