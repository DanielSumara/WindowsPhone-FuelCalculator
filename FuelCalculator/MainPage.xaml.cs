using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Phone.Controls;

namespace FuelCalculator
{
    /// <summary>
    /// Widok startowy "aplikacji"
    /// </summary>
    public partial class MainPage : PhoneApplicationPage
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MainPage()
        {
            InitializeComponent();

            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = new TimeSpan(0, 0, 0, 0, 500);
            dt.Tick += Timer_Tick;
            dt.Start();

            sbRunAppication.IsEnabled = true;
        }

        /// <summary>
        /// Event tiknięcia - zwiększa progress bar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
                { 
                    pbProgressBar.Value += 10;
                    if (pbProgressBar.Value >= 100)
                    {
                        (sender as DispatcherTimer).Stop();
                        sbRunAppication.IsEnabled = true;
                    }
                });
        }

        /// <summary>
        /// Event kliknięcia na przycisk
        /// Przenosi nas do menu aplikacji
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RunAppication_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Modules/Menu/Menu.xaml", UriKind.Relative));
        }
    }
}