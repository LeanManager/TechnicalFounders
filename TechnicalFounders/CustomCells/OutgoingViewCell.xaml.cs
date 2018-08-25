using System;
using System.Collections.Generic;
using TechnicalFounders.Interfaces;
using Xamarin.Forms;

namespace TechnicalFounders.CustomCells
{
    public partial class OutgoingViewCell : ViewCell
    {
        public OutgoingViewCell()
        {
            InitializeComponent();

            textLabel.FontSize = Device.RuntimePlatform == Device.iOS
                                 ? Device.GetNamedSize(NamedSize.Small, typeof(Label)) // 14
                                 : Device.GetNamedSize(NamedSize.Default, typeof(Label));
        }
    }
}
