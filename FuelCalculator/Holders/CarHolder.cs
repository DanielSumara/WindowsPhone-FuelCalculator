using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Xml.Serialization;
using FuelCalculator.Models;

namespace FuelCalculator.Holders
{
    /// <summary>
    /// Holder do przechowywania listy dodanych samochodów
    /// </summary>
    public class CarHolder
    {
        #region Singleton

        /// <summary>
        /// Akcesor do singletona
        /// </summary>
        public static CarHolder Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// Statyczna instancja holdera
        /// </summary>
        private static CarHolder instance = new CarHolder();

        /// <summary>
        /// Prywatny konstruktor
        /// </summary>
        private CarHolder()
        {
        }

        #endregion

        #region Properties

        public List<Car> Cars { get; private set; }

        #endregion

        #region Metody publiczne

        /// <summary>
        /// Inicjalizuje dane w holderze 
        /// </summary>
        public void Inicjalize()
        {
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (!isf.DirectoryExists("Data"))
                    isf.CreateDirectory("Data");

                string carsPath = System.IO.Path.Combine("Data", "Cars.bin");
                if (isf.FileExists(carsPath))
                {
                    using (IsolatedStorageFileStream rawStream = isf.OpenFile(carsPath, System.IO.FileMode.Open))
                    {
                        XmlSerializer xs = new XmlSerializer(typeof(List<Car>));
                        Cars = xs.Deserialize(rawStream) as List<Car>;
                    }
                }
                else
                {
                    Cars = new List<Car>();
                    SaveDataInStorage();
                }
            }
        }

        /// <summary>
        /// Zapisuje dane do IsolatedStorage
        /// </summary>
        public void SaveDataInStorage()
        {
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (!isf.DirectoryExists("Data"))
                    isf.CreateDirectory("Data");

                string carsPath = System.IO.Path.Combine("Data", "Cars.bin");

                using (IsolatedStorageFileStream rawStream = isf.OpenFile(carsPath, System.IO.FileMode.Create))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(List<Car>));
                    xs.Serialize(rawStream, Cars);
                }
            }
        }

        #endregion
    }
}
