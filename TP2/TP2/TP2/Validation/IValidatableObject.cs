using System;
using System.Collections.Generic;
using System.Text;

namespace TP2.Validation
{
    public interface IValidity<T>
    {
        List<string> Errors { get; }
        T Value { get; set; }
        bool IsValid { get; }
        void Validate();
        void AddValidationRule(IValidationRule<T> validationRule);
    }
}
