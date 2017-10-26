using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bagahe.Interfaces
{
    public interface IValidation
    {
        bool IsNull(string value);
        bool IsNumeric(string value);
        bool Is10Digits(string value);        
    }
}
