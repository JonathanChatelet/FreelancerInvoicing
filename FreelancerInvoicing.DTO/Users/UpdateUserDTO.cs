using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancerInvoicing.DTO.Users
{
    public class UpdateUserDTO
    {
        [MaxLength(50, ErrorMessage = "Name must be less than 50 caracters")]
        public string? Name { get; set; }

        [MaxLength(300, ErrorMessage = "Adress must be less than 300 caracters")]
        public string? Address { get; set; }

        [RegularExpression(@"^\d{14}$", ErrorMessage = "SIRET must contain exactly 14 digits.")]
        public string? Siret { get; set; }

        [MaxLength(34, ErrorMessage = "IBAN must be less than 34 caracters")]
        public string? Iban { get; set; }

        [MaxLength(11, ErrorMessage = "SWIFT must be less than 11 caracters")]
        public string? Swift { get; set; }
    }
}
