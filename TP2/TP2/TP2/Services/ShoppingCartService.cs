using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TP2.Models.Entities;

namespace TP2.Services
{
    public class ShoppingCartService : IShoppingCartService
    {        
        public ShoppingCartService()
        {
            TotalPrice = 0;
        }

        private List<Dog> _shoppingCartDogList = new List<Dog>()
        {
            new Dog()
            {
                Id= 10,
                Name = "Snoopy",
                Description = "Good boy",
                ImageUrl = "Url",
                Price = (float)299.99,
                Race = "Husky",
                Sex = "Male"
            }
        };
        public List<Dog> ShoppingCartDogList
        {
            get { return _shoppingCartDogList; }
        }

        private float _totalPrice;
        public float TotalPrice
        {
            get { return _totalPrice; }
            set
            {
                _totalPrice = value;
            }
        }

        public void AddDogToTheShoppingCart(Dog dogToAdd)
        {
            ShoppingCartDogList.Add(dogToAdd);
            TotalPrice += dogToAdd.Price;
        }

        public void RemoveDogFromTheShoppingCart(Dog dogToRemove) {
            ShoppingCartDogList.Remove(dogToRemove);
            TotalPrice -= dogToRemove.Price;
        }

        public Collection<Dog> GetAllDogsFromShoppingCart()
        {
            return new Collection<Dog>(_shoppingCartDogList);
        }
    }
}
