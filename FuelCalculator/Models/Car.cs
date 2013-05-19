using System;

namespace FuelCalculator.Models
{
    /// <summary>
    /// Obiekt przechowujący informację o posiadanych samochodach
    /// </summary>
    public class Car
    {
        /// <summary>
        /// Data dodania samochodu do listy
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Nazwa samochodu
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Typ silnika
        /// </summary>
        public DicItem Engine { get; set; }

        /// <summary>
        /// Porównywarka
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is Car)
                return Name == (obj as Car).Name;
            return false;
        }
        
        /// <summary>
        /// Kod mieszania
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return string.IsNullOrEmpty(Name) ? 0 : Name.GetHashCode();
        }

        /// <summary>
        /// Wyświetlarka
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
