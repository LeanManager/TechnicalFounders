using System;
using System.Collections.Generic;
using TechnicalFounders.ViewModels;
using Xamarin.Forms;

namespace TechnicalFounders.Views
{
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();

            BindingContext = new SignUpPageViewModel();

            if (Device.RuntimePlatform == Device.iOS)
            {
                var closeButton = new Button
                {
                    Text = "X",
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Color.DarkRed,
                    FontSize = 22,
                    HorizontalOptions = LayoutOptions.End
                };
                closeButton.SetBinding(Button.CommandProperty, "GoBackCommand", BindingMode.TwoWay);
                signUpGrid.Children.Add(closeButton, 1, 0);
            }
        }

        bool IsValid()
        {
            var userNameValidation = ValidateUserName();
            var emailValidation = ValidateEmail();
            var passwordValidation = ValidatePassword();

            return userNameValidation && emailValidation && passwordValidation;
        }

        void UserName_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            ValidateUserName();
            ((SignUpPageViewModel)BindingContext).IsValid = IsValid();
        }

        void Email_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            ValidateEmail();
            ((SignUpPageViewModel)BindingContext).IsValid = IsValid();
        }

        void Password_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            ValidatePassword();
            ((SignUpPageViewModel)BindingContext).IsValid = IsValid();
        }

        private bool ValidateUserName()
        {
            var isValid = (userNameEntry.Text ?? "").Length > 0;

            // Set visual state
            var state = isValid ? "Valid" : "Invalid";
            VisualStateManager.GoToState(userNameEntry, state);
            VisualStateManager.GoToState(userNameValidationLabel, state);

            return isValid;
        }

        private bool ValidateEmail()
        {
            bool isValid = IsValidEmail(emailEntry.Text);

            // Set visual state
            var state = isValid ? "Valid" : "Invalid";
            VisualStateManager.GoToState(emailEntry, state);
            VisualStateManager.GoToState(emailValidationLabel, state);

            return isValid;
        }

        private bool ValidatePassword()
        {
            var passwordLength = (passwordEntry.Text ?? "").Length;

            bool isValid = passwordLength >= 6;

            // Set visual state
            var state = isValid ? "Valid" : "Invalid";
            VisualStateManager.GoToState(passwordEntry, state);

            // Check password strength
            string strengthState = "Invalid";

            if (passwordLength >= 10)
                strengthState = "Strong";
            else if (passwordLength >= 6)
                strengthState = "Weak";
            
            VisualStateManager.GoToState(passwordValidationLabel, strengthState);

            return isValid;
        }

        bool IsValidEmail(string email)
        {
            try
            {
                var address = new System.Net.Mail.MailAddress(email);
                return address.Address == email;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return false;
            }
        }
    }
}
