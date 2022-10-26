using System.IO;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using UGB.Paysa.ApiClient;
using UGB.Paysa.Authorization.Users.Profile;
using UGB.Paysa.Commands;
using UGB.Paysa.UI.Assets;
using UGB.Paysa.ViewModels.Base;
using Xamarin.Forms;
using UGB.Paysa.Views.Wallet.Restauration;
using UGB.Paysa.ViewModels.Wallet.Transport;
using UGB.Paysa.Views.Wallet.Logement;
using UGB.Paysa.Views.Wallet.Consultation;
using UGB.Paysa.Views.Wallet.Transport;
using UGB.Paysa.Views.Wallet.Operations;

namespace UGB.Paysa.ViewModels.Wallet
{
    public class HomePageViewModel : XamarinViewModel
    {
        public ICommand PageAppearingCommand    => HttpRequestCommand.Create(PageAppearing);
        public ICommand RestaurationViewCommand => HttpRequestCommand.Create(GoToRestaurationPageAsync);
        public ICommand TransportViewCommand    => HttpRequestCommand.Create(GoToTransportPageAsync);
        public ICommand ConsultationViewCommand => HttpRequestCommand.Create(GoToConsultationPageAsync);
        public ICommand LogementViewCommand     => HttpRequestCommand.Create(GoToLogementPageAsync);
        public ICommand SeeAllOperationsViewCommand     => HttpRequestCommand.Create(GoToAllOperationsPageAsync);

        private ImageSource _photo;
        private bool _isInitialized;
        private string _userNameAndSurname;
        private byte[] _profilePictureBytes;

        private readonly IProfileAppService     _profileAppService;
        private readonly ProxyProfileControllerService  _profileControllerService;
        private readonly IApplicationContext    _applicationContext;
        public HomePageViewModel(IProfileAppService profileAppService,
            ProxyProfileControllerService profileControllerService,
            IApplicationContext applicationContext)
        {
            _profileAppService = profileAppService;
            _profileControllerService = profileControllerService;
            _applicationContext = applicationContext;
            _isInitialized = false;
        }
        public string UserNameAndSurname
        {
            get => _userNameAndSurname;
            set
            {
                _userNameAndSurname = value;
                RaisePropertyChanged(() => UserNameAndSurname);
            }
        }
        public ImageSource Photo
        {
            get => _photo;
            set
            {
                _photo = value;
                RaisePropertyChanged(() => Photo);
            }
        }
        private async Task PageAppearing()
        {
            if (_isInitialized)
            {
                return;
            }

            if (_applicationContext.LoginInfo == null)
            {
                return;
            }

            UserNameAndSurname = _applicationContext.LoginInfo.User.Name + " " + _applicationContext.LoginInfo.User.Surname;
            Photo = ImageSource.FromResource(AssetsHelper.ProfileImagePlaceholderNamespace);
            await GetUserPhoto(_applicationContext.LoginInfo.User.Id);
            _isInitialized = true;
        }
        private async Task GoToRestaurationPageAsync()
        {
            if (IsBusy)
            {
                return;
            }
            await NavigationService.SetDetailPageAsync(typeof(RestaurationView), pushToStack: true);
        }
        private async Task GoToTransportPageAsync()
        {
            if (IsBusy)
            {
                return;
            }
            await NavigationService.SetDetailPageAsync(typeof(TransportView), pushToStack: true);
        }
        private async Task GoToConsultationPageAsync()
        {
            if (IsBusy)
            {
                return;
            }
            await NavigationService.SetDetailPageAsync(typeof(ConsultationView), pushToStack: true);
        }
        private async Task GoToLogementPageAsync()
        {
            if (IsBusy)
            {
                return;
            }
            await NavigationService.SetDetailPageAsync(typeof(LogementView), pushToStack: true);
        }
        private async Task GoToAllOperationsPageAsync()
        {
            if (IsBusy)
            {
                return;
            }
            await NavigationService.SetDetailPageAsync(typeof(SeeAllOperationsView), pushToStack: true);
        }

        private async Task GetUserPhoto(long userId)
        {
            var result = await _profileAppService.GetProfilePictureByUser(userId);
            if (result.ProfilePicture != null)
            {
                _profilePictureBytes = Convert.FromBase64String(result.ProfilePicture);
                Photo = ImageSource.FromStream(() => new MemoryStream(_profilePictureBytes));
            }
        }
    }
}
