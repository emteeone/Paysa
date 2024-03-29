﻿namespace UGB.Paysa.Web.Areas.App.Startup
{
    public class AppPageNames
    {
        public static class Common
        {
            public const string PaiementLoyers = "Chambres.PaiementLoyers";
            public const string TypeOperations = "Operations.TypeOperations";
            public const string Tools = "Tools";
            public const string Transactions = "Operations";
            public const string Operations = "Operations.Operations";
            public const string Terminaux = "Tools.Terminaux";
            public const string Cartes = "Tools.Cartes";
            public const string Comptes = "Comptes.Comptes";
            public const string Etudiants = "Etudiants.Etudiants";
            public const string Chambres = "Chambres.Chambres";
            public const string Administration = "Administration";
            public const string Roles = "Administration.Roles";
            public const string Users = "Administration.Users";
            public const string AuditLogs = "Administration.AuditLogs";
            public const string OrganizationUnits = "Administration.OrganizationUnits";
            public const string Languages = "Administration.Languages";
            public const string DemoUiComponents = "Administration.DemoUiComponents";
            public const string UiCustomization = "Administration.UiCustomization";
            public const string WebhookSubscriptions = "Administration.WebhookSubscriptions";
            public const string DynamicProperties = "Administration.DynamicProperties";
            public const string DynamicEntityProperties = "Administration.DynamicEntityProperties";
        }

        public static class Host
        {
            public const string Tenants = "Tenants";
            public const string Editions = "Editions";
            public const string Maintenance = "Administration.Maintenance";
            public const string Settings = "Administration.Settings.Host";
            public const string Dashboard = "Dashboard";
        }

        public static class Tenant
        {
            public const string Dashboard = "Dashboard.Tenant";
            public const string Settings = "Administration.Settings.Tenant";
            public const string SubscriptionManagement = "Administration.SubscriptionManagement.Tenant";
        }
    }
}