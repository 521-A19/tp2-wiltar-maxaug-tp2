using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TP2.Validation.Rules
{
    public class ContainAtLeastOneLowercaseLetter<T> : IValidationRule<T>
    {
        public string ErrorMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }
            var hasLowerChar = new Regex(@"[a-z]+");
            var str = value as string;
            return hasLowerChar.IsMatch(str);
        }
    }
}
