using UGB.Paysa.Wallet.Tools.Dtos;

using Abp.Extensions;

namespace UGB.Paysa.Web.Areas.App.Models.Terminaux
{
    public class CreateOrEditTerminalModalViewModel
    {
        public CreateOrEditTerminalDto Terminal { get; set; }

        public bool IsEditMode => !Terminal.Id.IsNullOrWhiteSpace();
    }
}