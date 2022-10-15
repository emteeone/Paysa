using System.Collections.Generic;
using UGB.Paysa.Authorization.Delegation;
using UGB.Paysa.Authorization.Users.Delegation.Dto;

namespace UGB.Paysa.Web.Areas.App.Models.Layout
{
    public class ActiveUserDelegationsComboboxViewModel
    {
        public IUserDelegationConfiguration UserDelegationConfiguration { get; set; }
        
        public List<UserDelegationDto> UserDelegations { get; set; }
    }
}
