using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bagahe.ViewModels.Login
{
    class SignupResultViewModel : BaseViewModel
    {
        public ICommand SignInCommand
        {
            get
            {
                return new MvxCommand(() =>
                {
                    ShowViewModel<LoginViewModel>();
                });
            }
        }
    }
}
