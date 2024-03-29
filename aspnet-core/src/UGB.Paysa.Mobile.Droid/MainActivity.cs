﻿using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using ImageCircle.Forms.Plugin.Droid;
using UGB.Paysa.Core.Exception;
using Plugin.Permissions;
using CachedImageRenderer = FFImageLoading.Forms.Platform.CachedImageRenderer;
using Plugin.Media;

namespace UGB.Paysa
{
    [Activity(
        Label = "Paysa",
        Icon = "@drawable/ic_logo_pay",
        MainLauncher = false,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            SetStatusBarColor(new Android.Graphics.Color(33,46,83));

            base.Window.RequestFeature(WindowFeatures.ActionBar);
            // Name of the MainActivity theme you had there before.
            // Or you can use global::Android.Resource.Style.ThemeHoloLight
            base.SetTheme(Resource.Style.MainTheme);

            base.OnCreate(bundle);

            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;
 
            global::Xamarin.Forms.Forms.Init(this, bundle);

            global::Xamarin.Essentials.Platform.Init(this, bundle);

            ImageCircleRenderer.Init();

            CachedImageRenderer.Init(enableFastRenderer: true);

            Plugin.Iconize.Iconize.Init(Resource.Id.toolbar, Resource.Id.tabMode);

            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private static void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs unobservedTaskExceptionEventArgs)
        {
            ExceptionHandler.LogException(unobservedTaskExceptionEventArgs.Exception);
            unobservedTaskExceptionEventArgs.SetObserved();
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
        {
            ExceptionHandler.LogException(unhandledExceptionEventArgs.ExceptionObject as Exception);
        }
    }
}

