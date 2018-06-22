using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3DPrinter.utils;

namespace _3DPrinter.setting.model
{
    public class MaterialSettingsModel : NotifyPropertyChangedBase
    {

        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }


        private int _filamentDiameter = 1750;

        public int filamentDiameter
        {
            get { return _filamentDiameter; }
            set
            {
                _filamentDiameter = value;
                OnPropertyChanged("filamentDiameter");
            }
        }

        private int _filamentFlow = 100;

        public int filamentFlow
        {
            get { return _filamentFlow; }
            set
            {
                _filamentFlow = value;
                OnPropertyChanged("filamentFlow");
            }
        }


        private int _printTemper = -99;

        public int PrintTemperature
        {
            get { return _printTemper; }
            set
            {
                _printTemper = value;
                OnPropertyChanged("PrintTemperature");
            }
        }


        private int _bedTemper = -99;

        public int BedTemperature
        {
            get { return _bedTemper; }
            set
            {
                _bedTemper = value;
                OnPropertyChanged("BedTemperature");
            }
        }


        private int _fanSpeedMin = 50;

        public int fanSpeedMin
        {
            get { return _fanSpeedMin; }
            set
            {
                _fanSpeedMin = value;
                OnPropertyChanged("fanSpeedMin");
            }
        }



        private int _fanSpeedMax = 100;

        public int fanSpeedMax
        {
            get { return _fanSpeedMax; }
            set
            {
                _fanSpeedMax = value;
                OnPropertyChanged("fanSpeedMax");
            }
        }


        private int _minimalLayerTime = 5;

        public int minimalLayerTime
        {
            get { return _minimalLayerTime; }
            set
            {
                _minimalLayerTime = value;
                OnPropertyChanged("minimalLayerTime");
            }
        }



    }
}
