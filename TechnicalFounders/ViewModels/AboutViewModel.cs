using System;
using System.Windows.Input;
using TechnicalFounders.Models;
using TechnicalFounders.Utilities;
using TechnicalFounders.Views;
using Xamarin.Auth;
using Xamarin.Forms;

namespace TechnicalFounders.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        AccountManager accountManager;

        public AboutViewModel()
        {
            Title = "About";

            accountManager = new AccountManager();

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://xamarin.com/platform")));

            SignOutCommand = new Command(async () => 
            {
                bool result = await Application.Current.MainPage.DisplayAlert("Sign Out", "Are you sure you want to sign out?", "Cancel", "OK");

                if (result == false)
                    Application.Current.MainPage = new SignInPage();
            });

            AnimationsCommand = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(new AnimationsPage());
            });

            GetUserInformation();
        }

        private void GetUserInformation()
        {
            Account account = accountManager.CheckForAccount();

            if (account != null)
            {
                UserName = $"{account.Properties["username"]}";
                EmailAddress = account.Username;
                Password = $"{account.Properties["password"]} (cryptographic encryption)";
            }
            else
            {
                UserName = "Cannot be found.";
                EmailAddress = "Cannot be found.";
                Password = "Cannot be found.";
            }
        }

        public ICommand OpenWebCommand { get; }
        public ICommand SignOutCommand { get; }
        public ICommand AnimationsCommand { get; }

        private string userName;
        public string UserName
        {
            get => userName;
            set
            {
                if (SetProperty(ref userName, value))
                {

                }
            }
        }

        private string emailAddress;
        public string EmailAddress
        {
            get => emailAddress;
            set
            {
                if (SetProperty(ref emailAddress, value))
                {

                }
            }
        }

        private string password;
        public string Password
        {
            get => password;
            set
            {
                if (SetProperty(ref password, value))
                {

                }
            }
        }

    }
}