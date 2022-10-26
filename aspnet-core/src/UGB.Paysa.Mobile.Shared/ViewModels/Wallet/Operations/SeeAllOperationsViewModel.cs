using System.Threading.Tasks;
using System.Windows.Input;
using UGB.Paysa.Commands;
using UGB.Paysa.ViewModels.Base;

namespace UGB.Paysa.ViewModels.Wallet.Operations
{
    public class SeeAllOperationsViewModel : XamarinViewModel
    {
        public ICommand ShowAllOperationsCommand    => HttpRequestCommand.Create(ShowAllOperationsAsync);
        public ICommand ShowCreditOperationsCommand => HttpRequestCommand.Create(ShowCreditOperationsAsync);
        public ICommand ShowDebitOperationsCommand  => HttpRequestCommand.Create(ShowDebitOperationsAsync);

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

        public SeeAllOperationsViewModel()
        {
            IsInAll = true;
        }

        private async Task ShowAllOperationsAsync()
        {
            IsInDebit = false;
            IsInAll = true;
            IsInCredit = false;
        }
        private async Task ShowCreditOperationsAsync()
        {
            IsInDebit = false;
            IsInAll = false;
            IsInCredit = true;
        }
        private async Task ShowDebitOperationsAsync()
        {
            IsInDebit = true;
            IsInAll = false;
            IsInCredit = false;
        }
    }
}