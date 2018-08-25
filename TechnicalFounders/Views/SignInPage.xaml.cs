using System;
using System.Collections.Generic;
using System.Reflection;
using TechnicalFounders.CustomElements;
using TechnicalFounders.Effects;
using TechnicalFounders.ViewModels;
using Xamarin.Forms;

namespace TechnicalFounders.Views
{
    public partial class SignInPage : ContentPage
    {
        #region Views - class level fields
        Image backgroundImage;
        ScrollView scrollView;
        Grid grid;
        Label titleLabel;
        Entry emailAddressEntry;
        Entry passwordEntry;
        Button signInButton;
        Button forgotPasswordButton;
        BoxView separator;
        Button signUpButton;
        #endregion

        public SignInPage()
        {
            InitializeComponent();

            BindingContext = new SignInPageViewModel();

            CreateGrid();
            CreateViews();
            AddViewsToGrid();
            CreateContentLayout();
        }

        private void CreateGrid()
        {
            grid = new Grid
            {
                RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = 50 },
                    new RowDefinition { Height = 50 },
                    new RowDefinition { Height = 60 },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) }
                },
                Margin = new Thickness(40, 35, 40, 30),
                RowSpacing = 20
            };
        }

        private void CreateViews()
        {
            backgroundImage = new Image
            {
                Source = ImageSource.FromResource("TechnicalFounders.Images.dallasbackground.jpg", 
                                                  typeof(SignInPage).GetTypeInfo().Assembly),
                Aspect = Aspect.AspectFill,
                Opacity = 0.8
            };

            titleLabel = new Label
            {
                BackgroundColor = Color.Transparent,
                Text = "Technical Founders",
                FontSize = 50,
                TextColor = Color.White,
                HorizontalTextAlignment = TextAlignment.Center,
                HeightRequest = 100,
                Margin = new Thickness(0, 30, 0, 0),
                FontFamily = Device.RuntimePlatform == Device.iOS ? "Rancho-Regular" : "Rancho-Regular.ttf#Rancho-Regular"
            };

            // Also add corresponding image to the left
            emailAddressEntry = new CustomEntry
            {
                Placeholder = "Email Address",
                HeightRequest = 50
            };
            emailAddressEntry.SetBinding(Entry.TextProperty, "EmailAddress");
            emailAddressEntry.Effects.Add(new CaseTextEffect());

            // Also add corresponding image to the left
            passwordEntry = new CustomEntry
            {
                Placeholder = "Password",
                IsPassword = true,
                HeightRequest = 50
            };
            passwordEntry.SetBinding(Entry.TextProperty, "Password");
            passwordEntry.Effects.Add(new CaseTextEffect());

            signInButton = new Button
            {
                Text = "Sign In",
                CornerRadius = 10,
                TextColor = Color.White,
                FontSize = 18,
                FontAttributes = FontAttributes.Bold,
                BackgroundColor = Color.FromHex("#b40000"),
                HorizontalOptions = LayoutOptions.Fill,
                Margin = new Thickness(40, 10, 40, 0)
            };
            signInButton.SetBinding(Button.CommandProperty, "SignInCommand");
            signInButton.Effects.Add(new CaseTextEffect());

            forgotPasswordButton = new Button
            {
                Text = "Forgot password?",
                Style = Resources["smallButtonStyle"] as Style
            };
            forgotPasswordButton.SetBinding(Button.CommandProperty, "ForgotPasswordCommand");
            forgotPasswordButton.Effects.Add(new CaseTextEffect());

            separator = new BoxView
            {
                Color = Color.White,
                BackgroundColor = Color.White,
                HeightRequest = 1,
                HorizontalOptions = LayoutOptions.Fill
            };

            signUpButton = new Button
            {
                Text = "New here? Sign Up",
                VerticalOptions = LayoutOptions.Center,
                Style = Resources["smallButtonStyle"] as Style
            };
            signUpButton.SetBinding(Button.CommandProperty, "NavigateToSignUpCommand");
            signUpButton.Effects.Add(new CaseTextEffect());
        }

        private void AddViewsToGrid()
        {
            grid.Children.Add(titleLabel, 0, 0);
            grid.Children.Add(emailAddressEntry, 0, 1);
            grid.Children.Add(passwordEntry, 0, 2);
            grid.Children.Add(signInButton, 0, 3);
            grid.Children.Add(forgotPasswordButton, 0, 4);
            grid.Children.Add(separator, 0, 6);
            grid.Children.Add(signUpButton, 0, 7);
        }

        private void CreateContentLayout()
        {
            scrollView = new ScrollView
            {
                Content = grid
            };

            this.Content = new AbsoluteLayout
            {
                Children =
                {
                    { backgroundImage, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All },
                    { scrollView, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All }
                }
            };
        }
    }
}
