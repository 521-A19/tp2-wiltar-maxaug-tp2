using SQLiteNetExtensions.Attributes;
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
        public int DogId { get; set; }

        /*
        [TextBlob("UserDogListBlobbed")] //SQL ne prend pas en charge List<Object> sans l'extension
        public List<Dog> UserDogList { get; set; }
        public string UserDogListBlobbed { get; set; }*/
    }
}