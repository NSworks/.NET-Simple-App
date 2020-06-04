using System;
using System.Collections.Generic;

namespace KolProbne.DTOs.Request
{
    public class ZamowRequest
    {
        public DateTime dataPrzyjecia { get; set; }
        public string Uwagi { get; set; }
        public List<WyrobRequest> Wyroby { get; set; }
    }
}
