using System;
using Xamarin.Forms;

namespace TechnicalFounders.CustomElements
{
    public class CircleImage : Image
    {
        public CircleImage()
        {
        }

        // Bindable properties and use those in the custom renderers (vs current hardcoded values)

        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(CircleImage), Color.White);

        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public static readonly BindableProperty BorderWidthProperty =
            BindableProperty.Create(nameof(BorderWidth), typeof(int), typeof(CircleImage), 2);

        public int BorderWidth
        {
            get => (int)GetValue(BorderWidthProperty);
            set => SetValue(BorderWidthProperty, value);
        }
    }
}
