using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;

namespace Bagahe.Models
{
    [Table("BagInfoModel")]
    public class BagInfoModel
    {
        [PrimaryKey]
        public string Bagtag { get; set; }
        public string FltCode { get; set; }
        public string FltNum { get; set; }
        public string FltDate { get; set; }
        public string PaxItinerary { get; set; }
        public string PaxName { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }

        public List<BagScanPoint> BagScanPoints { get; set; }

        public string BagScanPointsBlobbed { get; set; }

    }

    public class BagScanPoint
    {
        public string ScanTime { get; set; }
        public string ScanPoint { get; set; }
        public string Icon { get; set; }

    }
}