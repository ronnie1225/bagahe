using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Bagahe.Interfaces;
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

        private string _inputData;
        public string InputData
        {
            get { return _inputData;  }
            set
            {
                _inputData = value;
                RaisePropertyChanged(() => InputData);
            }
        }

    }
}
