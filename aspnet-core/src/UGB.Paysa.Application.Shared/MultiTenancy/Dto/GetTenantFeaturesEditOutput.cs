using System.Collections.Generic;
using Abp.Application.Services.Dto;
using UGB.Paysa.Editions.Dto;

namespace UGB.Paysa.MultiTenancy.Dto
{
    public class GetTenantFeaturesEditOutput
    {
        public List<NameValueDto> FeatureValues { get; set; }

        public List<FlatFeatureDto> Features { get; set; }
    }
}