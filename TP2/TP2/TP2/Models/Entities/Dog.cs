using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace TP2.Models.Entities
{
    public class Dog : Entity
    {
        public string Name { get; set; }
        public string Race { get; set; }
        public string Sex { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public float Price { get; set; }
    }
}