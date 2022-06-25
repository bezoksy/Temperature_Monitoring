using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FishMonitoringCore;
using System.IO;

namespace FishMonitoringWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string currentReport;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            int interval = 10;
            if (Validate())
            {

                string tempData = temperature.Text;
                DateTime time = Convert.ToDateTime(datetime.Text);

                Quality quality = new TempQuality(interval, tempData, time);
                double maxtemp = Convert.ToDouble(maxTemp.Text);
                int maxtine = Convert.ToInt32(maxTIme.Text);
                int mintime = Convert.ToInt32(minTime.Text);
                double mintemp = Convert.ToDouble(minTemp.Text);
                Fish fish = new FrozenFish(quality, maxtemp, mintemp, new TimeSpan(0, maxtine, 0), new TimeSpan(0, mintime, 0));

                currentReport = fish.GetReport();
                MessageBox.Show(currentReport);
                btnSaveReport.IsEnabled = true;
            }
        }
        private bool Validate()
        {
            double temp = 0;
            if (!Double.TryParse(maxTemp.Text, out temp))
            {
                MessageBox.Show("Нужно ввести числа в поле с температурой");
                maxTemp.Focusable = true;
                Keyboard.Focus(maxTemp);
                return false;
            }
            double temp1 = 0;
            if (!Double.TryParse(minTemp.Text, out temp1))
            {
                MessageBox.Show("Нужно ввести числа в поле с температурой");
                minTemp.Focusable = true;
                Keyboard.Focus(minTemp);
                return false;
            }

            int temp2 = 0;
            if (!Int32.TryParse(maxTIme.Text, out temp2))
            {
                if (Convert.ToInt32(minTime.Text) < 0)
                {
                    MessageBox.Show("Нужно ввести числа в поле с временем");
                    maxTIme.Focusable = true;
                    Keyboard.Focus(maxTIme);
                    return false;

                }
            }

            int temp3 = 0;
            if (!Int32.TryParse(minTime.Text, out temp3))
            {
                if (Convert.ToInt32(minTime.Text) < 0)
                {
                    MessageBox.Show("Нужно ввести числа в поле с временем");
                    minTime.Focusable = true;
                    Keyboard.Focus(minTime);
                    return false;

                }
            }

            DateTime temp4;
            if (!DateTime.TryParse(datetime.Text, out temp4))
            {
                MessageBox.Show("Нужно ввести дату и время");
                datetime.Focusable = true;
                Keyboard.Focus(datetime);
                return false;
            }
            return true;

        }

        private void temperature_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            double temp = 0;
            if (!Double.TryParse(maxTemp.Text, out temp))
            {
                MessageBox.Show("Нужно ввести числа в поле с температурой");
                maxTemp.Focusable = true;
                Keyboard.Focus(maxTemp);
            }
        }
        private void btnSaveReport_Click(object sender, RoutedEventArgs e)
        {
            File.WriteAllText($"{Directory.GetCurrentDirectory()}\\test.txt", currentReport, Encoding.Default);
        }
    }
}
