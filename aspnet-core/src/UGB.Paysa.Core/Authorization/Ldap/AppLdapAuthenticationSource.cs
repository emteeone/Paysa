using Abp.Zero.Ldap.Authentication;
using Abp.Zero.Ldap.Configuration;
using UGB.Paysa.Authorization.Users;
using UGB.Paysa.MultiTenancy;

namespace UGB.Paysa.Authorization.Ldap
{
    public class AppLdapAuthenticationSource : LdapAuthenticationSource<Tenant, User>
    {
        public AppLdapAuthenticationSource(ILdapSettings settings, IAbpZeroLdapModuleConfig ldapModuleConfig)
            : base(settings, ldapModuleConfig)
        {
        }
    }
}