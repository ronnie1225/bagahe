using Bagahe.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bagahe.Models;
using Acr.UserDialogs;

namespace Bagahe.Services
{
    class SignupService : ISignupService
    {
        private Task addDelay()
        {
            return Task.Delay(2000);
        }

        async public Task<bool> AddNewUser(SignupFields SignupField)
        {
            bool isAdded = false;
            using (UserDialogs.Instance.Loading("Creating account..."))
            {
                //Code for adding the inputs to the database here...
                //Check if the inputs are existing in the DB
                await addDelay();
            }

            //Code for validating if the inputs are successfully added to the db here ...
            isAdded = true;

            return isAdded;
        }
    }
}
