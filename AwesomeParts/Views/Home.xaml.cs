namespace AwesomeParts
{
    using System.Windows.Controls;
    using System.Windows.Navigation;
    using AwesomeParts.Web.Services;
    using AwesomeParts.Web.POCOs;
    using System.ServiceModel.DomainServices.Client;
    using System;

    /// <summary>
    /// Home page for the application.
    /// </summary>
    public partial class Home : Page
    {
        /// <summary>
        /// Creates a new <see cref="Home"/> instance.
        /// </summary>
        public Home()
        {
            InitializeComponent();

            WebContext.Current.Authentication.LoggedIn += new System.EventHandler<System.ServiceModel.DomainServices.Client.ApplicationServices.AuthenticationEventArgs>(Authentication_LoggedIn);
            WebContext.Current.Authentication.LoggedOut += new System.EventHandler<System.ServiceModel.DomainServices.Client.ApplicationServices.AuthenticationEventArgs>(Authentication_LoggedOut);


            this.Title = ApplicationStrings.HomePageTitle;
        }

        /// <summary>
        /// Executes when the user navigates to this page.
        /// </summary>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            HomeText.Text = AppText.HomeMessage;
            WelcomeText.Text = AppText.WelcomeMessage;
        }


        void Authentication_LoggedIn(object sender, System.ServiceModel.DomainServices.Client.ApplicationServices.AuthenticationEventArgs e)
        {
            if (WebContext.Current.User.IsInRole("Klient"))
            {
                this.NavigationService.Navigate(new Uri("/Klient", UriKind.Relative));
            }
        }

        void Authentication_LoggedOut(object sender, System.ServiceModel.DomainServices.Client.ApplicationServices.AuthenticationEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Home", UriKind.Relative));
        }
    }
}