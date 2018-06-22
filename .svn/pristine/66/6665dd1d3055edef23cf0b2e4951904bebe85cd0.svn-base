using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3DPrinter.utils;

namespace _3DPrinter.setting
{

    public class RecentFilesSetting : NotifyPropertyChangedBase
    {

        public RecentFilesSetting()
        {
            Count = 5;
        }


        private ObservableCollection<string> _projectFiles = new ObservableCollection<string>();
        public ObservableCollection<string> ProjectFiles
        {
            get { return _projectFiles; }
            set
            {
                _projectFiles = value;
                OnPropertyChanged("ProjectFiles");
            }
        }


        public int Count { get; set; }


        public void Add(string file)
        {
            int p = ProjectFiles.IndexOf(file);
            if (p == -1)
            {
                ProjectFiles.Insert(0, file);
            }
            else
            {
                string s = ProjectFiles[p];
                ProjectFiles.RemoveAt(p);

                ProjectFiles.Insert(0, file);
            }

            if (ProjectFiles.Count > Count)
            {
                ProjectFiles.Remove(ProjectFiles[Count]);
            }
            OnPropertyChanged("ProjectFiles");
        }

    }



}
