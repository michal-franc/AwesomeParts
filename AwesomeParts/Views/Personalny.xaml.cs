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
            //GrupujCB.SelectionChanged += GrupujCB_SelectionChanged;
            PracownicyGrid.SelectionChanged += new SelectionChangedEventHandler(PracownicyGrid_SelectionChanged);
        }



        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void GrupujCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            //if (GrupujCB.SelectedIndex == 0)
            //{
            //    PracownicyGrid.ItemsSource = DataSourceByRodzaje.Data;
            //}
            //else if (GrupujCB.SelectedIndex == 1)
            //{
            //    PracownicyGrid.ItemsSource = DataSourceByStatus.Data;
            //}
        }

        void PracownicyGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AwesomePartsContext context = (AwesomePartsContext)this.Resources["PracowContext"];

        }
    }
}
