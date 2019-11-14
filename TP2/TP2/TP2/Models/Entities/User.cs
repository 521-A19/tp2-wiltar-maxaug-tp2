using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace TP2.Models.Entities
{
    public class User : Entity
    {
        public string Login { get; set; }
        public string HashedPassword { get; set; }
        public string PasswordSalt { get; set; }
        public string CreditCard { get; set; }
    }
}