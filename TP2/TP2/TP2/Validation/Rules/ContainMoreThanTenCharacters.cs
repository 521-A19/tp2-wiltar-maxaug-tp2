using System;
using System.Collections.Generic;
using System.Text;

namespace TP2.Validation.Rules
{
    public class ContainMoreThanTenCharacters<T> : IValidationRule<T>
    {
        const int MIN_LENGHT = 10;
        public string ErrorMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }
            var str = value as string;
            var hasMoreThanTenCharacters = str.Length > MIN_LENGHT;
            return hasMoreThanTenCharacters;
        }
    }
}
