using UGB.Paysa.Wallet.Etudiants;
using UGB.Paysa.Wallet.Chambres;
using System;
using System.Linq;
using Abp.Organizations;
using UGB.Paysa.Authorization.Roles;
using UGB.Paysa.MultiTenancy;

namespace UGB.Paysa.EntityHistory
{
    public static class EntityHistoryHelper
    {
        public const string EntityHistoryConfigurationName = "EntityHistory";

        public static readonly Type[] HostSideTrackedTypes =
        {
            typeof(Etudiant),
            typeof(Chambre),
            typeof(OrganizationUnit), typeof(Role), typeof(Tenant)
        };

        public static readonly Type[] TenantSideTrackedTypes =
        {
            typeof(Etudiant),
            typeof(Chambre),
            typeof(OrganizationUnit), typeof(Role)
        };

        public static readonly Type[] TrackedTypes =
            HostSideTrackedTypes
                .Concat(TenantSideTrackedTypes)
                .GroupBy(type => type.FullName)
                .Select(types => types.First())
                .ToArray();
    }
}