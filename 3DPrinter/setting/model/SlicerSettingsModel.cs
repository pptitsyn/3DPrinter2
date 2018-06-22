using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using _3DPrinter.utils;

namespace _3DPrinter.setting.model
{
    public class SlicerSettingsModel: NotifyPropertyChangedBase
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

        private int _layerThickness = 200;

        public int layerThickness
        {
            get { return _layerThickness; }
            set
            {
                _layerThickness = value;
                OnPropertyChanged("layerThickness");
            }
        }



        private int _initialLayerThickness = 300;

        public int initialLayerThickness
        {
            get { return _initialLayerThickness; }
            set
            {
                _initialLayerThickness = value;
                OnPropertyChanged("initialLayerThickness");
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

        private int _z_travel_speed = 100;
        public int Z_travel_speed
        {
            get { return _z_travel_speed; }
            set
            {
                _z_travel_speed = value;
                OnPropertyChanged("Z_travel_speed");
            }
        }

        private int _travel_speed = 4800;
        public int Travel_speed
        {
            get { return _travel_speed; }
            set
            {
                _travel_speed = value;
                OnPropertyChanged("Travel_speed");
            }
        }




        private int _extrusionWidth = 400;

        public int extrusionWidth
        {
            get { return _extrusionWidth; }
            set
            {
                _extrusionWidth = value;
                sparseInfillLineDistance = value;
                OnPropertyChanged("extrusionWidth");
            }
        }

        private int _layer0extrusionWidth = 400;

        public int layer0extrusionWidth
        {
            get { return _layer0extrusionWidth; }
            set
            {
                _layer0extrusionWidth = value;
                OnPropertyChanged("layer0extrusionWidth");
            }
        }

        private int _insetCount = 2;

        public int insetCount
        {
            get { return _insetCount; }
            set
            {
                _insetCount = value;
                OnPropertyChanged("insetCount");
            }
        }


        private int _infillOverlap = 15;

        public int infillOverlap
        {
            get { return _infillOverlap; }
            set
            {
                _infillOverlap = value;
                OnPropertyChanged("infillOverlap");
            }
        }

        private int _initialSpeedupLayers = 4;

        public int initialSpeedupLayers
        {
            get { return _initialSpeedupLayers; }
            set
            {
                _initialSpeedupLayers = value;
                OnPropertyChanged("initialSpeedupLayers");
            }
        }


        private int _initialLayerSpeed = 30;

        public int initialLayerSpeed
        {
            get { return _initialLayerSpeed; }
            set
            {
                _initialLayerSpeed = value;
                OnPropertyChanged("initialLayerSpeed");
            }
        }

        private int _printSpeed = 50;

        public int printSpeed
        {
            get { return _printSpeed; }
            set
            {
                _printSpeed = value;
                OnPropertyChanged("printSpeed");
            }
        }

        private int _infillSpeed = 80;

        public int infillSpeed
        {
            get { return _infillSpeed; }
            set
            {
                _infillSpeed = value;
                OnPropertyChanged("infillSpeed");
            }
        }

        private int _skinSpeed = 45;

        public int skinSpeed
        {
            get { return _skinSpeed; }
            set
            {
                _skinSpeed = value;
                OnPropertyChanged("skinSpeed");
            }
        }



        private int _inset0Speed = 45;

        public int inset0Speed
        {
            get { return _inset0Speed; }
            set
            {
                _inset0Speed = value;
                OnPropertyChanged("inset0Speed");
            }
        }

        private int _insetXSpeed = 60;

        public int insetXSpeed
        {
            get { return _insetXSpeed; }
            set
            {
                _insetXSpeed = value;
                OnPropertyChanged("insetXSpeed");
            }
        }

        private int _moveSpeed = 150;

        public int moveSpeed
        {
            get { return _moveSpeed; }
            set
            {
                _moveSpeed = value;
                OnPropertyChanged("moveSpeed");
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


        private int _supportAngle = 60;

        public int supportAngle
        {
            get { return _supportAngle; }
            set
            {
                _supportAngle = value;
                OnPropertyChanged("supportAngle");
            }
        }

        private int _supportEverywhere = 0;

        public int supportEverywhere
        {
            get { return _supportEverywhere; }
            set
            {
                _supportEverywhere = value;
                OnPropertyChanged("supportEverywhere");
            }
        }

        private int _supportLineDistance = 2666;

        public int supportLineDistance
        {
            get { return _supportLineDistance; }
            set
            {
                _supportLineDistance = value;
                OnPropertyChanged("supportLineDistance");
            }
        }

        private int _supportXYDistance = 700;

        public int supportXYDistance
        {
            get { return _supportXYDistance; }
            set
            {
                _supportXYDistance = value;
                OnPropertyChanged("supportXYDistance");
            }
        }

        private int _supportZDistance = 150;

        public int supportZDistance
        {
            get { return _supportZDistance; }
            set
            {
                _supportZDistance = value;
                OnPropertyChanged("supportZDistance");
            }
        }

        private int _supportExtruder = -1;

        public int supportExtruder
        {
            get { return _supportExtruder; }
            set
            {
                _supportExtruder = value;
                OnPropertyChanged("supportExtruder");
            }
        }

        private int _retractionAmount = 4000;

        public int retractionAmount
        {
            get { return _retractionAmount; }
            set
            {
                _retractionAmount = value;
                OnPropertyChanged("retractionAmount");
            }
        }

        private int _retractionSpeed = 40;

        public int retractionSpeed
        {
            get { return _retractionSpeed; }
            set
            {
                _retractionSpeed = value;
                OnPropertyChanged("retractionSpeed");
            }
        }

        private int _retractionMinimalDistance = 1500;

        public int retractionMinimalDistance
        {
            get { return _retractionMinimalDistance; }
            set
            {
                _retractionMinimalDistance = value;
                OnPropertyChanged("retractionMinimalDistance");
            }
        }

        private int _retractionAmountExtruderSwitch = 16000;

        public int retractionAmountExtruderSwitch
        {
            get { return _retractionAmountExtruderSwitch; }
            set
            {
                _retractionAmountExtruderSwitch = value;
                OnPropertyChanged("retractionAmountExtruderSwitch");
            }
        }

        private int _retractionZHop = 0;

        public int retractionZHop
        {
            get { return _retractionZHop; }
            set
            {
                _retractionZHop = value;
                OnPropertyChanged("retractionZHop");
            }
        }

        private int _minimalExtrusionBeforeRetraction = 20;

        public int minimalExtrusionBeforeRetraction
        {
            get { return _minimalExtrusionBeforeRetraction; }
            set
            {
                _minimalExtrusionBeforeRetraction = value;
                OnPropertyChanged("minimalExtrusionBeforeRetraction");
            }
        }

        private int _enableCombing = 1;

        public int enableCombing
        {
            get { return _enableCombing; }
            set
            {
                _enableCombing = value;
                OnPropertyChanged("enableCombing");
            }
        }

        private int _multiVolumeOverlap = 0;

        public int multiVolumeOverlap
        {
            get { return _multiVolumeOverlap; }
            set
            {
                _multiVolumeOverlap = value;
                OnPropertyChanged("multiVolumeOverlap");
            }
        }

        private int _objectSink = 0;

        public int objectSink
        {
            get { return _objectSink; }
            set
            {
                _objectSink = value;
                OnPropertyChanged("objectSink");
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

        private int _minimalFeedrate = 10;

        public int minimalFeedrate
        {
            get { return _minimalFeedrate; }
            set
            {
                _minimalFeedrate = value;
                OnPropertyChanged("minimalFeedrate");
            }
        }

        private int _coolHeadLift = 0;

        public int coolHeadLift
        {
            get { return _coolHeadLift; }
            set
            {
                _coolHeadLift = value;
                OnPropertyChanged("coolHeadLift");
            }
        }

        private int _perimeterBeforeInfill = 1;

        public int perimeterBeforeInfill
        {
            get { return _perimeterBeforeInfill; }
            set
            {
                _perimeterBeforeInfill = value;
                OnPropertyChanged("perimeterBeforeInfill");
            }
        }


        public int fixHorrible
        {
            get
            {
                return (Comb_Every_TypeA ? 1 : 0) + (Comb_Every_TypeB ? 2 : 0) + (KeepOpenFaces ? 16 : 0) + (ExtensiveStitching?4:0);
            }
            set
            {
                int temp = value;

                KeepOpenFaces = (temp/16 == 1);
                if (KeepOpenFaces)
                {
                    temp = temp%16;
                }

                ExtensiveStitching = (temp / 4 == 1);
                if (ExtensiveStitching)
                {
                    temp = temp % 4;
                }

                Comb_Every_TypeB = (temp / 2 == 1);
                if (Comb_Every_TypeB)
                {
                    temp = temp % 2;
                }

                Comb_Every_TypeA = (temp == 1);

                OnPropertyChanged("fixHorrible");
            }
        }


        private bool _comb_Every_typrA;
        [XmlIgnore]
        public bool Comb_Every_TypeA
        {
            get { return _comb_Every_typrA; }
            set
            {
                _comb_Every_typrA = value;
                OnPropertyChanged("Comb_Every_TypeA");
                OnPropertyChanged("fixHorrible");
            }
        }


        private bool _comb_Every_typrB;
        [XmlIgnore]
        public bool Comb_Every_TypeB
        {
            get { return _comb_Every_typrB; }
            set
            {
                _comb_Every_typrB = value;
                OnPropertyChanged("Comb_Every_TypeB");
                OnPropertyChanged("fixHorrible");
            }
        }

        private bool _keepOpenFaces;
        [XmlIgnore]
        public bool KeepOpenFaces
        {
            get { return _keepOpenFaces; }
            set
            {
                _keepOpenFaces = value;
                OnPropertyChanged("KeepOpenFaces");
                OnPropertyChanged("fixHorrible");
            }
        }


        private bool _extensiveStitching;
        [XmlIgnore]
        public bool ExtensiveStitching
        {
            get { return _extensiveStitching; }
            set
            {
                _extensiveStitching = value;
                OnPropertyChanged("ExtensiveStitching");
                OnPropertyChanged("fixHorrible");
            }
        }


        private int _fanFullOnLayerNr = 2;

        public int fanFullOnLayerNr
        {
            get { return _fanFullOnLayerNr; }
            set
            {
                _fanFullOnLayerNr = value;
                OnPropertyChanged("fanFullOnLayerNr");
            }
        }

        private int _supportType = 0;

        public int supportType
        {
            get { return _supportType; }
            set
            {
                _supportType = value;
                OnPropertyChanged("supportType");
            }
        }

        private int _infillPattern = 0;

        public int infillPattern
        {
            get { return _infillPattern; }
            set
            {
                _infillPattern = value;
                OnPropertyChanged("infillPattern");
            }
        }

        private int _sparseInfillLineDistance = 800;

        public int sparseInfillLineDistance
        {
            get { return _sparseInfillLineDistance; }
            set
            {
                _sparseInfillLineDistance = value;
                OnPropertyChanged("sparseInfillLineDistance");
            }
        }

        private int _downSkinCount = 3;

        public int downSkinCount
        {
            get { return _downSkinCount; }
            set
            {
                _downSkinCount = value;
                OnPropertyChanged("downSkinCount");
            }
        }

        private int _upSkinCount = 0;

        public int upSkinCount
        {
            get { return _upSkinCount; }
            set
            {
                _upSkinCount = value;
                OnPropertyChanged("upSkinCount");
            }
        }

        private int _skirtDistance = 3000;

        public int skirtDistance
        {
            get { return _skirtDistance; }
            set
            {
                _skirtDistance = value;
                OnPropertyChanged("skirtDistance");
            }
        }

        private int _skirtLineCount = 1;

        public int skirtLineCount
        {
            get { return _skirtLineCount; }
            set
            {
                _skirtLineCount = value;
                OnPropertyChanged("skirtLineCount");
            }
        }

        private int _skirtMinLength = 150000;

        public int skirtMinLength
        {
            get { return _skirtMinLength; }
            set
            {
                _skirtMinLength = value;
                OnPropertyChanged("skirtMinLength");
            }
        }

        private int _gcodeFlavor = 0;

        public int gcodeFlavor
        {
            get { return _gcodeFlavor; }
            set
            {
                _gcodeFlavor = value;
                OnPropertyChanged("gcodeFlavor");
            }
        }

        private int _spiralizeMode = 0;

        public int spiralizeMode
        {
            get { return _spiralizeMode; }
            set
            {
                _spiralizeMode = value;
                OnPropertyChanged("spiralizeMode");
            }
        }


        private int _enableOozeShield = 0;

        public int enableOozeShield
        {
            get { return _enableOozeShield; }
            set
            {
                _enableOozeShield = value;
                OnPropertyChanged("enableOozeShield");
            }
        }


        private int _autoCenter = 0;

        public int autoCenter
        {
            get { return _autoCenter; }
            set
            {
                _autoCenter = value;
                OnPropertyChanged("autoCenter");
            }
        }

      

        private string _preSwitchExtruderCode;

        public string preSwitchExtruderCode
        {
            get { return _preSwitchExtruderCode; }
            set
            {
                _preSwitchExtruderCode = value;
                OnPropertyChanged("preSwitchExtruderCode");
            }
        }

        private string _postSwitchExtruderCode;

        public string postSwitchExtruderCode
        {
            get { return _postSwitchExtruderCode; }
            set
            {
                _postSwitchExtruderCode = value;
                OnPropertyChanged("postSwitchExtruderCode");
            }
        }

        private int _infillDensity = 5;

        public int infillDensity
        {
            get { return _infillDensity; }
            set
            {
                _infillDensity = value;

                // calculate related properties
                if (_infillDensity == 0)
                {
                    sparseInfillLineDistance = -1;
                    downSkinCount = 6;
                    upSkinCount = 6;
                }
                else if (_infillDensity == 10)
                {
                    sparseInfillLineDistance = 500;
                    downSkinCount = 10000;
                    upSkinCount = 10000;
                }
                else
                {
                    sparseInfillLineDistance = 5000 / _infillDensity;
                    downSkinCount = 6;
                    upSkinCount = 6;
                }
                
                
                
                
                
                OnPropertyChanged("infillDensity");
            }
        }


        private float _shellThickness = 1.5f;

        public float shellThickness
        {
            get { return _shellThickness; }
            set
            {
                _shellThickness = value;
                
                // calculate related properties
                if (_shellThickness <= 0.1f)
                {
                    insetCount = 1;
                    extrusionWidth = 100;
                    layer0extrusionWidth = 100;
                    sparseInfillLineDistance = 100;
                    supportLineDistance = 500;
                                        
                }
                else if (_shellThickness <= 0.5f)
                {
                    insetCount = 1;
                    extrusionWidth = Convert.ToInt32(1000 *_shellThickness);
                    layer0extrusionWidth = Convert.ToInt32(1000 *_shellThickness);
                    sparseInfillLineDistance  = Convert.ToInt32(1000 *_shellThickness);
                    supportLineDistance = Convert.ToInt32(5000 *_shellThickness);
                    
                }
                else if (_shellThickness <= 1.0f)
                {
                    insetCount = 2;
                    extrusionWidth = 500;
                    layer0extrusionWidth = 500;
                    sparseInfillLineDistance = 500;
                    supportLineDistance = 2500;
                }
                else if (_shellThickness <= 1.5f)
                {
                    insetCount = 3;
                    extrusionWidth = 500;
                    layer0extrusionWidth = 500;
                    sparseInfillLineDistance = 500;
                    supportLineDistance = 2500;
                }
                else if (_shellThickness <= 2.0f)
                {
                    insetCount = 5;
                    extrusionWidth = 400;
                    layer0extrusionWidth = 400;
                    sparseInfillLineDistance = 400;
                    supportLineDistance = 2000;
                }
                else if (_shellThickness <= 5.0f)
                {
                    insetCount = 12;
                    extrusionWidth = 400;
                    layer0extrusionWidth = 400;
                    sparseInfillLineDistance = 400;
                    supportLineDistance = 2000;
                }
                else if (_shellThickness <= 10.0f)
                {
                    insetCount = 25;
                    extrusionWidth = 400;
                    layer0extrusionWidth = 400;
                    sparseInfillLineDistance = 400;
                    supportLineDistance = 2000;
                }
                else
                {
                    insetCount = 50;
                    extrusionWidth = 400;
                    layer0extrusionWidth = 400;
                    sparseInfillLineDistance = 400;
                    supportLineDistance = 2000;                    
                }


                OnPropertyChanged("shellThickness");
            }
        }


        private float _topBottomThickness = 1.0f;

        public float topBottomThickness
        {
            get { return _topBottomThickness; }
            set
            {
                _topBottomThickness = value;


                if (_topBottomThickness < 0.3f)
                {
                    downSkinCount = 1;
                    upSkinCount = 1;

                }
                else if (_topBottomThickness < 0.4f)
                {
                    downSkinCount = 2;
                    upSkinCount = 2;

                }
                else if (_topBottomThickness < 0.6f)
                {
                    downSkinCount = 3;
                    upSkinCount = 3;

                }
                else if (_topBottomThickness < 0.8f)
                {
                    downSkinCount = 4;
                    upSkinCount = 4;

                }
                else if (_topBottomThickness < 1.0f)
                {
                    downSkinCount = 5;
                    upSkinCount = 5;
                }
                else
                {
                    downSkinCount = Convert.ToInt32(_topBottomThickness*6);
                    upSkinCount = Convert.ToInt32(_topBottomThickness*6);

                }



                OnPropertyChanged("topBottomThickness");
            }
        }

        private string _startCode;

        public string startCode
        {
            get { return _startCode; }
            set
            {
                _startCode = value;
                OnPropertyChanged("startCode");
            }
        }


        private string _endCode;

        public string endCode
        {
            get { return _endCode; }
            set
            {
                _endCode = value;
                OnPropertyChanged("endCode");
            }
        }


/*
        private Point[] _extruderOffset = new Point[1];

        public Point[] extruderOffset
        {
            get { return _extruderOffset; }
            set
            {
                _extruderOffset = value;
                OnPropertyChanged("extruderOffset");
            }
        }

*/

    }
}
