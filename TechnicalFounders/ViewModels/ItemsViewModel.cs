using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using TechnicalFounders.Models;
using TechnicalFounders.Views;
using System.Windows.Input;
using TechnicalFounders.Utilities;
using Xamarin.Auth;

namespace TechnicalFounders.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public AccountManager accountManager;

        public ObservableCollection<Item> Items { get; set; }
        public ICommand LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Home";

            accountManager = new AccountManager();

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

                // Encrypt
                _item.CipherText = GetCipherText(_item.Description);

                if (_item.CipherText != null)
                {
                    Items.Add(_item);

                    await LocalDataStore.AddItemAsync(_item);
                    // Response code handling
                    //await CloudDataStore.AddItemAsync(_item);
                }
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

        // Decryption of goal description - coming from SQLite database
        string GetGoalText(byte[] cipherText)
        {
            Account account = accountManager.CheckForAccount();

            if (!account.Properties.TryGetValue("keymaterial", out string keyString))
                return String.Empty;

            byte[] keyMaterial = Convert.FromBase64String(keyString);

            return CryptoUtilities.ByteArrayToString(CryptoUtilities.Decrypt(cipherText, keyMaterial));
        }

        // Encryption of goal description
        byte[] GetCipherText(string diaryText)
        {
            Account account = accountManager.CheckForAccount();

            if (!account.Properties.TryGetValue("keymaterial", out string keyString))
                return null;

            byte[] keyMaterial = Convert.FromBase64String(keyString);

            return CryptoUtilities.Encrypt(CryptoUtilities.StringToByteArray(diaryText), keyMaterial);
        }
    }
}