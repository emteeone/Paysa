using System.ComponentModel;
using Abp.AutoMapper;
using UGB.Paysa.Authorization.Users.Dto;
using UGB.Paysa.Wallet.Operations.Dtos;
using Xamarin.Forms;

namespace UGB.Paysa.Models.Users
{
    [AutoMapFrom(typeof(GetOperationForViewDto))]
    public class OperationListModel : GetOperationForViewDto
    {
        
    }
}