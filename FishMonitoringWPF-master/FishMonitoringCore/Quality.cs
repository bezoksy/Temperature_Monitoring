using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishMonitoringCore
{
    public abstract class Quality
    {

    }

    public class TempQuality : Quality
    {
        public Dictionary<DateTime, double> temperature;

        public TempQuality()
        {
            temperature = new Dictionary<DateTime, double>();
        }

        public TempQuality(Dictionary<DateTime, double> temp)
        {
            this.temperature = temp;
        }
        public TempQuality(DateTime begin, int timeInterval, string temperatureData) : this()
        {
            var time = begin;
            var interval = TimeSpan.FromMinutes(timeInterval);
            foreach (var t in temperatureData.Split(' '))
            {
                temperature.Add(time, Double.Parse(t));
                time += interval;
            }
        }

        public TempQuality(int timeInterval, string temperatureData) : this()
        {

            var time = DateTime.Now;
            var interval = TimeSpan.FromMinutes(timeInterval);
            foreach (var t in temperatureData.Split(' '))
            {
                temperature.Add(time, Double.Parse(t));
                time += interval;
            }

        }

        public double GetMaxTemp()
        {
            return 10.1;
        }

        public double GetMinTemp()
        {
            return 1.1;
        }

        public TimeSpan GetTempUpperTime(double temp)
        {
            TimeSpan result = new TimeSpan();
            foreach (DateTime k1 in temperature.Keys)
            {
                foreach (DateTime k2 in temperature.Keys)
                {
                    if (k2 > k1 && temperature[k1] > temp && temperature[k2] > temp)
                    {
                        result += k2 - k1;
                    }
                }
            }
            return result;
        }

        public TimeSpan GetTempLowerTime(double temp)
        {
            return new TimeSpan();
        }
    }
}
