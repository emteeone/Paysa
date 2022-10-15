using System.Collections.Generic;
using MvvmHelpers;
using UGB.Paysa.Models.NavigationMenu;

namespace UGB.Paysa.Services.Navigation
{
    public interface IMenuProvider
    {
        ObservableRangeCollection<NavigationMenuItem> GetAuthorizedMenuItems(Dictionary<string, string> grantedPermissions);
    }
}