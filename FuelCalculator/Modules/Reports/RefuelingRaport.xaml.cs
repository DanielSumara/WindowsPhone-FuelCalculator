using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using FuelCalculator.Holders;
using FuelCalculator.Models;
using Microsoft.Phone.Controls;

namespace FuelCalculator.Modules.Reports
{
    /// <summary>
    /// Raporty
    /// </summary>
    public partial class RefuelingRaport : PhoneApplicationPage
    {
        /// <summary>
        /// Klasa do wykresu kołowego 
        /// </summary>
        public class PieChartEntry
        {
            /// <summary>
            /// Samochoód
            /// </summary>
            public Car Car { get; set; }

            /// <summary>
            /// Ilość tankowań
            /// </summary>
            public double Count { get; set; }
        }

        /// <summary>
        /// Klasa prezentująca wykres "serialowy"
        /// </summary>
        public class SerialChartEntry
        {
            /// <summary>
            /// Miesiąc
            /// </summary>
            public string Mounth { get; set; }

            /// <summary>
            /// Ilość paliwa
            /// </summary>
            public double Fuel { get; set; }

            /// <summary>
            /// Przebyta odległość
            /// </summary>
            public double Distance { get; set; }

            /// <summary>
            /// Cena paliwa
            /// </summary>
            public double Price { get; set; }

            /// <summary>
            /// Średnie spalanie
            /// </summary>
            public double FuelPerDistance { get; set; }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public RefuelingRaport()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Załadowanie widoku
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            pcTankingPerCar.DataSource = RefuelingHolder.Instance.Refuels.GroupBy(p => p.RefueledCar).Select(p => new PieChartEntry() { Car = p.Key, Count = p.Count() });
            
            var list = RefuelingHolder.Instance.Refuels.GroupBy(p => p.CreateTime.ToString("yyyy-MM")).Select(p => new SerialChartEntry()
            {
                Mounth = p.Key,
                Fuel = (double)p.Sum(r => r.FuelAmount),
                Distance = (double)p.Sum(r => r.Distance),
                Price = (double)p.Sum(r => r.Price),
                FuelPerDistance = (double)((p.Sum(r => r.FuelAmount) / p.Sum(r => r.Distance)) * 100)
            });

            scStats1.DataSource = list;
            scStats2.DataSource = list;
        }
    }
}