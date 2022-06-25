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

namespace FishMonitoringWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            int interval = 10;
            string tempData = temperature.Text;
            DateTime time = Convert.ToDateTime(datetime.Text);

            Quality quality = new TempQuality(interval, tempData, time);
            double maxtemp = Convert.ToDouble(maxTemp.Text);
            int maxtine = Convert.ToInt32(maxTIme.Text);
            int mintime = Convert.ToInt32(minTime.Text);
            double mintemp = Convert.ToDouble(minTemp.Text);
            Fish fish = new FrozenFish(quality, maxtemp, mintemp, new TimeSpan(0, maxtine, 0), new TimeSpan(0, mintime, 0));
            MessageBox.Show(fish.GetReport());
        }
    }
}
