using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;
using System.Xml;
using _3DPrinter.model;
using _3DPrinter.projectManager;
using _3DPrinter.setting.model;
using _3DPrinter.utils;
using MessageBox = Xceed.Wpf.Toolkit.MessageBox;

namespace _3DPrinter.setting
{

    public class SettingsProvider: NotifyPropertyChangedBase
    {

        private SettingsProvider()
        {
            Custom.Initialize();
            loadConfigs("configData.xml");
        }

        private void loadConfigs(string path)
        {
            if (File.Exists(path))
            {
                readFromXML(path);
            }
            else
            {
                setDefaultConfig();
            }
        }

        public void setDefaultConfig()
        {

        }

        public SettingsModel GetSettingsModel()
        {
            return _settingsModel;
        }

        public void ChangeSettings(SettingsModel newSettings)
        {
            _settingsModel.PropertyChanged -= SettingsModelOnPropertyChanged;
            _settingsModel = newSettings;
            if (newSettings == null)
                readFromXML("configData.xml");
            else
                _settingsModel.PropertyChanged += SettingsModelOnPropertyChanged;
        }

        public void readFromXML(string path)
        {
            StreamReader reader = new StreamReader(path);
            try
            {
                XmlSerializer x = new XmlSerializer(typeof (SettingsModel));
                _settingsModel = (SettingsModel) x.Deserialize(reader);
                _settingsModel.PropertyChanged += SettingsModelOnPropertyChanged;
            }
            catch (Exception exp)
            {
                setDefaultConfig();
            }
            reader.Close();
        }

        private void SettingsModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
           // OnPropertyChanged("");
        }

        public void saveToXML(string path)
        {
            XmlSerializer x = new XmlSerializer(typeof(SettingsModel));
            StreamWriter writer = new StreamWriter(path);
            x.Serialize(writer, _settingsModel);
            writer.Close();
            
        }


        public void saveConfigurationToINI(string path)
        {
            StringBuilder content = new StringBuilder();

            foreach (var prop in _settingsModel.SelectedSlicerSettings.GetType().GetProperties())
            {
                // miss these props
                if (prop.Name == "infillDensity" || prop.Name == "shellThickness" || prop.Name == "topBottomThickness") continue;

                
                
                if (!excludePropertyList.Contains(prop.Name))
                {
                    if (prop.PropertyType.Name == "String")
                    {
                        content.AppendLine(prop.Name + @"=""""""");
                        if (patternPropertyList.Contains(prop.Name))
                        {
                          content.AppendLine(getTextByPattern(prop.GetValue(_settingsModel.SelectedSlicerSettings, null).ToString()));
                        }
                        else
                        {
                            content.AppendLine(prop.GetValue(_settingsModel.SelectedSlicerSettings, null).ToString());
                        }
                        content.AppendLine(@"""""""");
                    }
                    else
                    {
//                        if (prop.Name.Equals("extruderOffset"))
//                        {
//                            content.AppendLine(SerializePoints((System.Drawing.Point[])prop.GetValue(_settingsModel.SelectedSlicerSettings, null), prop.Name));
//                        }
//                        else
//                        {
                            content.AppendLine(prop.Name + "=" + prop.GetValue(_settingsModel.SelectedSlicerSettings, null));
//                        }
                    }
                }
            }


            for (int i =0; i< Printer_Settings.Extruders.Count; i++)
            {
                content.AppendLine("extruderOffset[" + i + "].X=" + (Printer_Settings.Extruders[i].OffsetX * 1000));
                content.AppendLine("extruderOffset[" + i + "].Y=" + (Printer_Settings.Extruders[i].OffsetY * 1000));
            }

            System.IO.File.WriteAllText(path, content.ToString());
        }

        private string SerializePoints(System.Drawing.Point[] points, string name)
        {
            string result = "";

            for (int i = 0; i < points.Length; i++)
            {
                result += name + "[" + i + "].X = " + points[i].X + "\r\n";
                result += name + "[" + i + "].Y = " + points[i].Y + "\r\n";
            }
            return result;
        }

        Dictionary<string, string> literals = new Dictionary<string, string>()
        {
            {"{TRAVEL_SPEED}","Travel_speed"},
            {"{Z_TRAVEL_SPEED}","Z_travel_speed"},
            {"{BED}","BedTemperature"},
            {"{TEMP0}","PrintTemperature"},
            {"{TEMP1}",""},
            {"{IF_BED}",""},
            {"{IF_EXT0}",""}
        };

        private string getTextByPattern(string pattern)
        {
            string text = pattern;
            text.Replace("\n", Environment.NewLine);
            foreach (KeyValuePair<string, string> literal in literals)
            {
                string value = "";

                if (literal.Value == "BedTemperature")
                {
                    MaterialSettingsModel matterial = _settingsModel.SelectedMaterialSetting;
                    value = matterial.BedTemperature.ToString();
                }
                else if (literal.Value == "PrintTemperature")
                {
                    MaterialSettingsModel matterial = _settingsModel.SelectedMaterialSetting;
                    value = matterial.PrintTemperature.ToString();
                }
                else if (literal.Value.Length > 0)
                {
                    value =
                        this._settingsModel.SelectedSlicerSettings.GetType()
                            .GetProperty(literal.Value)
                            .GetValue(this._settingsModel.SelectedSlicerSettings)
                            .ToString();
                }

                text = text.Replace(literal.Key, value);

            }
            return text;
        }


        private List<string> excludePropertyList = new List<string>()
        {
            "Name",
            "Comb_Every_TypeA",
            "Comb_Every_TypeB",
            "KeepOpenFaces",
            "ExtensiveStitching",
            "PrintTemperature",
            "BedTemperature",
            "Z_travel_speed",
            "Travel_speed"

        };

        private List<string> patternPropertyList = new List<string>()
        {
            "startCode",
            "endCode",
            "preSwitchExtruderCode",
            "postSwitchExtruderCode"
        };


        public bool IsExistsMaterialSettings(string materialName)
        {
            return _settingsModel.MaterialSettingsCollection.FirstOrDefault(x => x.Name == materialName) != null;
        }

        public bool IsExistsSlicerSettings(string slicerName)
        {
            return _settingsModel.SlicerSettingsCollection.FirstOrDefault(x => x.Name == slicerName) != null;
        }

        public bool IsExistsPrinterSettings(string printerName)
        {
            return _settingsModel.PrinterSettingsCollection.FirstOrDefault(x => x.Name == printerName) != null;
        }

        public void selectPrinterSettings(string printerName)
        {
            _settingsModel.PrinterSettings = _settingsModel.PrinterSettingsCollection.FirstOrDefault(x => x.Name == printerName);
            OnPropertyChanged("SelectedPrinterSettings");
            OnPropertyChanged("SelectedPrinterSettingsIndex");
        }

        public void selectMaterialSettings(string materialName,ComboBox combo)
        {
            combo.SelectedItem = _settingsModel.MaterialSettingsCollection.FirstOrDefault(x => x.Name == materialName);
        }

        public void selectSlicerSettings(string slicerName)
        {
            _settingsModel.SelectedSlicerSettings = _settingsModel.SlicerSettingsCollection.FirstOrDefault(x => x.Name == slicerName);
            OnPropertyChanged("SelectedSlicerSettings");
            OnPropertyChanged("SelectedSlicerSettingsIndex");
        }



        public void addNewPrinterSettings(string printerName)
        {
            _settingsModel.PrinterSettingsCollection.Add(new PrinterSettingsModel() { Name = printerName});
            OnPropertyChanged("PrinterSettingsCollection");
            OnPropertyChanged("PrinterNameList");
        }


        public void addNewSlicerSettings(string slicerName)
        {
            _settingsModel.SlicerSettingsCollection.Add(new SlicerSettingsModel() { Name = slicerName });
            OnPropertyChanged("SlicerSettingsCollection");
        }
        public void addNewMaterialSettings(string materialName)
        {
            _settingsModel.MaterialSettingsCollection.Add(new MaterialSettingsModel() { Name = materialName });
            OnPropertyChanged("MaterialSettingsCollection");
            OnPropertyChanged("MaterialList");
        }


        public void DeleteSelectedPrinterSettingQuestion()
        {
            if (MessageBox.Show(Localization.Instance.CurrentLanguage.RemovePrinterConfigQuestion, Localization.Instance.CurrentLanguage.DeleteTitle,
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                DeleteSelectedPrinterSetting();
            }
        }

        public void DeleteSelectedSlicerSettingQuestion(SlicerSettingsModel  current)
        {
            if (MessageBox.Show(Localization.Instance.CurrentLanguage.RemoveSlicerConfigQuestion, Localization.Instance.CurrentLanguage.DeleteTitle,
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                DeleteSelectedSlicerSetting(current);
            }
        }

        public bool validatePrinterSettingsCount()
        {
            if (_settingsModel.PrinterSettingsCollection.Count > 1)
            {
                return true;
            }
            else
            {
                MessageBox.Show(Localization.Instance.CurrentLanguage.ForbiddenMessage, Localization.Instance.CurrentLanguage.WarningTitle2,
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
        }


        public bool validateMaterialSettingsCount()
        {
            if (_settingsModel.MaterialSettingsCollection.Count > 1)
            {
                return true;
            }
            else
            {
                MessageBox.Show(Localization.Instance.CurrentLanguage.ForbiddenMessage, Localization.Instance.CurrentLanguage.WarningTitle2,
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
        }

        public bool validateSlicerSettingsCount()
        {
            if (_settingsModel.SlicerSettingsCollection.Count > 1)
            {
                return true;
            }
            else
            {
                MessageBox.Show(Localization.Instance.CurrentLanguage.ForbiddenMessage, Localization.Instance.CurrentLanguage.WarningTitle2,
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
        }

        public void DeleteSelectedPrinterSetting()
        {
            if (validatePrinterSettingsCount())
            {
                _settingsModel.PrinterSettingsCollection.Remove(_settingsModel.PrinterSettings);
                OnPropertyChanged("PrinterSettingsCollection");
                OnPropertyChanged("PrinterNameList");

                OnPropertyChanged("SelectedPrinterSettings");
                OnPropertyChanged("SelectedPrinterSettingsIndex");
            }
        }

        public void DeleteSelectedSlicerSetting(SlicerSettingsModel current)
        {
            if (validateSlicerSettingsCount())
            {
                _settingsModel.SlicerSettingsCollection.Remove(current);
                OnPropertyChanged("SlicerSettingsCollection");
            }
        }


        public void DeleteSelectedMaterialSetting(MaterialSettingsModel current)
        {
            if (validateMaterialSettingsCount())
            {
                _settingsModel.MaterialSettingsCollection.Remove(current);
                SetDefaultSelectedIndex();
                OnPropertyChanged("MaterialSettingsCollection");
                OnPropertyChanged("MaterialList");
            }
        }

        private void SetDefaultSelectedIndex()
        {
            if (_settingsModel.SelectedMaterialIndex >= _settingsModel.MaterialSettingsCollection.Count)
            {
                _settingsModel.SelectedMaterialIndex = 0;
                OnPropertyChanged("SelectedMaterialSettingsIndex");
            }
        }

        public bool InputPrinterSettings(string printerName)
        {
            if (!IsExistsPrinterSettings(printerName))
            {
                if (MessageBox.Show(Localization.Instance.CurrentLanguage.CreatePrinterConfig, Localization.Instance.CurrentLanguage.UnknownProfileName,
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    addNewPrinterSettings(printerName);
                    selectPrinterSettings(printerName);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                selectPrinterSettings(printerName);
                return true;
            }
        }

        public bool InputMaterialSettings(string materialName,ComboBox combo)
        {
            if (!IsExistsMaterialSettings(materialName))
            {
                if (MessageBox.Show(Localization.Instance.CurrentLanguage.CreateMaterialConfig, Localization.Instance.CurrentLanguage.UnknownProfileName,
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    addNewMaterialSettings(materialName);
                    selectMaterialSettings(materialName,combo);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                selectMaterialSettings(materialName, combo);
                return true;
            }
        }

        public bool InputSlicerSettings(string slicerName)
        {
            if (!IsExistsSlicerSettings(slicerName))
            {
                if (MessageBox.Show(Localization.Instance.CurrentLanguage.CreateSlicerConfig, Localization.Instance.CurrentLanguage.UnknownProfileName,
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    addNewSlicerSettings(slicerName);
                    selectSlicerSettings(slicerName);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                selectSlicerSettings(slicerName);
                return true;
            }
        }

        private static SettingsProvider _instance;

        public static SettingsProvider Instance
        {
            get
            {
                if (_instance == null)
                {
                _instance = new SettingsProvider();
                }
                return _instance;
            }
        }

        private SettingsModel _settingsModel;

        public GlobalSettingsModel Global_Settings
        {
            get { return _settingsModel.GlobalSettings; }
        }

        public ObservableCollection<PrinterSettingsModel> PrinterSettingsCollection
        {
            get
            {
                return _settingsModel.PrinterSettingsCollection;
            }
            set
            {
                _settingsModel.PrinterSettingsCollection = value;
                OnPropertyChanged("PrinterSettingsCollection");
            }
        }




        private PrinterSettingsModel _selectedPrinterSettings;
        public PrinterSettingsModel SelectedPrinterSettings
        {
                        get { return _settingsModel.PrinterSettings; }
/*
            get { return _selectedPrinterSettings; }
            set
            {
                _selectedPrinterSettings = value;
                OnPropertyChanged("SelectedPrinterSettings");
            }
*/
        }

        public int SelectedPrinterSettingsIndex
        {
            get { return _settingsModel.SelectedPrinterSettingsIndex; }
            set
            {
                if (value != -1)
                {
                    _settingsModel.SelectedPrinterSettingsIndex = value;
                    OnPropertyChanged("SelectedPrinterSettingsIndex");
                    OnPropertyChanged("SelectedPrinterSettings");
                }
            }
        }

        public List<string> PrinterNameList
        {
            get { return new List<string>(_settingsModel.PrinterSettingsCollection.Select(x => x.Name)); }
        }

        public List<string> SlicerNameList
        {
            get { return new List<string>(_settingsModel.SlicerSettingsCollection.Select(x => x.Name)); }
        }



        
        private List<string> _infillDensityTypeList = new List<string>() { "0", "10%", "20%", "30%", "40%", "50%", "60%", "70%", "80%", "90%", "100%" };
        public List<string> InfillDensityTypeList
        {
            get { return _infillDensityTypeList; }
        }


        public int SelectedInfillDensityTypeIndex
        {
            get { return _settingsModel.SelectedSlicerSettings.infillDensity; }
            set
            {
                _settingsModel.SelectedSlicerSettings.infillDensity = value;
                OnPropertyChanged("SelectedInfillDensityType");
            }
        }
        

        private List<string> _supportTypeList = new List<string>() {"Нет","Сплошная опора","Точечная опора"};
        public List<string> SupportTypeList
        {
            get { return _supportTypeList; }
        }


        public int SelectedSupportTypeIndex
        {
            get
            {
                if (_settingsModel.SelectedSlicerSettings.supportAngle == -1)
                {
                    return 0;
                }
                else
                {
                    if (_settingsModel.SelectedSlicerSettings.supportEverywhere == 0)
                    {
                        return 1;
                    }
                    else
                    {
                        return 2;
                    }
                }
            }
            set
            {
                switch (value)
                {
                    case 0:
                        _settingsModel.SelectedSlicerSettings.supportAngle = -1;
                        _settingsModel.SelectedSlicerSettings.supportEverywhere = 0;
                    break;
                    case 1:
                    _settingsModel.SelectedSlicerSettings.supportAngle = 60;
                    _settingsModel.SelectedSlicerSettings.supportEverywhere = 0;
                    break;
                    case 2:
                    _settingsModel.SelectedSlicerSettings.supportAngle = 60;
                    _settingsModel.SelectedSlicerSettings.supportEverywhere = 1;
                    break;
                }
                OnPropertyChanged("SelectedSupportType");
            }
        }


        public PrinterSettingsModel Printer_Settings
        {
            get { return _settingsModel.PrinterSettings; }
        }

        public StatisticModel PrintingStatistic
        {
            get { return _settingsModel.PrintStatistic; }
        }

        public ThreeDSettingsModel ThreeDSettings
        {
                    get { return _settingsModel.ThreeDSettings; }
        }



        public ObservableCollection<SlicerSettingsModel> SlicerSettingsCollection
        {
            get
            {
                return _settingsModel.SlicerSettingsCollection;
            }
            set
            {
                _settingsModel.SlicerSettingsCollection = value;
                OnPropertyChanged("SlicerSettingsCollection");
            }
        }


        public ObservableCollection<MaterialSettingsModel> MaterialSettingsCollection
        {
            get
            {
                return _settingsModel.MaterialSettingsCollection;
            }
            set
            {
                _settingsModel.MaterialSettingsCollection = value;
                OnPropertyChanged("MaterialSettingsCollection");
                OnPropertyChanged("MaterialList");
            }
        }

        public List<string> MaterialList
        {
            get { return _settingsModel.MaterialSettingsCollection.Select(x => x.Name).ToList(); }
        }

        public MaterialSettingsModel SelectedMaterialSettingsModel {
            get { return _settingsModel.SelectedMaterialSetting; }
        }

        public SlicerSettingsModel SelectedSlicerSettings
        {
            get { return _settingsModel.SelectedSlicerSettings; }
        }

        public CommonSettingsModel CommonSettings
        {
            get { return _settingsModel.CommonSettings; }
        }

        public int SelectedSlicerSettingsIndex
        {
            get { return _settingsModel.SelectedSlicerSettingsIndex; }
            set
            {
                if (value != -1)
                {
                    _settingsModel.SelectedSlicerSettingsIndex = value;
                    OnPropertyChanged("SelectedSlicerSettingsIndex");
                    OnPropertyChanged("SelectedSlicerSettings");
                }
            }
        }

        public int SelectedMaterialSettingsIndex
        {
            get { return _settingsModel.SelectedMaterialIndex; }
            set
            {
                if (value != -1)
                {
                    _settingsModel.SelectedMaterialIndex = value;
                    OnPropertyChanged("SelectedMaterialSettingsIndex");
                    OnPropertyChanged("SelectedMaterialSettingsModel");
                }
            }
        }

        public void Add(string file)
        {
            RecentFiles.Add(file);
            OnPropertyChanged("RecentFiles");
        }

        public RecentFilesSetting RecentFiles
        {
            get { return _settingsModel.RecentFiles; }
        }


    }
}
