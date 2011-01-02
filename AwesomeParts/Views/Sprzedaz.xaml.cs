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
using System.Windows.Data;


namespace AwesomeParts.Views
{
    public partial class Sprzedaz : Page
    {
        private readonly int UserID;   // Connects with ASP.NET Membership
        private int ZamowienieID { get; set; }
        private DomainDataSource KoszykNieprzydzieloneSource { get; set; }
        private DomainDataSource KoszykPrzydzieloneSource { get; set; }

        //public static readonly DependencyProperty RazemProperty = DependencyProperty.Register("Razem", typeof(decimal), typeof(Sprzedaz), new PropertyMetadata(0));
        //public decimal Razem
        //{
        //    get { return (decimal)GetValue(RazemProperty); }
        //    set { SetValue(RazemProperty, value); }
        //}

        private const string RAZEM_TEXT = "Cena zamówienia: {0:C2}";


        public Sprzedaz()
        {
            InitializeComponent();

            UserID = 1; // Hardcoded for testing...
            
            var context = (AwesomePartsContext)this.Resources["SprzedazContext"];
            KoszykNieprzydzieloneSource = CreateAndInitializeDomainDataSource("KoszykNieprzydzieloneSource", context, false, "GetKoszykByZamowienieId");
            KoszykPrzydzieloneSource = CreateAndInitializeDomainDataSource("KoszykPrzydzieloneSource", context, false, "GetKoszykByZamowienieId");

            KoszykNieprzydzieloneSource.LoadedData += new EventHandler<LoadedDataEventArgs>(KoszykNieprzydzieloneSource_LoadedData);
            KoszykPrzydzieloneSource.LoadedData += new EventHandler<LoadedDataEventArgs>(KoszykPrzydzieloneSource_LoadedData);
            ZamowieniaNieprzydzieloneGrid.SelectionChanged += new SelectionChangedEventHandler(ZamowieniaNieprzydzieloneGrid_SelectionChanged);
            //ZamowieniaNieprzydzieloneGrid.LoadingRow +=new EventHandler<DataGridRowEventArgs>(ZamowieniaNieprzydzieloneGrid_LoadingRow);
            ZamowieniaTabs.SelectionChanged += new SelectionChangedEventHandler(ZamowieniaTabs_SelectionChanged);
            ZamowieniaPrzydzieloneGrid.SelectionChanged += new SelectionChangedEventHandler(ZamowieniaPrzydzieloneGrid_SelectionChanged);
        }

        #region Events

        void ZamowieniaNieprzydzieloneGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int zamowienieID = ((sender as DataGrid).SelectedItem as ZamowieniePOCO).Id;

            LoadKoszykDomainDataSource(KoszykNieprzydzieloneSource, KoszykNieprzydzieloneGrid, zamowienieID);
        }

        void ZamowieniaPrzydzieloneGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int zamowienieID = ((sender as DataGrid).SelectedItem as ZamowieniePOCO).Id;

            LoadKoszykDomainDataSource(KoszykPrzydzieloneSource, KoszykPrzydzieloneGrid, zamowienieID);
        }

        void ZamowieniaTabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var tabs = sender as TabControl;

            //if (tabs.SelectedIndex == 0)
            //{
            //    ZamowieniaNieprzydzieloneSource.Load();
            //}
            //else
            if (tabs.SelectedIndex == 1)
            {
                ZamowieniaPrzydzieloneSource.Load();
            }
            
        }

        void KoszykNieprzydzieloneSource_LoadedData(object sender, LoadedDataEventArgs e)
        {
            CenaZamowieniaNiezrealizowane.Text = String.Format(RAZEM_TEXT, SumOrderPrice(sender as DomainDataSource));
        }

        void KoszykPrzydzieloneSource_LoadedData(object sender, LoadedDataEventArgs e)
        {
            CenaZamowieniaZrealizowane.Text = String.Format(RAZEM_TEXT, SumOrderPrice(sender as DomainDataSource));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var context = this.Resources["SprzedazContext"] as AwesomePartsContext;
            (button.DataContext as ZamowieniePOCO).PracownikID = UserID;

            button.Visibility = System.Windows.Visibility.Collapsed;

            context.SubmitChanges();
        }

        private void SubmitChanges_Click(object sender, RoutedEventArgs e)
        {
            var context = this.Resources["SprzedazContext"] as AwesomePartsContext;
            context.SubmitChanges();
        }

        private void RevertChanges_Click(object sender, RoutedEventArgs e)
        {
            var context = this.Resources["SprzedazContext"] as AwesomePartsContext;
            context.RejectChanges();

            NavigationService.Refresh();
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

        private void LoadKoszykDomainDataSource(DomainDataSource koszykSource, DataGrid koszykGrid, int zamowienieID)
        {
            if (koszykSource.CanLoad)
            {
                koszykSource.QueryParameters.Clear();
                koszykSource.QueryParameters.Add(new Parameter
                {
                    ParameterName = "zamowienieID",
                    Value = zamowienieID
                });
                koszykSource.Load();

                koszykGrid.ItemsSource = koszykSource.Data;
            }
            else
                koszykGrid.ItemsSource = null;
        }

        private decimal SumOrderPrice(DomainDataSource koszyk)
        {
            decimal razem = 0;

            foreach (ZamowieniaKoszykPOCO produkt in koszyk.Data)
                razem += produkt.CenaCalosciowa;

            return razem;
        }

        #endregion Helper Methods

        #region Rubbish

        //
        // Autoincrementing L.p.
        //
        void ZamowieniaNieprzydzieloneGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            FrameworkElement fe = ZamowieniaNieprzydzieloneGrid.Columns[1].GetCellContent(e.Row);
            FrameworkElement result = GetParent(fe, typeof(TextBlock));

            if (result != null)
            {
                TextBlock tb = result as TextBlock;

                tb.Text = (e.Row.GetIndex() + 1).ToString();
            }
        }

        private FrameworkElement GetParent(FrameworkElement child, Type targetType)
        {
            object parent = child.Parent;
            if (parent != null)
            {
                if (parent.GetType() == targetType)
                {
                    return (FrameworkElement)parent;
                }
                else
                {
                    return GetParent((FrameworkElement)parent, targetType);
                }
            }
            return null;
        }

        #endregion Rubbish
        
        

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //if (!WebContext.Current.User.IsInRole("DzialSprzedazy"))
            //{
            //    this.NavigationService.Navigate(new Uri("/NoAcces", UriKind.Relative));
            //}

            ZamowieniaPrzydzieloneSource.QueryParameters.Clear();
            ZamowieniaPrzydzieloneSource.QueryParameters.Add(new Parameter
            {
                ParameterName = "pracownikID",
                Value = UserID
            });
            ZamowieniaPrzydzieloneSource.Load();

            ZamowieniaPrzydzieloneGrid.ItemsSource = ZamowieniaPrzydzieloneSource.Data;
        }
    }
}
