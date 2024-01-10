using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace Marketing.Models
{
    public class RegisterViewModel
    {

        [Required]
        public string FullName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password),ErrorMessage ="the confirm password does not match the password !")]
        public string ConfirmPassword { get; set; }


        [Required]
        public string Country { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public int DoorNumber { get; set; }


        [Required]
        public string TelefonNumber { get; set; }

    }
}
