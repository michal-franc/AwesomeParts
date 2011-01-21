using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ServiceModel.DomainServices.Server;

namespace AwesomeParts.Web.POCOs.MiniPOCOs
{
    public class KoszykPOCO
    {
        [Key]
        public int Id { get; set; }

        public string Nazwa { get; set; }
        public string Producent { get; set; }
        public int Ilosc { get; set; }
        public decimal CenaJednostkowa { get; set; }
        public decimal CenaCalosciowa { get; set; }
        public int ProduktID { get; set; }
        public int ZamowienieID { get; set; }


        [Include]
        [Association("ZamowienieKoszykZamowienie", "ZamowienieID", "Id", IsForeignKey = true)]
        public ZamowieniePOCO Zamowienie { get; set; }


        public string KlientNazwa
        {
            get
            {
                return Zamowienie.KlientNazwa;
            }
        }

        public int RokZrealizowania
        {
            get
            {
                if (Zamowienie.DataZrealizowania.HasValue)
                    return Zamowienie.DataZrealizowania.Value.Year;
                else
                    return 0;
            }
        }

        public int MiesiacZrealizowania
        {
            get
            {
                if (Zamowienie.DataZrealizowania.HasValue)
                    return Zamowienie.DataZrealizowania.Value.Month;
                else
                    return 0;
            }
        }

        public string MiesiacZrealizowaniaString
        {
            get
            {
                return ConvertIntToMonth(MiesiacZrealizowania);
            }
        }
        public string ConvertIntToMonth(int number)
        {
            switch(number)
            {
                case 1: return "Styczeń";
                case 2: return "Luty";
                case 3: return "Marzec";
                case 4: return "Kwiecień";
                case 5: return "Maj";
                case 6: return "Czerwiec";
                case 7: return "Lipiec";
                case 8: return "Sierpień";
                case 9: return "Wrzesień";
                case 10: return "Październik";
                case 11: return "Listopad";
                case 12: return "Grudzień";
                default: return "Error";
            }
        }
    }


}