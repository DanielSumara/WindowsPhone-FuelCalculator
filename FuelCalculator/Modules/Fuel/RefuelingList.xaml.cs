using System.Collections.Generic;
using System.Windows;
using System.Windows.Navigation;
using FuelCalculator.Holders;
using FuelCalculator.Models;
using Microsoft.Phone.Controls;

namespace FuelCalculator.Modules.Fuel
{
    /// <summary>
    /// Lista tankowań
    /// </summary>
    public partial class RefuelingList : PhoneApplicationPage
    {
        /// <summary>
        /// Konstruktor 
        /// </summary>
        public RefuelingList()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Event ładowania widoku
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            InitList();

            base.OnNavigatedTo(e);
        }

        /// <summary>
        /// Usuwa zaznaczony element
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Refueling clrToRemove = (sender as MenuItem).DataContext as Refueling;

            if (MessageBox.Show(string.Format("Jesteś pewien że chcesz tankowanie z {0}?", clrToRemove.CreateTime), "Pytanie", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                RefuelingHolder.Instance.Refuels.Remove(clrToRemove);

                RefuelingHolder.Instance.SaveDataInStorage();
                InitList();
            }
        }

        /// <summary>
        /// Inicjalizuje dane na widoku
        /// </summary>
        private void InitList()
        {
            if (RefuelingHolder.Instance.Refuels.Count == 0)
            {
                MessageBox.Show("Dodaj tankowanie do systemu!", "Informacja", MessageBoxButton.OK);
                NavigationService.GoBack();
            }
            else
            {
                List<Refueling> list = new List<Refueling>(RefuelingHolder.Instance.Refuels);
                list.Reverse();
                lbCarsInfo.ItemsSource = list;
            }
        }
    }
}