using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public TimeSpan deathTime;

        public FrozenFish(Quality q, double t, TimeSpan d) : base(q)
        {
            maxStoreTemp = t;
            deathTime = d;
        }

        public override bool isValid()
        {
            return !((quality as TempQuality).GetTempUpperTime(maxStoreTemp) > deathTime);
        }

        public override string GetReport()
        {
            string report = "Fish is bad";
            if (!isValid())
            {
                foreach (DateTime key in (quality as TempQuality).temperature.Keys)
                {
                    if ((quality as TempQuality).temperature[key] > maxStoreTemp)
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
    }
}
