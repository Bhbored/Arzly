using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace Arzly.Shared.DTOs.Request.Auth
{
    public class RegisterDTO
    {

        [Required(ErrorMessage = "Name can't be blank")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number can't be blank")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone number should contain digits only")]
        public string PhoneNumber { get; set; } = string.Empty;


        [Required(ErrorMessage = "Email can't be blank")]
        [EmailAddress(ErrorMessage = "Email should be in a proper email address format")]
        public string Email { get; set; } = string.Empty;


        [Required(ErrorMessage = "Password can't be blank")]
        public string Password { get; set; } = string.Empty;


        
    }
}
