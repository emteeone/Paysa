using Android.Content;
using Android.Graphics.Drawables;
using Android.Views;
using Avimtoo.AvimtooProd.Droid.Renderer;
using System.ComponentModel;
using UGB.Paysa.Controls;
using UGB.Paysa.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MyPicker), typeof(MyPickerRenderer))]
namespace Avimtoo.AvimtooProd.Droid.Renderer
{
    class MyPickerRenderer : PickerRenderer
    {
        public MyPickerRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                GradientDrawable gd = new GradientDrawable();
                gd.SetStroke(0, Android.Graphics.Color.Transparent);
                Control.SetBackground(gd);
            }
        }

    }
}