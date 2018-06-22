using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3DPrinter.model;
using _3DPrinter.utils;

namespace _3DPrinter.projectManager
{
    public class ProjectManager : NotifyPropertyChangedBase
    {

        private ProjectManager()
        {
            CurrentProject = new PrintCamProject();
        }

        private static ProjectManager _projectManager;
        public static ProjectManager Instance
        {
            get
            {
                if (_projectManager == null)
                {
                    _projectManager = new ProjectManager();
                }
                return _projectManager;
            }
        }


        private PrintCamProject _currentProject;

        public PrintCamProject CurrentProject
        {
            get { return _currentProject; }
            set
            {
                _currentProject = value;
                OnPropertyChanged("CurrentProject");
            }
        }

        private ObservableCollection<ThreeDModel> _models = new ObservableCollection<ThreeDModel>();
        public ObservableCollection<ThreeDModel> Models
        {
            get { return _models; }
            set
            {
                _models = value;
                OnPropertyChanged("Models");
                OnPropertyChanged("IsNotEmpty");
            }
        }

        private ThreeDModel _selectedModel;
        public ThreeDModel SelectedModel
        {
            get { return _selectedModel; }
            set
            {
                if (_selectedModel != null)
                    _selectedModel.Selected = false;
                _selectedModel = value;
                if (_selectedModel != null)
                    _selectedModel.Selected = true;
                OnPropertyChanged("SelectedModel");
            }
        }


        public bool IsNotEmpty
        {
            get { return Models.Count > 0; }
            set
            {
                OnPropertyChanged("IsNotEmpty");
            }
        }

        public LinkedList<PrintModel> ListObjects()
        {
            LinkedList<PrintModel> list = new LinkedList<PrintModel>();
            foreach (ThreeDModel item in _models)
                list.AddLast((PrintModel)item);
            return list;
        }

    }
}
