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

            this.loginContainer.Child = new LoginStatus();
            WebContext.Current.Authentication.LoggedIn += new System.EventHandler<System.ServiceModel.DomainServices.Client.ApplicationServices.AuthenticationEventArgs>(Authentication_LoggedIn);
            WebContext.Current.Authentication.LoggedOut +=new System.EventHandler<System.ServiceModel.DomainServices.Client.ApplicationServices.AuthenticationEventArgs>(Authentication_LoggedOut);

            //LoadLogo();
            //HideHyperLinks();
            
        }

        void Authentication_LoggedIn(object sender, System.ServiceModel.DomainServices.Client.ApplicationServices.AuthenticationEventArgs e)
        {
        }

        void Authentication_LoggedOut(object sender, System.ServiceModel.DomainServices.Client.ApplicationServices.AuthenticationEventArgs e)
        {
            HideHyperLinks();
        }

        private void HideHyperLinks()
        {
            foreach (HyperlinkButton hb in LinksStackPanel.Children)
            {
                hb.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        //private void LoadLogo()
        //{
        //    Uri uri = new Uri("Images/Logo.jpg", UriKind.Relative);
        //    ImageSource imgSource = new BitmapImage(uri);
        //    ImageLogo.Source = imgSource;
        //}

        /// <summary>
        /// After the Frame navigates, ensure the <see cref="HyperlinkButton"/> representing the current page is selected
        /// </summary>
        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            foreach (UIElement child in LinksStackPanel.Children)
            {
                HyperlinkButton hb = child as HyperlinkButton;
                if (hb != null && hb.NavigateUri != null)
                {
                    if (hb.NavigateUri.ToString().Equals(e.Uri.ToString()))
                    {
                        VisualStateManager.GoToState(hb, "ActiveLink", true);
                    }
                    else
                    {
                        VisualStateManager.GoToState(hb, "InactiveLink", true);
                    }
                }
            }
        }

        /// <summary>
        /// If an error occurs during navigation, show an error window
        /// </summary>
        private void ContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            e.Handled = true;
            ErrorWindow.CreateNew(e.Exception);
        }
    }
}