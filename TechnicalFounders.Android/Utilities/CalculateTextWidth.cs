using System;
using Android.Content.Res;
using Android.Graphics;
using Android.Widget;
using TechnicalFounders.Droid.Utilities;
using TechnicalFounders.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(CalculateTextWidth))]
namespace TechnicalFounders.Droid.Utilities
{
    public class CalculateTextWidth : ICalculateTextWidth
    {
        public CalculateTextWidth()
        {
        }

        public double CalculateWidth(string text)
        {
            Rect bounds = new Rect();
            TextView textView = new TextView(global::Android.App.Application.Context);
            textView.Paint.GetTextBounds(text, 0, text.Length, bounds);
            var length = bounds.Width();
            return length / Resources.System.DisplayMetrics.ScaledDensity;
        }
    }
}
