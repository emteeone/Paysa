using UGB.Paysa.Wallet.Chambres.Dtos;

using Abp.Extensions;

namespace UGB.Paysa.Web.Areas.App.Models.Chambres
{
    public class CreateOrEditChambreModalViewModel
    {
        public CreateOrEditChambreDto Chambre { get; set; }

        public bool IsEditMode => !Chambre.Id.IsNullOrWhiteSpace();
    }
}