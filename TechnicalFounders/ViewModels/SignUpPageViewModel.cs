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
        private AccountManager accountManager;

        public SignUpPageViewModel()
        {
            CreateAccountCommand = new Command<bool>(async (canCreate) => await CreateAccount(canCreate));
            GoBackCommand = new Command(async () => await Application.Current.MainPage.Navigation.PopModalAsync());

            accountManager = new AccountManager();
        }

        private async Task CreateAccount(bool canCreate)
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
            // EmailAddress is Xamarin.Auth account "Username"
            bool succeeded = accountManager.CreateAndSaveAccount(EmailAddress.ToLower(), Password, UserName, Guid.NewGuid().ToString());

            if (succeeded == true)
            {
                await Application.Current.MainPage.DisplayAlert("Success!", "Your account has been created.", "OK");

                await Application.Current.MainPage.Navigation.PushModalAsync(new MainPage());

                MessagingCenter.Send(new DisableBackButtonMessage(), "DisableBackButton");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "An account already exists for this email address.", "OK");
            }
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
