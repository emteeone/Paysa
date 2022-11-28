using MvvmHelpers;
using NUglify.JavaScript.Syntax;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using UGB.Paysa.Commands;
using UGB.Paysa.Core.Threading;
using UGB.Paysa.Models.Users;
using UGB.Paysa.UI.Assets;
using UGB.Paysa.ViewModels.Base;
using UGB.Paysa.Wallet.Operations;
using UGB.Paysa.Wallet.Operations.Dtos;

namespace UGB.Paysa.ViewModels.Wallet.Operations
{
    public class SeeAllOperationsViewModel : XamarinViewModel
    {
        public ICommand PageAppearingCommand => HttpRequestCommand.Create(PageAppearing);
        public ICommand ShowAllOperationsCommand    => HttpRequestCommand.Create(ShowAllOperationsAsync);
        public ICommand ShowCreditOperationsCommand => HttpRequestCommand.Create(ShowCreditOperationsAsync);
        public ICommand ShowDebitOperationsCommand  => HttpRequestCommand.Create(ShowDebitOperationsAsync);
        public ObservableRangeCollection<OperationListModel> AllOperations { get; set; }
        
        private readonly IOperationsAppService _operationsAppService;
        
        private GetAllOperationsInput _input;
        private bool _isInitialized;
        private string NumeroCompte;

        public bool _isInAll;
        public bool IsInAll
        {
            get => _isInAll;
            set
            {
                _isInAll = value;
                RaisePropertyChanged(() => IsInAll);
            }
        }

        public bool _isInCredit;
        public bool IsInCredit
        {
            get => _isInCredit;
            set
            {
                _isInCredit = value;
                RaisePropertyChanged(() => IsInCredit);
            }
        }

        public bool _isInDebit;
        public bool IsInDebit
        {
            get => _isInDebit;
            set
            {
                _isInDebit = value;
                RaisePropertyChanged(() => IsInDebit);
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
        public SeeAllOperationsViewModel(IOperationsAppService operationsAppService)
        {
            IsInAll = true;
            _operationsAppService = operationsAppService;

            AllOperations = new ObservableRangeCollection<OperationListModel>();

            _input = new GetAllOperationsInput
            {
                Filter = "",
                MaxResultCount = PageDefaults.PageSize,
                SkipCount = 0
            };
        }

        public override async Task InitializeAsync(object navigationData)
        {
            NumeroCompte = (string)navigationData;
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
        private async Task ShowAllOperationsAsync()
        {
            if (IsInAll)
            {
                return;
            }
            IsInDebit = false;
            IsInAll = true;
            IsInCredit = false;
            _input.DiscriminatorFilter = "";
            await RefreshOperationsAsync();
        }
        private async Task ShowCreditOperationsAsync()
        {
            if (IsInCredit)
            {
                return;
            }
            IsInDebit = false;
            IsInAll = false;
            IsInCredit = true;
            _input.DiscriminatorFilter = "Credit";
            await RefreshOperationsAsync();
        }
        private async Task ShowDebitOperationsAsync()
        {
            if (IsInDebit)
            {
                return;
            }
            IsInDebit = true;
            IsInAll = false;
            IsInCredit = false;
            _input.DiscriminatorFilter = "Debit";
            await RefreshOperationsAsync();
        }
        private async Task FetchAllOperationsAsync()
        {
            _input.CompteNumeroCompteFilter = NumeroCompte;
            await WebRequestExecuter.Execute(async () => await _operationsAppService.GetAll(_input), result =>
            {
                var operations = ObjectMapper.Map<List<OperationListModel>>(result.Items);
                foreach (var operation in operations)
                {
                    AllOperations.Add(operation);
                }
                CheckNoOperationsFound();
                OperationListHeight = AllOperations.Count;

                return Task.CompletedTask;
            });
        }
        private async Task RefreshOperationsAsync()
        {
            AllOperations.Clear();

            _input.SkipCount = 0;

            await SetBusyAsync(FetchAllOperationsAsync);
        }
        private void CheckNoOperationsFound()
        {
            NoOperationsFound = AllOperations.Count == 0;
        }

    }
}