using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TP2.Models.Entities;

namespace TP2.Services
{
    public interface IShoppingCartService
    {
        List<Dog> ShoppingCartDogList { get; }
        float TotalPrice { get; }
        void AddDogToTheShoppingCart(Dog dogToAdd);
        void RemoveDogFromTheShoppingCart(Dog dogToRemove);
        Collection<Dog> GetAllDogsFromShoppingCart();
    }
}
