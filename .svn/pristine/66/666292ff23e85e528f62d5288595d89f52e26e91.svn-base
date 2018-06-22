/*
   Copyright 2011 repetier repetierdev@gmail.com

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;
using OpenTK.Platform.Windows;
using OpenTK;
using _3DPrinter.projectManager;
using _3DPrinter.setting.model;
using _3DPrinter.utils;

namespace _3DPrinter.model
{

    public class Coord3D : NotifyPropertyChangedBase
    {
        private float x = 0, y = 0, z = 0;

        public bool updatedInfo = true;

        public bool IsLanding = true;

        public Coord3D() { }
        public Coord3D(float _x, float _y, float _z)
        {
            x = Round(_x);
            y = Round( _y);
            z = Round(_z);
        }

        public Coord3D(float _x, float _y, float _z, bool _lock)
            :this(_x,_y,_z)
        {
            _IsLock = _lock;
        }


        public float X
        {
            get { return x; }
            set
            {
                if (Lock)
                    SetValue(Round(value));
                else
                    x = Round(value);
                    OnPropertyChanged("X");
            }
        }

        public float Y
        {
            get { return y; }
            set
            {
                if (Lock)
                    SetValue(Round(value));
                else
                    y = Round(value);
                    OnPropertyChanged("Y");
            }
        }

        public float Z
        {
            get { return z; }
            set
            {
                if (Lock)
                    SetValue(Round(value));
                else
                    z = Round(value);
                    OnPropertyChanged("Z");
            }
        }


        private float Round(float value)
        {
            return Convert.ToSingle(Math.Round((Decimal)value, 2, MidpointRounding.AwayFromZero));
        }


        private bool _IsLock = false;
        public bool Lock
        {
            get { return _IsLock; }
            set
            {
                _IsLock = value;
                    OnPropertyChanged("Lock");
            }
        }

        private void SetValue(float value)
        {
            z = x = y = value;
            if (updatedInfo)
            {
                OnPropertyChanged("X");
                OnPropertyChanged("Y");
                OnPropertyChanged("Z");
            }
        }

    }
    public abstract class ThreeDModel : NotifyPropertyChangedBase
    {
        private bool selected = false;
//        private Coord3D position = null; //new Coord3D();
//        private Coord3D rotation = null; // new Coord3D();
//        private Coord3D scale = null; //new Coord3D(1, 1, 1);
        public LinkedList<ModelAnimation> animations = new LinkedList<ModelAnimation>();
        public float xMin = 0, yMin = 0, zMin = 0, xMax = 0, yMax = 0, zMax = 0;

        public ThreeDModel()
        {
            Position = new Coord3D() { updatedInfo = false, IsLanding = false };
            Rotation = new Coord3D();
            Scale = new Coord3D(1, 1, 1,true);
        }

        private int _extruderNumber = 0;

        protected ModelFromFile _fromFile = new ModelFromFile();

        public virtual int ExtruderNumber
        {
            get { return _extruderNumber; }
            set
            {
                _extruderNumber = value;
                OnPropertyChanged("ExtruderNumber");
            }
        }



        public void addAnimation(ModelAnimation anim)
        {
            animations.AddLast(anim);
        }
        public void removeAnimationWithName(string aname)
        {
            bool found = true;
            while (found)
            {
                found = false;
                foreach (ModelAnimation a in animations)
                {
                    if (a.name.Equals(aname))
                    {
                        found = true;
                        animations.Remove(a);
                        break;
                    }
                }
            }
        }
        public bool hasAnimationWithName(string aname)
        {
            foreach (ModelAnimation a in animations)
            {
                if (a.name.Equals(aname))
                {
                    return true;
                }
            }
            return false;
        }
        public void clearAnimations()
        {
            animations.Clear();
        }
        public bool hasAnimations
        {
            get { return animations.Count > 0; }
        }
        public void AnimationBefore()
        {
            foreach (ModelAnimation a in animations)
                a.BeforeAction(this);
        }
        /// <summary>
        /// Plays the after action and removes finished animations.
        /// </summary>
        public void AnimationAfter()
        {
            bool remove = false;
            foreach (ModelAnimation a in animations)
            {
                a.AfterAction(this);
                remove |= a.AnimationFinished();
            }
            if (remove)
            {
                bool found = true;
                while (found)
                {
                    found = false;
                    foreach (ModelAnimation a in animations)
                    {
                        if (a.AnimationFinished())
                        {
                            found = true;
                            animations.Remove(a);
                            break;
                        }
                    }
                }
            }
        }

        private bool _visible = true;
        public bool Visible
        {
            get { return _visible; }
            set
            {
                _visible = value;
                OnPropertyChanged("Visible");
            }
        }



        public void ForceViewRegeneration()
        {
            ForceRefresh = true;
        }


        public bool ForceRefresh = false;

        public bool Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                ForceRefresh = true;
            }
        }

        public Coord3D Position
        {
            get { return _fromFile.Position; }
            set
            {
                if (_fromFile.Position != null)
                {
                    _fromFile.Position.PropertyChanged -= CoordOnPropertyChanged;
                }
                _fromFile.Position = value;
                _fromFile.Position.PropertyChanged += CoordOnPropertyChanged;

                OnPropertyChanged("Position");
            }
        }

        public Coord3D Rotation
        {
            get { return _fromFile.Rotation; }
            set
            {
                if (_fromFile.Rotation != null)
                {
                    _fromFile.Rotation.PropertyChanged -= CoordOnPropertyChanged;
                }
                _fromFile.Rotation = value;
                _fromFile.Rotation.PropertyChanged += CoordOnPropertyChanged;
                OnPropertyChanged("Rotation");
            }
        }

        public Coord3D Scale
        {
            get { return _fromFile.Scale; }
            set
            {
                if (_fromFile.Scale != null)
                {
                    _fromFile.Scale.PropertyChanged -= CoordOnPropertyChanged;
                }
                _fromFile.Scale = value;
                _fromFile.Scale.PropertyChanged += CoordOnPropertyChanged;
                OnPropertyChanged("Scale");
            }
        }

        private void CoordOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            Coord3D coord = (Coord3D)sender;
            UpdateBoundingBox();
            ProjectManager.Instance.CurrentProject.updateSelectedModel();

            if (coord.IsLanding)
            {
                Position.Z -= zMin;
                UpdateBoundingBox();
            }
            if (coord.updatedInfo)
            {
                Analyse();
            }
        }

        public void update()
        {

            UpdateBoundingBox();
            ProjectManager.Instance.CurrentProject.updateSelectedModel();

        }

        public virtual void Analyse() { }

        public virtual void Land() { }

        public virtual void Center(float x, float y) { }


        public virtual void UpdateBoundingBox() { }

        public virtual void ReduceQuality() {
        }
        public virtual void ResetQuality() { }
        /// <summary>
        /// Has the model changed since last paint?
        /// </summary>
        public virtual bool Changed
        {
            get { return false; }
        }
        public virtual void Clear() { }
        abstract public void Paint();
        public virtual Vector3 getCenter()
        {
            return new Vector3(0, 0, 0);
        }
    }
}
