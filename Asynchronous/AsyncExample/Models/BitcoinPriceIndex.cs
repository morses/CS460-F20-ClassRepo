using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AsyncExample.Models
{
    // Model class
    public class BitcoinPriceIndex
    {
        public List<DateTime> Days { get; set; } = new List<DateTime>();
        public List<double> ClosingPrices { get; set; } = new List<double>();
        public string Disclaimer { get; set; }
    }

    public class CoinDeskAPI
    {
        /*
         * API returns the following (note that the prices are in an object rather than an array, making it much
         * harder to extract)
         * {"bpi":{"2021-01-19":35929.1683,"2021-01-20":35510.67,"2021-01-21":30838.5,"2021-01-22":33018.825,"2021-01-23":32106.7033,"2021-01-24":32297.165,"2021-01-25":32255.35,"2021-01-26":32518.3583,"2021-01-27":30425.3933,"2021-01-28":33420.045,"2021-01-29":34264.01,"2021-01-30":34324.2717,"2021-01-31":33129.7433,"2021-02-01":33543.77,"2021-02-02":35528.31,"2021-02-03":37685.2767,"2021-02-04":36984.6783,"2021-02-05":38306.2467,"2021-02-06":39269.3417,"2021-02-07":38862.35,"2021-02-08":46436.09,"2021-02-09":46502.2933,"2021-02-10":44855.6167,"2021-02-11":48004.6533,"2021-02-12":47410.4033,"2021-02-13":47211.6683,"2021-02-14":48633.26,"2021-02-15":47934.1267,"2021-02-16":49185.7283,"2021-02-17":52127.32,"2021-02-18":51573.4067},"disclaimer":"This data was produced from the CoinDesk Bitcoin Price Index. BPI value data returned as USD.","time":{"updated":"Feb 19, 2021 00:03:00 UTC","updatedISO":"2021-02-19T00:03:00+00:00"}}
         **/

        public static BitcoinPriceIndex GetBPI()
        {
            // Make the request
            RestClient client = new RestClient("https://api.coindesk.com/v1");
            RestRequest request = new RestRequest("bpi/historical/close.json", DataFormat.Json);

            var response = client.Get(request);

            // Parse json using Newtonsoft and store in our model class
            JObject obj = JObject.Parse(response.Content);
            BitcoinPriceIndex bpi = new BitcoinPriceIndex();
            bpi.Disclaimer = (string)obj["disclaimer"];
            var priceDict = obj["bpi"].ToObject<Dictionary<string,double>>();
            foreach(KeyValuePair<string,double> entry in priceDict)
            {
                bpi.Days.Add(DateTime.Parse(entry.Key));
                bpi.ClosingPrices.Add(entry.Value);
            }
            return bpi;
        }

        public static async Task<BitcoinPriceIndex> GetBPIAsync()
        {
            // Make the request using Async
            RestClient client = new RestClient("https://api.coindesk.com/v1");
            RestRequest request = new RestRequest("bpi/historical/close.json", DataFormat.Json);

            var response = await client.ExecuteAsync(request);

            // Parse json using Newtonsoft and store in our model class
            JObject obj = JObject.Parse(response.Content);
            BitcoinPriceIndex bpi = new BitcoinPriceIndex();
            bpi.Disclaimer = (string)obj["disclaimer"];
            var priceDict = obj["bpi"].ToObject<Dictionary<string, double>>();
            foreach (KeyValuePair<string, double> entry in priceDict)
            {
                bpi.Days.Add(DateTime.Parse(entry.Key));
                bpi.ClosingPrices.Add(entry.Value);
            }
            return bpi;
        }


    }
}
