using MvvmHelpers;
using UGB.Paysa.Models.Users;
using UGB.Paysa.ViewModels.Base;
using UGB.Paysa.Wallet.Operations;
using UGB.Paysa.Wallet.Operations.Dtos;

namespace UGB.Paysa.ViewModels.Wallet.Logement
{
    public class LogementViewModel : XamarinViewModel
    {
        public ObservableRangeCollection<OperationListModel> LogementOperations { get; set; }

        private readonly IOperationsAppService _operationsAppService;
        private GetAllOperationsInput _input;
        private bool _isInitialized;
        public LogementViewModel(IOperationsAppService operationsAppService)
        {
            _operationsAppService = operationsAppService;

            LogementOperations = new ObservableRangeCollection<OperationListModel>();

            _input = new GetAllOperationsInput
            {
                Filter = "",
                MaxResultCount = PageDefaults.PageSize,
                SkipCount = 0
            };
        }
    }
}
