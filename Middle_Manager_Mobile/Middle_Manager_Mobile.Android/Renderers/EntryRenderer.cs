using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Content;
using Middle_Manager_Mobile.Droid.Renderers;
using Middle_Manager_Mobile.Controls;
using Android.Graphics.Drawables;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(MyEntryRenderer))]
namespace Middle_Manager_Mobile.Droid.Renderers
{
    class MyEntryRenderer : EntryRenderer
    {
        public MyEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {

                var gd = new GradientDrawable();
                gd.SetColor(Element.BackgroundColor.ToAndroid());
                gd.SetCornerRadius(10);
                gd.SetStroke(5, global::Android.Graphics.Color.Black);
                Control.SetBackground(gd);

                var padTop = (int)Context.ToPixels(5);
                var padBottom = (int)Context.ToPixels(5);
                var padLeft = (int)Context.ToPixels(5);
                var padRight = (int)Context.ToPixels(5);

                Control.SetPadding(padLeft, padTop, padRight, padBottom);
            }
        }
    }
}