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
using AwesomeParts.Web;
using System.ServiceModel.DomainServices.Client;
using AwesomeParts.Web.Services;
using AwesomeParts.Web.Models;

namespace AwesomeParts.Controls
{
    public partial class UmowaWindow : ChildWindow
    {
        public static readonly DependencyProperty RegistrationDataProperty =
            DependencyProperty.Register("RegistrationData", typeof(ProfileData), typeof(UmowaWindow), new PropertyMetadata(null));

        private ProfileDisplay parentWindow;
        private ProfileContext profileContext;
        public ProfileData RegistrationData
        {
            get { return (ProfileData)GetValue(RegistrationDataProperty); }
            set { SetValue(RegistrationDataProperty, value); }
        }
        
        
        public UmowaWindow()
        {
            InitializeComponent();
        }

        public void SetParentWindow(ProfileDisplay window)
        {
            this.parentWindow = window;
            this.RegistrationData = parentWindow.RegistrationData;
            profileContext = parentWindow.Resources["ProfilContext"] as ProfileContext;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.registerForm.ValidateItem())
            {
                profileContext.UpdateUserData(this.RegistrationData, UpdatingUserDataCompleted, null);

            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UpdatingUserDataCompleted(InvokeOperation io)
        {
            this.Close();
        }
    }
}

