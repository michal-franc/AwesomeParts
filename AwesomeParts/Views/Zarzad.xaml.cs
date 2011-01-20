using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;
using AwesomeParts.Web.Services;
using Telerik.Windows.Controls.Charting;

namespace AwesomeParts.Views
{
    public partial class Zarzad : Page
    {
        private DomainDataSource zamowieniaContext;
        private DomainDataSource produktyContext;


        private string _selectedYear = String.Empty;

        private bool _first = true;
        private bool _first1 = true;
        private bool _first2 = true;

        public Zarzad()
        {
            InitializeComponent();

            zamowieniaContext = new DomainDataSource();
            zamowieniaContext.Name = "GetIloscProduktow";
            zamowieniaContext.AutoLoad = false;
            zamowieniaContext.DomainContext = (AwesomePartsContext)this.Resources["ZamowieniaContext"];
            zamowieniaContext.QueryName = "GetKoszyki";
            zamowieniaContext.LoadedData += new EventHandler<LoadedDataEventArgs>(zamowieniaContext_LoadedData);

            produktyContext = new DomainDataSource();
            produktyContext.Name = "GetProducts";
            produktyContext.AutoLoad = false;
            produktyContext.DomainContext = (AwesomePartsContext)this.Resources["ZamowieniaContext"];
            produktyContext.LoadedData += new EventHandler<LoadedDataEventArgs>(produktyContext_LoadedData);

        }

        void zamowieniaContext_LoadedData(object sender, LoadedDataEventArgs e)
        {
            this.busyIndicator1.IsBusy = false;
            this.busyIndicator2.IsBusy = false;
        }

        void produktyContext_LoadedData(object sender, LoadedDataEventArgs e)
        {
            chartProductZamowienia.ItemsSource = produktyContext.Data;
            this.busyIndicator3.IsBusy = false;
        }


        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //if (!WebContext.Current.User.IsInRole("Zarzad"))
            //{
            //    this.NavigationService.Navigate(new Uri("/NoAcces", UriKind.Relative));
            //}
            zamowieniaContext.Load();
            this.busyIndicator1.IsBusy = true;
            this.busyIndicator2.IsBusy = true;
            chartYearlyZamowienia.ItemsSource = zamowieniaContext.Data;
            chartMonthlyZamowienia.ItemsSource = zamowieniaContext.Data;
        }

        private void chartareaYearlyZamowienia_ItemClick(object sender, ChartItemClickEventArgs e)
        {

            if (!_first)
            {
                produktyContext.Clear();
            }


            ChartFilterDescriptor descriptor = new ChartFilterDescriptor();
            descriptor.Member = "RokZrealizowania";
            descriptor.Operator = Telerik.Windows.Data.FilterOperator.IsEqualTo;
            descriptor.Value = e.DataPoint.XCategory;

            ChartFilterDescriptor descriptor1 = new ChartFilterDescriptor();
            descriptor1.Member = "Ilosc";
            descriptor1.Operator = Telerik.Windows.Data.FilterOperator.IsGreaterThan;
            descriptor1.Value = "0";

            this.chartMonthlyZamowienia.FilterDescriptors.Clear();
            this.chartMonthlyZamowienia.FilterDescriptors.Add(descriptor);
            this.chartMonthlyZamowienia.FilterDescriptors.Add(descriptor1);


            this.busyIndicator3.IsBusy = true;
            produktyContext.QueryParameters.Clear();
            produktyContext.QueryName = "";
            produktyContext.QueryParameters.Add(new Parameter() { ParameterName = "year", Value = e.DataPoint.XCategory });
            produktyContext.QueryName = "GetProductsSoldByYear";

            produktyContext.Load();
            _selectedYear = e.DataPoint.XCategory;

            chartTitle2.Content = String.Format("Sprzedaż na Rok :{0}", _selectedYear);

            _first = false;
        }


        private void chartMonthlyZamowienia_ItemClick(object sender, ChartItemClickEventArgs e)
        {
            this.busyIndicator3.IsBusy = true;
            if (!_first)
            {
                produktyContext.Clear();
            }


            string month = ConvertMonthToInt(e.DataPoint.XCategory);

            produktyContext.QueryParameters.Clear();
            produktyContext.QueryName = "";
            produktyContext.QueryParameters.Add(new Parameter() { ParameterName = "month", Value = month });
            produktyContext.QueryParameters.Add(new Parameter() { ParameterName = "year", Value = _selectedYear });
            produktyContext.QueryName = "GetProductsSoldByMonthAndYear";
            produktyContext.Load();

            _first = false;
        }

        private string ConvertMonthToInt(string month)
        {
            switch (month)
            {
                case "Styczeń":return "1";
                case "Luty":return "2";
                case "Marzec":return "3";
                case "Kwiecień":return "4";
                case "Maj":return "5";
                case "Czerwiec":return "6";
                case "Lipiec":return "7";
                case "Sierpień":return "8";
                case "Wrzesień":return "9";
                case "Październik":return "10";
                case  "Listopad":return "11";
                case "Grudzień": return "12";
                default: return "0";
            }
        }
    }
}
