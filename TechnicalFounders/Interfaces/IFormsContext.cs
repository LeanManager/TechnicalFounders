using System;
using Xamarin.Auth;

namespace TechnicalFounders.Interfaces
{
    public interface IFormsContext
    {
        AccountStore CreateAccount();
    }
}
