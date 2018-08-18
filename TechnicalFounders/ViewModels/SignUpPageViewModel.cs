using System;
using System.Threading.Tasks;
using System.Windows.Input;
using TechnicalFounders.Models;
using TechnicalFounders.Utilities;
using TechnicalFounders.Views;
using Xamarin.Forms;

namespace TechnicalFounders.ViewModels
{
    public class SignUpPageViewModel : BaseViewModel
    {
        public SignUpPageViewModel()
        {
            CreateAccountCommand = new Command<bool>(CreateAccount);
            GoBackCommand = new Command(GoBack);
        }

        private async void CreateAccount(bool canCreate)
        {
            if (canCreate == true)
            {
                try
                {
                    await ProcessRegistration();
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please enter the required information.", "OK");
            }
        }

        private async Task ProcessRegistration()
        {
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                EmailAddress = EmailAddress,
                Password = Password,
                UserName = UserName
            };

            bool succeeded = await App.UserInformation.AddUserAsync(user);

            if (succeeded == true)
            {
                await Application.Current.MainPage.DisplayAlert("Success!", "Your account has been created.", "OK");

                await Application.Current.MainPage.Navigation.PushModalAsync(new MainPage());

                MessagingCenter.Send(new DisableBackButtonMessage(), "DisableBackButton");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "An error occurred while creating your account. Please try again.", "OK");
            }
        }

        private async void GoBack()
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }

        public ICommand CreateAccountCommand { private set; get; }
        public ICommand GoBackCommand { private set; get; }

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

        private bool isValid;
        public bool IsValid
        {
            get => isValid;
            set
            {
                if (SetProperty(ref isValid, value))
                {

                }
            }
        }
    }
}
