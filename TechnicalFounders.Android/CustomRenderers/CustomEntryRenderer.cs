﻿using System;
using TechnicalFounders.CustomElements;
using TechnicalFounders.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Util;
using Android.Graphics;
using Android.Views;
using Android.Text;
using Android.Content.Res;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace TechnicalFounders.Droid.CustomRenderers
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            this.SetWillNotDraw(false);

            if (e.OldElement == null)
            {

                if ((int)Android.OS.Build.VERSION.SdkInt < 18)
                    SetLayerType(LayerType.Software, null);
            }

            if (e.NewElement != null)  
            {  
                var view = (CustomEntry)Element;  

                if (view.IsCurvedCornersEnabled)  
                {  
                    // Creating gradient drawable for the curved background  
                    var _gradientBackground = new GradientDrawable();  

                    _gradientBackground.SetShape(ShapeType.Rectangle);  
                    _gradientBackground.SetColor(view.BackgroundColor.ToAndroid()); 

                    // Thickness of the stroke line    
                    _gradientBackground.SetStroke(view.BorderWidth, view.BorderColor.ToAndroid()); 

                    // Radius for the curves  
                    _gradientBackground.SetCornerRadius(DpToPixels(this.Context, Convert.ToSingle(view.CornerRadius))); 

                    // Set the background of the entry  
                    Control.SetBackground(_gradientBackground);  
                }  
                // Set padding for the internal text from border  
                Control.SetPadding((int)DpToPixels(this.Context, Convert.ToSingle(12)), Control.PaddingTop,  
                                   (int)DpToPixels(this.Context, Convert.ToSingle(12)), Control.PaddingBottom);  
            }
        }

        public static float DpToPixels(Context context, float valueInDp)  
        {  
            DisplayMetrics metrics = context.Resources.DisplayMetrics; 
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, valueInDp, metrics);  
        }

        public override void Draw(Canvas canvas)
        {
            CustomEntry customEntry = (CustomEntry)this.Element;

            // Create path to clip
            var path = new Path();
            path.AddRoundRect(0, 0, Width, Height, DpToPixels(this.Context, Convert.ToSingle(customEntry.CornerRadius)), 
                              DpToPixels(this.Context, Convert.ToSingle(customEntry.CornerRadius)), Path.Direction.Ccw);

            canvas.Save();
            canvas.ClipPath(path);

            base.Draw(canvas);
        }
    }
}
