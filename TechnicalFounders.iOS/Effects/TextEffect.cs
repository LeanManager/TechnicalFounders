using System;
using TechnicalFounders.iOS.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

//[assembly: ResolutionGroupName("TechnicalFounders")]
[assembly: ExportEffect(typeof(TextEffect), "CaseTextEffect")]
namespace TechnicalFounders.iOS.Effects
{
    public class TextEffect : PlatformEffect
    {
        public TextEffect()
        {
        }

        protected override void OnAttached()
        {
            if (Control == null && Element == null)
                return;

            if (Element is Label)
            {
                var label = Control as UILabel;
                //label.Font = UIFont.FromName("Rancho-Regular", (System.nfloat)((Label)Element).FontSize);
            }

            if (Element is Xamarin.Forms.Button)
            {
                var button = Control as UIButton;
                //button.TitleLabel.Font = UIFont.BoldSystemFontOfSize((System.nfloat)((Button)Element).FontSize);
            }
        }

        protected override void OnDetached()
        {

        }
    }
}
