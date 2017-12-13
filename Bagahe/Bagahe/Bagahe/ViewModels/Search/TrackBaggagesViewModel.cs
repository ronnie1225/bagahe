using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Bagahe.Interfaces;
using Bagahe.Entities;
using Bagahe.Views;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Core.Navigation;
using Acr.UserDialogs;
using Xamarin.Forms;


namespace Bagahe.ViewModels.Search
{
    class TrackBaggagesViewModel : BaseViewModel
    {
        private readonly IValidation _iValidator;
        private readonly IMvxNavigationService _iNavigator;

        public TrackBaggagesViewModel(IValidation validator, IMvxNavigationService navigator)
        {
            _iValidator = validator;
            _iNavigator = navigator;
        }

        private string _bagtag;
        public string Bagtag
        {
            get { return _bagtag;  }
            set
            {
                _bagtag = value;
                RaisePropertyChanged(() => Bagtag);
            }
        }

        private string _PNR;
        public string PNR
        {
            get { return _PNR; }
            set
            {
                _PNR = value;
                RaisePropertyChanged(() => PNR);
            }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                RaisePropertyChanged(() => LastName);
            }
        }

        private string _inputErrorMsg;
        public string InputErrorMsg
        {
            get { return _inputErrorMsg; }
            set
            {
                _inputErrorMsg = value;
                RaisePropertyChanged(() => InputErrorMsg);
            }
        }

        public ICommand TrackResultCommand
        {
            get
            {
                return new MvxCommand( () =>
                {
                    if (IsValid() && Application.Current.Properties.ContainsKey("token"))
                    {
                        try
                        {
                           ShowViewModel<TrackBaggageResultViewModel>(new TrackBaggageResultViewModel.Params { Bagtag = Bagtag, PNR = PNR, LastName = LastName});
                        }
                        catch (Exception e)
                        {
                            Mvx.Resolve<IUserDialogs>().Toast(e.Message, null);
                        }
                    }
                    else
                    {
                        InputErrorMsg = "Incorrect Bagtag or Claim/File Number or Lastname";
                    }
                });
            }
        }

        private bool IsValid()
        {
            return (IsLastNameValid() && IsPNRValid() && IsBagtagValid()) ? true : false;
        }

        private bool IsLastNameValid()
        {
            bool ret = false;
            if (Application.Current.Properties.ContainsKey("name"))
            {
                var name = Convert.ToString(Application.Current.Properties["name"]);
                string[] substrings = name.Split(' ');
                foreach (var substring in substrings)
                {
                    if (substring.ToLower() == _lastName.ToLower())
                    {
                        ret = true;
                    }
                }

            }
            return ret;
        }

        private bool IsPNRValid()
        {
            return _iValidator.IsPNRValid(_PNR);
        }

        private bool IsBagtagValid()
        {
            return !_iValidator.IsNull(_bagtag) && _iValidator.Is10Digits(_bagtag)
                        && _iValidator.IsNumeric(_bagtag);
        }
    }
}
