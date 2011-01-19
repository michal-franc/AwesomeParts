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


        public Zarzad()
        {
            InitializeComponent();

            zamowieniaContext = new DomainDataSource();
            zamowieniaContext.Name = "zamowienia";
            zamowieniaContext.AutoLoad = false;
            zamowieniaContext.DomainContext = (AwesomePartsContext)this.Resources["ZamowieniaContext"];
            zamowieniaContext.QueryName = "GetZamowienia";

            produktyContext = new DomainDataSource();
            produktyContext.Name = "produkty";
            produktyContext.AutoLoad = false;
            produktyContext.DomainContext = (AwesomePartsContext)this.Resources["ZamowieniaContext"];
            produktyContext.QueryName = "GetProdukty";
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //if (!WebContext.Current.User.IsInRole("Zarzad"))
            //{
            //    this.NavigationService.Navigate(new Uri("/NoAcces", UriKind.Relative));
            //}

            zamowieniaContext.Load();
            produktyContext.Load();
            chartYearlyZamowienia.ItemsSource = zamowieniaContext.Data;
            chartMonthlyZamowienia.ItemsSource = zamowieniaContext.Data;
            chartProductZamowienia.ItemsSource = produktyContext.Data;

        }

        private void chartareaYearlyZamowienia_ItemClick(object sender, ChartItemClickEventArgs e)
        {
            ChartFilterDescriptor descriptor = new ChartFilterDescriptor();
            descriptor.Member = "RokZrealizowania";
            descriptor.Operator = Telerik.Windows.Data.FilterOperator.IsEqualTo;
            descriptor.Value = e.DataPoint.XCategory;
            this.chartMonthlyZamowienia.FilterDescriptors.Clear();
            this.chartMonthlyZamowienia.FilterDescriptors.Add(descriptor);
        }

        private void chartMonthlyZamowienia_ItemClick(object sender, ChartItemClickEventArgs e)
        {
        }
    }
}
