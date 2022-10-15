using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace UGB.Paysa.Web.Areas.App.Models.Users
{
    public class UserLoginAttemptsViewModel
    {
        public List<ComboboxItemDto> LoginAttemptResults { get; set; }
    }
}