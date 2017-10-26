﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bagahe.Interfaces;

namespace Bagahe.Services
{
    public class ValidationService : IValidation
    {
        public bool IsNull(string value)
        {
            return string.IsNullOrEmpty(value);
        }
        public bool IsNumeric(string value)
        {
            int num;
            return int.TryParse(value,out num);
        }
        public bool Is10Digits(string value)
        {
            return value.Length == 10 ? true : false;
        }
    }
}
