using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using _3DPrinter.utils;

namespace _3DPrinter.setting
{
    public class MenuData : NotifyPropertyChangedBase
    {

        private static MenuData _inctance = null;

        public static MenuData Instance
        {
            get
            {
                if (_inctance == null) 
                    _inctance = new MenuData();
                return _inctance;
            }
        }

        private MenuData()
        {
            Load();
        }

        public void Load()
        {
            Data = new MyData();
        }

        private MyData _data;

        public MyData Data
        {
            get { return _data; }
            set
            {
                _data = value;
                OnPropertyChanged("Data");
            }
        }


    }
}
