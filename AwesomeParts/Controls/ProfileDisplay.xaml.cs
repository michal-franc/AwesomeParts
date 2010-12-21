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
using AwesomeParts.Web.Services;
using System.ServiceModel.DomainServices.Client;
using AwesomeParts.Web.POCOs;

namespace AwesomeParts.Controls
{
    public partial class ProfileDisplay : UserControl
    {
        private AwesomeParts.Web.User _user;

        public AwesomeParts.Web.User User 
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
                Reload();
            }
        } 

        public ProfileDisplay()
        {
            InitializeComponent();
        }

        private void Reload()
        {
            AwesomePartsContext context = new AwesomePartsContext();

            EntityQuery<PracownikPOCO> pracownikQuery = context.GetPracownicyQuery();
            context.Load<PracownikPOCO>(pracownikQuery);

            PracownikPOCO pracownik = context.PracownikPOCOs.GetEnumerator().Current;
                //context.PracownikPOCOs.Where(x => x.Id == 1).Select(x => x).First();

        }

        private void btnAction1_Click(object sender, RoutedEventArgs e)
        {
            UmowaWindow umowa = new UmowaWindow();
            umowa.Show();
        }
    }
}
