using System;
using TechnicalFounders.Utilities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TechnicalFounders.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        private bool isHardwareBackEnabled = true;

        public MainPage()
        {
            InitializeComponent();

            MessagingCenter.Subscribe<DisableBackButtonMessage>(this, "DisableBackButton", message =>
            {
                isHardwareBackEnabled = false;
            });
        }

        protected override bool OnBackButtonPressed()
        {
            if (isHardwareBackEnabled == true)
            {
                return base.OnBackButtonPressed();
            }
            else
            {
                return true;
            }
        }
    }
}