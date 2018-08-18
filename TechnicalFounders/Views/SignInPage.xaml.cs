using System;
using System.Collections.Generic;
using System.Reflection;
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
        Image logoImage;
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
                    new RowDefinition { Height = 100 },
                    new RowDefinition { Height = 110 },
                    new RowDefinition { Height = 40 },
                    new RowDefinition { Height = 50 },
                    new RowDefinition { Height = new GridLength (1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength (1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength (1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength (1, GridUnitType.Auto) }
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
                Aspect = Aspect.AspectFill
            };

            logoImage = new Image
            {
                Source = ImageSource.FromResource("TechnicalFounders.Images.atomlogo.jpg",
                                                  typeof(SignInPage).GetTypeInfo().Assembly),
                Aspect = Aspect.AspectFit,
                HorizontalOptions = LayoutOptions.Center
            };

            // Also add corresponding image to the left
            emailAddressEntry = new Entry
            {
                Placeholder = "Email Address",
                Margin = new Thickness(0, 70, 0, 0)
            };
            emailAddressEntry.SetBinding(Entry.TextProperty, "EmailAddress");

            // Also add corresponding image to the left
            passwordEntry = new Entry
            {
                Placeholder = "Password",
                IsPassword = true
            };
            passwordEntry.SetBinding(Entry.TextProperty, "Password");

            signInButton = new Button
            {
                Text = "Sign In",
                CornerRadius = 10,
                TextColor = Color.White,
                FontSize = 18,
                FontAttributes = FontAttributes.Bold,
                BackgroundColor = Color.IndianRed,
                HorizontalOptions = LayoutOptions.Fill,
                Margin = new Thickness(40, 0, 40, 0)
            };
            signInButton.SetBinding(Button.CommandProperty, "SignInCommand");

            forgotPasswordButton = new Button
            {
                Text = "Forgot password?",
                Style = Resources["smallButtonStyle"] as Style
            };

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
        }

        private void AddViewsToGrid()
        {
            grid.Children.Add(logoImage, 0, 0);
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
