using System;
using System.Collections.Generic;
using System.Text;

namespace TP2.Validation
{
    public interface IValidationRule<T>
    {
        string ErrorMessage { get; set; }

        bool Check(T value);
    }
}
