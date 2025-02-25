﻿namespace User_Management_Application.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Status { get; set; }
        public string? RegistrationStatus { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }
      
    }
}
