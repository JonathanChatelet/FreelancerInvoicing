using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using FreelancerInvoicing.Models.Entities;
using System.Reflection.Metadata;

namespace FreelancerInvoicing.Services.Users.Tools
{
    public static class UserValidator
    {

        public static void ValidateUserParameterMaxLength(User user)
        {
            ValidateStringLength(user.Name, 50, "Name");
            ValidateStringLength(user.Address, 300, "Address");
            ValidateStringLength(user.Email, 80, "Email");
            ValidateStringLength(user.Siret, 14, "Siret");
            ValidateStringLength(user.Iban, 34, "IBAN");
            ValidateStringLength(user.Swift, 11, "Swift");
        }
        private static void ValidateStringLength(String str, int maxLength, String parameter)
        {
            if(str.Length > maxLength) 
            {
                throw new ArgumentException($"{parameter} cannot exceed {maxLength} characters.");
            }
        }

        public static void ValidateIfUserEmailNotAlreadyUsed (IEnumerable<User> users, String toSearch, String parameterName)
        {
            var parameter = typeof(User).GetProperty(parameterName);
            foreach (User user in users) 
            {
                if(user.parameter.Equals(email))
                {
                    
                }
            }
        }

    }
}
