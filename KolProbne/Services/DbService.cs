using KolProbne.DTOs.Request;
using KolProbne.DTOs.Response;
using KolProbne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KolProbne.Services
{
    public class DbService : ICukierniaDbService
    {
        public CukierniaDbContext _context { get; set; }
        public DbService(CukierniaDbContext context)
        {
            _context = context;
        }
        public List<ZamowResponse> GetOrders(string nazwisko)
        {
            var listaOdp = new List<ZamowResponse>();
            try
            {
                if ((_context.Klienci.Any(e => e.Nazwisko.Equals(nazwisko))))
                {

                    var lista = _context.Zamowienia.Where(e => e.Klient.Nazwisko.Equals(nazwisko)).ToList();
                    foreach (Zamowienie zam in lista)
                    {
                        var id = _context.Zamowienie_WyrobyCukiernicze.Where(e => e.IdZamowienia == zam.IdZamowienia).Select(e => e.IdWyrobuCukierniczego).ToList();
                        var list2 = new List<string>();
                        foreach (int Id in id)
                        {
                            list2.Add(_context.WyrobCukiernicze.Where(e => e.IdWyrobuCukierniczego == Id).Select(e => e.Nazwa).FirstOrDefault());
                        }
                        var odp = new ZamowResponse { Zam = zam, Wyr = list2 };
                        listaOdp.Add(odp);
                    }
                    return listaOdp;

                }
                else
                {
                    Zamowienie zamo = new Zamowienie { Uwagi = "Error" };
                    var odp = new ZamowResponse { Zam = zamo, Wyr = null };
                    listaOdp.Add(odp);
                    return listaOdp;
                }

            }
            catch (Exception e)
            {
                Zamowienie zamo = new Zamowienie { Uwagi = "Baza" };
                var odp = new ZamowResponse { Zam = zamo, Wyr = null };
                listaOdp.Add(odp);
                return listaOdp;
            }

        }

        public List<ZamowResponse> GetOrders()
        {
            var listaOdp = new List<ZamowResponse>();
            try
            {
                var lista = _context.Zamowienia.ToList();
                foreach (Zamowienie zam in lista)
                {
                    var id = _context.Zamowienie_WyrobyCukiernicze.Where(e => e.IdZamowienia == zam.IdZamowienia).Select(e => e.IdWyrobuCukierniczego).ToList();
                    var list2 = new List<string>();
                    foreach (int Id in id)
                    {
                        list2.Add(_context.WyrobCukiernicze.Where(e => e.IdWyrobuCukierniczego == Id).Select(e => e.Nazwa).FirstOrDefault());
                    }
                    var odp = new ZamowResponse { Zam = zam, Wyr = list2 };
                    listaOdp.Add(odp);
                }
                return listaOdp;
            }
            catch (Exception e)
            {

                Zamowienie zamo = new Zamowienie { Uwagi = "Baza" };
                var odp = new ZamowResponse { Zam = zamo, Wyr = null };
                listaOdp.Add(odp);
                return listaOdp;
            }
        }

        public ZamowResponse NewOrder(int id, ZamowRequest zam)
        {
            if (!(_context.Klienci.Any(e => e.IdKlient == id)))
            {
                var zamow = new Zamowienie
                {

                    Uwagi = "klient brak"
                };
                var zamowie = new ZamowResponse { Zam = zamow };
                return zamowie;
            }
            foreach (WyrobRequest w in zam.Wyroby)
            {
                if (!(_context.WyrobCukiernicze.Any(wyrob => wyrob.Nazwa == w.Wyrob)))
                {
                    var zamow = new Zamowienie
                    {

                        Uwagi = "brak"
                    };
                    var zamowie = new ZamowResponse { Zam = zamow };
                    return zamowie;
                }
            }
            try
            {
                var zamowienieNowe = new Zamowienie { IdPracownik = 1, DataPrzyjecia = zam.dataPrzyjecia, IdKlient = id, Uwagi = zam.Uwagi, Zamowienie_WyrobCukiernicze = new List<Zamowienie_WyrobCukierniczy>() };
                var wyrobyNazwy = new List<string>();
                foreach (WyrobRequest wyr in zam.Wyroby)
                {
                    int Id = _context.WyrobCukiernicze.FirstOrDefault(wyrob => wyrob.Nazwa == wyr.Wyrob).IdWyrobuCukierniczego;
                    zamowienieNowe.Zamowienie_WyrobCukiernicze.Add(new Zamowienie_WyrobCukierniczy { IdWyrobuCukierniczego = Id, Uwagi = wyr.Uwagi, Ilosc = wyr.Ilosc });
                    wyrobyNazwy.Add(wyr.Wyrob);
                }

                var zamowie = new ZamowResponse { Zam = zamowienieNowe, Wyr = wyrobyNazwy };
                _context.Add(zamowienieNowe);
                _context.SaveChanges();

                return zamowie;
            }
            catch (Exception e)
            {
                var zamow = new Zamowienie
                {
                    Uwagi = "error"
                };
                var zamowie = new ZamowResponse { Zam = zamow };
                return zamowie;
            }

        }

        List<ZamowResponse> ICukierniaDbService.GetOrders(string nazwisko)
        {
            throw new NotImplementedException();
        }

        List<ZamowResponse> ICukierniaDbService.GetOrders()
        {
            throw new NotImplementedException();
        }

    }
}
