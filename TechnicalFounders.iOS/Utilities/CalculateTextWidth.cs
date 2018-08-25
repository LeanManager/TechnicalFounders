using System;
using Foundation;
using TechnicalFounders.Interfaces;
using TechnicalFounders.iOS.Utilities;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(CalculateTextWidth))]
namespace TechnicalFounders.iOS.Utilities
{
    public class CalculateTextWidth : ICalculateTextWidth
    {
        public CalculateTextWidth()
        {
        }

        public double CalculateWidth(string text)
        {
            var label = new UILabel();
            label.Text = text;
            var length = label.Text.StringSize(label.Font);
            //var l = NSString.GetSizeUsingAttributes()
            return length.Width;
        }
    }
}
