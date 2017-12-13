using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bagahe.Entities;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Core.Navigation;
using Xamarin.Forms;
using Bagahe.Interfaces;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace Bagahe.ViewModels.Search
{
    public class TrackBaggageResultViewModel : BaseViewModel, IMvxNotifyPropertyChanged
    {
        private readonly IUserDialogs _udialog;
        private readonly IMvxNavigationService _iNavigator;
        private readonly IValidation _validation;

        public TrackBaggageResultViewModel(IMvxNavigationService navigator, IUserDialogs dialog, IValidation validation)
        {
            _udialog = dialog;
            _iNavigator = navigator;
            _validation = validation;
        }

        public TrackBaggageResultViewModel()
        {
        }

        private Params _params = new Params();
        public class Params
        {
            public string Bagtag { get; set; }

            public string PNR { get; set; }

            public string LastName { get; set; }
        }

        public void Init(Params input)
        {
            _params = input;
            DisplayBaggageLocation();
        }
        
        public void DisplayBaggageLocation()
        {
            if (Application.Current.Properties.ContainsKey("token"))
            {
                var token = Convert.ToString(Application.Current.Properties["token"]);
            }
            BagTrackUserInput UserInput = new BagTrackUserInput();
            var trackResponse = new TrackResponse();
            try
            {
                trackResponse = Mvx.Resolve<IRestService>().GetTrackBagScanPoints(UserInput);
                var scanList = new ScanList();
                foreach (var item in trackResponse.BagScanPoint)
                {
                    scanList.Add(new ScanPoints() { Station = item.Station, Location = item.Location, ScanTime = item.ScanTime.ToString("HH:mm MMM dd, yyyy")});
                }
                scanList.Bagtag = _params.Bagtag;
                scanList.PNR = _params.PNR;
                scanList.LastName = _params.LastName;
                var list = new List<ScanList>()
                {
                    scanList
                };
                ScanPointList = list;

            }
            catch (Exception e)
            {
                Mvx.Resolve<IUserDialogs>().Toast(e.Message, null);
            }

            //to be used when BagWebservice is already deployed
            /*try
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("bagtag", input.Bagtag);
                parameters.Add("pnr", input.PNR);
                parameters.Add("lastname", input.LastName);
                parameters.Add("token", Convert.ToString(Application.Current.Properties["token"]));
                var restService = Mvx.Resolve<IRestService>();
                restService.WebMethod = "TrackBagScanPoints";
                restService.Parameters = parameters;

                string returnResponse = await restService.Consume();
                TrackResponse trackResponse = JsonConvert.DeserializeObject<TrackResponse>(returnResponse);
                //var scanpoints = trackResponse.BagScanPoint.ToList();
                var scanpoints = new ScanList();
                foreach (var item in trackResponse.BagScanPoint.ToList())
                {
                    scanpoints.Add(new ScanPoints() { Station = item.Station, Location = item.Location, ScanTime = item.ScanTime });
                }
                scanpoints.Input = _inputData;
                var list = new List<ScanList>()
                {
                    scanpoints
                };
                ScanPointList = list;
            }
            catch (Exception e)
            {
                Mvx.Resolve<IUserDialogs>().Toast(e.Message, null);
            }*/

        }

        public ListView listview;
        private ObservableCollection<ScanPoint> _scanPointCollection;
        public ObservableCollection<ScanPoint> ScanPointCollection
        {
            get { return _scanPointCollection; }
            set
            {
                ScanPointCollection = value;
                base.RaisePropertyChanged();

            }
        }

        public class ScanPoints
        {
            public string Location { get; set; }
            public string Station { get; set; }
            public string ScanTime { get; set; }
        }

        public class ScanList : List<ScanPoints>
        {
            public string Bagtag { get; set; }
            public string PNR { get; set; }
            public string LastName { get; set; }
            public List<ScanPoints> ScanPoints => this;
        }

        private List<ScanList> _scanList;
        public List<ScanList> ScanPointList
        {
            get { return _scanList; }
            set
            {
                _scanList = value;
                base.RaisePropertyChanged();
            }
        }

        //private string _inputData;
        //public string InputData
        //{
        //    get { return _inputData; }
        //    set
        //    {
        //        _inputData = value;
        //        RaisePropertyChanged(() => InputData);
        //    }
        //}



    }
}
