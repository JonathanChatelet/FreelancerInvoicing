using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancerInvoicing.DTO.Users
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "ID is required.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Email format is invalid.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must contain at least 8 characters.")]
        [RegularExpression(@" ^ (?=.* [^A - Za - z0 - 9])\S + $", ErrorMessage = "Password must contain at least one spacial caracter and no space.")]
        public string Password { get; set; } = string.Empty;
    }
}
