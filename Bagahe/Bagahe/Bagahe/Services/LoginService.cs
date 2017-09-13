using Acr.UserDialogs;
using Bagahe.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bagahe.Services
{
    public class LoginService : ILoginService
    {

        private Task addDelay()
        {
            return Task.Delay(2000);
        }

        async Task<bool> ILoginService.ValidateLogin(string username, string password)
        {
            bool isExisting = false;
            using (UserDialogs.Instance.Loading("Logging in..."))
            {
                await addDelay();
            }
            
            if (username == "admin" && password == "admin")
                isExisting = true;

            return isExisting;
        }

    }
}