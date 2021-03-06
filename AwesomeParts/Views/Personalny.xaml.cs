﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;
using AwesomeParts.Web.Services;
using AwesomeParts.Web.POCOs;
using System.ServiceModel.DomainServices.Client;


namespace AwesomeParts.Views
{
    public partial class Personalny : Page
    {
        private int PracownikID { get; set; }
        private DomainDataSource dds;
        private bool PendingChanges { get; set; }
        private bool AddingNewItem { get; set; }

        private const string UMOWY_TAB_HEADER = "Umowy pracownika {0} {1}";

        public Personalny()
        {
            InitializeComponent();

            PendingChanges = false;
            AddingNewItem = false;

            dds = new DomainDataSource();
            dds.Name = "dds";
            dds.AutoLoad = false;
            dds.DomainContext = (AwesomePartsContext)this.Resources["PracowContext"];
            dds.QueryName = "GetUmowyByPracownikId";

            GrupujCB.SelectionChanged += GrupujCB_SelectionChanged;
            PracownicyGrid.SelectionChanged += new SelectionChangedEventHandler(PracownicyGrid_SelectionChanged);
            PracownicyDataForm.GotFocus += new RoutedEventHandler(PracownicyDataForm_GotFocus);
            PracownicyDataForm.AddingNewItem += new EventHandler<DataFormAddingNewItemEventArgs>(PracownicyDataForm_AddingNewItem);
            PracownicyDataForm.EditEnded += new EventHandler<DataFormEditEndedEventArgs>(PracownicyDataForm_EditEnded);
            UmowyDataForm.AddingNewItem += new EventHandler<DataFormAddingNewItemEventArgs>(UmowyDataForm_AddingNewItem);
            UmowyDataForm.EditEnded += new EventHandler<DataFormEditEndedEventArgs>(UmowyDataForm_EditEnded);
            UmowyDataForm.GotFocus += new RoutedEventHandler(UmowyDataForm_GotFocus);
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //if (!WebContext.Current.User.IsInRole("DzialPersonalny"))
            //{
            //    this.NavigationService.Navigate(new Uri("/NoAcces", UriKind.Relative));
            //}
        }

        #region Events

        void PracownicyDataForm_EditEnded(object sender, DataFormEditEndedEventArgs e)
        {
            CheckPendingChanges(e);

            if (e.EditAction == DataFormEditAction.Commit)
                UmowyDataForm.IsEnabled = false;
        }

        void PracownicyDataForm_GotFocus(object sender, RoutedEventArgs e)
        {
            if (AddingNewItem && PendingChanges)
            {
                PracownicyDataForm.IsEnabled = false;
                ShowErrorWindow("Zatwierdź zmiany!", "Najpierw musisz zatwierdzić zmiany, które wprowadziłeś do tej pory w systemie.");
            }
        }

        void UmowyDataForm_GotFocus(object sender, RoutedEventArgs e)
        {
            if (AddingNewItem && PendingChanges)
            {
                UmowyDataForm.IsEnabled = false;
                ShowErrorWindow("Zatwierdź zmiany!", "Najpierw musisz zatwierdzić zmiany, które wprowadziłeś do tej pory w systemie.");
            }
        }

        void UmowyDataForm_EditEnded(object sender, DataFormEditEndedEventArgs e)
        {
            CheckPendingChanges(e);

            if (e.EditAction == DataFormEditAction.Commit)
                PracownicyDataForm.IsEnabled = false;
        }

        void UmowyDataForm_AddingNewItem(object sender, DataFormAddingNewItemEventArgs e)
        {
            AddingNewItem = true;
        }

        void PracownicyDataForm_AddingNewItem(object sender, DataFormAddingNewItemEventArgs e)
        {
            AddingNewItem = true;
        }

        private void GrupujCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckGrupujCB();
        }

        private void PracownicyGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PracownikPOCO p = (PracownicyGrid.SelectedItem as PracownikPOCO);
            if (p != null)
            {
                PracownikID = p.Id;
                UmowyTab.Header = String.Format(UMOWY_TAB_HEADER, p.Imie, p.Nazwisko);

                if (dds.CanLoad)
                {
                    dds.QueryParameters.Clear();
                    dds.QueryParameters.Add(new Parameter
                    {
                        ParameterName = "pracownikID",
                        Value = PracownikID,
                    });
                    dds.Load();
                    UmowyGrid.ItemsSource = dds.Data;
                    UmowyDataForm.ItemsSource = dds.Data;
                }
                else
                {
                    UmowyGrid.ItemsSource = null;
                    UmowyDataForm.ItemsSource = null;
                }
            }
        }

        private void SubmitChanges_Click(object sender, RoutedEventArgs e)
        {
            AwesomePartsContext context = (AwesomePartsContext)this.Resources["PracowContext"];
            foreach (var umowa in context.PracownikUmowaPOCOs)
            {
                umowa.PracownikID = this.PracownikID;
            }
            context.SubmitChanges();

            CheckGrupujCB();
            PendingChanges = false;
            AddingNewItem = false;

            NavigationService.Refresh();
        }

        private void RevertChanges_Click(object sender, RoutedEventArgs e)
        {
            AwesomePartsContext context = (AwesomePartsContext)this.Resources["PracowContext"];
            context.RejectChanges();
            PendingChanges = false;
            AddingNewItem = false;
            NavigationService.Refresh();
        }

        #endregion Events

        #region Helper Methods

        private void CheckGrupujCB()
        {
            if (DataSource.CanLoad == true)
            {
                if (GrupujCB.SelectedIndex == 0)
                {
                    DataSource.GroupDescriptors.Clear();
                    DataSource.Load();
                    PracownicyGrid.ItemsSource = DataSource.Data;
                    PracownicyGrid.SelectedIndex = 0;
                }
                else if (GrupujCB.SelectedIndex == 1)
                {
                    DataSource.GroupDescriptors.Clear();
                    DataSource.GroupDescriptors.Add(new GroupDescriptor
                    {
                        PropertyPath = "Rodzaj.Rodzaj"
                    });
                    DataSource.Load();

                    PracownicyGrid.ItemsSource = DataSource.Data;
                    PracownicyGrid.SelectedIndex = 0;
                }
                else if (GrupujCB.SelectedIndex == 2)
                {
                    DataSource.GroupDescriptors.Clear();
                    DataSource.GroupDescriptors.Add(new GroupDescriptor
                    {
                        PropertyPath = "Status.Status"
                    });
                    DataSource.Load();

                    PracownicyGrid.ItemsSource = DataSource.Data;
                    PracownicyGrid.SelectedIndex = 0;
                }
            }
        }

        private bool CheckPendingChanges(EventArgs e)
        {
            DataFormEditEndedEventArgs dfe = e as DataFormEditEndedEventArgs;

            if (dfe != null)
            {
                if (dfe.EditAction == DataFormEditAction.Cancel && AddingNewItem)
                {
                    AddingNewItem = false;
                    PendingChanges = false;

                    return false;
                }
                else if (dfe.EditAction == DataFormEditAction.Cancel)
                {
                    PendingChanges = false;

                    return false;
                }
                else
                {
                    PendingChanges = true;
                    SubmitChanges.Background = new SolidColorBrush(Colors.Red);
                    SubmitChanges2.Background = new SolidColorBrush(Colors.Red);
                    PracownicyGrid.IsEnabled = false;
                    UmowyGrid.IsEnabled = false;

                    return true;
                }
            }
            else
            {
                if (PendingChanges == true)
                {
                    ShowErrorWindow("Zatwierdź zmiany!", "Najpierw musisz zatwierdzić zmiany, które wprowadziłeś do tej pory w systemie.");
                    return true;
                }
                else
                    return false;
            }
        }

        private void ShowErrorWindow(string title, string message)
        {
            ErrorWindow error = new ErrorWindow(title, message);
            error.Show();
        }

        #endregion Helper Methods
    }
}
