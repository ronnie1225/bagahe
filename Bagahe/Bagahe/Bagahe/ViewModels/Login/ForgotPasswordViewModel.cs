using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using System.Windows.Input;
using System.Text.RegularExpressions;
using Bagahe.Interfaces;
using Acr.UserDialogs;

namespace Bagahe.ViewModels.Login
{
    class ForgotPasswordViewModel : BaseViewModel
    {
        private readonly IForgotPasswordService _service;
        private readonly IUserDialogs _udialog;

        public ForgotPasswordViewModel(IForgotPasswordService service, IUserDialogs dialog)
        {
            _service = service;
            _udialog = dialog;
        }


        public ICommand ResetPasswordCommand
        {
            get
            {
                return new MvxCommand(async () => {
                    if (isValidEmail())
                    {
                        bool isExist = false;
                        //check if the email exists in the DB
                        using (_udialog.Loading("Checking email..."))
                        {
                            isExist = await _service.CheckEmail(EmailAdd);
                        }

                        if (!isExist)
                            ErrorMsg = "The email you have entered is not yet registered.";
                        else
                        {
                            //Send email
                            using (_udialog.Loading("Sending email..."))
                            {
                                await Task.Delay(2000);
                            }
                            //Redirect
                            ErrorMsg = "Valid Email";
                        }
                            
                    }
                });
            }

        }


        public ICommand BackCommand
        {
            get
            {
                return new MvxCommand(() => ShowViewModel<LoginViewModel>());
            }
        }

        private bool isValidEmail()
        {
            bool isValid = true;
            if (("").Equals(EmailAdd) || EmailAdd == null)
            {
                ErrorMsg = "The email address is required.";
                isValid = false;
            }
            else
            {
                string regex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
                bool isEmail = Regex.IsMatch(EmailAdd, regex, RegexOptions.IgnoreCase);
                if (!isEmail)
                {
                    ErrorMsg = "The email address you entered is not valid.";
                    isValid = false;
                }
            }

            return isValid;
        }

        private string _emailAdd;
        public string EmailAdd
        {
            get { return _emailAdd; }
            set
            {
                _emailAdd = value;
                RaisePropertyChanged(() => EmailAdd);
            }
        }

        private string _errorMsg;
        public string ErrorMsg
        {
            get { return _errorMsg; }
            set
            {
                _errorMsg = value;
                RaisePropertyChanged(() => ErrorMsg);
            }
        }
    }
}
