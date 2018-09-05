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
        public AccountManager accountManager;

        public SignInPageViewModel()
        {
            SignInCommand = new Command(SignIn, CanExecute);
            NavigateToSignUpCommand = new Command(NavigateToSignUpPage, CanExecute);
            ForgotPasswordCommand = new Command(ForgotPasswordAction, CanExecute);

            accountManager = new AccountManager();
        }

        public ICommand SignInCommand { private set; get; }
        public ICommand NavigateToSignUpCommand { private set; get; }
        public ICommand ForgotPasswordCommand { private set; get; }

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
            // TODO: SQL Server user information lookup (optional; requires network connectivity).

            // Xamarin.Auth is more persistent than SQLite.
            // EmailAddress is Xamarin.Auth account "username"
            bool success = accountManager.LoginToAccount(EmailAddress.ToLower(), Password);

            if (success == true)
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(new MainPage());

                MessagingCenter.Send(new DisableBackButtonMessage(), "DisableBackButton");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Unable to sign in. Please check your email address and password.", "OK");
            }
        }

        private async void NavigateToSignUpPage()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new SignUpPage());
        }

        private async void ForgotPasswordAction()
        {
            IsBusy = true;
            RaiseCanExecutes();

            try
            {
                // Entry DisplayAlert asking for email address,
                // Validation, etc...

                await Application.Current.MainPage.DisplayAlert("Coming soon!", "Feature pending implementation.", "OK");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
                RaiseCanExecutes();
            }
        }

        private bool CanExecute()
        {
            if (IsBusy == true)
                return false;

            return true;
        }

        private void RaiseCanExecutes()
        {
            ((Command)SignInCommand).ChangeCanExecute();
            ((Command)NavigateToSignUpCommand).ChangeCanExecute();
            ((Command)ForgotPasswordCommand).ChangeCanExecute();
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
