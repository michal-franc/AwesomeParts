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

namespace AwesomeParts.Views
{
    public partial class Zaopatrzenie : Page
    {
        public Zaopatrzenie()
        {
            InitializeComponent();
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!WebContext.Current.User.IsInRole("DzialZaopatrzenia"))
            {
                this.NavigationService.Navigate(new Uri("/NoAcces", UriKind.Relative));
            }
        }

    }
}
