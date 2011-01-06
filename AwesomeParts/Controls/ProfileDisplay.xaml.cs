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
using AwesomeParts.Web;
using AwesomeParts.Web.Models;

namespace AwesomeParts.Controls
{
    public partial class ProfileDisplay : UserControl
    {
        public static readonly DependencyProperty RegistrationDataProperty =
            DependencyProperty.Register("RegistrationData", typeof(ProfileData), typeof(ProfileDisplay), new PropertyMetadata(null));

        private AwesomeParts.Web.User _user;

        public ProfileData RegistrationData
        {
            get { return (ProfileData)GetValue(RegistrationDataProperty); }
            set { SetValue(RegistrationDataProperty, value); }
        }

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
            ProfileContext context = this.Resources["ProfilContext"] as ProfileContext;
            string userID = (string)_user.GetIdentity();

            EntityQuery<ProfileData> query = context.GetUserByUserNameQuery(userID);
            context.Load<ProfileData>(query, ProfileDataLoadCompleted, null);
        }

        private void btnAction1_Click(object sender, RoutedEventArgs e)
        {
            UmowaWindow umowa = new UmowaWindow();
            umowa.SetParentWindow(this);
            umowa.Show();
        }

        private void ProfileDataLoadCompleted(LoadOperation<ProfileData> lo)
        {
            ProfileContext context = this.Resources["ProfilContext"] as ProfileContext;
            RegistrationData = context.ProfileDatas.First();
        }
    }
}
