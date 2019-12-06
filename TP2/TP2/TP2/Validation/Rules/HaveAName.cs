using System;
using System.Collections.Generic;
using System.Text;

namespace TP2.Validation.Rules
{
    public class HaveAName<T> : IValidationRule<T>
    {
        public string ErrorMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }
            return true;
        }
    }
}
