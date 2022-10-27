using System;
using Xamarin.Forms;

namespace UGB.Paysa.Controls
{
    public class MyEntry : Entry
    {
        public static BindableProperty CornerRadiusProperty =
           BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(MyEntry), 0);

        public static BindableProperty BorderThicknessProperty =
            BindableProperty.Create(nameof(BorderThickness), typeof(int), typeof(MyEntry), 0);

        public static BindableProperty PaddingProperty =
            BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(MyEntry), new Thickness(5));

        public static BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(MyEntry), Color.Transparent);
        
        public int CornerRadius
        {
            get => (int)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public int BorderThickness
        {
            get => (int)GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }
        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }
        /// <summary>
        /// This property cannot be changed at runtime in iOS.
        /// </summary>
        public Thickness Padding
        {
            get => (Thickness)GetValue(PaddingProperty);
            set => SetValue(PaddingProperty, value);
        }

        public delegate void BackspaceEventHandler(object sender, EventArgs e);

        public event BackspaceEventHandler OnBackspace;

        public void OnBackspacePressed()
        {
            if (OnBackspace != null)
            {
                OnBackspace(null, null);
            }

        }

        public MyEntry() { }
    }
}
