using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;
using _3DPrinter.projectManager;
using _3DPrinter.setting.model;
using _3DPrinter.utils;

namespace _3DPrinter.setting
{
    public class Localization : NotifyPropertyChangedBase
    {

        private Localization()
        {
            LoadFiles();
        }

        private static Localization _instance = null;
        public static Localization Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Localization();
                }
                return _instance;
            }
        }

        private ObservableCollection<LingualModel> _languages = new ObservableCollection<LingualModel>();
        public ObservableCollection<LingualModel> Languages
        {
            get { return _languages; }
            set
            {
                _languages = value;
                OnPropertyChanged("Languages");
            }
        }


        private LingualModel _currentLanguage;
        public LingualModel CurrentLanguage
        {
            get { return _currentLanguage; }
            set
            {
                _currentLanguage = value;

                OnPropertyChanged("CurrentLanguage");
            }
        }

        private void LoadFiles()
        {
            _languages = new ObservableCollection<LingualModel>();

            List<string> files = Directory.GetFiles("languages", "resources.*.xml").ToList();

            foreach (string file in files)
            {
                try
                {
                    XmlSerializer x = new XmlSerializer(typeof (LingualModel));
                    StreamReader reader = new StreamReader(file);
                    LingualModel model = (LingualModel) x.Deserialize(reader);

                    Languages.Add(model);

                    if (model.Name == ProjectManager.Instance.CurrentProject.projectSettings.GlobalSettings.Language)
                    {
                        CurrentLanguage = model;
                    }
                }
                catch (Exception exp)
                {

                }
            }

            // set by default
            if (CurrentLanguage == null) CurrentLanguage = Languages[0];

        }

    }
}
