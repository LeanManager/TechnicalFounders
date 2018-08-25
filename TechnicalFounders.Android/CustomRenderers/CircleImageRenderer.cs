using System;
using Android.Content;
using TechnicalFounders.CustomElements;
using TechnicalFounders.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Views;
using Android.Graphics;

[assembly: ExportRenderer(typeof(CircleImage), typeof(CircleImageRenderer))]
namespace TechnicalFounders.Droid.CustomRenderers
{
    public class CircleImageRenderer : ImageRenderer
    {
        public CircleImageRenderer(Context context) : base(context)
        {
        }

        /* Android is a unique platform; you don’t have a physical layer to interact with. 
         * However, you are able to modify how Xamarin.Forms draws the child and cut out a 
         * path from the canvas.This method is a bit of complex math, but calculates the 
         * path to clip and then adds a circular border around the child.
         */

        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                if ((int)Android.OS.Build.VERSION.SdkInt < 18)
                    SetLayerType(LayerType.Software, null);
            }
        }

        protected override bool DrawChild(Canvas canvas, global::Android.Views.View child, long drawingTime)
        {
            try
            {
                var radius = Math.Min(Width, Height) / 2;
                var strokeWidth = 10;
                radius -= strokeWidth / 2;

                //Create path to clip
                var path = new Path();
                path.AddCircle(Width / 2, Height / 2, radius, Path.Direction.Ccw);
                canvas.Save();
                canvas.ClipPath(path);

                var result = base.DrawChild(canvas, child, drawingTime);

                canvas.Restore();

                // Create path for circle border
                path = new Path();
                path.AddCircle(Width / 2, Height / 2, radius, Path.Direction.Ccw);

                var paint = new Paint();
                paint.AntiAlias = true;
                //paint.StrokeWidth = ((CircleImage)this.Element).BorderWidth;
                paint.StrokeWidth = 5;
                paint.SetStyle(Paint.Style.Stroke);
                //paint.Color = global::Android.Graphics.Color.White;
                paint.Color = ((CircleImage)this.Element).BorderColor.ToAndroid();

                canvas.DrawPath(path, paint);

                //Properly dispose
                paint.Dispose();
                path.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Unable to create circle image: " + ex);
            }

            return base.DrawChild(canvas, child, drawingTime);
        }
    }
}
