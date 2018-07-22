using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Web.Country.FactBook.ApiIntegrations.HttpHelpers
{
    internal static class Urls
    {
        public static string BaseUrl { get; set; }
        public static string Token
        {
            get { return BaseUrl + "/oauth2/accesstoken"; }
        }
    }
}