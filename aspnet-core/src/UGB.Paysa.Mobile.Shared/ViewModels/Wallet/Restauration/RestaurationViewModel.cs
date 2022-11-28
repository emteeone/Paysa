using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using UGB.Paysa.Commands;
using UGB.Paysa.Core.Threading;
using UGB.Paysa.Models.Users;
using UGB.Paysa.ViewModels.Base;
using UGB.Paysa.Wallet.Operations;
using UGB.Paysa.Wallet.Operations.Dtos;

namespace UGB.Paysa.ViewModels.Wallet.Restauration
{
    public class RestaurationViewModel : XamarinViewModel
    {
        public ICommand PageAppearingCommand => HttpRequestCommand.Create(PageAppearing);
        public ICommand ShowMenuDinerCommand => HttpRequestCommand.Create(ShowMenuDinerAsync);
        public ICommand ShowMenuRepasCommand => HttpRequestCommand.Create(ShowMenuRepasAsync);
        public ObservableRangeCollection<OperationListModel> RestaurationOperations { get; set; }
        private readonly IOperationsAppService _operationsAppService; 
        private GetAllOperationsInput _input;
        private bool _isInitialized;

        public bool _isInMenuDiner;
        public bool _isInMenuRepas;
        public bool IsInMenuDiner
        {
            get => _isInMenuDiner;
            set
            {
                _isInMenuDiner = value;
                RaisePropertyChanged(() => IsInMenuDiner);
            }
        }
        public bool IsInMenuRepas
        {
            get => _isInMenuRepas;
            set
            {
                _isInMenuRepas = value;
                RaisePropertyChanged(() => IsInMenuRepas);
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
        public RestaurationViewModel(IOperationsAppService operationsAppService)
        {
            IsInMenuRepas = true;
            _operationsAppService = operationsAppService;

            RestaurationOperations = new ObservableRangeCollection<OperationListModel>();

            _input = new GetAllOperationsInput
            {
                TypeOperationNomFilter ="RESTAURATION",
                Filter = "",
                MaxResultCount = PageDefaults.PageSize,
                SkipCount = 0
            };
        }

        private async Task ShowMenuRepasAsync()
        {
            IsInMenuDiner = false;
            IsInMenuRepas = true;
        }
        private async Task ShowMenuDinerAsync()
        {
            IsInMenuDiner = true;
            IsInMenuRepas = false;
        }

        private async Task FetchAllOperationsAsync()
        {
            await WebRequestExecuter.Execute(async () => await _operationsAppService.GetAll(_input), result =>
            {
                var operations = ObjectMapper.Map<List<OperationListModel>>(result.Items);
                foreach (var operation in operations)
                {
                    RestaurationOperations.Add(operation);
                }
                CheckNoOperationsFound();
                OperationListHeight = RestaurationOperations.Count;

                return Task.CompletedTask;
            });
        }
        private async Task RefreshOperationsAsync()
        {
            RestaurationOperations.Clear();

            _input.SkipCount = 0;

            await SetBusyAsync(FetchAllOperationsAsync);
        }
        private void CheckNoOperationsFound()
        {
            NoOperationsFound = RestaurationOperations.Count == 0;
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
