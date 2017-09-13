using Acr.UserDialogs;
using Bagahe.Interfaces;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Bagahe.ViewModels.Login
{
    class LoginViewModel : BaseViewModel
    {
        private readonly ILoginService _service;
        private readonly IUserDialogs _udialog;
       

        public LoginViewModel(ILoginService service, IUserDialogs dialog)
        {
            _service = service;
            _udialog = dialog;
          
        }

        private string _userName;
        public string Username
        {
            get { return _userName; }
            set
            {
                _userName = value;
                RaisePropertyChanged(() => Username);
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }

        private string _loginErrorMsg;
        public string LoginErrorMsg
        {
            get { return _loginErrorMsg; }
            set
            {
                _loginErrorMsg = value;
                RaisePropertyChanged(() => LoginErrorMsg);
            }
        }
        public ICommand ShowMenuPageCommand
        {
            get
            {
                return new MvxCommand(async () => {

                    bool isValid = validateUserInput();
                    bool isValidUser = false;
                    if (isValid)
                    {
                        UserDialogs.Instance.ShowLoading("Loading", MaskType.Black);

                        

                        isValidUser = await _service.ValidateLogin(Username, Password);
                        

                        if (isValidUser)
                        {
                            LoginErrorMsg = "SUCCESS!";
                        }
                        else
                        {
                            LoginErrorMsg = "Incorrect Username or Password";
                        }

                    }

                });
            }
        }

        public ICommand ShowSignUpCommand
        {
            get
            {
                return new MvxCommand(() => {
                    ShowViewModel<SignupViewModel>();
                });
            }
        }

        private bool validateUserInput()
        {
            bool isValid = false;
            bool isUsernameValid = false;
            bool isPasswordValid = false;

            LoginErrorMsg = "";
            UsernameErrorMsg = "Username is required.";
            PasswordErrorMsg = "Password is required.";

            if (!("").Equals(Username) && Username != null)
            {
                isUsernameValid = true;
                UsernameErrorMsg = "";
            }

            if (!("").Equals(Password) && Password != null)
            {
                isPasswordValid = true;
                PasswordErrorMsg = "";
            }

            if (isPasswordValid && isUsernameValid)
                isValid = true;

            return isValid;
        }

        private string _usernameErrorMsg;
        public string UsernameErrorMsg
        {
            get { return _usernameErrorMsg; }
            set
            {
                _usernameErrorMsg = value;
                RaisePropertyChanged(() => UsernameErrorMsg);
            }
        }

        private string _passwordErrorMsg;
        public string PasswordErrorMsg
        {
            get { return _passwordErrorMsg; }
            set
            {
                _passwordErrorMsg = value;
                RaisePropertyChanged(() => PasswordErrorMsg);
            }
        }




    }
}
