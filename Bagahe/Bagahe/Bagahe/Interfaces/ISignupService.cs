using Bagahe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bagahe.Interfaces
{
    interface ISignupService
    {
        Task<bool> AddNewUser(SignupFields SignupField);
    }
}
