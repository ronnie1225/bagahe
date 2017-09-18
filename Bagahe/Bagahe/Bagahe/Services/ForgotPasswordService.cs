using Acr.UserDialogs;
using Bagahe.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bagahe.Services
{
    class ForgotPasswordService : IForgotPasswordService
    {
        async Task<bool> IForgotPasswordService.CheckEmail(string email)
        {
            bool isExisting = false;
            using (UserDialogs.Instance.Loading("Checking email..."))
            {
                //checking the email to DB
                await addDelay();
            }
            if (email.Equals("admin@admin.com"))
            {
                //Send email
                using(UserDialogs.Instance.Loading("Sending email..."))
                {
                    isExisting = true;
                    await addDelay();
                }
               
            }
            return isExisting;
        }

        private Task addDelay()
        {
            return Task.Delay(2000);
        }
    }
}
