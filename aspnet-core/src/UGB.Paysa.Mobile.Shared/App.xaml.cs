using System;
using System.Threading.Tasks;
using UGB.Paysa.ApiClient;
using UGB.Paysa.Core.Dependency;
using UGB.Paysa.Localization.Resources;
using UGB.Paysa.Services.Account;
using UGB.Paysa.Services.Navigation;
using UGB.Paysa.Services.Storage;
using UGB.Paysa.ViewModels.Base;
using Xamarin.Forms;

[assembly: ExportFont("Montserrat_Regular.ttf", Alias = "Montserrat_Regular")]
[assembly: ExportFont("Montserrat_Bold.ttf", Alias = "Montserrat_Bold")]
[assembly: ExportFont("Montserrat_Black.ttf", Alias = "Montserrat_Black")]
[assembly: ExportFont("Montserrat_Light.ttf", Alias = "Montserrat_Light")]
[assembly: ExportFont("Montserrat_ExtraBold.ttf", Alias = "Montserrat_ExtraBold")]
[assembly: ExportFont("Montserrat_SemiBold.ttf", Alias = "Montserrat_SemiBold")]
[assembly: ExportFont("Montserrat_ThicccAF.ttf", Alias = "Montserrat_ThicccAF")]
[assembly: ExportFont("Montserrat_Thin.ttf", Alias = "Montserrat_Thin")]
[assembly: ExportFont("Montserrat_Medium.ttf", Alias = "Montserrat_Medium")]
namespace UGB.Paysa
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            InstallFontPlugins();
        }

        public static Action ExitApplication;

        public static async Task OnSessionTimeout()
        {
            await DependencyResolver.Resolve<IAccountService>().LogoutAsync();
        }

        public static async Task OnAccessTokenRefresh(string newAccessToken)
        {
            await DependencyResolver.Resolve<IDataStorageService>().StoreAccessTokenAsync(newAccessToken);
        }


        public static void LoadPersistedSession()
        {
            var accessTokenManager = DependencyResolver.Resolve<IAccessTokenManager>();
            var dataStorageService = DependencyResolver.Resolve<IDataStorageService>();
            var applicationContext = DependencyResolver.Resolve<IApplicationContext>();

            accessTokenManager.AuthenticateResult = dataStorageService.RetrieveAuthenticateResult();
            applicationContext.Load(dataStorageService.RetrieveTenantInfo(), dataStorageService.RetrieveLoginInfo());
        }

        protected override async void OnStart()
        {
            base.OnStart();

            if (Device.RuntimePlatform == Device.iOS)
            {
                SetInitialScreenForIos();
                await UserConfigurationManager.GetIfNeedsAsync();
            }

            await DependencyResolver.Resolve<INavigationService>().InitializeAsync();

            OnResume();
        }

        private void SetInitialScreenForIos()
        {
            MainPage = new ContentPage
            {
                BackgroundColor = (Color)Current.Resources["LoginBackgroundColor"],
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    Children =
                    {
                        new ActivityIndicator
                        {
                            IsRunning = true,
                            Color = Color.White
                        },
                        new Label
                        {
                            Text = LocalTranslation.Initializing,
                            TextColor = Color.White,
                            HorizontalTextAlignment = TextAlignment.Center,
                            VerticalTextAlignment = TextAlignment.Center
                        }
                    }
                }
            };
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        /// <summary>
        /// https://github.com/jsmarcus/Xamarin.Plugins/tree/master/Iconize
        /// </summary>
        private static void InstallFontPlugins()
        {
            Plugin.Iconize.Iconize
                .With(new Plugin.Iconize.Fonts.FontAwesomeSolidModule())
                .With(new Plugin.Iconize.Fonts.MaterialModule());

            /*
            // FontAwesome 5 Solid Icons:
            // You can get the list of icon keys with the below code 
            // Alternatively, you can visit http://aalmiray.github.io/ikonli/cheat-sheet-fontawesome5.html#_solid
            foreach (var module in Plugin.Iconize.Iconize.Modules)
            {
                var iconsList = module.Keys.ToList();
            }
            */
        }
    }
}
