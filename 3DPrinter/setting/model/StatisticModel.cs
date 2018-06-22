using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics;
using _3DPrinter.model;
using _3DPrinter.model.geom;
using _3DPrinter.utils;

namespace _3DPrinter.setting.model
{
    public class StatisticModel : NotifyPropertyChangedBase
    {


        private double _printingTime;
        public double PrintingTime
        {
            get { return _printingTime; }
            set
            {
                _printingTime = value;
                OnPropertyChanged("PrintingTime");
                OnPropertyChanged("PrintingTimeFormat");
            }
        }

        public string PrintingTimeFormat
        {
            get
            {
                double pT = _printingTime;
                int sec = (int)(pT * (1 + 0.01 * 8));
                int hours = sec / 3600;
                sec -= 3600 * hours;
                int min = sec / 60;
                sec -= min * 60;
                StringBuilder s = new StringBuilder();
                if (hours > 0)
                    s.Append(hours + " ч ");
                if (min > 0 || hours > 0)
                    s.Append(min + " мин ");
                s.Append(sec + " с");

                return s.ToString();
            }
        }


        private int _layersCount;
        public int LayersCount
        {
            get { return _layersCount; }
            set
            {
                _layersCount = value;
                OnPropertyChanged("LayersCount");
            }
        }

        private int _filamentLength;
        public int FilamentLength
        {
            get { return _filamentLength; }
            set
            {
                _filamentLength = value;
                OnPropertyChanged("FilamentLength");
            }
        }


        private int _lineCodeCount;
        public int LineCodeCount
        {
            get { return _lineCodeCount; }
            set
            {
                _lineCodeCount = value;
                OnPropertyChanged("LineCodeCount");
            }
        }

        private string _Volume;

        public string Volume
        {
            get { return _Volume; }
            set
            {
                _Volume = value;
                OnPropertyChanged("Volume");
            }
        }

        private string _Surface;

        public string Surface
        {
            get { return _Surface; }
            set
            {
                _Surface = value;
                OnPropertyChanged("Surface");
            }
        }

        private string _Edges;

        public string Edges
        {
            get { return _Edges; }
            set
            {
                _Edges = value;
                OnPropertyChanged("Edges");
            }
        }

        private string _Points;

        public string Points
        {
            get { return _Points; }
            set
            {
                _Points = value;
                OnPropertyChanged("Points");
            }
        }

        private string _Faces;

        public string Faces
        {
            get { return _Faces; }
            set
            {
                _Faces = value;
                OnPropertyChanged("Faces");
            }
        }

        private string _SizeX;

        public string SizeX
        {
            get { return _SizeX; }
            set
            {
                _SizeX = value;
                OnPropertyChanged("SizeX");
            }
        }

        private string _SizeY;

        public string SizeY
        {
            get { return _SizeY; }
            set
            {
                _SizeY = value;
                OnPropertyChanged("SizeY");
            }
        }

        private string _SizeZ;

        public string SizeZ
        {
            get { return _SizeZ; }
            set
            {
                _SizeZ = value;
                OnPropertyChanged("SizeZ");
            }
        }

        public void UpadateModelInfo(TopoModel m,PrintModel pm)
        {
            if (pm != null)
            {
                Volume = (0.001*m.Volume()).ToString("0.00") + " cm³";
                Surface = (0.01*m.Surface()).ToString("0.00") + " cm²";
//            infoShells.Text = pm.Model.shells.ToString();
                Points = pm.Model.vertices.Count.ToString();
                Edges = pm.Model.edges.Count.ToString();
                Faces = pm.Model.triangles.Count.ToString();

                SizeX = m.boundingBox.Size.x.ToString("0.00") + " mm";
                SizeY = m.boundingBox.Size.y.ToString("0.00") + " mm";
                SizeZ = m.boundingBox.Size.z.ToString("0.00") + " mm";
                
/*
                            infoMinX.Text = m.boundingBox.minPoint.x.ToString("0.00") + " mm";
                            infoMaxX.Text = m.boundingBox.maxPoint.x.ToString("0.00") + " mm";
                            infoMinY.Text = m.boundingBox.minPoint.y.ToString("0.00") + " mm";
                            infoMaxY.Text = m.boundingBox.maxPoint.y.ToString("0.00") + " mm";
                            infoMinZ.Text = m.boundingBox.minPoint.z.ToString("0.00") + " mm";
                            infoMaxZ.Text = m.boundingBox.maxPoint.z.ToString("0.00") + " mm";
                            groupBox1.Text = pm.name;
*/
            }
            else
            {
                Volume = "";
                Surface = "";
                //            infoShells.Text = pm.Model.shells.ToString();
                Points = "";
                Edges = "";
                Faces = "";
                SizeX = "";
                SizeY = "";
                SizeZ = "";
            }

        }

    }
}
