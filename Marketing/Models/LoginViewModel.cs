using Microsoft.Build.Framework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace Marketing.Models
{
    public class LoginViewModel
    {

        [Required,EmailAddress]
        public string Email { set; get; }

        [Required]
        public string Password { set; get; }


        public bool RememberMe { set; get; }


    }
}
