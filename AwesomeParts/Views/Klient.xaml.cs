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
using AwesomeParts.Web.POCOs;
using System.ServiceModel.DomainServices.Client;


namespace AwesomeParts.Views
{
    public partial class Klient : Page
    {
        #region Dependency Properties

        public static readonly DependencyProperty AktualneZamowienieIDProperty =
            DependencyProperty.Register("AktualneZamowienieID", typeof(int), typeof(Klient), new PropertyMetadata(0));
        public static readonly DependencyProperty KlientIDProperty =
            DependencyProperty.Register("KlientID", typeof(int), typeof(Klient), new PropertyMetadata(0));

        #endregion Dependency Properties

        #region Wrapper Properties

        public int AktualneZamowienieID
        {
            get { return (int)GetValue(AktualneZamowienieIDProperty); }
            set { SetValue(AktualneZamowienieIDProperty, value); }
        }
        public int KlientID
        {
            get { return (int)GetValue(KlientIDProperty); }
            set { SetValue(KlientIDProperty, value); }
        }

        #endregion Wrapper Properties

        #region Properties

        private DomainDataSource KoszykSource { get; set; }
        private DomainDataSource ZamowienieSource { get; set; }
        private ZamowieniePOCO AktualneZamowienie { get; set; }
        
        #endregion Properties

        #region Constants

        private const string RAZEM_TEXT = "Cena zamówienia: {0:C2}";

        #endregion Constants

        #region Events

        void ZamowienieSource_SubmittedChanges(object sender, SubmittedChangesEventArgs e)
        {
            KoszykGrid.ItemsSource = null;
            PustyKoszykTextBlock.Visibility = System.Windows.Visibility.Visible;
        }

        void KoszykSource_LoadedData(object sender, LoadedDataEventArgs e)
        {
            CenaZamowienia.Text = String.Format(RAZEM_TEXT, SumOrderPrice(sender as DomainDataSource));
        }

        void ZamowienieSource_LoadedData(object sender, LoadedDataEventArgs e)
        {
            if (e.Entities.Count() > 0)
            {
                AktualneZamowienie = e.Entities.Cast<ZamowieniePOCO>().First<ZamowieniePOCO>();
                AktualneZamowienieID = AktualneZamowienie.Id;
                PopulateKoszykGrid(AktualneZamowienieID);
            }
            else
            {
                AktualneZamowienieID = 0;
                PustyKoszykTextBlock.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void KoszykAdder_AddToKoszykCompleted(object sender, EventArgs e)
        {
            PustyKoszykTextBlock.Visibility = System.Windows.Visibility.Collapsed;
            PopulateKoszykGrid(AktualneZamowienieID);
        }

        private void SubmitOrder_Click(object sender, RoutedEventArgs e)
        {
            AktualneZamowienie.DataZlozenia = DateTime.Now;
            ZamowienieSource.SubmitChanges();

            AktualneZamowienieID = 0;
        }

        #endregion Events

        #region Helper Methods

        private DomainDataSource CreateAndInitializeDomainDataSource(string name, DomainContext context, bool autoLoad, string queryName)
        {
            DomainDataSource dds = new DomainDataSource();
            dds.Name = name;
            dds.AutoLoad = autoLoad;
            dds.DomainContext = context;
            dds.QueryName = queryName;
            DataGridTextColumn d = new DataGridTextColumn();
            d.CellStyle = new Style();

            return dds;
        }

        private void PopulateKoszykGrid(int zamowienieID)
        {
            KoszykSource.QueryParameters.Clear();
            KoszykSource.QueryParameters.Add(new Parameter
            {
                ParameterName = "zamowienieID",
                Value = zamowienieID

            });
            KoszykSource.Load();

            KoszykGrid.ItemsSource = KoszykSource.Data;
        }

        private decimal SumOrderPrice(DomainDataSource koszyk)
        {
            decimal razem = 0;

            foreach (ZamowieniaKoszykPOCO produkt in koszyk.Data)
                razem += produkt.CenaCalosciowa;

            return razem;
        }

        #endregion Helper Methods

        public Klient()
        {
            InitializeComponent();

            KlientID = 1;

            var context = this.Resources["KlientContext"] as AwesomePartsContext;
            ZamowienieSource = CreateAndInitializeDomainDataSource("dds", context, false, "GetAktualneZamowienieByKlientId");
            ZamowienieSource.LoadedData += new EventHandler<LoadedDataEventArgs>(ZamowienieSource_LoadedData);
            ZamowienieSource.SubmittedChanges += new EventHandler<SubmittedChangesEventArgs>(ZamowienieSource_SubmittedChanges);
            KoszykSource = CreateAndInitializeDomainDataSource("KoszykSource", context, false, "GetKoszykByZamowienieId");
            KoszykSource.LoadedData += new EventHandler<LoadedDataEventArgs>(KoszykSource_LoadedData);
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //if (!WebContext.Current.User.IsInRole("Klient"))
            //{
            //    this.NavigationService.Navigate(new Uri("/NoAcces", UriKind.Relative));
            //}

            ZamowienieSource.QueryParameters.Clear();
            ZamowienieSource.QueryParameters.Add(new Parameter
            {
                ParameterName = "klientID",
                Value = KlientID
            });
            ZamowienieSource.Load();
        }
    }
}
