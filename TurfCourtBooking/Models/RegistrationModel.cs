using System;
using System.ComponentModel.DataAnnotations;

namespace TurfCourtBooking.Models
{
    public class RegistrationModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime Dateofbirth { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        public string Usertype { get; set; }
    }
}