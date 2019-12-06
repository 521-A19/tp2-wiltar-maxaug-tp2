using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TP2.Validation.Rules
{
    public class IsBiggerThanZero<T> : IValidationRule<T>
    {
        public string ErrorMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }
            var str = value.ToString();
            if (str == "0")
            {
                return false;
            }

            Regex reg = new Regex(@"^-\d*\.?\d+$");
            return !reg.IsMatch(str);
        }
    }
}
