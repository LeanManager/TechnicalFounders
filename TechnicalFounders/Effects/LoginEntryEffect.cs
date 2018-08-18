using System;
using Xamarin.Forms;

namespace TechnicalFounders.Effects
{
    public class LoginEntryEffect : RoutingEffect
    {
        public LoginEntryEffect() : base("TechnicalFounders.LoginEntryEffect")
        {
        }

        public string EntryText
        {
            get;
            set;
        }
    }
}
