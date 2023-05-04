using Abp.Application.Services.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using UGB.Paysa.Authorization.Users;
using UGB.Paysa.Authorization.Users.Dto;
using UGB.Paysa.Models.Users;
using UGB.Paysa.UI.Assets;
using UGB.Paysa.ViewModels.Base;

namespace UGB.Paysa.ViewModels
{
    public class ProfileViewModel : XamarinViewModel
    {
        private readonly IUserAppService _userAppService;
        private UserEditOrCreateModel _model;

        public ProfileViewModel(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }
        public UserEditOrCreateModel Model
        {
            get => _model;
            set
            {
                _model = value;
                RaisePropertyChanged(() => Model);
            }
        }
        public override async Task InitializeAsync(object navigationData)
        {
            await SetBusyAsync(async () =>
            {
                var userListModel = (long)navigationData;
                var user = await _userAppService.GetUserForEdit(new NullableIdDto<long>(userListModel));
                Model = ObjectMapper.Map<UserEditOrCreateModel>(user);
            });
        }
    }
}
