using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FuelCalculator.Models;

namespace FuelCalculator.Modules.Cars.Models
{
    /// <summary>
    /// Model do widoku z listą samochodów
    /// </summary>
    public class CarListEntry
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public CarListEntry()
        {
            RefuelingList = new List<Refueling>();
        }

        /// <summary>
        /// Samochód
        /// </summary>
        public Car Car { get; set; }

        /// <summary>
        /// Lista tankowań
        /// </summary>
        public List<Refueling> RefuelingList { get; set; }

        /// <summary>
        /// Geter do wyświetlenia wartości
        /// </summary>
        public string Engine { get { return " [" + Car.Engine + "]"; } }

        /// <summary>
        /// Data utworzenia w systemie
        /// </summary>
        public string CreateTime { get { return Car.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"); } }

        /// <summary>
        /// Ilość tankowań
        /// </summary>
        public string FuelingCount { get { return RefuelingList.Count.ToString(); } }

        /// <summary>
        /// Wydana gotówka na paliwo
        /// </summary>
        public string FullCash { get { return RefuelingList.Sum(p => p.Price).ToString("n2"); } }

        /// <summary>
        /// Ilość paliwa na ilość przejechanych kilometrów
        /// </summary>
        public string FuelPerKilometers
        {
            get
            {
                return RefuelingList.Sum(p => p.FuelAmount).ToString("n2") + " / " + RefuelingList.Sum(p => p.Distance).ToString("n2");
            }
        }

        /// <summary>
        /// Średnie spalanie
        /// </summary>
        public string AverageFuel
        {
            get
            {
                if (RefuelingList.Count == 0)
                    return "Brak danych";
                decimal fuelAmount = RefuelingList.Sum(p => p.FuelAmount);
                decimal distance = RefuelingList.Sum(p => p.Distance);

                if (distance == 0)
                    return "Błędne dane!";

                return ((fuelAmount / distance) * 100).ToString("n2");
            } 
        }
    }
}
