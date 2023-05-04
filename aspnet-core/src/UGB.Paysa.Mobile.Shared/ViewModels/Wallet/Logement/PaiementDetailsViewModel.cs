using System.Threading.Tasks;
using UGB.Paysa.Models.Users;
using UGB.Paysa.ViewModels.Base;

namespace UGB.Paysa.ViewModels
{
    public class PaiementDetailsViewModel : XamarinViewModel
    {
        public PaiementChambreListModel PaiementInfo;
        public string ReferenceChambre;
        public PaiementDetailsViewModel()
        {

        }
        public override async Task InitializeAsync(object navigationData)
        {
            PaiementInfo = (PaiementChambreListModel)navigationData;
            ReferenceChambre = "hhhhhhhhhhhh";
        }
    }
}
