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

namespace UGB.Paysa.ViewModels.Wallet.Consultation
{
    public class ConsultationViewModel : XamarinViewModel
    {
        public ICommand PageAppearingCommand => HttpRequestCommand.Create(PageAppearing);

        public ObservableRangeCollection<OperationListModel> ConsultationOperations { get; set; }

        private readonly IOperationsAppService _operationsAppService;
        private GetAllOperationsInput _input;
        private string NumeroCompte;
        private bool _isInitialized;
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
        public ConsultationViewModel(IOperationsAppService operationsAppService)
        {
            
            _operationsAppService = operationsAppService;

            ConsultationOperations = new ObservableRangeCollection<OperationListModel>();

            _input = new GetAllOperationsInput
            {
                TypeOperationNomFilter = "CONSULTATION",
                Filter = "",
                MaxResultCount = PageDefaults.PageSize,
                SkipCount = 0
            };
        }
        public override async Task InitializeAsync(object navigationData)
        {
            NumeroCompte = (string)navigationData;
            _input.CompteNumeroCompteFilter = NumeroCompte;
        }
        private async Task FetchAllOperationsAsync()
        {
            await WebRequestExecuter.Execute(async () => await _operationsAppService.GetAll(_input), result =>
            {
                var operations = ObjectMapper.Map<List<OperationListModel>>(result.Items);
                foreach (var operation in operations)
                {
                    ConsultationOperations.Add(operation);
                }
                CheckNoOperationsFound();
                OperationListHeight = ConsultationOperations.Count;

                return Task.CompletedTask;
            });
        }
        private async Task RefreshOperationsAsync()
        {
            ConsultationOperations.Clear();

            _input.SkipCount = 0;

            await SetBusyAsync(FetchAllOperationsAsync);
        }
        private void CheckNoOperationsFound()
        {
            NoOperationsFound = ConsultationOperations.Count == 0;
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
