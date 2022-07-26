using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Caching;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Embarque_Escritorio.Sunat.ConsultaRuc
{
    public class CsSunat
    {
        public dynamic GetTipoCambio(string url)
        {
            HttpWebResponse myHttpWebResponse;
            //try
            //{
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create(url);
            //myWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:23.0) Gecko/20100101 Firefox/23.0";
            ////myWebRequest.CookieContainer = myCookie;
            myWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:38.0) Gecko/20100101 Firefox/38.0";
            myWebRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            myWebRequest.Headers.Add("Accept-Encoding", "gzip, deflate");

            myWebRequest.Credentials = CredentialCache.DefaultCredentials;
            myWebRequest.Proxy = null;
            myHttpWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
            //if (myHttpWebResponse.StatusCode == (HttpStatusCode)429)
            //{
            //    // Add delay here
            //    Thread.Sleep(5000);
            //}
            Stream myStream = myHttpWebResponse.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myStream);
            //Leemos los datos
            string Datos = HttpUtility.HtmlDecode(myStreamReader.ReadToEnd());


            dynamic data = JsonConvert.DeserializeObject(Datos);

            return data;
            //}
            //catch (WebException)
            //{
            //    //if (myHttpWebResponse.StatusCode == (HttpStatusCode)429)
            //    //{
            //    //    Throttle();
            //    //}
            //    Throttle();
            //    //throw;
            //}

        }


        public void Throttle()
        {
            var maxPerPeriod = 250;
            //If you utilize multiple accounts, you can throttle per account. If not, don't use this:
            var keyPrefix = "a_unique_id_for_the_basis_of_throttling";
            var intervalPeriod = 300000;//5 minutes
            var sleepInterval = 5000;//period to "sleep" before trying again (if the limits have been reached)
            var recentTransactions = MemoryCache.Default.Count(x => x.Key.StartsWith(keyPrefix));
            while (recentTransactions >= maxPerPeriod)
            {
                System.Threading.Thread.Sleep(sleepInterval);
                recentTransactions = MemoryCache.Default.Count(x => x.Key.StartsWith(keyPrefix));
            }
            var key = keyPrefix + "_" + DateTime.Now.ToUniversalTime().ToString("yyyyMMddHHmm");
            var existing = MemoryCache.Default.Where(x => x.Key.StartsWith(key));
            if (existing != null && existing.Any())
            {
                var counter = 2;
                var last = existing.OrderBy(x => x.Key).Last();
                var pieces = last.Key.Split('_');
                if (pieces.Count() > 2)
                {
                    var lastCount = 0;
                    if (int.TryParse(pieces[2], out lastCount))
                    {
                        counter = lastCount + 1;
                    }
                }
                key = key + "_" + counter;
            }
            var policy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddMilliseconds(intervalPeriod)
            };
            MemoryCache.Default.Set(key, 1, policy);
        }

    }
}
