using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Win32;
using _3DPrinter.model;
using _3DPrinter.utils;

namespace _3DPrinter.setting.model
{
    public class GlobalSettingsModel : NotifyPropertyChangedBase
    {

        public GlobalSettingsModel()
        {
        }


        [XmlIgnore]
        private string _workDir;

        [XmlElement("Workdir")]
        public string Workdir
        {
            get { return _workDir; }
            set
            {
                if (File.Exists(value))
                {
                    _workDir = value;
                }
                else
                {
                    _workDir = System.AppDomain.CurrentDomain.BaseDirectory.Remove(System.AppDomain.CurrentDomain.BaseDirectory.Count()-1);

                }
                OnPropertyChanged("Workdir");
                
            }
        }


        [XmlIgnore]
        private string _language;

        [XmlElement("Language")]
        public string Language
        {
            get { return _language; }
            set
            {
                _language = value;
                OnPropertyChanged("Language");
            }
        }



        [XmlIgnore]
        private bool _logEnable;

        [XmlElement("LogEnabled")]
        public Boolean LogEnabled
        {
            get { return _logEnable; }
            set
            {
                _logEnable = value;
                OnPropertyChanged("LogEnabled");
                
            }
        }

        [XmlIgnore]
        private bool _redGreenSwitch;

        [XmlElement("RedGreenSwitch")]
        public Boolean RedGreenSwitch
        {
            get { return _redGreenSwitch; }
            set
            {
                _redGreenSwitch = value;
                OnPropertyChanged("RedGreenSwitch");
                
            }
        }


        [XmlIgnore]
        private bool _disQualityReduction;

        [XmlElement("DisableQualityReduction")]
        public Boolean DisableQualityReduction
        {
            get { return _disQualityReduction; }
            set
            {
                _disQualityReduction = value;
                OnPropertyChanged("DisableQualityReduction");
                
            }
        }


        [XmlIgnore]
        private bool _reduceToolBar;

        [XmlElement("ReduceToolbarSize")]
        public Boolean ReduceToolbarSize
        {
            get { return _reduceToolBar; }
            set
            {
                _reduceToolBar = value;
                OnPropertyChanged("ReduceToolbarSize");
                
            }
        }

        public bool ValidateWorkDir(string path)
        {
            if (path.Length == 0 || !Directory.Exists(path))
            {
                return false;
            }
            return true;
        }

        [XmlIgnore]
        private bool _stlFile;

        [XmlElement("StlFile")]
        public Boolean StlFile
        {
            get { return _stlFile; }
            set
            {
                _stlFile = value;
                OnPropertyChanged("StlFile");
                
            }
        }

        [XmlIgnore]
        private bool _objFile;
        [XmlElement("ObjFile")]
        public Boolean ObjFile
        {
            get { return _objFile; }
            set
            {
                _objFile = value;
                OnPropertyChanged("ObjFile");
                
            }
        }

        [XmlIgnore]
        private bool _gFile;
        [XmlElement("GFile")]
        public Boolean GFile
        {
            get { return _gFile; }
            set
            {
                _gFile = value;
                OnPropertyChanged("GFile");
                
            }

        }

        [XmlIgnore]
        private bool _gcoFile;
        [XmlElement("GcoFile")]
        public Boolean GcoFile
        {
            get { return _gcoFile; }
            set
            {
                _gcoFile = value;
                OnPropertyChanged("GcoFile");
                
            }
        }

        [XmlIgnore]
        private bool _gcodeFile;
        [XmlElement("GcodeFile")]
        public Boolean GcodeFile
        {
            get { return _gcodeFile; }
            set
            {
                _gcodeFile = value;
                OnPropertyChanged("GcodeFile");
                
            }
        }

        [XmlIgnore]
        private bool _ncFile;
        [XmlElement("NcFile")]
        public Boolean NcFile
        {
            get { return _ncFile; }
            set
            {
                _ncFile = value;
                OnPropertyChanged("NcFile");
               
            }
        }
        

    }
}
