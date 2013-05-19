using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;
using FuelCalculator.Holders;
using FuelCalculator.Models;
using Microsoft.Phone.Controls;
using Phone.Controls;

namespace FuelCalculator.Modules.Fuel
{
    /// <summary>
    /// Widok dodawania nowego tankowania
    /// </summary>
    public partial class CreateOrUpdateFueling : PhoneApplicationPage
    {
        #region Zmienne prywatne

        /// <summary>
        /// Kontrolka do wybierania elementów z listy
        /// </summary>
        private PickerBoxDialog Cars { get; set; }

        #endregion

        /// <summary>
        /// KOnstruktor
        /// </summary>
        public CreateOrUpdateFueling()
        {
            InitializeComponent();

            Cars = new PickerBoxDialog();
            Cars.Title = "Samochód:";
            Cars.Closed += Engines_Closed;
        }
        
        /// <summary>
        /// Załadowanie widoku
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Cars.ItemSource = CarHolder.Instance.Cars;

            if (RefuelingHolder.Instance.Refuels.Count > 0)
                tbCarName.DataContext = RefuelingHolder.Instance.Refuels[RefuelingHolder.Instance.Refuels.Count - 1].RefueledCar;
            
            base.OnNavigatedTo(e);
        }

        /// <summary>
        /// Ustawia wybraną wartość w kontrolce z nazwą samochodu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Engines_Closed(object sender, EventArgs e)
        {
            tbCarName.DataContext = Cars.SelectedItem;
        }

        /// <summary>
        /// Akcja kliknięcia na samochód
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectCar_Click(object sender, RoutedEventArgs e)
        {
            Cars.Show();
        }

        /// <summary>
        /// Zapisanie tankowania
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Click(object sender, EventArgs e)
        {
            if (tbCarName.DataContext == null)
            {
                MessageBox.Show("Nie wybrałeś samochodu!", "Błąd!", MessageBoxButton.OK);
                return;
            }
            if (string.IsNullOrEmpty(tbFuelAmount.Text))
            {
                //MessageBox.Show("Nie wprowadziłeś ilości paliwa!", "Błąd!", MessageBoxButton.OK);
                tbFuelAmount.Focus();
                return;
            }
            if (string.IsNullOrEmpty(tbPrice.Text))
            {
                //MessageBox.Show("Nie wprowadziłeś ilości kwoty za paliwo!", "Błąd!", MessageBoxButton.OK);
                tbPrice.Focus();
                return;
            }
            if (string.IsNullOrEmpty(tbDistance.Text))
            {
                //MessageBox.Show("Nie wprowadziłeś przejechanego dystansu!", "Błąd!", MessageBoxButton.OK);
                tbDistance.Focus();
                return;
            }
            else
            {
                if (decimal.Parse(tbDistance.Text.Replace(".", ",")) == 0)
                {
                    tbDistance.Focus();
                    return;
                }
            }

            Refueling rf = new Refueling();
            rf.CreateTime = dpFuelingDate.Value.Value;
            rf.Distance = decimal.Parse(tbDistance.Text.Replace(".", ","));
            rf.FuelAmount = decimal.Parse(tbFuelAmount.Text.Replace(".", ","));
            rf.Price = decimal.Parse(tbPrice.Text.Replace(".", ","));
            rf.RefueledCar = tbCarName.DataContext as Car;

            RefuelingHolder.Instance.Refuels.Add(rf);
            RefuelingHolder.Instance.SaveDataInStorage();

            MessageBox.Show("Zapisałeś tankownie!", "Informacja", MessageBoxButton.OK);
            NavigationService.GoBack();
        }

        /// <summary>
        /// Event kliknięcia na powrót
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelClick(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}