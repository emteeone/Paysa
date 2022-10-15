using Abp.Application.Services.Dto;

namespace UGB.Paysa.Authorization.Users.Dto
{
    public interface IGetLoginAttemptsInput: ISortedResultRequest
    {
        string Filter { get; set; }
    }
}