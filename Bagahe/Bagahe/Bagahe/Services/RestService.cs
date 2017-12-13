using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Bagahe.Entities;
using Bagahe.Interfaces;
using System.Net.Http;
using Newtonsoft.Json;
using System.Reflection;
using System.Xml.Linq;
using System.Net.Http.Headers;

namespace Bagahe.Services
{
    public class RestService : IRestService
    {
        HttpClient client;

        public RestService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private Dictionary<string, string> _parameters;
        public Dictionary<string, string> Parameters
        {
            set
            {
                _parameters = value;
            }
        }

        private Dictionary<string, string> _webConfig;

        public string WebMethod
        {
            set
            {
                _webConfig = new Dictionary<string, string>();
                Assembly assembly = typeof(CustomAppStart).GetTypeInfo().Assembly;

                using (var stream = assembly.GetManifestResourceStream("Bagahe.webapi.config"))
                using (var reader = new StreamReader(stream))
                {
                    var doc = XDocument.Parse(reader.ReadToEnd());
                    foreach (XElement xe in doc.Elements("Services").Elements("Service"))
                    {
                        if (xe.Attribute("Name").Value == value)
                        {
                            _webConfig.Add("method", xe.Element("Method").Value);
                            _webConfig.Add("methodUri", xe.Element("MethodURI").Value);
                            _webConfig.Add("actionUri", xe.Element("ActionURI").Value);
                            _webConfig.Add("contentType", xe.Element("ContentType").Value);
                        }
                    }
                    /// use the to update value of _webConfig like uri, method, content etc.
                    /// read the values from a local XML config
                }
            }
        }

        public async Task<AuthenticateUserResponse> AuthenticateUser(AuthenticateUserInput input)
        {
            var items = new AuthenticateUserResponse();
            //URL of Passenger Authenticate Web service
            var restUrl = "http://172.26.82.69:5000/PassengerWebservice/login";
            var uri = new Uri(string.Format(restUrl, string.Empty));
            var json = JsonConvert.SerializeObject(input);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                var contents = await response.Content.ReadAsStringAsync();
                items = JsonConvert.DeserializeObject<AuthenticateUserResponse>(contents);
            }
                       
            return items;
        }

        public async Task<string> Consume()
        {
            Uri uri = new Uri(_webConfig["actionUri"]);
            /////write the rest of the parameters in postData
            _parameters.Add("Station", "MNL");
            _parameters.Add("Device", "Android");
            _parameters.Add("Version", "0.1");
            string json = JsonConvert.SerializeObject(_parameters, Formatting.Indented);
            var content = new StringContent(json, Encoding.UTF8, _webConfig["contentType"]);

            var response = await client.PostAsync(uri, content);
            var returnResponse = await response.Content.ReadAsStringAsync();
            

            return returnResponse;
        }

        public GetBagInfoResponse GetBagInfo(GetBagInfoInput input)
        {
            GetBagInfoResponse getBagInfoResponse = new GetBagInfoResponse();
            getBagInfoResponse.FltCode = "DL";
            getBagInfoResponse.FltDate = DateTime.Now.ToString("MMMdd");
            getBagInfoResponse.FltNum = "1234";
            getBagInfoResponse.StatusCode = "0";
            getBagInfoResponse.PaxName = "Admin Admin";
            getBagInfoResponse.PaxItinerary = "MNL-NRT-MSP";
            getBagInfoResponse.Latitude = "47.636372";
            getBagInfoResponse.Longitude = "-122.126888";


            List<ScanPoint> scanHistory = new List<ScanPoint>();
            scanHistory.Add(newscanPoint("ic_checkin", "Checkin - MNL - Manila, Philippines"));
            scanHistory.Add(newscanPoint("ic_departure", "Departure - MNL - Manila, Philippines"));
            scanHistory.Add(newscanPoint("ic_arrival", "Arrival - NRT - Tokyo, Japan"));
            scanHistory.Add(newscanPoint("ic_departure", "Departure - NRT - Tokyo, Japan"));
            scanHistory.Add(newscanPoint("ic_arrival", "Arrival - MSP - Minneapolis, USA"));
            scanHistory.Add(newscanPoint("ic_departure", "Departure - MSP - Minneapolis, USA"));
            getBagInfoResponse.BagHistory = scanHistory.ToArray<ScanPoint>();

            return getBagInfoResponse;

        }

        private ScanPoint newscanPoint(string icon, string name)
        {
            ScanPoint scanPoint = new ScanPoint();
            scanPoint.ScanType = icon;
            scanPoint.Location = name;
            scanPoint.DateTime = DateTime.Now.ToString("HH:mm MMM dd, yyyy");
            return scanPoint;
        }

        public async Task<TrackResponse> TrackBagScanPoints(BagTrackUserInput input)
        {
            var items = new TrackResponse();
            //URL of BagWebservice Web service Track method
            var restUrl = "";
            var uri = new Uri(string.Format(restUrl, string.Empty));
            var json = JsonConvert.SerializeObject(input);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                var contents = await response.Content.ReadAsStringAsync();
                items = JsonConvert.DeserializeObject<TrackResponse>(contents);
            }

            return items;
        }

        public TrackResponse GetTrackBagScanPoints(BagTrackUserInput input)
        {
            TrackResponse response = new TrackResponse();

            List<BagScanPoints> scanPoints = new List<BagScanPoints>();
            scanPoints.Add(scanPoint("Pier", "MSP"));
            scanPoints.Add(scanPoint("Claim", "MSP"));
            scanPoints.Add(scanPoint("BSO", "MSP"));

            response.BagScanPoint = scanPoints.ToArray<BagScanPoints>();

            return response;
        }

        private BagScanPoints scanPoint(string location, string station)
        {
            BagScanPoints scanPoint = new BagScanPoints();
            scanPoint.Location = location;
            scanPoint.Station = station;
            scanPoint.ScanTime = DateTime.Now;
            return scanPoint;
        }
    }
}
