﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TP2.Models.Entities;

namespace TP2.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<Dog> _dogRepository;
        private List<Dog> _shoppingCartDogList;
        public ShoppingCartService(IRepository<Dog> dogRepository)
        {
            _dogRepository = dogRepository;
            SetNewEmptyShoppingCart();
            /* Seeder pour tester
            _shoppingCartDogList  = new List<Dog>()
            {
                new Dog()
                {
                    Id= 1,
                    Name = "Rex",
                    Description = "fsdkfdsfdfd",
                    ImageUrl = "url",
                    Price = (float)299.99,
                    Race = "Husky",
                    Sex = "Male"
                },
                new Dog()
                {
                    Id= 2,
                    Name = "Cloud",
                    Description = "fsdkfdsfdfd",
                    ImageUrl = "url",
                    Price = (float)199.99,
                    Race = "Samo",
                    Sex = "Male"
                }
            };
            TotalPrice = _shoppingCartDogList[0].Price + _shoppingCartDogList[1].Price;*/
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

        public void SetNewEmptyShoppingCart()
        {
            //_shoppingCartDogList.RemoveAll();
            _shoppingCartDogList = new List<Dog>();
            TotalPrice = 0;
        }

        public void BuyShoppingCart()
        {
            foreach (Dog cur in ShoppingCartDogList)
            {
                _dogRepository.Delete(cur);
            }
            SetNewEmptyShoppingCart();
        }
    }
}
