using System;
using Xamarin.Forms;
using TechnicalFounders.Services;
using TechnicalFounders.Views;
using Xamarin.Forms.Xaml;
using TechnicalFounders.Models;
using System.Diagnostics;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TechnicalFounders
{
    public partial class App : Application
    {
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        public static string AzureBackendUrl = "*.azurewebsites.net";
        public static bool UseMockDataStore = false;

        public static Repo Repository;
        public static UserInfo UserInformation;

        public App(string dbPath)
        {
            InitializeComponent();

            if (UseMockDataStore == true)
                DependencyService.Register<MockDataStore>();
            else
                DependencyService.Register<AzureDataStore>();

            // Log database path and file name
            Debug.WriteLine($"Database located at: {dbPath}");

            Repository = new Repo(dbPath);
            UserInformation = new UserInfo(dbPath);

            CheckLocalDatabaseForUserInformation();
        }

        // Warning: Doing more than one thing - fix
        private async void CheckLocalDatabaseForUserInformation()
        {
            User user = await App.UserInformation.GetUserAsync();

            if (user == null || user.EmailAddress.Length <= 1 || user.Password.Length <= 1 
                || string.IsNullOrEmpty(user.EmailAddress) || string.IsNullOrEmpty(user.Password))
            {
                IsLoggedIn = false;
            }
            else
            {
                IsLoggedIn = true;
            }

            SetMainPage();
        }

        private void SetMainPage()
        {
            if (IsLoggedIn == true)
                MainPage = new MainPage();
            else
                MainPage = new SignInPage();
        }

        private bool isLoggedIn = false;
        public bool IsLoggedIn
        {
            get => isLoggedIn;
            set => isLoggedIn = value;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
