using MvvmHelpers;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using UGB.Paysa.Commands;
using UGB.Paysa.Core.Threading;
using UGB.Paysa.Models.Users;
using UGB.Paysa.ViewModels.Base;
using UGB.Paysa.Wallet.Operations;
using UGB.Paysa.Wallet.Operations.Dtos;

namespace UGB.Paysa.ViewModels.Wallet.Transport
{
    public class TransportViewModel : XamarinViewModel
    {
        public ICommand PageAppearingCommand => HttpRequestCommand.Create(PageAppearing);
        public ICommand ShowTrajetRetourCommand => HttpRequestCommand.Create(ShowTrajetRetourAsync);
        public ICommand ShowTrajetDepartCommand => HttpRequestCommand.Create(ShowTrajetDepartAsync);
        public ObservableRangeCollection<OperationListModel> TransportOperations { get; set; }

        private readonly IOperationsAppService _operationsAppService;
        private GetAllOperationsInput _input;
        private string NumeroCompte;
        private bool _isInitialized;
        public bool _isInRetour;
        public bool _isInDepart;
        public bool IsInRetour
        {
            get => _isInRetour;
            set
            {
                _isInRetour = value;
                RaisePropertyChanged(() => IsInRetour);
            }
        }
        public bool IsInDepart
        {
            get => _isInDepart;
            set
            {
                _isInDepart = value;
                RaisePropertyChanged(() => IsInDepart);
            }
        }
        public bool _noOperationsFound;
        public bool NoOperationsFound
        {
            get => _noOperationsFound;
            set
            {
                _noOperationsFound = value;
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
        public TransportViewModel(IOperationsAppService operationsAppService)
        {
            IsInDepart = true;
            _operationsAppService = operationsAppService;

            TransportOperations = new ObservableRangeCollection<OperationListModel>();

            _input = new GetAllOperationsInput
            {
                TypeOperationNomFilter = "TRANSPORT_UGB_VILLE",
                Filter = "",
                MaxResultCount = PageDefaults.PageSize,
                SkipCount = 0
            };

        }
        public override async Task InitializeAsync(object navigationData)
        {
             NumeroCompte = (string)navigationData;
        }
        private async Task ShowTrajetRetourAsync()
        {
            IsInRetour = true;
            IsInDepart = false;
        }
        private async Task ShowTrajetDepartAsync()
        {
            IsInRetour = false;
            IsInDepart = true;
        }
        private async Task FetchAllOperationsAsync()
        {
            _input.CompteNumeroCompteFilter = NumeroCompte;
            await WebRequestExecuter.Execute(async () => await _operationsAppService.GetAll(_input), result =>
            {
                var operations = ObjectMapper.Map<List<OperationListModel>>(result.Items);
                foreach (var operation in operations)
                {
                    TransportOperations.Add(operation);
                }
                CheckNoOperationsFound();
                OperationListHeight = TransportOperations.Count;

                return Task.CompletedTask;
            });
        }
        private async Task RefreshOperationsAsync()
        {
            TransportOperations.Clear();

            _input.SkipCount = 0;

            await SetBusyAsync(FetchAllOperationsAsync);
        }
        private void CheckNoOperationsFound()
        {
            NoOperationsFound = TransportOperations.Count == 0;
        }
        private async Task PageAppearing()
        {
            if (_isInitialized)
            {
                return;
            }
            await RefreshOperationsAsync();
            _isInitialized = true;
        }
    }
}
