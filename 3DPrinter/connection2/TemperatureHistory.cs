/*
 Copyright 2011 repetier repetierdev@gmail.com
 
 Licensed under the Apache License, Version 2.0 (the "License");
 you may not use this file except in compliance with the License.
 You may obtain a copy of the License at
 
 http://www.apache.org/licenses/LICENSE-2.0
 
 Unless required by applicable law or agreed to in writing, software
 distributed under the License is distributed on an "AS IS" BASIS,
 WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 See the License for the specific language governing permissions and
 limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
// PTITSYN using RepetierHost.view.utils;

namespace RepetierHostTester.model
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
    public class TemperatureList
    {
        public double minTime;
        public double maxTime;
        public LinkedList<TemperatureEntry> entries;
        public TemperatureList()
        {
            entries = new LinkedList<TemperatureEntry>();
        }
    }

    
        
}
