﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bagahe.Entities
{
    public class TrackResponse : BaseResponse
    {
        public BagScanPoints[] BagScanPoint { get; set; }
    }

    public class BagScanPoints
    {
        public string Location { get; set; }

        public string Station { get; set; }

        public DateTime ScanTime { get; set; }
    }
}
