using Prism;
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

            await NavigationService.NavigateAsync("NavigationPage/LoginPage");
        }

        private void SeedTestData()
        {
            var productsRepository = Container.Resolve<IRepository<User>>();
            productsRepository.DeleteAll(); // **** CLEAN BD ****
            // Les données seront ajoutées une seul foi dans la BD. 
            if (productsRepository.GetAll().Count() != 0)
                return;

            //ICryptoService cryptoService = new CryptoService();
            //ISecureStorageService secureStorageService = new SecureStorageService();
            //string salt = cryptoService.GenerateSalt();
            //string key = cryptoService.GenerateEncryptionKey();
            /*var user1 = new User()
            {
                Login = "123",
                HashedPassword = cryptoService.HashSHA512("456", salt),
                PasswordSalt = salt,
                CreditCard = cryptoService.Encrypt("5162042483342023", key)
            };*/
            //secureStorageService.SetUserEncryptionKeyAsync(user1, key);
            //productsRepository.Add(user1);   // après le add, product1 contient un id
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Navigation pages
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();

            //Repositories
            var databasePath = FileSystem.AppDataDirectory;  // FileSystem voir https://docs.microsoft.com/en-us/xamarin/essentials/file-system-helpers?tabs=android
            var databaseFileName = "Database.db3";
            _datbaseFileLocation = Path.Combine(databasePath, databaseFileName);
            var databaseSqLiteConnection = new SQLiteConnection(_datbaseFileLocation); // SQLiteConnection voir:https://docs.microsoft.com/en-us/xamarin/android/data-cloud/data-access/using-sqlite-orm

            containerRegistry.RegisterInstance(databaseSqLiteConnection);
            containerRegistry.RegisterSingleton<IRepository<User>, SqLiteRepository<User>>();

            //Services PARTOUT DANS L'APP
            //containerRegistry.RegisterSingleton<IAuthenticationService, AuthenticationService>();
        }
    }
}
