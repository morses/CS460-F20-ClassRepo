using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AsyncExample.Models
{
    public class EarthquakeAPI
    {
        public string Source { get; set; }

		public EarthquakeAPI(string endpoint)
        {
			Source = endpoint;
        }

		/*{"type":"FeatureCollection","metadata":{"generated":1604797642000,"url":"https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/2.5_hour.geojson","title":"USGS Magnitude 2.5+ Earthquakes, Past Hour","status":200,"api":"1.10.3","count":1},"features":[{"type":"Feature","properties":{"mag":5.3,"place":"133 km SW of Lata, Solomon Islands","time":1604795943808,"updated":1604797004040,"tz":null,"url":"https://earthquake.usgs.gov/earthquakes/eventpage/us7000cbyq","detail":"https://earthquake.usgs.gov/earthquakes/feed/v1.0/detail/us7000cbyq.geojson","felt":null,"cdi":null,"mmi":null,"alert":null,"status":"reviewed","tsunami":0,"sig":432,"net":"us","code":"7000cbyq","ids":",us7000cbyq,","sources":",us,","types":",moment-tensor,origin,phase-data,","nst":null,"dmin":4.358,"rms":0.71,"gap":60,"magType":"mww","type":"earthquake","title":"M 5.3 - 133 km SW of Lata, Solomon Islands"},"geometry":{"type":"Point","coordinates":[164.9895,-11.6348,12.63]},"id":"us7000cbyq"}]} */

		public IEnumerable<Earthquake> GetRecentEarthquakes()
        {
			string jsonResponse = SendRequest(Source);
			Debug.WriteLine(jsonResponse);

			JObject geo = JObject.Parse(jsonResponse);
			int count = (int)geo["metadata"]["count"];
			List<Earthquake> output = new List<Earthquake>();
			for(int i = 0; i < count; i++)
            {
				string place = (string)geo["features"][i]["properties"]["place"];
				double mag = (double)geo["features"][i]["properties"]["mag"];
				long ticks = (long)geo["features"][i]["properties"]["time"];
				output.Add(new Earthquake { Location = place, Magnitude = mag, ETime = ticks });
			}

			return output;
        }

        private static string SendRequest(string uri)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
			request.Accept = "application/json";

			string jsonString = null;
			// TODO: You should handle exceptions here
			using (WebResponse response = request.GetResponse())
			{
				Stream stream = response.GetResponseStream();
				StreamReader reader = new StreamReader(stream);
				jsonString = reader.ReadToEnd();
				reader.Close();
				stream.Close();
			}
			return jsonString;
		}

		public async Task<IEnumerable<Earthquake>> GetRecentEarthquakesAsync()
		{
			string jsonResponse = await SendRequestAsync(this.Source);
			//Debug.WriteLine(jsonResponse);

			JObject geo = JObject.Parse(jsonResponse);
			int count = (int)geo["metadata"]["count"];
			List<Earthquake> output = new List<Earthquake>();
			for (int i = 0; i < count; i++)
			{
				string place = (string)geo["features"][i]["properties"]["place"];
				double mag = (double)geo["features"][i]["properties"]["mag"];
				output.Add(new Earthquake { Location = place, Magnitude = mag });
			}

			return output;
		}

		private async static Task<string> SendRequestAsync(string uri)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
			request.Accept = "application/json";

			string jsonString = null;
            try
            {
				HttpWebResponse response = (HttpWebResponse)(await request.GetResponseAsync());

				using (var stream = response.GetResponseStream())
				using (var reader = new StreamReader(stream))
				{
					jsonString = reader.ReadToEnd();
				}
			}
            catch (Exception)
            {
				return jsonString;
            }
			return jsonString;
		}



	}
}
