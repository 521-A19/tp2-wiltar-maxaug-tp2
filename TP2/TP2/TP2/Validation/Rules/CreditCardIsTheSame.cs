using System;
using System.Collections.Generic;
using System.Text;

namespace TP2.Validation.Rules
{
    public class CreditCardIsTheSame<T> : IValidationRule<T>
    {
        private string _creditCard;
        public string ErrorMessage { get; set; }

        public CreditCardIsTheSame(string creditCard){
            _creditCard = creditCard;
        }

    public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }
            var str = value as string;
            if (_creditCard == str)
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
