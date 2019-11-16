using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TP2.Validation.Rules
{
    public class ContainAtLeastOneUpercaseLetter<T> : IValidationRule<T>
    {
        public string ErrorMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }
            var hasUpperChar = new Regex(@"[A-Z]+");
            var str = value as string;
            return hasUpperChar.IsMatch(str);
        }
    }
}
