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
using System.ServiceModel.DomainServices.Client;
using AwesomeParts.Web.POCOs;

namespace AwesomeParts.Views
{
    public partial class Klienci : Page
    {
        public Klienci()
        {
            InitializeComponent();
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            KlientContext context = new KlientContext();

            EntityQuery<KlientPOCO> query = context.GetKlienciQuery();
            context.Load<KlientPOCO>(query);
            KlienciGrid.ItemsSource = context.KlientPOCOs;
        }

    }
}
