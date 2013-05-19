using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using FuelCalculator.Holders;
using FuelCalculator.Models;
using FuelCalculator.Modules.Cars.Models;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace FuelCalculator.Modules.Cars
{
    /// <summary>
    /// Widok dla listy samochodów w systemie
    /// </summary>
    public partial class CarsList : PhoneApplicationPage
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public CarsList()
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
            CarListEntry clrToRemove = (sender as MenuItem).DataContext as CarListEntry;

            if (MessageBox.Show(string.Format("Jesteś pewien że chcesz usunąć {0}? Spowoduje do usunięcie {1} tankowań!", clrToRemove.Car.Name, clrToRemove.FuelingCount), "Pytanie", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                CarHolder.Instance.Cars.Remove(clrToRemove.Car);
                foreach (Refueling rf in clrToRemove.RefuelingList)
                    RefuelingHolder.Instance.Refuels.Remove(rf);

                CarHolder.Instance.SaveDataInStorage();
                RefuelingHolder.Instance.SaveDataInStorage();

                InitList();
            }
        }

        /// <summary>
        /// Inicjalizuje dane na widoku
        /// </summary>
        private void InitList()
        {
            List<CarListEntry> data = new List<CarListEntry>();

            if (CarHolder.Instance.Cars.Count == 0)
            {
                MessageBox.Show("Dodaj samochód do systemu aby wyświetlić jego statystyki!", "Informacja", MessageBoxButton.OK);
                NavigationService.GoBack();
            }
            else
            {
                CarListEntry entry = null;
                foreach (Car car in CarHolder.Instance.Cars)
                {
                    entry = new CarListEntry();
                    entry.Car = car;
                    entry.RefuelingList = RefuelingHolder.Instance.Refuels.Where(p => p.RefueledCar.Equals(car)).Select(p => p).ToList<Refueling>();

                    data.Add(entry);
                }
            }

            lbCarsInfo.ItemsSource = data;
        }
    }
}