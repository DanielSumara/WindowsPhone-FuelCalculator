using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FuelCalculator.Models
{
    /// <summary>
    /// Wartość słownikowa
    /// </summary>
    public class DicItem
    {
        /// <summary>
        /// Kod elementu
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Wyświetlana wartość
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Porównywarka
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is DicItem)
                return Code == (obj as DicItem).Code;
            return false;
        }

        /// <summary>
        /// Kod mieszania
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return string.IsNullOrEmpty(Code) ? 0 : Code.GetHashCode();
        }

        /// <summary>
        /// Wyświetlarka
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Value;
        }
    }
}
