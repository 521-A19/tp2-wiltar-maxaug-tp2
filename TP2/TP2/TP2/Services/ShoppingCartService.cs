using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TP2.Models.Entities;

namespace TP2.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private List<Dog> _shoppingCartDogList;
        public ShoppingCartService()
        {
            TotalPrice = 0;
            _shoppingCartDogList = new List<Dog>();
        }

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

        public bool Contains(int id)
        {
            foreach (Dog cur in ShoppingCartDogList)
            {
                if(cur.Id == id)
                {
                    return true;
                }
            }
            return false;
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
