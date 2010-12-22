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


namespace AwesomeParts.Views
{
    public partial class Personalny : Page
    {
        public Personalny()
        {
            InitializeComponent();
            GrupujCB.SelectionChanged += GrupujCB_SelectionChanged;
            PracownicyGrid.MouseWheel += new MouseWheelEventHandler(PracownicyGrid_MouseWheel);
            //PracownicyGrid.SelectionChanged += new SelectionChangedEventHandler(PracownicyGrid_SelectionChanged);
        }

        void PracownicyGrid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!e.Handled)
            {
                int rowsToMove = 0;
                if (e.Delta < 0)
                {
                    rowsToMove = e.Delta / 120 * -1;
                }
                else
                {
                    rowsToMove = e.Delta / 120 * -1;
                }

                if (PracownicyGrid.SelectedIndex == 0
                        || PracownicyGrid.SelectedIndex == (PracownicyGrid.ItemsSource.Cast<PracownikPOCO>().ToList().Count - 1))
                { return; }

                PracownicyGrid.SelectedIndex = PracownicyGrid.SelectedIndex + rowsToMove;
                PracownicyGrid.ScrollIntoView(PracownicyGrid.SelectedItem, PracownicyGrid.Columns[0]);
            }
        }



        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!WebContext.Current.User.IsInRole("DzialPersonalny"))
            {
                this.NavigationService.Navigate(new Uri("/NoAcces", UriKind.Relative));
            }
        }

        private void GrupujCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (GrupujCB.SelectedIndex == 0)
            {
                PracownicyGrid.ItemsSource = DataSource.Data;
            }
            else if (GrupujCB.SelectedIndex == 1)
            {
                PracownicyGrid.ItemsSource = DataSourceByRodzaje.Data;
            }
            else if (GrupujCB.SelectedIndex == 2)
            {
                PracownicyGrid.ItemsSource = DataSourceByStatus.Data;
            }
        }

        void PracownicyGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AwesomePartsContext context = (AwesomePartsContext)this.Resources["PracowContext"];

        }
    }
}
