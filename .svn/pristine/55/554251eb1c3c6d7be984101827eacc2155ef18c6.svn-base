using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Win32;
using OpenTK;
using _3DPrinter.gcode;
using _3DPrinter.model;
using _3DPrinter.utils;

namespace _3DPrinter.setting.model
{
    public class ThreeDSettingsModel : NotifyPropertyChangedBase
    {

        [XmlIgnore]
        public bool useVBOs = false;
        [XmlIgnore]
        public int drawMethod = 0;
        [XmlIgnore]
        public float openGLVersion = 1.0f;
        [XmlIgnore]
        private bool _showEdges = false;
        [XmlIgnore]
        private bool _showFaces = true;
        [XmlIgnore]
        private bool _showCompass = true;


        [XmlIgnore]
        RegistryKey programKey;
        [XmlIgnore]
        RegistryKey threedKey;


        public ThreeDSettingsModel()
        {
/*
            comboDrawMethod = 0;
            programKey = Custom.BaseKey; //  Registry.CurrentUser.CreateSubKey("SOFTWARE\\Repetier");
            threedKey = programKey.CreateSubKey("3D");
            if (comboFilamentVisualization < 0) comboFilamentVisualization = 1;
            RegistryToForm();
*/

        }



        private void RegistryToForm()
        {
            try
            {
                backgroundTop = Color.FromArgb((int)threedKey.GetValue("backgroundTopColor", backgroundTop.ToArgb()));
                backgroundBottom = Color.FromArgb((int)threedKey.GetValue("backgroundBottomColor", backgroundBottom.ToArgb()));
                faces = Color.FromArgb((int)threedKey.GetValue("facesColor", faces.ToArgb()));
                edges = Color.FromArgb((int)threedKey.GetValue("edgesColor", faces.ToArgb()));
                selectedFaces = Color.FromArgb((int)threedKey.GetValue("selectedFacesColor", selectedFaces.ToArgb()));
                printerBase = Color.FromArgb((int)threedKey.GetValue("printerBaseColor", printerBase.ToArgb()));
                printerFrame = Color.FromArgb((int)threedKey.GetValue("printerFrameColor", printerFrame.ToArgb()));
                filament = Color.FromArgb((int)threedKey.GetValue("filamenColor", filament.ToArgb()));
                filament2 = Color.FromArgb((int)threedKey.GetValue("filament2Color", filament2.ToArgb()));
                filament3 = Color.FromArgb((int)threedKey.GetValue("filament3Color", filament3.ToArgb()));
                hotFilament = Color.FromArgb((int)threedKey.GetValue("hotFilamentColor", hotFilament.ToArgb()));
                travelMoves = Color.FromArgb((int)threedKey.GetValue("travelColor", travelMoves.ToArgb()));
                selectedFilament = Color.FromArgb((int)threedKey.GetValue("selectedFilamentColor", selectedFilament.ToArgb()));
                outsidePrintbed = Color.FromArgb((int)threedKey.GetValue("outsidePrintbedColor", outsidePrintbed.ToArgb()));
                _showEdges = 0 != (int)threedKey.GetValue("showEdges", _showEdges ? 1 : 0);
                _showFaces = 0 != (int)threedKey.GetValue("showFaces", _showFaces ? 1 : 0);
                _showCompass = 0 != (int)threedKey.GetValue("showCompass", _showCompass ? 1 : 0);
                pulseOutside = 0 != (int)threedKey.GetValue("pulseOutside", pulseOutside ? 1 : 0);
                showPrintbed = 0 != (int)threedKey.GetValue("showPrintbed", showPrintbed ? 1 : 0);
                checkDisableFilamentVisualization = 0 != (int)threedKey.GetValue("disableFilamentVisualization", checkDisableFilamentVisualization ? 1 : 0);
                checkDisableTravelMoves = 0 != (int)threedKey.GetValue("disableTravelVisualization", checkDisableTravelMoves ? 1 : 0);
                enableLight1 = 0 != (int)threedKey.GetValue("enableLight1", enableLight1 ? 1 : 0);
                enableLight2 = 0 != (int)threedKey.GetValue("enableLight2", enableLight2 ? 1 : 0);
                enableLight3 = 0 != (int)threedKey.GetValue("enableLight3", enableLight3 ? 1 : 0);
                enableLight4 = 0 != (int)threedKey.GetValue("enableLight4", enableLight4 ? 1 : 0);

                // useVBOs = 0 != (int)threedKey.GetValue("useVBOs", useVBOs ? 1 : 0);
                comboDrawMethod = (int)threedKey.GetValue("drawMethod", 0);
                textLayerHeight = (string)threedKey.GetValue("layerHeight", textLayerHeight);
                textDiameter = (string)threedKey.GetValue("filamentDiameter", textDiameter);
                radioHeight = 0 != (int)threedKey.GetValue("useLayerHeight", radioHeight ? 1 : 0);
                checkCorrectNormals = 0 != (int)threedKey.GetValue("correctNormals", checkCorrectNormals ? 1 : 0);
                radioDiameter = !radioHeight;
                textWidthOverThickness = (string)threedKey.GetValue("widthOverHeight", textWidthOverThickness);
                textHotFilamentLength = (string)threedKey.GetValue("hotFilamentLength", textHotFilamentLength);
                comboFilamentVisualization = (int)threedKey.GetValue("filamentVisualization", comboFilamentVisualization);
                ambient1 = Color.FromArgb((int)threedKey.GetValue("ambient1Color", ambient1.ToArgb()));
                diffuse1 = Color.FromArgb((int)threedKey.GetValue("diffuse1Color", diffuse1.ToArgb()));
                specular1 = Color.FromArgb((int)threedKey.GetValue("specular1Color", specular1.ToArgb()));
                ambient2 = Color.FromArgb((int)threedKey.GetValue("ambient2Color", ambient2.ToArgb()));
                diffuse2 = Color.FromArgb((int)threedKey.GetValue("diffuse2Color", diffuse2.ToArgb()));
                specular2 = Color.FromArgb((int)threedKey.GetValue("specular2Color", specular2.ToArgb()));
                ambient3 = Color.FromArgb((int)threedKey.GetValue("ambient3Color", ambient3.ToArgb()));
                diffuse3 = Color.FromArgb((int)threedKey.GetValue("diffuse3Color", diffuse3.ToArgb()));
                specular3 = Color.FromArgb((int)threedKey.GetValue("specular3Color", specular3.ToArgb()));
                ambient4 = Color.FromArgb((int)threedKey.GetValue("ambient4Color", ambient4.ToArgb()));
                diffuse4 = Color.FromArgb((int)threedKey.GetValue("diffuse4Color", diffuse4.ToArgb()));
                specular4 = Color.FromArgb((int)threedKey.GetValue("specular4Color", specular4.ToArgb()));
                selectionBox = Color.FromArgb((int)threedKey.GetValue("selectionBoxColor", selectionBox.ToArgb()));
                errorModel = Color.FromArgb((int)threedKey.GetValue("errorModelColor", errorModel.ToArgb()));
                insideFaces = Color.FromArgb((int)threedKey.GetValue("insideFacesColor", insideFaces.ToArgb()));
                xdir1 = (string)threedKey.GetValue("light1X", xdir1);
                ydir1 = (string)threedKey.GetValue("light1Y", ydir1);
                zdir1 = (string)threedKey.GetValue("light1Z", zdir1);
                xdir2 = (string)threedKey.GetValue("light2X", xdir2);
                ydir2 = (string)threedKey.GetValue("light2Y", ydir2);
                zdir2 = (string)threedKey.GetValue("light2Z", zdir2);
                xdir3 = (string)threedKey.GetValue("light3X", xdir3);
                ydir3 = (string)threedKey.GetValue("light3Y", ydir3);
                zdir3 = (string)threedKey.GetValue("light3Z", zdir3);
                xdir4 = (string)threedKey.GetValue("light4X", xdir4);
                ydir4 = (string)threedKey.GetValue("light4Y", ydir4);
                zdir4 = (string)threedKey.GetValue("light4Z", zdir4);
                checkAutoenableParallelInTopView = 0 != (int)threedKey.GetValue("autoenableParallelForTopView", checkAutoenableParallelInTopView ? 1 : 0);
                //GCodePath.correctNorms = checkCorrectNormals;
                if (threedKey.GetValue("backgroundColor", null) != null)
                {
                    buttonModelColorsDefaults_Click(null, null);
                    buttonGeneralColorDefaults_Click(null, null);
                    threedKey.DeleteValue("backgroundColor");
                }
            }
            catch { }
        }


        [XmlElement("ShowEdges")]
        public bool ShowEdges
        {
            get { return _showEdges; }
            set
            {
                if (value == _showEdges) return;
                _showEdges = value;
                OnPropertyChanged("ShowEdges");
            }
        }

        [XmlElement("ShowFaces")]
        public bool ShowFaces
        {
            get { return _showFaces; }
            set
            {
                if (value == _showFaces) return;
                _showFaces = value;
                OnPropertyChanged("ShowFaces");
            }
        }

        [XmlElement("ShowCompass")]
        public bool ShowCompass
        {
            get { return _showCompass; }
            set
            {
                if (value == _showCompass) return;
                _showCompass = value;
                OnPropertyChanged("ShowCompass");
            }
        }
        [XmlIgnore]
        public int filamentVisualization
        {
            get { return comboFilamentVisualization; }
        }
        [XmlIgnore]
        public float layerHeight
        {
            get { float h; float.TryParse(textLayerHeight, NumberStyles.Float, GCode.format, out h); if (h < 0.05) h = 0.05f; return h; }
        }
        [XmlIgnore]
        public float filamentDiameter
        {
            get { float h; float.TryParse(textDiameter, NumberStyles.Float, GCode.format, out h); if (h < 0.05) h = 0.05f; return h; }
        }
        [XmlIgnore]
        public float widthOverHeight
        {
            get { float h; float.TryParse(textWidthOverThickness, NumberStyles.Float, GCode.format, out h); if (h < 0.05) h = 0.05f; return h; }
        }
        [XmlIgnore]
        public float hotFilamentLength
        {
            get { float h; float.TryParse(textHotFilamentLength, NumberStyles.Float, GCode.format, out h); if (h < 0) h = 0; return h; }
        }
        [XmlIgnore]
        public bool useLayerHeight
        {
            get { return radioHeight; }
        }

        private float[] toGLColor(Color c)
        {
            float[] a = new float[4];
            a[3] = 1;
            a[0] = (float)c.R / 255.0f;
            a[1] = (float)c.G / 255.0f;
            a[2] = (float)c.B / 255.0f;
            return a;
        }
        private Vector4 toDir(string x, string y, string z)
        {
            float xf, yf, zf;
            float.TryParse(x, NumberStyles.Float, GCode.format, out xf);
            float.TryParse(y, NumberStyles.Float, GCode.format, out yf);
            float.TryParse(z, NumberStyles.Float, GCode.format, out zf);
            return new Vector4(xf, yf, zf, 0);
        }
        public Vector4 Dir1() { return toDir(xdir1, ydir1, zdir1); }
        public Vector4 Dir2() { return toDir(xdir2, ydir2, zdir2); }
        public Vector4 Dir3() { return toDir(xdir3, ydir3, zdir3); }
        public Vector4 Dir4() { return toDir(xdir4, ydir4, zdir4); }
        public float[] Diffuse1() { return toGLColor(diffuse1); }
        public float[] Ambient1() { return toGLColor(ambient1); }
        public float[] Specular1() { return toGLColor(specular1); }
        public float[] Diffuse2() { return toGLColor(diffuse2); }
        public float[] Ambient2() { return toGLColor(ambient2); }
        public float[] Specular2() { return toGLColor(specular2); }
        public float[] Diffuse3() { return toGLColor(diffuse3); }
        public float[] Ambient3() { return toGLColor(ambient3); }
        public float[] Specular3() { return toGLColor(specular3); }
        public float[] Diffuse4() { return toGLColor(diffuse4); }
        public float[] Ambient4() { return toGLColor(ambient4); }
        public float[] Specular4() { return toGLColor(specular4); }

        private void buttonGeneralColorDefaults_Click(object sender, EventArgs e)
        {
            backgroundTop = Color.WhiteSmoke;
            backgroundBottom = Color.CornflowerBlue;
            printerBase = Color.PaleGoldenrod;
            printerFrame = Color.Black;
        }

        private void buttonModelColorsDefaults_Click(object sender, EventArgs e)
        {
            faces = Color.Gold;
            edges = Color.DarkGray;
            selectedFaces = Color.Fuchsia;
            errorModel = Color.Red;
            selectionBox = Color.DodgerBlue;
            errorModelEdge = Color.Cyan;
            outsidePrintbed = Color.Aquamarine;
            cutFaces = Color.RoyalBlue;
            insideFaces = Color.Lime;
        }


        [XmlIgnore]
        private Color _faces = Color.Gold;
        [XmlElement("faces", Type = typeof(XmlColor))]
        public Color faces
        {
            get { return _faces; }
            set
            {
                _faces = value;
                OnPropertyChanged("faces");
            }
        }

        [XmlIgnore]
        private Color _backgroundTop;

        [XmlElement("backgroundTop", Type = typeof(XmlColor))]
        public Color backgroundTop
        {
            get { return _backgroundTop; }
            set
            {
                _backgroundTop = value;
                OnPropertyChanged("backgroundTop");
            }
        }


        [XmlIgnore]
        private Color _selectedFaces;
        [XmlElement("selectedFaces", Type = typeof(XmlColor))]
        public Color selectedFaces
        {
            get { return _selectedFaces; }
            set
            {
                _selectedFaces = value;
                OnPropertyChanged("faces");
            }
        }

        [XmlIgnore]
        private Color _filament;

        [XmlElement("filament", Type = typeof(XmlColor))]
        public Color filament
        {
            get { return _filament; }
            set
            {
                _filament = value;
                OnPropertyChanged("filament");
            }
        }

        [XmlIgnore]
        private Color _printerBase;
        [XmlElement("printerBase", Type = typeof(XmlColor))]
        public Color printerBase
        {
            get { return _printerBase; }
            set
            {
                _printerBase = value;
                OnPropertyChanged("printerBase");
            }
        }


        [XmlIgnore]
        private bool correctNorms;
        [XmlElement("CorrectNorms")]
        public bool CorrectNorms
        {
            get { return correctNorms; }
            set
            {
                correctNorms = value;
                OnPropertyChanged("CorrectNorms");
            }
        }

        [XmlIgnore]
        private bool _showPrintbed;
        [XmlElement("showPrintbed")]
        public bool showPrintbed
        {
            get { return _showPrintbed; }
            set
            {
                _showPrintbed = value;
                OnPropertyChanged("showPrintbed");
            }
        }

        [XmlIgnore]
        private bool _enableLight4;
        [XmlElement("enableLight4")]
        public bool enableLight4
        {
            get { return _enableLight4; }
            set
            {
                _enableLight4 = value;
                OnPropertyChanged("enableLight4");
            }
        }

        [XmlIgnore]
        private bool _enableLight3;
        [XmlElement("enableLight3")]
        public bool enableLight3
        {
            get { return _enableLight3; }
            set
            {
                _enableLight3 = value;
                OnPropertyChanged("enableLight3");
            }
        }

        [XmlIgnore]
        private bool _enableLight2;
        [XmlElement("enableLight2")]
        public bool enableLight2
        {
            get { return _enableLight2; }
            set
            {
                _enableLight2 = value;
                OnPropertyChanged("enableLight2");
            }
        }

        [XmlIgnore]
        private bool _enableLight1;
        [XmlElement("enableLight1")]
        public bool enableLight1
        {
            get { return _enableLight1; }
            set
            {
                _enableLight1 = value;
                OnPropertyChanged("enableLight1");
            }
        }

        [XmlIgnore]
        private Color _edges;
        [XmlElement("edges", Type = typeof(XmlColor))]
        public Color edges
        {
            get { return _edges; }
            set
            {
                _edges = value;
                OnPropertyChanged("edges");
            }
        }

        [XmlIgnore]
        private string _textWidthOverThickness;
        [XmlElement("textWidthOverThickness")]
        public string textWidthOverThickness
        {
            get { return _textWidthOverThickness; }
            set
            {
                _textWidthOverThickness = value;
                OnPropertyChanged("textWidthOverThickness");
            }
        }

        [XmlIgnore]
        private string _textLayerHeight;
        [XmlElement("textLayerHeight")]
        public string textLayerHeight
        {
            get { return _textLayerHeight; }
            set
            {
                _textLayerHeight = value;
                OnPropertyChanged("textLayerHeight");
            }
        }

        [XmlIgnore]
        private int _comboFilamentVisualization;
        [XmlElement("comboFilamentVisualization")]
        public int comboFilamentVisualization
        {
            get { return _comboFilamentVisualization; }
            set
            {
                _comboFilamentVisualization = value;
                OnPropertyChanged("comboFilamentVisualization");
            }
        }

        [XmlIgnore]
        private string _textDiameter;
        [XmlElement("textDiameter")]
        public string textDiameter
        {
            get { return _textDiameter; }
            set
            {
                _textDiameter = value;
                OnPropertyChanged("textDiameter");
            }
        }

        [XmlIgnore]
        private bool _radioDiameter;
        [XmlElement("radioDiameter")]
        public bool radioDiameter
        {
            get { return _radioDiameter; }
            set
            {
                _radioDiameter = value;
                OnPropertyChanged("radioDiameter");
            }
        }

        [XmlIgnore]
        private bool _radioHeight;
        [XmlElement("radioHeight")]
        public bool radioHeight
        {
            get { return _radioHeight; }
            set
            {
                _radioHeight = value;
                OnPropertyChanged("radioHeight");
            }
        }

        [XmlIgnore]
        private string _textHotFilamentLength;

        [XmlElement("textHotFilamentLength")]
        public string textHotFilamentLength
        {
            get { return _textHotFilamentLength; }
            set
            {
                _textHotFilamentLength = value;
                OnPropertyChanged("textHotFilamentLength");
            }
        }

        [XmlIgnore]
        private Color _hotFilament;

        [XmlElement("hotFilament", Type = typeof(XmlColor))]
        public Color hotFilament
        {
            get { return _hotFilament; }
            set
            {
                _hotFilament = value;
                OnPropertyChanged("hotFilament");
            }
        }

        [XmlIgnore]
        private bool _checkDisableFilamentVisualization;

        [XmlElement("checkDisableFilamentVisualization")]
        public bool checkDisableFilamentVisualization
        {
            get { return _checkDisableFilamentVisualization; }
            set
            {
                _checkDisableFilamentVisualization = value;
                OnPropertyChanged("checkDisableFilamentVisualization");
            }
        }

        [XmlIgnore]
        private int _comboDrawMethod;
        [XmlElement("comboDrawMethod")]
        public int comboDrawMethod
        {
            get { return _comboDrawMethod; }
            set
            {
                _comboDrawMethod = value;
                OnPropertyChanged("comboDrawMethod");
            }
        }

        [XmlIgnore]
        private Color _outsidePrintbed;

        [XmlElement("outsidePrintbed", Type = typeof(XmlColor))]
        public Color outsidePrintbed
        {
            get { return _outsidePrintbed; }
            set
            {
                _outsidePrintbed = value;
                OnPropertyChanged("outsidePrintbed");
            }
        }

        [XmlIgnore]
        private Color _selectedFilament;
        [XmlElement("selectedFilament", Type = typeof(XmlColor))]
        public Color selectedFilament
        {
            get { return _selectedFilament; }
            set
            {
                _selectedFilament = value;
                OnPropertyChanged("selectedFilament");
            }
        }

        [XmlIgnore]
        private Color _filament3;
        [XmlElement("filament3", Type = typeof(XmlColor))]
        public Color filament3
        {
            get { return _filament3; }
            set
            {
                _filament3 = value;
                OnPropertyChanged("filament3");
            }
        }

        [XmlIgnore]
        private Color _filament2;
        [XmlElement("filament2", Type = typeof(XmlColor))]
        public Color filament2
        {
            get { return _filament2; }
            set
            {
                _filament2 = value;
                OnPropertyChanged("filament2");
            }
        }

        [XmlIgnore]
        private bool _pulseOutside;

        [XmlElement("pulseOutside")]
        public bool pulseOutside
        {
            get { return _pulseOutside; }
            set
            {
                _pulseOutside = value;
                OnPropertyChanged("pulseOutside");
            }
        }

        [XmlIgnore]
        private Color _ambient1;
        [XmlElement("ambient1", Type = typeof(XmlColor))]
        public Color ambient1
        {
            get { return _ambient1; }
            set
            {
                _ambient1 = value;
                OnPropertyChanged("ambient1");
            }
        }

        [XmlIgnore]
        private Color _ambient2;

        [XmlElement("ambient2", Type = typeof(XmlColor))]
        public Color ambient2
        {
            get { return _ambient2; }
            set
            {
                _ambient2 = value;
                OnPropertyChanged("ambient2");
            }
        }

        [XmlIgnore]
        private Color _ambient3;
        [XmlElement("ambient3", Type = typeof(XmlColor))]
        public Color ambient3
        {
            get { return _ambient3; }
            set
            {
                _ambient3 = value;
                OnPropertyChanged("ambient3");
            }
        }

        [XmlIgnore]
        private Color _ambient4;
        [XmlElement("ambient4", Type = typeof(XmlColor))]
        public Color ambient4
        {
            get { return _ambient4; }
            set
            {
                _ambient4 = value;
                OnPropertyChanged("ambient4");
            }
        }

        [XmlIgnore]
        private Color _diffuse1;
        [XmlElement("diffuse1", Type = typeof(XmlColor))]
        public Color diffuse1
        {
            get { return _diffuse1; }
            set
            {
                _diffuse1 = value;
                OnPropertyChanged("diffuse1");
            }
        }

        [XmlIgnore]
        private Color _specular1;
        [XmlElement("specular1", Type = typeof(XmlColor))]
        public Color specular1
        {
            get { return _specular1; }
            set
            {
                _specular1 = value;
                OnPropertyChanged("specular1");
            }
        }

        [XmlIgnore]
        private Color _diffuse2;
        [XmlElement("diffuse2", Type = typeof(XmlColor))]
        public Color diffuse2
        {
            get { return _diffuse2; }
            set
            {
                _diffuse2 = value;
                OnPropertyChanged("diffuse2");
            }
        }

        [XmlIgnore]
        private Color _specular2;
        [XmlElement("specular2", Type = typeof(XmlColor))]
        public Color specular2
        {
            get { return _specular2; }
            set
            {
                _specular2 = value;
                OnPropertyChanged("specular2");
            }
        }

        [XmlIgnore]
        private Color _diffuse3;
        [XmlElement("diffuse3", Type = typeof(XmlColor))]
        public Color diffuse3
        {
            get { return _diffuse3; }
            set
            {
                _diffuse3 = value;
                OnPropertyChanged("diffuse3");
            }
        }

        [XmlIgnore]
        private Color _specular3;
        [XmlElement("specular3", Type = typeof(XmlColor))]
        public Color specular3
        {
            get { return _specular3; }
            set
            {
                _specular3 = value;
                OnPropertyChanged("specular3");
            }
        }

        [XmlIgnore]
        private Color _specular4;
        [XmlElement("specular4", Type = typeof(XmlColor))]
        public Color specular4
        {
            get { return _specular4; }
            set
            {
                _specular4 = value;
                OnPropertyChanged("specular4");
            }
        }

        [XmlIgnore]
        private Color _diffuse4;
        [XmlElement("diffuse4", Type = typeof(XmlColor))]
        public Color diffuse4
        {
            get { return _diffuse4; }
            set
            {
                _diffuse4 = value;
                OnPropertyChanged("diffuse4");
            }
        }

        [XmlIgnore]
        private string _xdir4;
        [XmlElement("xdir4")]
        public string xdir4
        {
            get { return _xdir4; }
            set
            {
                _xdir4 = value;
                OnPropertyChanged("xdir4");
            }
        }

        [XmlIgnore]
        private string _ydir4;
        [XmlElement("ydir4")]
        public string ydir4
        {
            get { return _ydir4; }
            set
            {
                _ydir4 = value;
                OnPropertyChanged("ydir4");
            }
        }

        [XmlIgnore]
        private string _zdir4;
        [XmlElement("zdir4")]
        public string zdir4
        {
            get { return _zdir4; }
            set
            {
                _zdir4 = value;
                OnPropertyChanged("zdir4");
            }
        }

        [XmlIgnore]
        private string _xdir1;
        [XmlElement("xdir1")]
        public string xdir1
        {
            get { return _xdir1; }
            set
            {
                _xdir1 = value;
                OnPropertyChanged("xdir1");
            }
        }

        [XmlIgnore]
        private string _xdir2;
        [XmlElement("xdir2")]
        public string xdir2
        {
            get { return _xdir2; }
            set
            {
                _xdir2 = value;
                OnPropertyChanged("xdir2");
            }
        }

        [XmlIgnore]
        private string _xdir3;
        [XmlElement("xdir3")]
        public string xdir3
        {
            get { return _xdir3; }
            set
            {
                _xdir3 = value;
                OnPropertyChanged("xdir3");
            }
        }

        [XmlIgnore]
        private string _ydir2;

        [XmlElement("ydir2")]
        public string ydir2
        {
            get { return _ydir2; }
            set
            {
                _ydir2 = value;
                OnPropertyChanged("ydir2");
            }
        }


        [XmlIgnore]
        private string _ydir1;
        [XmlElement("ydir1")]
        public string ydir1
        {
            get { return _ydir1; }
            set
            {
                _ydir1 = value;
                OnPropertyChanged("ydir1");
            }
        }

        [XmlIgnore]
        private string _zdir1;
        [XmlElement("zdir1")]
        public string zdir1
        {
            get { return _zdir1; }
            set
            {
                _zdir1 = value;
                OnPropertyChanged("zdir1");
            }
        }

        [XmlIgnore]
        private string _zdir2;
        [XmlElement("zdir2")]
        public string zdir2
        {
            get { return _zdir2; }
            set
            {
                _zdir2 = value;
                OnPropertyChanged("zdir2");
            }
        }

        [XmlIgnore]
        private string _zdir3;
        [XmlElement("zdir3")]
        public string zdir3
        {
            get { return _zdir3; }
            set
            {
                _zdir3 = value;
                OnPropertyChanged("zdir3");
            }
        }

        [XmlIgnore]
        private string _ydir3;
        [XmlElement("ydir3")]
        public string ydir3
        {
            get { return _ydir3; }
            set
            {
                _ydir3 = value;
                OnPropertyChanged("ydir3");
            }
        }

        [XmlIgnore]
        private Color _selectionBox;
        [XmlElement("selectionBox", Type = typeof(XmlColor))]
        public Color selectionBox
        {
            get { return _selectionBox; }
            set
            {
                _selectionBox = value;
                OnPropertyChanged("selectionBox");
            }
        }

        [XmlIgnore]
        private Color _travelMoves;
        [XmlElement("travelMoves", Type = typeof(XmlColor))]
        public Color travelMoves
        {
            get { return _travelMoves; }
            set
            {
                _travelMoves = value;
                OnPropertyChanged("travelMoves");
            }
        }

        [XmlIgnore]
        private bool _checkDisableTravelMoves;
        [XmlElement("checkDisableTravelMoves")]
        public bool checkDisableTravelMoves
        {
            get { return _checkDisableTravelMoves; }
            set
            {
                _checkDisableTravelMoves = value;
                OnPropertyChanged("checkDisableTravelMoves");
            }
        }

        [XmlIgnore]
        private bool _checkCorrectNormals;
        [XmlElement("checkCorrectNormals")]
        public bool checkCorrectNormals
        {
            get { return _checkCorrectNormals; }
            set
            {
                _checkCorrectNormals = value;
                OnPropertyChanged("checkCorrectNormals");
            }
        }

        
        [XmlIgnore]
        private bool _parallelProjection;
        [XmlElement("parallelProjection")]
        public bool parallelProjection
        {
            get { return _parallelProjection; }
            set
            {
                _parallelProjection = value;
                OnPropertyChanged("parallelProjection");
            }
        }


        [XmlIgnore]
        private Color _errorModel;
        [XmlElement("errorModel", Type = typeof(XmlColor))]
        public Color errorModel
        {
            get { return _errorModel; }
            set
            {
                _errorModel = value;
                OnPropertyChanged("errorModel");
            }
        }

        [XmlIgnore]
        private Color _cutFaces;
        [XmlElement("cutFaces", Type = typeof(XmlColor))]
        public Color cutFaces
        {
            get { return _cutFaces; }
            set
            {
                _cutFaces = value;
                OnPropertyChanged("cutFaces");
            }
        }

        [XmlIgnore]
        private Color _errorModelEdge;
        [XmlElement("errorModelEdge", Type = typeof(XmlColor))]
        public Color errorModelEdge
        {
            get { return _errorModelEdge; }
            set
            {
                _errorModelEdge = value;
                OnPropertyChanged("errorModelEdge");
            }
        }

        [XmlIgnore]
        private Color _insideFaces;
        [XmlElement("insideFaces", Type = typeof(XmlColor))]
        public Color insideFaces
        {
            get { return _insideFaces; }
            set
            {
                _insideFaces = value;
                OnPropertyChanged("insideFaces");
            }
        }

        [XmlIgnore]
        private Color _backgroundBottom;
        [XmlElement("backgroundBottom", Type = typeof(XmlColor))]
        public Color backgroundBottom
        {
            get { return _backgroundBottom; }
            set
            {
                _backgroundBottom = value;
                OnPropertyChanged("backgroundBottom");
            }
        }

        [XmlIgnore]
        private Color _printerFrame;
        [XmlElement("printerFrame", Type = typeof(XmlColor))]
        public Color printerFrame
        {
            get { return _printerFrame; }
            set
            {
                _printerFrame = value;
                OnPropertyChanged("printerFrame");
            }
        }



        [XmlIgnore]
        private bool _checkAutoenableParallelInTopView;
        [XmlElement("checkAutoenableParallelInTopView")]
        public bool checkAutoenableParallelInTopView
        {
            get { return _checkAutoenableParallelInTopView; }
            set
            {
                _checkAutoenableParallelInTopView = value;
                OnPropertyChanged("checkAutoenableParallelInTopView");
            }
        }
        [XmlIgnore]
        private bool _showCoordinate;
        [XmlElement("showCoordinate")]
        public bool showCoordinate
        {
            get { return _showCoordinate; }
            set
            {
                _showCoordinate = value;
                OnPropertyChanged("showCoordinate");
            }
        }


    }
}
