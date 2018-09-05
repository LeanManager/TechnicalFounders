using System;
using CarouselView.FormsPlugin.Abstractions;
using Xamarin.Forms;

namespace TechnicalFounders.CustomElements
{
    public class CustomCarouselView : CarouselViewControl
    {
        public static readonly BindableProperty CurrentItemProperty =
            BindableProperty.Create(nameof(CurrentItem), typeof(object), typeof(CustomCarouselView), null);

        public object CurrentItem
        {
            get => GetValue(CurrentItemProperty);
            set => SetValue(CurrentItemProperty, value);
        }
    }
}
