using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Country.FactBook.ApiIntegrations.HttpHelpers
{
    public static class Mapper<T>
    {
        public static T MapJsonStringToObject(string json, string parentToken = null)
        {
            var jsonToParse = string.IsNullOrEmpty(parentToken) ? json : JObject.Parse(json).SelectToken(parentToken).ToString();
            return JsonConvert.DeserializeObject<T>(jsonToParse);
        }

        public static string MapObjectToJsonString(dynamic objCreateOptions)
        {
            return JsonConvert.SerializeObject(objCreateOptions);
        }
    }
}
