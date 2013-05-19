using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;
using FuelCalculator.Holders;
using FuelCalculator.Models;
using Microsoft.Phone.Controls;
using Phone.Controls;

namespace FuelCalculator.Modules.Cars
{
    /// <summary>
    /// Tworzy lub aktualizuje entry samochodu
    /// </summary>
    public partial class CreateOrUpdateCar : PhoneApplicationPage
    {
        #region Zmienne prywatne

        /// <summary>
        /// Kontrolka do wybierania elementów z listy
        /// </summary>
        private PickerBoxDialog Engines { get; set; }

        #endregion

        /// <summary>
        /// Konstruktor
        /// </summary>
        public CreateOrUpdateCar()
        {
            InitializeComponent();

            Engines = new PickerBoxDialog();
            List<DicItem> engines = new List<DicItem>();
            engines.Add(new DicItem() { Code = "NO", Value = "Diesel" });
            engines.Add(new DicItem() { Code = "LPG", Value = "Gaz" });
            engines.Add(new DicItem() { Code = "PETROL", Value = "Benzyna" });
            Engines.ItemSource = engines;
            Engines.Title = "Typ silnika:";
            Engines.Closed += Engines_Closed;
        }

        #region IAddNewCar

        /// <summary>
        /// Wybrany silnik
        /// </summary>
        public DicItem Engine
        {
            get
            {
                return Engines.SelectedItem as DicItem;
            }
            set
            {
                Engines.SelectedItem = value;
            }
        }

        /// <summary>
        /// Wyświetlana nazwa samochodu
        /// </summary>
        public string CarName {
            get
            {
                return tbCarName.Text;
            }
            set
            {
                tbCarName.Text = value;
            }
        }

        #endregion

        #region Lista rozwijana

        /// <summary>
        /// Event zamknięcia listy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Engines_Closed(object sender, EventArgs e)
        {
            tbEngineName.DataContext = Engines.SelectedItem;
        }

        /// <summary>
        /// Event otwarcia listy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Engine_Click(object sender, RoutedEventArgs e)
        {
            Engines.Show();
        }

        #endregion

        #region Przyciski

        /// <summary>
        /// Akcja kliknięcia na Zapisz 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveMenu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CarName))
            {
                //MessageBox.Show("Wprowadź nazwę nowego samochodu!");
                tbCarName.Focus();
                return;
            }

            if (Engine == null)
            {
                //MessageBox.Show("Wybierz silnik!");
                Engines.Show();
                return;
            }

            if (CarHolder.Instance.Cars.FirstOrDefault(p => p.Name.ToUpper() == CarName.ToUpper()) != null)
            {
                MessageBox.Show(string.Format("Istnieje już samochód o nazwie {0}!", CarName), "Błąd!", MessageBoxButton.OK);
                return;
            }

            MessageBox.Show(string.Format("Dodałeś {0} [{1}]!\n Zostaniesz przekierowany do menu.", CarName, Engine), "Informacja", MessageBoxButton.OK);
            CarHolder.Instance.Cars.Add(new Car() { CreateTime = DateTime.Now, Engine = Engine, Name = CarName });
            CarHolder.Instance.SaveDataInStorage();
            NavigationService.GoBack();
        }

        /// <summary>
        /// Akcja kliknięcia na Anuluj
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelMenu_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }

        #endregion
    }
}