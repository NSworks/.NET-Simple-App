using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KolProbne.Models
{
    public class Zamowienie_WyrobCukierniczy
    {
        public int Ilosc { get; set; }
        public string Uwagi { get; set; }
        public int IdWyrobuCukierniczego { get; set; }
        public int IdZamowienia { get; set; }
        public virtual WyrobCukierniczy WyrobCukierniczy { get; set; }
        public virtual Zamowienie Zamowienie { get; set; }
    }
}
