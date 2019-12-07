using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace TP2.Validation.Rules
{
    public class IsValidEmailAddress<T> : IValidationRule<T>
    {
        public string ErrorMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }
            var str = value as string;
            return IsValidEmail(str);
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
