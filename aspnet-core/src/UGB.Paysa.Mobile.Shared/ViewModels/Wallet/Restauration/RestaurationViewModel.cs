using System;
using System.Threading.Tasks;
using System.Windows.Input;
using UGB.Paysa.Commands;
using UGB.Paysa.ViewModels.Base;

namespace UGB.Paysa.ViewModels.Wallet.Restauration
{
    public class RestaurationViewModel : XamarinViewModel
    {
        public ICommand ShowMenuDinerCommand => HttpRequestCommand.Create(ShowMenuDinerAsync);
        public ICommand ShowMenuRepasCommand => HttpRequestCommand.Create(ShowMenuRepasAsync);
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

        public RestaurationViewModel()
        {
            IsInMenuRepas = true;
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
    }
}
