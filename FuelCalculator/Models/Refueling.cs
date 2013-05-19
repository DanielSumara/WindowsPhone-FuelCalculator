using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FuelCalculator.Models
{
    /// <summary>
    /// Obiekt tankowania
    /// </summary>
    public class Refueling : IEquatable<Refueling>
    {
        /// <summary>
        /// Samochód który tankujemy
        /// </summary>
        public Car RefueledCar { get; set; }

        /// <summary>
        /// Data tankowania
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Ilość litrów 
        /// </summary>
        public decimal FuelAmount { get; set; }

        /// <summary>
        /// Kwota za tankowanie
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Ilość kilometrów jaką pokonaliśmy
        /// </summary>
        public decimal Distance { get; set; }

        /// <summary>
        /// Średnie spalanie
        /// </summary>
        public string AverageFuel
        {
            get
            {
                return ((FuelAmount / Distance) * 100).ToString("n2");
            }
        }

        /// <summary>
        /// Porównywarka
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is Refueling)
                return Equals(obj as Refueling);
            return false;
        }

        /// <summary>
        /// Kod mieszania
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return RefueledCar != null ? RefueledCar.GetHashCode() ^ CreateTime.GetHashCode() : 0;
        }

        #region IEquatable<Refueling> Members

        /// <summary>
        /// Porównywarka
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Refueling other)
        {
            return RefueledCar.Equals(other.RefueledCar) && CreateTime.Equals(other.CreateTime);
        }

        #endregion
    }
}
