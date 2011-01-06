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
using AwesomeParts.Web.POCOs;
using AwesomeParts.Web.Services;
using System.ServiceModel.DomainServices.Client;


namespace AwesomeParts.Controls
{
    public partial class KoszykAdder : UserControl
    {
        #region Dependency Properties

        public static readonly DependencyProperty DomainContextProperty =
            DependencyProperty.Register("DomainContext", typeof(DomainContext), typeof(KoszykAdder), new PropertyMetadata(null));
        public static readonly DependencyProperty ProduktIDProperty =
            DependencyProperty.Register("ProduktID", typeof(int), typeof(KoszykAdder), new PropertyMetadata(0));
        public static readonly DependencyProperty ZamowienieIDProperty =
            DependencyProperty.Register("ZamowienieID", typeof(int), typeof(KoszykAdder), new PropertyMetadata(0));
        public static readonly DependencyProperty KlientIDProperty =
            DependencyProperty.Register("KlientID", typeof(int), typeof(KoszykAdder), new PropertyMetadata(0));

        #endregion Dependency Properties

        #region Wrapper Properties

        public DomainContext DomainContext
        {
            get { return (DomainContext)GetValue(DomainContextProperty); }
            set { SetValue(DomainContextProperty, value); }
        }
        public int ProduktID
        {
            get { return (int)GetValue(ProduktIDProperty); }
            set { SetValue(ProduktIDProperty, value); }
        }
        public int ZamowienieID
        {
            get { return (int)GetValue(ZamowienieIDProperty); }
            set { SetValue(ZamowienieIDProperty, value); }
        }
        public int KlientID
        {
            get { return (int)GetValue(KlientIDProperty); }
            set { SetValue(KlientIDProperty, value); }
        }

        #endregion Wrapper Properties

        #region Event Handlers

        public event EventHandler AddToKoszykCompleted;

        #endregion Event Handlers

        #region Events

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddToKoszyk(ProduktID, int.Parse(IloscProduktow.Text), ZamowienieID, KlientID);
        }

        private void AddNewProduktToKoszykSubmitChangesCallback(SubmitOperation so)
        {
            (so.UserState as KoszykData).Context.ZamowieniaKoszykPOCOs.Clear();

            if (AddToKoszykCompleted != null)
                AddToKoszykCompleted(this, new EventArgs());
        }

        private void GenerateNewZamowienieSubmitChangesCallback(SubmitOperation so)
        {
            var kd = so.UserState as KoszykData;

            EntityQuery<ZamowieniePOCO> query = kd.Context.GetAktualneZamowienieByKlientIdQuery(kd.KlientID);
            kd.Context.Load<ZamowieniePOCO>(query, AktualneZamowienieLoadCompletedCallback, kd);
        }

        private void AktualneZamowienieLoadCompletedCallback(LoadOperation<ZamowieniePOCO> lo)
        {
            var kd = lo.UserState as KoszykData;
            int zamowienieID = lo.Entities.Cast<ZamowieniePOCO>().First<ZamowieniePOCO>().Id;
            
            ZamowienieID = zamowienieID;
            kd.ZamowienieID = zamowienieID;
            AddNewProduktToKoszyk(kd);
        }

        #endregion Events

        #region Helper Methods

        public virtual void AddToKoszyk(int produktID, int ilosc, int zamowienieID, int klientID)
        {
            var apContext = DomainContext as AwesomePartsContext;
            KoszykData kd = new KoszykData
            {
                Context = apContext,
                ProduktID = produktID,
                KlientID = klientID,
                Ilosc = ilosc,
                ZamowienieID = zamowienieID
            };

            if (zamowienieID > 0)
            {
                AddNewProduktToKoszyk(kd);
            }
            else
            {
                GenerateNewZamowienie(kd);
            }
        }

        private void GenerateNewZamowienie(KoszykData kd)
        {
            kd.Context.ZamowieniePOCOs.Clear();

            kd.Context.ZamowieniePOCOs.Add(new ZamowieniePOCO
            {
                KlientID = kd.KlientID
            });

            kd.Context.SubmitChanges(GenerateNewZamowienieSubmitChangesCallback, kd);
        }

        private void AddNewProduktToKoszyk(KoszykData kd)
        {
            kd.Context.ZamowieniaKoszykPOCOs.Add(new ZamowieniaKoszykPOCO
            {
                Ilosc = kd.Ilosc,
                ProduktID = kd.ProduktID,
                ZamowienieID = kd.ZamowienieID
            });

            kd.Context.SubmitChanges(AddNewProduktToKoszykSubmitChangesCallback, kd);
        }

        #endregion Helper Methods

        public KoszykAdder()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Contains context and data needed to add new Koszyk entity.
        /// </summary>
        private class KoszykData
        {
            public AwesomePartsContext Context { get; set; }
            public int ProduktID { get; set; }
            public int KlientID { get; set; }
            public int ZamowienieID { get; set; }
            public int Ilosc { get; set; }
        }
    }
}
