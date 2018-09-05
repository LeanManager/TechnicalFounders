using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using CarouselView.FormsPlugin.Abstractions;
using TechnicalFounders.Models;
using TechnicalFounders.ViewModels;
using Xamarin.Forms;

namespace TechnicalFounders.Views
{
    public partial class AnimationsPage : ContentPage
    {
        AnimationsPageViewModel viewModel;

        public AnimationsPage()
        {
            InitializeComponent();

            this.BindingContext = viewModel = new AnimationsPageViewModel();
        }

        async void Handle_Translation(object sender, System.EventArgs e)
        {
            var button = sender as Button;

            await button.TranslateTo(100, 0, 500, Easing.SpringOut);
            await button.TranslateTo(-100, 0, 500, Easing.SpringOut);
            await button.TranslateTo(0, 0);
        }

        async void Handle_Scaling(object sender, System.EventArgs e)
        {
            var button = sender as Button;

            await button.ScaleTo(5, 1000);
            await button.ScaleTo(0.1, 500, Easing.BounceIn);
            await button.ScaleTo(1, 1000, Easing.BounceOut);
        }

        async void Handle_Rotation(object sender, System.EventArgs e)
        {
            var button = sender as Button;

            await button.RotateTo(360, 2000);
            button.Rotation = 0;
        }

        async void Handle_Fading(object sender, System.EventArgs e)
        {
            var button = sender as Button;

            await button.FadeTo(0.5, 1000, Easing.SinInOut);
            await button.FadeTo(1, 1000, Easing.SinInOut);
        }

        async void Handle_Combined(object sender, System.EventArgs e)
        {
            var button = sender as Button;

            await button.TranslateTo(100, 0, 500, Easing.SpringOut);
            await button.TranslateTo(-100, 0, 500, Easing.SpringOut);
            await button.TranslateTo(0, 0);
            await button.ScaleTo(3, 500);
            await button.ScaleTo(1, 500, Easing.SpringOut);
            await button.RotateTo(180);
            await button.RotateTo(0, 500, Easing.SpringOut);
        }

        async void OnClosePage(object sender, System.EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }

        //void CarouselScrolled(object sender, CarouselView.FormsPlugin.Abstractions.ScrolledEventArgs e)
        //{
        //    Debug.WriteLine("Scrolled to " + e.NewValue + " percent.");
        //    Debug.WriteLine("Direction = " + e.Direction);
        //    viewModel.RotationAngle = e.NewValue;
        //    var carousel = sender as CarouselViewControl;
        //    var position = carousel.Position;
        //}
    }
}
