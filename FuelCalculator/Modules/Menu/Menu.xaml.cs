using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Navigation;
using FuelCalculator.Holders;
using Microsoft.Phone.Controls;

namespace FuelCalculator.Modules.Menu
{
    /// <summary>
    /// Menu aplikacji
    /// </summary>
    public partial class Menu : PhoneApplicationPage
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public Menu()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event odpalany podczas ładwoania widoku
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            tbDescription.Inlines.Clear();
            tbDescription.Inlines.Add(Environment.NewLine);
            tbDescription.Inlines.Add(new Run() { Text = "Kontekst danych:" + Environment.NewLine, FontWeight = FontWeights.Bold });
            tbDescription.Inlines.Add(new Run() { Text = "* Ilość samochodów: " });
            tbDescription.Inlines.Add(new Run() { Text = CarHolder.Instance.Cars.Count.ToString(), FontWeight = FontWeights.Bold, Foreground = new SolidColorBrush((Color)Application.Current.Resources["PhoneAccentColor"]) });
            tbDescription.Inlines.Add(Environment.NewLine);
            tbDescription.Inlines.Add(new Run() { Text = "* Ilość tankowań: " });
            tbDescription.Inlines.Add(new Run() { Text = RefuelingHolder.Instance.Refuels.Count.ToString(), FontWeight = FontWeights.Bold, Foreground = new SolidColorBrush((Color)Application.Current.Resources["PhoneAccentColor"]) });
            tbDescription.Inlines.Add(Environment.NewLine);
            tbDescription.Inlines.Add(Environment.NewLine);
            tbDescription.Inlines.Add(Environment.NewLine);
            tbDescription.Inlines.Add(Environment.NewLine);

            sbAddNewEntry.IsEnabled = CarHolder.Instance.Cars.Count > 0;
            sbPrintRaport.IsEnabled = RefuelingHolder.Instance.Refuels.Count > 0; 
        }

        /// <summary>
        /// Akcja kliknięcia na przycisk dodania nowego samochodu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddNewCar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Modules/Cars/CreateOrUpdateCar.xaml", UriKind.Relative));
        }

        /// <summary>
        /// Akcja kliknięcia na Samochody w menu aplikacji
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AppBarCars_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Modules/Cars/CarsList.xaml", UriKind.Relative));
        }

        /// <summary>
        /// Akcja kliknięcia na Tankowania w menu aplikacji
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AppBarFueling_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Modules/Fuel/RefuelingList.xaml", UriKind.Relative));
        }

        /// <summary>
        /// Akcja kliknięcia na dodanie tankowania
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddNewEntry_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Modules/Fuel/CreateOrUpdateFueling.xaml", UriKind.Relative));
        }

        /// <summary>
        /// Akcja kliknięcia na statystyki
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintRaport_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Modules/Reports/RefuelingRaport.xaml", UriKind.Relative));
        }
        
    }
}