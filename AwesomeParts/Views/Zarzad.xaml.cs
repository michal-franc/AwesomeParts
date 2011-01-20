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
        private DomainDataSource produktyContextByYear;
        private DomainDataSource produktyContextByMonth;

        private string _selectedYear = String.Empty;
        private bool _firstYear = true;
        private bool _firstMonth = true;

        public Zarzad()
        {
            InitializeComponent();

            zamowieniaContext = new DomainDataSource();
            zamowieniaContext.Name = "zamowienia";
            zamowieniaContext.AutoLoad = false;
            zamowieniaContext.DomainContext = (AwesomePartsContext)this.Resources["ZamowieniaContext"];
            zamowieniaContext.QueryName = "GetZamowienia";

            produktyContextByYear = new DomainDataSource();
            produktyContextByYear.Name = "produktyByYear";
            produktyContextByYear.AutoLoad = false;
            produktyContextByYear.DomainContext = (AwesomePartsContext)this.Resources["ZamowieniaContext"];
            produktyContextByYear.QueryName = "GetProductsSoldByYear";
            produktyContextByYear.LoadedData += new EventHandler<LoadedDataEventArgs>(produktyContextByYear_LoadedData);

            produktyContextByMonth = new DomainDataSource();
            produktyContextByMonth.Name = "produktyByMonth";
            produktyContextByMonth.AutoLoad = false;
            produktyContextByMonth.DomainContext = (AwesomePartsContext)this.Resources["ZamowieniaContext"];
            produktyContextByMonth.QueryName = "GetProductsSoldByMonthAndYear";
            produktyContextByMonth.LoadedData += new EventHandler<LoadedDataEventArgs>(produktyContextByMonth_LoadedData);
        }

        void produktyContextByMonth_LoadedData(object sender, LoadedDataEventArgs e)
        {
            chartProductZamowienia.ItemsSource = produktyContextByMonth.Data;
        }

        void produktyContextByYear_LoadedData(object sender, LoadedDataEventArgs e)
        {
            this.chartProductZamowienia.ItemsSource = produktyContextByYear.Data;
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //if (!WebContext.Current.User.IsInRole("Zarzad"))
            //{
            //    this.NavigationService.Navigate(new Uri("/NoAcces", UriKind.Relative));
            //}
            if (zamowieniaContext.CanLoad)
            {
                zamowieniaContext.Load();
                chartYearlyZamowienia.ItemsSource = zamowieniaContext.Data;
                chartMonthlyZamowienia.ItemsSource = zamowieniaContext.Data;
            }
        }

        private void chartareaYearlyZamowienia_ItemClick(object sender, ChartItemClickEventArgs e)
        {
            ChartFilterDescriptor descriptor = new ChartFilterDescriptor();
            descriptor.Member = "RokZrealizowania";
            descriptor.Operator = Telerik.Windows.Data.FilterOperator.IsEqualTo;
            descriptor.Value = e.DataPoint.XCategory;
            this.chartMonthlyZamowienia.FilterDescriptors.Clear();
            this.chartMonthlyZamowienia.FilterDescriptors.Add(descriptor);

            if (!_firstYear)
            {
                produktyContextByYear.Clear();
            }

            produktyContextByYear.QueryParameters.Clear();
            produktyContextByYear.QueryParameters.Add(new Parameter() { ParameterName = "year", Value = e.DataPoint.XCategory });

            produktyContextByYear.Load();
            _selectedYear = e.DataPoint.XCategory;
            _firstYear = false;
        }


        private void chartMonthlyZamowienia_ItemClick(object sender, ChartItemClickEventArgs e)
        {
            if (!_firstMonth)
            {
                produktyContextByMonth.Clear();
            }
            
            produktyContextByMonth.QueryParameters.Clear();
            produktyContextByMonth.QueryParameters.Add(new Parameter() { ParameterName = "month", Value = e.DataPoint.XCategory });
            produktyContextByMonth.QueryParameters.Add(new Parameter() { ParameterName = "year", Value = _selectedYear });



            produktyContextByMonth.Load();

            _firstMonth = false;

        }
    }
}
