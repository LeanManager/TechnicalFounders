using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace TechnicalFounders.CustomCells
{
    public partial class IncomingViewCell : ViewCell
    {
        public IncomingViewCell()
        {
            InitializeComponent();

            textLabel.FontSize = Device.RuntimePlatform == Device.iOS 
                                 ? Device.GetNamedSize(NamedSize.Small, typeof(Label)) // 14
                                 : Device.GetNamedSize(NamedSize.Default, typeof(Label));
        }
    }
}
