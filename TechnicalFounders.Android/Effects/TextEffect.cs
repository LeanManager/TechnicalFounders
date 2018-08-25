using System;
using TechnicalFounders.CustomElements;
using TechnicalFounders.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

//[assembly: ResolutionGroupName("TechnicalFounders")]
[assembly: ExportEffect(typeof(TextEffect), "CaseTextEffect")]
namespace TechnicalFounders.Droid.Effects
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

            if (Element is Xamarin.Forms.Button)
            {
                var button = Control as Android.Widget.Button;
                button.SetAllCaps(false);
                //button.SetTypeface(Android.Graphics.Typeface.Default, Android.Graphics.TypefaceStyle.Normal);
            }
            else if (Element is CustomEntry)
            {
                var control = Control as Android.Widget.EditText;
                control.SetTypeface(Android.Graphics.Typeface.Default, Android.Graphics.TypefaceStyle.Normal);
            }
            else if (Element is Label)
            {

            }
        }

        protected override void OnDetached()
        {
            
        }
    }
}
