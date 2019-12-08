using System;
using System.Collections.Generic;
using System.Text;

namespace TP2.Validation.Rules
{
    public class PasswordIsTheSame<T> : IValidationRule<T>
    {
        private string _lastPassword;
        public string ErrorMessage { get; set; }

        public PasswordIsTheSame(string lastPassword){
            _lastPassword = lastPassword;
        }

    public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }
            var str = value as string;
            if (_lastPassword == str)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
