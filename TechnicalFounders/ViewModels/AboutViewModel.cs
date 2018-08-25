using System;
using System.Windows.Input;
using TechnicalFounders.Models;
using TechnicalFounders.Views;
using Xamarin.Forms;

namespace TechnicalFounders.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://xamarin.com/platform")));

            SignOutCommand = new Command(async () => 
            {
                bool result = await Application.Current.MainPage.DisplayAlert("Sign Out", "Are you sure you want to sign out?", "Cancel", "OK");

                if (result == false)
                    Application.Current.MainPage = new SignInPage();
            });

            GetUserInformation();
        }

        private async void GetUserInformation()
        {
            User user = await App.UserInformation.GetUserAsync();

            if (user == null || user.EmailAddress.Length <= 1 || user.Password.Length <= 1
                || string.IsNullOrEmpty(user.EmailAddress) || string.IsNullOrEmpty(user.Password))
            {
                UserName = "User name not found";
                EmailAddress = "Email address not found";
                Password = "Password not found";
            }
            else
            {
                UserName = user.UserName;
                EmailAddress = user.EmailAddress;
                Password = $"{user.Password} (cryptographic encryption required)";
            }
        }

        public ICommand OpenWebCommand { get; }
        public ICommand SignOutCommand { get; }

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