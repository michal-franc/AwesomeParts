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
            this.Title = ApplicationStrings.HomePageTitle;
            
        }

        /// <summary>
        /// Executes when the user navigates to this page.
        /// </summary>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            HomeText.Text = String.Format(AppText.HomeMessage, Environment.NewLine);
            WelcomeText.Text = AppText.WelcomeMessage;
        }
    }
}