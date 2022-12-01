using Abp.Application.Services.Dto;
using MvvmHelpers;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using UGB.Paysa.ApiClient;
using UGB.Paysa.Authorization.Users.Dto;
using UGB.Paysa.Commands;
using UGB.Paysa.Core.Threading;
using UGB.Paysa.Models.Users;
using UGB.Paysa.UGB.Paysa.Chambres;
using UGB.Paysa.UI.Assets;
using UGB.Paysa.ViewModels.Base;
using UGB.Paysa.Wallet.Chambres;
using UGB.Paysa.Wallet.Chambres.Dtos;
using UGB.Paysa.Wallet.Comptes.Dtos;
using UGB.Paysa.Wallet.Operations;
using UGB.Paysa.Wallet.Operations.Dtos;

namespace UGB.Paysa.ViewModels.Wallet.Logement
{
    public class LogementViewModel : XamarinViewModel
    {
        public ICommand PageAppearingCommand => HttpRequestCommand.Create(PageAppearing);
        public ObservableRangeCollection<OperationListModel> LogementOperations { get; set; }

        private readonly IOperationsAppService _operationsAppService;
        private readonly IChambresAppService _chambresAppService;
        private readonly IPaiementLoyersAppService _paiementLoyersAppService;
        private readonly IApplicationContext _applicationContext;

        private GetChambreForViewDto Chambre;
        private GetAllOperationsInput _input;
        private bool _isInitialized;
        public  bool _noOperationsFound;
        public  string _referenceChambre;
        public  string NumeroCompte;
        public bool NoOperationsFound
        {
            get => _noOperationsFound;
            set
            {
                _noOperationsFound = value;
                RaisePropertyChanged(() => NoOperationsFound);
            }
        }
        public string ReferenceChambre
        {
            get => _referenceChambre;
            set
            {
                _referenceChambre = value;
                RaisePropertyChanged(() => ReferenceChambre);
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

        public override async Task InitializeAsync(object navigationData)
        {
             NumeroCompte = (string)navigationData;
            _input.CompteNumeroCompteFilter = NumeroCompte;
        }
        public LogementViewModel(IOperationsAppService operationsAppService,
            IChambresAppService chambresAppService,
            IPaiementLoyersAppService paiementLoyersAppService,
            IApplicationContext applicationContext)
        {
            _operationsAppService = operationsAppService;
            _chambresAppService = chambresAppService;
            _paiementLoyersAppService = paiementLoyersAppService;
            _applicationContext = applicationContext;
            LogementOperations = new ObservableRangeCollection<OperationListModel>();

            _input = new GetAllOperationsInput
            {
                TypeOperationNomFilter = "LOCATION",
                Filter = "",
                MaxResultCount = PageDefaults.PageSize,
                SkipCount = 0
            };
        }
        private async Task FetchAllOperationsAsync()
        {
            await WebRequestExecuter.Execute(async () => await _operationsAppService.GetAll(_input), result =>
            {
                var operations = ObjectMapper.Map<List<OperationListModel>>(result.Items);
                foreach (var operation in operations)
                {
                    LogementOperations.Add(operation);
                }
                CheckNoOperationsFound();
                OperationListHeight = LogementOperations.Count;

                return Task.CompletedTask;
            });
        }
        private async Task RefreshOperationsAsync()
        {
            LogementOperations.Clear();

            _input.SkipCount = 0;

            await SetBusyAsync(FetchAllOperationsAsync);
        }
        private void CheckNoOperationsFound()
        {
            NoOperationsFound = LogementOperations.Count == 0;
        }
        private async Task GetUserChambreInfo(long userId)
        {
            Chambre = await _chambresAppService.GetChambreByUserId(new EntityDto<long> { Id = userId });
            ReferenceChambre = Chambre.Chambre.Reference;
        }
        private async Task PageAppearing()
        {
            if (_isInitialized)
            {
                return;
            }
            await RefreshOperationsAsync();
            await GetUserChambreInfo(_applicationContext.LoginInfo.User.Id);
            _isInitialized = true;
        }
    }
}
