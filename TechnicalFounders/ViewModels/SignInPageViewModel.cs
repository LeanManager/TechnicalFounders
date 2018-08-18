using System;
using System.Threading.Tasks;
using System.Windows.Input;
using TechnicalFounders.Models;
using TechnicalFounders.Utilities;
using TechnicalFounders.Views;
using Xamarin.Forms;

namespace TechnicalFounders.ViewModels
{
    public class SignInPageViewModel : BaseViewModel
    {
        public SignInPageViewModel()
        {
            SignInCommand = new Command(SignIn, CanExecute);
            NavigateToSignUpCommand = new Command(NavigateToSignUpPage, CanExecute);
        }

        private bool CanExecute()
        {
            if (IsBusy == true)
                return false;
            
            return true;
        }

        public ICommand SignInCommand { private set; get; }
        public ICommand NavigateToSignUpCommand { private set; get; }

        private async void SignIn()
        {
            IsBusy = true;
            RaiseCanExecutes();

            try
            {
                await ValidateLoginCredentials();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                RaiseCanExecutes();
            }
        }

        private async Task ValidateLoginCredentials()
        {
            if (String.IsNullOrEmpty(EmailAddress) || String.IsNullOrEmpty(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please provide the required login information.", "OK");
                return;
            }

            // Check SQLite database first (secured local data) only (for now - fast portfolio reasons)
            // LATER - If not found, check SQL Server (check for network connectivity)
            // SignInPage is only relevant once this is in place.

            await LookUpUserInDatabase();
        }

        private async Task LookUpUserInDatabase()
        {
            User user = await App.UserInformation.GetUserAsync();

            if (user == null || user.EmailAddress.Length <= 1 || user.Password.Length <= 1
                || string.IsNullOrEmpty(user.EmailAddress) || string.IsNullOrEmpty(user.Password))
            {
                await Application.Current.MainPage.DisplayAlert("Credentials not found", "Please sign up before signing in.", "OK");
                return;
            }
            if (EmailAddress.ToLower() == user.EmailAddress.ToLower() && Password.ToLower() == user.Password.ToLower())
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(new MainPage());

                MessagingCenter.Send(new DisableBackButtonMessage(), "DisableBackButton");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please provide your correct email address and password.", "OK");
            }
        }

        private async void NavigateToSignUpPage()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new SignUpPage());
        }

        private void RaiseCanExecutes()
        {
            ((Command)SignInCommand).ChangeCanExecute();
            ((Command)NavigateToSignUpCommand).ChangeCanExecute();
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
