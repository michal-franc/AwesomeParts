namespace AwesomeParts
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Navigation;
    using AwesomeParts.LoginUI;
    using System;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// <see cref="UserControl"/> class providing the main UI for the application.
    /// </summary>
    public partial class MainPage : UserControl
    {
        /// <summary>
        /// Creates a new <see cref="MainPage"/> instance.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();

            WebContext.Current.Authentication.LoggedIn += new System.EventHandler<System.ServiceModel.DomainServices.Client.ApplicationServices.AuthenticationEventArgs>(Authentication_LoggedIn);
            WebContext.Current.Authentication.LoggedOut += new System.EventHandler<System.ServiceModel.DomainServices.Client.ApplicationServices.AuthenticationEventArgs>(Authentication_LoggedOut);
            

            this.loginContainer.Child = new LoginStatus();

        }

        /// <summary>
        /// After the Frame navigates, ensure the <see cref="HyperlinkButton"/> representing the current page is selected
        /// </summary>
        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            //foreach (UIElement child in LinksStackPanel.Children)
            //{
            //    HyperlinkButton hb = child as HyperlinkButton;
            //    if (hb != null && hb.NavigateUri != null)
            //    {
            //        if (hb.NavigateUri.ToString().Equals(e.Uri.ToString()))
            //        {
            //            VisualStateManager.GoToState(hb, "ActiveLink", true);
            //        }
            //        else
            //        {
            //            VisualStateManager.GoToState(hb, "InactiveLink", true);
            //        }
            //    }
            //}
        }

        /// <summary>
        /// If an error occurs during navigation, show an error window
        /// </summary>
        private void ContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            e.Handled = true;
            ErrorWindow.CreateNew(e.Exception);
        }


        void Authentication_LoggedIn(object sender, System.ServiceModel.DomainServices.Client.ApplicationServices.AuthenticationEventArgs e)
        {
            AwesomeParts.Web.User user = WebContext.Current.User;
            if (user != null )
            {
                if (user.IsInRole("Klient"))
                {
                    this.ContentFrame.Navigate(new Uri("/Klient", UriKind.Relative));
                    DisplayUser(user);
                }
                if (user.IsInRole("Administrator"))
                {
                    this.ContentFrame.Navigate(new Uri("/Administrator", UriKind.Relative));
                }
                if (user.IsInRole("DzialPersonalny"))
                {
                    this.ContentFrame.Navigate(new Uri("/Personalny", UriKind.Relative));
                    DisplayUser(user);
                }
                if (user.IsInRole("DzialSprzedazy"))
                {
                    this.ContentFrame.Navigate(new Uri("/Sprzedaz", UriKind.Relative));
                    DisplayUser(user);
                }
                if (user.IsInRole("DzialZaopatrzenia"))
                {
                    this.ContentFrame.Navigate(new Uri("/Zaopatrzenie", UriKind.Relative));
                    DisplayUser(user);
                }
                if (user.IsInRole("Zarzad"))
                {
                    this.ContentFrame.Navigate(new Uri("/Zarzad", UriKind.Relative));
                }
            }



        }

        void Authentication_LoggedOut(object sender, System.ServiceModel.DomainServices.Client.ApplicationServices.AuthenticationEventArgs e)
        {
            this.ContentFrame.Navigate(new Uri("/Home", UriKind.Relative));
            this.ProfileDisplay.Visibility = System.Windows.Visibility.Collapsed;
        }

        void DisplayUser(AwesomeParts.Web.User user)
        {
            ProfileDisplay.User = user;
            ProfileDisplay.Visibility = System.Windows.Visibility.Visible;
        }
    }
}