using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TP2.Validation.Rules
{
    public class ContainAtLeastOneNumericCharacter<T> : IValidationRule<T>
    {
        public string ErrorMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }
            var hasNumber = new Regex(@"[0-9]+");
            var str = value as string;
            return hasNumber.IsMatch(str);
        }
    }
}
