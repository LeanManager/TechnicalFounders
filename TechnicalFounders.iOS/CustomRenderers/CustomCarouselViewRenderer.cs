using System;
using CarouselView.FormsPlugin.Abstractions;
using CarouselView.FormsPlugin.iOS;
using TechnicalFounders.CustomElements;
using TechnicalFounders.iOS.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomCarouselView), typeof(CustomCarouselViewRenderer))]
namespace TechnicalFounders.iOS.CustomRenderers
{
    public class CustomCarouselViewRenderer : CarouselViewRenderer
    {
        public CustomCarouselViewRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<CarouselViewControl> e)
        {
            base.OnElementChanged(e);

            var element = Element as CustomCarouselView;
        }
    }
}
