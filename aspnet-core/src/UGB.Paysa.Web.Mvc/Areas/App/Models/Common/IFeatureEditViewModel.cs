using System.Collections.Generic;
using Abp.Application.Services.Dto;
using UGB.Paysa.Editions.Dto;

namespace UGB.Paysa.Web.Areas.App.Models.Common
{
    public interface IFeatureEditViewModel
    {
        List<NameValueDto> FeatureValues { get; set; }

        List<FlatFeatureDto> Features { get; set; }
    }
}