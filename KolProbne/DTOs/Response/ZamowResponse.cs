using KolProbne.Models;
using System.Collections.Generic;

namespace KolProbne.DTOs.Response
{
    public class ZamowResponse
    {
        public Zamowienie Zam { get; set; }
        public List<string> Wyr { get; set; }
    }
}
