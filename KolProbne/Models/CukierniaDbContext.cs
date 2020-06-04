using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KolProbne.Models
{
    public class CukierniaDbContext : DbContext
    {
        public DbSet<Pracownik> Pracownicy { get; set; }
        public DbSet<Klient> Klienci { get; set; }
        public DbSet<WyrobCukierniczy> WyrobCukiernicze { get; set; }
        public DbSet<Zamowienie> Zamowienia { get; set; }
        public DbSet<Zamowienie_WyrobCukierniczy> Zamowienie_WyrobyCukiernicze { get; set; }

        public CukierniaDbContext()
        {

        }
        public CukierniaDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Pracownik>((builder) =>
            {
                builder.HasKey(e => e.IdPracownik);
                builder.Property(e => e.IdPracownik).ValueGeneratedOnAdd();
                builder.Property(e => e.Imie).HasMaxLength(50).IsRequired();
                builder.Property(e => e.Nazwisko).HasMaxLength(60).IsRequired();

                builder.HasMany(e => e.Zamowienia).WithOne(e => e.Pracownik).HasForeignKey(e => e.IdPracownik).IsRequired();

            });

            modelBuilder.Entity<Klient>((builder) =>
            {
                builder.HasKey(e => e.IdKlient);
                builder.Property(e => e.IdKlient).ValueGeneratedOnAdd();
                builder.Property(e => e.Imie).HasMaxLength(50).IsRequired();
                builder.Property(e => e.Nazwisko).HasMaxLength(60).IsRequired();

                builder.HasMany(e => e.Zamowienia).WithOne(e => e.Klient).HasForeignKey(e => e.IdKlient).IsRequired();

            });

            modelBuilder.Entity<WyrobCukierniczy>((builder) =>
            {
                builder.HasKey(e => e.IdWyrobuCukierniczego);
                builder.Property(e => e.IdWyrobuCukierniczego).ValueGeneratedOnAdd();
                builder.Property(e => e.Nazwa).HasMaxLength(200).IsRequired();
                builder.Property(e => e.CenaZaSzt).IsRequired();
                builder.Property(e => e.Typ).HasMaxLength(40).IsRequired();

                builder.HasMany(e => e.Zamowienie_WyrobCukiernicze)
                       .WithOne(e => e.WyrobCukierniczy)
                       .HasForeignKey(e => e.IdWyrobuCukierniczego)
                       .IsRequired();

            });

            modelBuilder.Entity<Zamowienie>((builder) =>
            {
                builder.HasKey(e => e.IdZamowienia);
                builder.Property(e => e.IdZamowienia).ValueGeneratedOnAdd();
                builder.Property(e => e.DataPrzyjecia).IsRequired();
                builder.Property(e => e.DataRealizacji);
                builder.Property(e => e.Uwagi).HasMaxLength(300);
                builder.HasMany(p => p.Zamowienie_WyrobCukiernicze)
                       .WithOne(p => p.Zamowienie)
                       .HasForeignKey(p => p.IdZamowienia)
                       .IsRequired();

            });

            modelBuilder.Entity<Zamowienie_WyrobCukierniczy>((builder) =>
            {
                builder.HasKey(e => new
                {
                    e.IdWyrobuCukierniczego,
                    e.IdZamowienia
                });

                builder.Property(e => e.Ilosc).IsRequired();
                builder.Property(e => e.Uwagi).HasMaxLength(300);

            });
            var dictProcownicy = new List<Pracownik>();
            dictProcownicy.Add(new Pracownik { IdPracownik = 1, Imie = "Pracownik1", Nazwisko = "1" });
            dictProcownicy.Add(new Pracownik { IdPracownik = 2, Imie = "Pracownik2", Nazwisko = "2" });
            dictProcownicy.Add(new Pracownik { IdPracownik = 3, Imie = "Pracownik3", Nazwisko = "3" });
            modelBuilder.Entity<Pracownik>().HasData(dictProcownicy);

            var dictKlienci = new List<Klient>();
            dictKlienci.Add(new Klient { IdKlient = 1, Imie = "Klient1", Nazwisko = "1" });
            dictKlienci.Add(new Klient { IdKlient = 2, Imie = "Klient2", Nazwisko = "2" });
            dictKlienci.Add(new Klient { IdKlient = 3, Imie = "Klient3", Nazwisko = "3" });
            modelBuilder.Entity<Klient>().HasData(dictKlienci);


            var dictWyroby = new List<WyrobCukierniczy>();
            dictWyroby.Add(new WyrobCukierniczy { IdWyrobuCukierniczego = 1, Nazwa = "Wyrob1", CenaZaSzt = 1, Typ = "Typ1" });
            dictWyroby.Add(new WyrobCukierniczy { IdWyrobuCukierniczego = 2, Nazwa = "Wyrob2", CenaZaSzt = 2, Typ = "Typ2" });
            dictWyroby.Add(new WyrobCukierniczy { IdWyrobuCukierniczego = 3, Nazwa = "Wyrob3", CenaZaSzt = 3, Typ = "Typ3" });
            modelBuilder.Entity<WyrobCukierniczy>().HasData(dictWyroby);

            var dictZamowienia = new List<Zamowienie>();
            dictZamowienia.Add(new Zamowienie { IdZamowienia = 1, DataPrzyjecia = DateTime.Now, DataRealizacji = DateTime.Now, Uwagi = "brak1", IdPracownik = 1, IdKlient = 1 });
            dictZamowienia.Add(new Zamowienie { IdZamowienia = 2, DataPrzyjecia = DateTime.Now, DataRealizacji = DateTime.Now, Uwagi = "brak2", IdPracownik = 2, IdKlient = 2 });
            dictZamowienia.Add(new Zamowienie { IdZamowienia = 3, DataPrzyjecia = DateTime.Now, DataRealizacji = DateTime.Now, Uwagi = "brak3", IdPracownik = 3, IdKlient = 3 });
            modelBuilder.Entity<Zamowienie>().HasData(dictZamowienia);

            var dictZamowienia_WyrobCukierniczy = new List<Zamowienie_WyrobCukierniczy>();
            dictZamowienia_WyrobCukierniczy.Add(new Zamowienie_WyrobCukierniczy { IdWyrobuCukierniczego = 1, IdZamowienia = 1, Ilosc = 1, Uwagi = "Uwaga1" });
            dictZamowienia_WyrobCukierniczy.Add(new Zamowienie_WyrobCukierniczy { IdWyrobuCukierniczego = 2, IdZamowienia = 2, Ilosc = 2, Uwagi = "Uwag2" });
            dictZamowienia_WyrobCukierniczy.Add(new Zamowienie_WyrobCukierniczy { IdWyrobuCukierniczego = 3, IdZamowienia = 3, Ilosc = 3, Uwagi = "Uwaga3" });
            modelBuilder.Entity<Zamowienie_WyrobCukierniczy>().HasData(dictZamowienia_WyrobCukierniczy);

        }
    }
}
