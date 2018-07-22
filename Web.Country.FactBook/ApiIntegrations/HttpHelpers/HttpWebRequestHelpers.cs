using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Web.Country.FactBook.ApiIntegrations.HttpHelpers
{
    public class HttpWebRequestHelpers
    {
        public static string GetWebRequest(string endPoint, string method)
        {
            try
            {
                HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(endPoint);
                objRequest.Method = method;
                objRequest.ContentType = "application/json";
                objRequest.Accept = "application/json";

                string postResponse = "";
                HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
                using (StreamReader responseStream = new StreamReader(objResponse.GetResponseStream()))
                {
                    postResponse = responseStream.ReadToEnd();
                    responseStream.Close();
                }

                return postResponse;
            }
            catch (Exception ex)
            {
            }
            return null;
        }
    }
}