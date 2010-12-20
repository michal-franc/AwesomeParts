namespace AwesomeParts
{
    using System.Windows.Controls;
    using System.Windows.Navigation;
    using AwesomeParts.Web.Services;
    using AwesomeParts.Web.POCOs;
    using System.ServiceModel.DomainServices.Client;
    using System.Collections.Generic;

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
<<<<<<< HEAD
        //    ProduktyContext context = new ProduktyContext();

        //    EntityQuery<ProduktPOCO> query = context.GetProduktyQuery();
        //    context.Load<ProduktPOCO>(query);
        //    ProduktyGrid.ItemsSource = context.ProduktPOCOs;
            //ProduktyContext context = new ProduktyContext();
            //EntityQuery<ProduktProducentPOCO> query = context.GetProducenciQuery();
            //context.Load<ProduktProducentPOCO>(query);
            //ProducenciComboBox.ItemsSource = context.ProduktProducentPOCOs;
        }

=======
            HomeText.Text = AppText.HomeMessage;
            WelcomeText.Text = AppText.WelcomeMessage;
        }
>>>>>>> 55a2f3e896af6ef87f9b63d908ba0d0d65b9a831
    }
}