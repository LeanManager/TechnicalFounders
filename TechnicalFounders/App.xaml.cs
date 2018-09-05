using System;
using Xamarin.Forms;
using TechnicalFounders.Services;
using TechnicalFounders.Views;
using Xamarin.Forms.Xaml;
using TechnicalFounders.Models;
using System.Diagnostics;
using Xamarin.Auth;
using TechnicalFounders.Utilities;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TechnicalFounders
{
    public partial class App : Application
    {
        public static string AzureBackendUrl = "https://*.azurewebsites.net";
        public static bool UseMockDataStore = false;

        public static Repo Repository;
        public static UserInfo UserInformation;

        AccountManager accountManager = new AccountManager();

        public bool IsLoggedIn { get; set; } = false;

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

            CheckStoreForUserInformation();
        }

        // Warning: Doing more than one thing - fix
        private void CheckStoreForUserInformation()
        {
            Account account = accountManager.CheckForAccount();

            IsLoggedIn = account != null && !string.IsNullOrEmpty(account.Username);

            SetMainPage();
        }

        private void SetMainPage()
        {
            if (IsLoggedIn == true)
                MainPage = new MainPage();
            else
                MainPage = new SignInPage();
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
