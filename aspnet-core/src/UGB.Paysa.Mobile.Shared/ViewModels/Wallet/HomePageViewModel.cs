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
using UGB.Paysa.Wallet.Comptes;
using Abp.Application.Services.Dto;
using UGB.Paysa.Wallet.Comptes.Dtos;
using UGB.Paysa.Core.Threading;
using UGB.Paysa.Wallet.Operations;
using UGB.Paysa.Authorization.Users.Dto;
using UGB.Paysa.Wallet.Operations.Dtos;
using System.Collections.Generic;
using UGB.Paysa.Models.Users;
using MvvmHelpers;

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
        public ObservableRangeCollection<OperationListModel> RecentOperations { get; set; }

        private ImageSource _photo;
        private bool _isInitialized;
        private string _userNameAndSurname;
        private byte[] _profilePictureBytes;
        private double _balance;
        private GetCompteForViewDto Compte;
        private GetAllOperationsInput _input;
        private readonly IProfileAppService     _profileAppService;
        private readonly IComptesAppService     _compteAppService;
        private readonly IOperationsAppService _operationsAppService;
        private readonly ProxyProfileControllerService  _profileControllerService;
        private readonly IApplicationContext    _applicationContext;
        public HomePageViewModel(IProfileAppService profileAppService,
            ProxyProfileControllerService profileControllerService,
            IComptesAppService compteAppService,
            IOperationsAppService operationsAppService,
            IApplicationContext applicationContext)
        {
            _profileAppService = profileAppService;
            _operationsAppService = operationsAppService;
            _compteAppService = compteAppService;
            _profileControllerService = profileControllerService;
            _applicationContext = applicationContext;
            _isInitialized = false;
            RecentOperations = new ObservableRangeCollection<OperationListModel>();

            _input = new GetAllOperationsInput
            {
                Filter = "",
                MaxResultCount = 5,
                SkipCount = 0
            };

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
        public double Balance
        {
            get => _balance;
            set
            {
                _balance = value;
                RaisePropertyChanged(() => Balance);
            }
        }
        public bool _nOperationsFound;
        public bool NoOperationsFound
        {
            get => _nOperationsFound;
            set
            {
                _nOperationsFound = value;
                RaisePropertyChanged(() => NoOperationsFound);
            }
        }

        public int _operationListHeight;
        public int OperationListHeight
        {
            get => _operationListHeight * 105;
            set
            {
                _operationListHeight = value;
                RaisePropertyChanged(() => OperationListHeight);
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
            await GetUserCompteBalance(_applicationContext.LoginInfo.User.Id);
            await FetchRecentOperationsAsync();
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
            await NavigationService.SetDetailPageAsync(typeof(SeeAllOperationsView), Compte.Compte.NumeroCompte, pushToStack: true);
        }
        private async Task RefreshRecentPerationsAsync()
        {
            RecentOperations.Clear();

            _input.SkipCount = 0;

            await SetBusyAsync(FetchRecentOperationsAsync);
        }
        private async Task FetchRecentOperationsAsync()
        {
            _input.CompteNumeroCompteFilter = Compte.Compte.NumeroCompte;
            await WebRequestExecuter.Execute(async () => await _operationsAppService.GetAll(_input), result =>
            {
                var operations = ObjectMapper.Map<List<OperationListModel>>(result.Items);
                foreach (var operation in operations)
                {
                    RecentOperations.Add(operation);
                }
                CheckNoOperationsFound();
                OperationListHeight = RecentOperations.Count;

                return Task.CompletedTask;
            });
        }
        private void CheckNoOperationsFound()
        {
            NoOperationsFound = RecentOperations.Count == 0;
        }
        private async Task GetUserPhoto(long userId)
        {
            var result = await _profileAppService.GetProfilePictureByUser(userId);
            if (! String.IsNullOrWhiteSpace(result.ProfilePicture) )
            {
                _profilePictureBytes = Convert.FromBase64String(result.ProfilePicture);
                Photo = ImageSource.FromStream(() => new MemoryStream(_profilePictureBytes));
            }
        }
        private async Task GetUserCompteBalance(long userId)
        {
            Compte = await _compteAppService.GetCompteBalanceByUserId( new EntityDto<long> { Id = userId });
            Balance = Compte.Compte.Solde;
        }
    }
}
