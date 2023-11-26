﻿using System;
using Abp.Extensions;
using static System.Net.WebRequestMethods;

namespace UGB.Paysa.ApiClient
{
    public static class ApiUrlConfig
    {
        private const string DefaultHostUrl = "https://blobus-backend-wepapi.azurewebsites.net/"; //"https://localhost:44301/"; //"http://192.168.1.110:45456/";  //; // ";//; //TODO: Replace with PROD WebAPI URL.

        public static string BaseUrl { get; private set; }

        static ApiUrlConfig()
        {
            ResetBaseUrl();
        }

        public static void ChangeBaseUrl(string baseUrl)
        {
            BaseUrl = ReplaceLocalhost(NormalizeUrl(baseUrl));
        }

        public static void ResetBaseUrl()
        {
            BaseUrl = ReplaceLocalhost(DefaultHostUrl);
        }

        public static bool IsLocal => DefaultHostUrl.Contains("localhost");

        private static string NormalizeUrl(string baseUrl)
        {
            if (!Uri.TryCreate(baseUrl, UriKind.Absolute, out var uriResult) ||
                (uriResult.Scheme != "http" && uriResult.Scheme != "https"))
            {
                throw new ArgumentException("Unexpected base URL: " + baseUrl);
            }

            return uriResult.ToString().EnsureEndsWith('/');
        }

        private static string ReplaceLocalhost(string url)
        {
            return url.Replace("localhost", DebugServerIpAddresses.Current);
        }
    }
}