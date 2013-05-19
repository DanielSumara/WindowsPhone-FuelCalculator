using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Xml.Serialization;
using FuelCalculator.Models;

namespace FuelCalculator.Holders
{
    public class RefuelingHolder
    {
        #region Singleton

        /// <summary>
        /// Akcesor do singletona
        /// </summary>
        public static RefuelingHolder Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// Statyczna instancja holdera
        /// </summary>
        private static RefuelingHolder instance = new RefuelingHolder();

        /// <summary>
        /// Prywatny konstruktor
        /// </summary>
        private RefuelingHolder()
        {
        }

        #endregion

        #region Properties

        public List<Refueling> Refuels { get; private set; }

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

                string carsPath = System.IO.Path.Combine("Data", "Fuels.bin");
                if (isf.FileExists(carsPath))
                {
                    using (IsolatedStorageFileStream rawStream = isf.OpenFile(carsPath, System.IO.FileMode.Open))
                    {
                        XmlSerializer xs = new XmlSerializer(typeof(List<Refueling>));
                        Refuels = xs.Deserialize(rawStream) as List<Refueling>;
                    }
                }
                else
                {
                    Refuels = new List<Refueling>();
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

                string carsPath = System.IO.Path.Combine("Data", "Fuels.bin");

                using (IsolatedStorageFileStream rawStream = isf.OpenFile(carsPath, System.IO.FileMode.Create))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(List<Refueling>));
                    xs.Serialize(rawStream, Refuels);
                }
            }
        }

        #endregion
    }
}
