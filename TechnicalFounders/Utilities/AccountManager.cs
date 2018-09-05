using System;
using Xamarin.Auth;
using System.Linq;
using Xamarin.Forms;
using TechnicalFounders.Interfaces;

namespace TechnicalFounders.Utilities
{
    public class AccountManager
    {
        const string serviceID = "Xamarin";
        const string passwordKey = "password";
        const string usernameKey = "username";
        const string idKey = "id";
        const string kmKey = "keymaterial";
        const string saltKey = "salt";

        public AccountManager()
        {
        }

        public bool CreateAndSaveAccount(string username, string password, string name, string id)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return false;

            var store = GetAccountStore();

            if (GetAccountFromStore(store, username) != null)
                return false;

            // Store salt in account

            byte[] salt = CryptoUtilities.Get256BitSalt();

            byte[] hashedPassword = CryptoUtilities.GetHash(CryptoUtilities.StringToByteArray(password), salt);

            var account = new Account(username);

            account.Properties.Add(passwordKey, Convert.ToBase64String(hashedPassword));
            account.Properties.Add(saltKey, Convert.ToBase64String(salt));
            account.Properties.Add(usernameKey, name);
            account.Properties.Add(idKey, id);

            // Store key for symmetric encryption
            account.Properties.Add(kmKey, Convert.ToBase64String(CryptoUtilities.GetAES256KeyMaterial()));

            store.Save(account, serviceID);

            return true;
        }

        public bool LoginToAccount(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return false;

            var store = GetAccountStore();

            var account = GetAccountFromStore(store, username);

            if (account == null)
                return false;

            byte[] salt, hashedPassword;

            // Get original salt
            salt = Convert.FromBase64String(account.Properties[saltKey]);

            hashedPassword = CryptoUtilities.GetHash(CryptoUtilities.StringToByteArray(password), salt);

            // May do more granular validation in production
            return account.Properties[passwordKey] == Convert.ToBase64String(hashedPassword);
        }

        AccountStore GetAccountStore()
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                var context = DependencyService.Get<IFormsContext>();
                AccountStore store = context.CreateAccount();
                return store;
            }
            else
            {
                AccountStore store = AccountStore.Create();
                return store;
            }
        }

        public Account GetAccount(string username)
        {
            return GetAccountFromStore(GetAccountStore(), username);
        }

        private Account GetAccountFromStore(AccountStore store, string username)
        {
            if (store == null || username == null)
                return null;

            var accounts = store.FindAccountsForService(serviceID);

            return accounts.FirstOrDefault(a => a.Username == username);
        }

        public Account CheckForAccount()
        {
            AccountStore store = GetAccountStore();

            var accounts = store.FindAccountsForService(serviceID);

            //store.Delete(accounts.FirstOrDefault(), serviceID);

            return accounts.FirstOrDefault();
        }
    }
}
