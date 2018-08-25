﻿using System;
using CoreGraphics;
using TechnicalFounders.CustomElements;
using TechnicalFounders.iOS.CustomRenderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace TechnicalFounders.iOS.CustomRenderers
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)  
            {  
                var view = (CustomEntry)Element;  
  
                Control.LeftView = new UIView(new CGRect(0f, 0f, 9f, 20f));  
                Control.LeftViewMode = UITextFieldViewMode.Always;  
  
                Control.KeyboardAppearance = UIKeyboardAppearance.Dark;  
                Control.ReturnKeyType = UIReturnKeyType.Done;  

                // Radius for the curves  
                Control.Layer.CornerRadius = Convert.ToSingle(view.CornerRadius); 

                // Thickness of the Border Color  
                Control.Layer.BorderColor = view.BorderColor.ToCGColor();  

                // Thickness of the Border Width  
                Control.Layer.BorderWidth = view.BorderWidth;  
                Control.ClipsToBounds = true;  
            }  
        }
    }
}
