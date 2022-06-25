//Giniyatullin_Ilyas_220_task_FishMonitoring 25.06.2022
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FishMonitoringCore
{
    public abstract class Fish
    {
        public string name;
        public Quality quality;

        public Fish(Quality q)
        {
            quality = q;
        }
        public abstract bool isValid();

        public abstract string GetReport();

    }

    public class FrozenFish : Fish
    {
        public double maxStoreTemp;
        public double minStoreTemp;
        public TimeSpan deathTimeUpper;
        public TimeSpan deathTimeLower;

        public FrozenFish(Quality q, double maxTemp, double minTemp, TimeSpan dUpper, TimeSpan dLower) : base(q)
        {
            maxStoreTemp = maxTemp;
            minStoreTemp = minTemp;
            deathTimeUpper = dUpper;
            deathTimeLower = dLower;
        }

        public override bool isValid()
        {
            return !((quality as TempQuality).GetTempUpperTime(maxStoreTemp) > deathTimeUpper || ((quality as TempQuality).GetTempLowerTime(minStoreTemp) > deathTimeLower));
        }

        public override string GetReport()
        {
            string report = "Fish is bad";
            if (!isValid())
            {
                foreach (DateTime key in (quality as TempQuality).temperature.Keys)
                {
                    if ((quality as TempQuality).temperature[key] > maxStoreTemp || (quality as TempQuality).temperature[key] < minStoreTemp)
                    {
                        report += "\n" + $"{key}\t{(quality as TempQuality).temperature[key]}";
                    }
                }
            }
            else
            {
                report = "All OK";
            }

            return report;
        }

        public void SaveReport()
        {

            File.WriteAllText($"{Directory.GetCurrentDirectory()}\\test.txt", GetReport(), Encoding.Default);
        }
    }
}
