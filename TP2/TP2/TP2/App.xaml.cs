﻿using Prism;
using Prism.Ioc;
using TP2.ViewModels;
using TP2.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;
using TP2.Services;
using System.IO;
using SQLite;
using System.Linq;
using TP2.Models.Entities;
using Xamarin.Essentials;
using System.Collections.Generic;
using TP2.Views.MasterDetailPages;
using TP2.ViewModels.MasterDetailViews;
using TP2.Externalization;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TP2
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        private string _datbaseFileLocation;

        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            SeedTestData();

            //await NavigationService.NavigateAsync("/NavigationPage/CustomMasterDetailPage/MainPage");
            //await NavigationService.NavigateAsync(new System.Uri("/NavigationPage/CustomMasterDetailPage/MainPage", System.UriKind.Absolute));
            await NavigationService.NavigateAsync(new System.Uri("/CustomMasterDetailPage/NavigationPage/MainPage", System.UriKind.Absolute));
            //await NavigationService.NavigateAsync("NavigationPage/CustomMasterDetailPage");
        }

        private void SeedTestData()
        {
            var dogsRepository = Container.Resolve<IRepository<Dog>>();
            dogsRepository.DeleteAll(); // **** CLEAN BD ****
            // Les données seront ajoutées une seul foi dans la BD. 
            if (dogsRepository.GetAll().Count() != 0)
                return;

            var dog1 = new Dog()
            {
                Name = UiText.ANY_DOG_NAME,
                ImageUrl = UiText.ANY_DOG_IMAGE_URL,
                Price = UiText.ANY_DOG_PRICE,
                Race = UiText.ANY_DOG_RACE,
                Sex = UiText.ANY_DOG_SEX,
                Description = UiText.ANY_DOG_DESCRIPTION
            };
            var dog2 = new Dog()
            {
                Name = "Cloud",
                ImageUrl = "https://images.dog.ceo/breeds/shiba/shiba-11.jpg",
                Price = (float)399.99,
                Race = "Samoyede",
                Sex = "Male",
                Description = "13 mois, chien blanc"

            };
            var dog3 = new Dog()
            {
                Name = "Leo",
                ImageUrl = "https://images.dog.ceo/breeds/pug/n02110958_1975.jpg",
                Price = (float)269.99,
                Race = "Husky",
                Sex = "Male",
                Description = "Gentil et calme"

            };
            dogsRepository.Add(dog1);
            dogsRepository.Add(dog2);
            dogsRepository.Add(dog3);

            var usersRepository = Container.Resolve<IRepository<User>>();
            usersRepository.DeleteAll(); // **** CLEAN BD ****
            // Les données seront ajoutées une seul foi dans la BD. 
            if (usersRepository.GetAll().Count() != 0)
                return;

            ICryptoService cryptoService = new CryptoService();
            ISecureStorageService secureStorageService = new SecureStorageService();
            string salt = cryptoService.GenerateSalt();
            string key = cryptoService.GenerateEncryptionKey();
 
            var user1 = new User()
            {
                Login = "123",
                HashedPassword = cryptoService.HashSHA512("456", salt),
                PasswordSalt = salt,
                CreditCard = cryptoService.Encrypt("1234", key),
                DogId = dog1.Id
            };
            secureStorageService.SetUserEncryptionKeyAsync(user1, key);
            usersRepository.Add(user1);   // après le add, user1 contient un id

            salt = cryptoService.GenerateSalt();
            key = cryptoService.GenerateEncryptionKey();

            var user2 = new User()
            {
                Login = "456",
                HashedPassword = cryptoService.HashSHA512("789", salt),
                PasswordSalt = salt,
                CreditCard = cryptoService.Encrypt("1234", key),
                DogId = -1
            };
            secureStorageService.SetUserEncryptionKeyAsync(user2, key);
            usersRepository.Add(user2);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Navigation pages
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<CustomMasterDetailPage, CustomMasterDetailViewModel>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<DogsListPage, DogsListViewModel>();
            containerRegistry.RegisterForNavigation<DogDetailPage, DogDetailViewModel>();
            containerRegistry.RegisterForNavigation<DogShopPage, DogShopViewModel>();
            containerRegistry.RegisterForNavigation<RegisterPage, RegisterPageViewModel>();
            containerRegistry.RegisterForNavigation<AddNewDogPage, AddNewDogViewModel>();
            containerRegistry.RegisterForNavigation<UserProfilePage, UserProfileViewModel>();
            containerRegistry.RegisterForNavigation<ShoppingCartPage, ShoppingCartViewModel>();

            //Repositories
            var databasePath = FileSystem.AppDataDirectory;  // FileSystem voir https://docs.microsoft.com/en-us/xamarin/essentials/file-system-helpers?tabs=android
            var databaseFileName = "Database.db3";
            _datbaseFileLocation = Path.Combine(databasePath, databaseFileName);
            var databaseSqLiteConnection = new SQLiteConnection(_datbaseFileLocation); // SQLiteConnection voir:https://docs.microsoft.com/en-us/xamarin/android/data-cloud/data-access/using-sqlite-orm

            containerRegistry.RegisterInstance(databaseSqLiteConnection);
            containerRegistry.RegisterSingleton<IRepository<User>, SqLiteRepository<User>>();
            containerRegistry.RegisterSingleton<IRepository<Dog>, SqLiteRepository<Dog>>();

            //Services
            containerRegistry.RegisterSingleton<ICryptoService, CryptoService>();
            containerRegistry.RegisterSingleton<ISecureStorageService, SecureStorageService>();
            containerRegistry.RegisterSingleton<IRegistrationService, RegistrationService>();
            containerRegistry.RegisterSingleton<IAuthenticationService, AuthenticationService>();
            containerRegistry.RegisterSingleton<IDogApiService, DogApiService>();
            containerRegistry.RegisterSingleton<IShoppingCartService, ShoppingCartService>();
        }
    }
}
