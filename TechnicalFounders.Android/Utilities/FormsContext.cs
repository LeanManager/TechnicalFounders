using System;
using Android.Content;
using TechnicalFounders.Droid.Utilities;
using TechnicalFounders.Interfaces;
using Xamarin.Auth;

[assembly: Xamarin.Forms.Dependency(typeof(FormsContext))]
namespace TechnicalFounders.Droid.Utilities
{
    public class FormsContext : IFormsContext
    {
        public FormsContext()
        {
        }

        public Context Context => Android.App.Application.Context;

        public AccountStore CreateAccount()
        {
            return AccountStore.Create(Context);
        }
    }
}
