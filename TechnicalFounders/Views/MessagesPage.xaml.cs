using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalFounders.Utilities;
using TechnicalFounders.ViewModels;
using Xamarin.Forms;

namespace TechnicalFounders.Views
{
    public partial class MessagesPage : ContentPage
    {
        public MessagesPage()
        {
            InitializeComponent();

            Title = "Chat";

            BindingContext = new MessagesPageViewModel();

            MessagingCenter.Subscribe<ScrollListViewMessage>(this, "ScrollToBottom", message =>
            {
                var item = ((ObservableCollection<MessageViewModel>)(this.MessagesListView.ItemsSource)).Last();
                MessagesListView.ScrollTo(item, ScrollToPosition.End, false);
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Have one initial message from Technical Founders as default. "Welcome to Technical Founders!"

            // Fix Android (not scrolling initially) - might need a custom renderer

            var item = ((ObservableCollection<MessageViewModel>)(this.MessagesListView.ItemsSource)).Last();
            MessagesListView.ScrollTo(item, ScrollToPosition.End, false);
        }

        private void MyListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            MessagesListView.SelectedItem = null;
        }

        private void MyListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            MessagesListView.SelectedItem = null;
        }
    }
}
