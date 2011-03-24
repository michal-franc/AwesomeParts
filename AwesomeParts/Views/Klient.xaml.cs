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
using AwesomeParts.Web.Models;


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

        private void GrupujCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProduktySource.CanLoad == true)
            {
                if (GrupujCB.SelectedIndex == 0)
                {
                    ProduktySource.GroupDescriptors.Clear();
                    ProduktySource.Load();
                    ProduktyGrid.ItemsSource = ProduktySource.Data;
                    ProduktyGrid.SelectedIndex = 0;
                }
                else if (GrupujCB.SelectedIndex == 1)
                {
                    ProduktySource.GroupDescriptors.Clear();
                    ProduktySource.GroupDescriptors.Add(new GroupDescriptor
                    {
                        PropertyPath = "Producent.Nazwa"
                    });
                    ProduktySource.Load();

                    ProduktyGrid.ItemsSource = ProduktySource.Data;
                    ProduktyGrid.SelectedIndex = 0;
                }
            }
        }

        void ZamowienieSource_SubmittedChanges(object sender, SubmittedChangesEventArgs e)
        {
            KoszykGrid.ItemsSource = null;
            AktualneZamowienie = null;
            AktualneZamowienieID = 0;
            KoszykSource.Clear();
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
                AktualneZamowienie = e.Entities.Cast<ZamowieniePOCO>().First<ZamowieniePOCO>(); // Nie działa przy pustym koszyku !!!!
                AktualneZamowienieID = AktualneZamowienie.Id;
                PustyKoszykTextBlock.Visibility = System.Windows.Visibility.Collapsed;
                PopulateKoszykGrid(AktualneZamowienieID);
            }
            else
            {
                AktualneZamowienieID = 0;
                PustyKoszykTextBlock.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void ZamowienieSource_LoadedDataWhileSubmittingOrder(object sender, LoadedDataEventArgs e)
        {
            ZamowienieSource.LoadedData += ZamowienieSource_LoadedData;
            ZamowienieSource.LoadedData -= ZamowienieSource_LoadedDataWhileSubmittingOrder;

            AktualneZamowienie = e.Entities.Cast<ZamowieniePOCO>().First();
            AktualneZamowienie.DataZlozenia = DateTime.Now;
            ZamowienieSource.SubmitChanges();
        }

        private void KoszykAdder_AddToKoszykCompleted(object sender, EventArgs e)
        {
            PustyKoszykTextBlock.Visibility = System.Windows.Visibility.Collapsed;
            PopulateKoszykGrid(AktualneZamowienieID);
        }

        private void SubmitOrder_Click(object sender, RoutedEventArgs e)
        {
            if (AktualneZamowienie == null)
            {
                ZamowienieSource.LoadedData -= ZamowienieSource_LoadedData;
                ZamowienieSource.LoadedData += ZamowienieSource_LoadedDataWhileSubmittingOrder;
                ZamowienieSource.Clear();
                ZamowienieSource.QueryParameters.Clear();
                ZamowienieSource.QueryParameters.Add(new Parameter
                {
                    ParameterName = "klientID",
                    Value = KlientID
                });
                ZamowienieSource.Load();
            }
            else
            {
                AktualneZamowienie.DataZlozenia = DateTime.Now;
                ZamowienieSource.SubmitChanges();
            }

            //AktualneZamowienieID = 0;
            //AktualneZamowienie = null;
        }

        private void LoadUserIdCompleted(LoadOperation<ProfileData> lo)
        {
            KlientID = lo.Entities.First().id;

            ZamowienieSource.QueryParameters.Clear();
            ZamowienieSource.QueryParameters.Add(new Parameter
            {
                ParameterName = "klientID",
                Value = KlientID
            });
            ZamowienieSource.Load();
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

            var context = this.Resources["KlientContext"] as AwesomePartsContext;
            var profileContext = new ProfileContext();

            string userName = WebContext.Current.User.GetIdentity() as string;
            EntityQuery<ProfileData> query = profileContext.GetUserIdByUserNameQuery(userName);
            profileContext.Load<ProfileData>(query, LoadUserIdCompleted, null);

            
            ZamowienieSource = CreateAndInitializeDomainDataSource("dds", context, false, "GetAktualneZamowienieByKlientId");
            ZamowienieSource.LoadedData += new EventHandler<LoadedDataEventArgs>(ZamowienieSource_LoadedData);
            ZamowienieSource.SubmittedChanges += new EventHandler<SubmittedChangesEventArgs>(ZamowienieSource_SubmittedChanges);
            KoszykSource = CreateAndInitializeDomainDataSource("KoszykSource", context, false, "GetKoszykByZamowienieId");
            KoszykSource.LoadedData += new EventHandler<LoadedDataEventArgs>(KoszykSource_LoadedData);
            GrupujCB.SelectionChanged +=new SelectionChangedEventHandler(GrupujCB_SelectionChanged);
            
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //if (!WebContext.Current.User.IsInRole("Klient"))
            //{
            //    this.NavigationService.Navigate(new Uri("/NoAcces", UriKind.Relative));
            //}
        }
    }
}
