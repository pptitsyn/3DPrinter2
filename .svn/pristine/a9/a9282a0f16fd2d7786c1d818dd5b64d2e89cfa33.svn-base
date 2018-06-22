using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DPrinter.connection
{
    public class TemperatureEntry
    {
        public long time;
        // values <-1000 are not present 
        public float output;
        public float extruder;
        public float bed;
        public float avgExtruder;
        public float avgBed;
        public float avgOutput;
        public float targetBed;
        public float targetExtruder;

        public TemperatureEntry(float ext, float _bed, float tar, float tare)
        {
            time = DateTime.UtcNow.Ticks;
            output = avgExtruder = avgBed = avgOutput = -10000;
            bed = _bed;
            extruder = ext;
            targetBed = tar;
            targetExtruder = tare;
        }
        public TemperatureEntry(int mon, float tmp, float outp, float tar, float tare)
        {
            time = DateTime.UtcNow.Ticks;
            output = outp;
            avgExtruder = bed = extruder = avgBed = avgOutput = -10000;
            targetBed = tar;
            targetExtruder = tare;
            switch (mon)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    extruder = tmp;
                    break;
                case 100:
                    bed = tmp;
                    break;
            }
        }
    }
}
