using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BazaDanych.Entities;
using AwesomeParts.Web.POCOs;
using AwesomeParts.Web.POCOs.MiniPOCOs;

namespace AwesomeParts.Web
{
    public static class POCOHelpers
    {
        #region Produkt

        public static ProduktPOCO MapProduktToPOCO(Produkty produkt)
        {
            return new ProduktPOCO
            {
                Id = produkt.Id,
                Nazwa = produkt.Nazwa,
                Cena = produkt.Cena,
                ProducentID = produkt.Producent.Id,
                Producent = new ProduktProducentPOCO
                {
                    Id = produkt.Producent.Id,
                    Nazwa = produkt.Producent.Nazwa
                },
                Ilosc = produkt.Ilosc,
                DocelowaIlosc = produkt.DocelowaIlosc
            };
        }

        #endregion Produkt

        #region Klient

        public static KlientPOCO MapKlientToPOCO(Klient klient)
        {
            return new KlientPOCO
            {
                Id = klient.Id,
                Imie = klient.Imie,
                Nazwisko = klient.Nazwisko,
                Telefon = klient.Telefon,
                Firma = klient.Firma,
                NIP = klient.NIP,
                Ulica = klient.Ulica,
                Numer = klient.Numer,
                Miasto = klient.Miasto,
                KodPocztowy = klient.KodPocztowy,
                Kraj = klient.Kraj,
                Rodzaj = new KlientRodzajPOCO
                {
                    Id = klient.Rodzaj.Id,
                    Rodzaj = klient.Rodzaj.Rodzaj
                }
            };
        }

        //public static Klient MapPOCOToKlient(KlientPOCO poco)
        //{
        //    return new Klient
        //    {
        //        Imie = poco.Imie,
        //        Nazwisko = poco.Nazwisko,
        //        Login = poco.Login,
        //        Haslo = poco.Haslo,
        //        Telefon = poco.Telefon,
        //        Firma = poco.Firma,
        //        NIP = poco.NIP,
        //        Ulica = poco.Ulica,
        //        Numer = poco.Numer,
        //        Miasto = poco.Miasto,
        //        KodPocztowy = poco.KodPocztowy,
        //        Kraj = poco.Kraj,
        //        Rodzaj = new KlientRodzajPOCO
        //        {
        //            Id = poco.Rodzaj.Id,
        //            Rodzaj = poco.Rodzaj.Rodzaj
        //        },
        //        LoginRola = new LoginRolaPOCO
        //        {
        //            ID = poco.LoginRola.ID,
        //            Rola = poco.LoginRola.Rola
        //        }
        //    };
        //}

        #endregion Klient

        #region Zamowienia

        public static ZamowieniePOCO MapZamowienieToPOCO(Zamowienie zamowienie)
        {
            return new ZamowieniePOCO
            {
                Id = zamowienie.Id,
                DataZlozenia = zamowienie.DataZlozenia,
                DataZrealizowania = zamowienie.DataZrealizowania,
                Klient = MapKlientToPOCO(zamowienie.Klient)
            };
        }

        #endregion Zamowienia

        #region Pracownik

        public static PracownikPOCO MapPracownikToPOCO(Pracownik pracownik)
        {
            return new PracownikPOCO
            { 
                Id = pracownik.Id,
                Imie = pracownik.Imie,
                Nazwisko = pracownik.Nazwisko,
                Pesel = pracownik.Pesel,
                RodzajID = pracownik.Rodzaj.Id,
                StatusID = pracownik.Status.Id,
                UwagiDoStatusu = pracownik.UwagiDoStatusu,
                Rodzaj = new PracownikRodzajPOCO
                {
                    Id = pracownik.Rodzaj.Id,
                    Rodzaj = pracownik.Rodzaj.Rodzaj
                },
                Status = new PracownikStatusPOCO
                {
                    Id = pracownik.Status.Id,
                    Status = pracownik.Status.Status
                }
            };
        }

        public static PracownikUmowaPOCO MapPracownikUmowaToPOCO(PracownikUmowa pracownik)
        {
            return new PracownikUmowaPOCO
            {
                Id = pracownik.Id,
                DataPodpisania = pracownik.DataPodpisania,
                DataWygasniecia = pracownik.DataWygasniecia,
                Placa = pracownik.Placa,
                Aktualna = pracownik.Aktualna,
                Uwagi = pracownik.Uwagi,
                PracownikID = pracownik.Pracownik.Id
            };
        }

        private static IList<ZamowienieMiniPOCO> CopyZamowieniaToMiniPOCOCollection(IList<Zamowienie> z)
        {
            IList<ZamowienieMiniPOCO> zPOCO = new List<ZamowienieMiniPOCO>();
            foreach (var zamowienie in z)
            {
                zPOCO.Add(new ZamowienieMiniPOCO
                {
                    Id = zamowienie.Id,
                    DataZlozenia = zamowienie.DataZlozenia,
                    Zrealizowano = zamowienie.Zrealizowano
                });
            }

            return zPOCO;
        }

        private static IList<PracownikUmowaMiniPOCO> CopyUmowyToMiniPOCOCollection(IList<PracownikUmowa> u)
        {
            IList<PracownikUmowaMiniPOCO> uPOCO = new List<PracownikUmowaMiniPOCO>();
            foreach (var umowa in u)
            {
                uPOCO.Add(new PracownikUmowaMiniPOCO
                {
                    Id = umowa.Id,
                    DataPodpisania = umowa.DataPodpisania,
                    Aktualna = umowa.Aktualna
                });
            }

            return uPOCO;
        }

        #endregion Pracownik

    }
}