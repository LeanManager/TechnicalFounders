using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using TechnicalFounders.Models;
using TechnicalFounders.Views;
using System.Windows.Input;

namespace TechnicalFounders.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public ICommand LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Home";

            Items = new ObservableCollection<Item>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<ItemsPage, Item>(this, "RemoveItem", async (obj, item) =>
            {
                bool result = await Application.Current.MainPage.DisplayAlert("Complete", "Have you completed this goal?", "No", "Yes");

                if (result == false)
                {
                    Items.Remove(item);
                    await LocalDataStore.DeleteItemAsync(item.Id);
                }
            });

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var _item = item as Item;
                Items.Add(_item);
                await LocalDataStore.AddItemAsync(_item);
                // Response code handling
                await CloudDataStore.AddItemAsync(_item);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await LocalDataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}