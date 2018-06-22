using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

//using OpenTK;
//using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using Tao.OpenGl;
using _3DPrinter.model;
using _3DPrinter.model.geom;
using _3DPrinter.projectManager;
using _3DPrinter.setting;
using _3DPrinter.setting.model;

using _3DPrinter.view.sliceVisual;
using BeginMode = OpenTK.Graphics.OpenGL.BeginMode;
using BlendingFactorDest = OpenTK.Graphics.OpenGL.BlendingFactorDest;
using BlendingFactorSrc = OpenTK.Graphics.OpenGL.BlendingFactorSrc;
using BufferTarget = OpenTK.Graphics.OpenGL.BufferTarget;
using BufferUsageHint = OpenTK.Graphics.OpenGL.BufferUsageHint;
using ClearBufferMask = OpenTK.Graphics.OpenGL.ClearBufferMask;
using DepthFunction = OpenTK.Graphics.OpenGL.DepthFunction;
using EnableCap = OpenTK.Graphics.OpenGL.EnableCap;
using FogParameter = OpenTK.Graphics.OpenGL.FogParameter;
using GenerateMipmapTarget = OpenTK.Graphics.OpenGL.GenerateMipmapTarget;
using GetPName = OpenTK.Graphics.OpenGL.GetPName;
using GL = OpenTK.Graphics.OpenGL.GL;
using HintMode = OpenTK.Graphics.OpenGL.HintMode;
using HintTarget = OpenTK.Graphics.OpenGL.HintTarget;
using LightName = OpenTK.Graphics.OpenGL.LightName;
using LightParameter = OpenTK.Graphics.OpenGL.LightParameter;
using MaterialFace = OpenTK.Graphics.OpenGL.MaterialFace;
using MaterialParameter = OpenTK.Graphics.OpenGL.MaterialParameter;
using MatrixMode = OpenTK.Graphics.OpenGL.MatrixMode;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using PixelInternalFormat = OpenTK.Graphics.OpenGL.PixelInternalFormat;
using PixelType = OpenTK.Graphics.OpenGL.PixelType;
using Point = System.Windows.Point;
using RenderingMode = OpenTK.Graphics.OpenGL.RenderingMode;
using ShadingModel = OpenTK.Graphics.OpenGL.ShadingModel;
using StringName = OpenTK.Graphics.OpenGL.StringName;
using TextureMagFilter = OpenTK.Graphics.OpenGL.TextureMagFilter;
using TextureMinFilter = OpenTK.Graphics.OpenGL.TextureMinFilter;
using TextureParameterName = OpenTK.Graphics.OpenGL.TextureParameterName;
using TextureTarget = OpenTK.Graphics.OpenGL.TextureTarget;
using TextureUnit = OpenTK.Graphics.OpenGL.TextureUnit;
using TextureWrapMode = OpenTK.Graphics.OpenGL.TextureWrapMode;
using UserControl = System.Windows.Controls.UserControl;
using VertexPointerType = OpenTK.Graphics.OpenGL.VertexPointerType;

namespace _3DPrinter.view.ThreeD
{
    /// <summary>
    /// Interaction logic for ThreeDControl.xaml
    /// </summary>
    public partial class ThreeDControl : UserControl
    {
//        public RHOpenGL gl;

        public ThreeDCamera cam;
        float bedRadius;
        private PrinterSettingsModel ps = SettingsProvider.Instance.Printer_Settings;
        bool loaded = false;
        int xPos, yPos, lastXpos, lastYpos;
        float rotateX, rotateY;
        Stopwatch fpsTimer = new Stopwatch();
        int mode = 0;
        int slowCounter = 0; // Indicates slow framerates
        uint timeCall = 0;
        public float zoom = 1.0f;
        public Matrix4 lookAt, persp, modelView;
        public float nearDist, farDist, aspectRatio, nearHeight, midHeight;
        Coordinate coord;
        bool render;
        private int changeMode = 0;
        private uint SelectedTriangle;

        private bool IsLeftButtonMousePressed = false;

//        public List<ThreeDModel> models = new List<ThreeDModel>();

        float lastMoveBodyX = 0.0f, lastMoveBodyY = 0.0f;
        float lastMoveViewpointX, lastMoveViewpointY;
        float zoomSpeed, lastZoomSpeed;
        float lastRotateX, lastRotateY;

        float filter = 0.7f; // filter of moves/rotation
        float moveFilter = 0.35f; // filter for movements of objects

        private bool autosizeFailed = false;


        bool oldCut = false;
        int oldPosition = -1;
        int oldInclination = -1;
        int oldAzimuth = -1;
        public bool updateCuts = false;
        public RHVector3 cutPos = new RHVector3(0, 0, 0);
        public RHVector3 cutDirection = new RHVector3(0, 0, 1);
        RHBoundingBox cutBBox = new RHBoundingBox();

        Geom3DPlane movePlane = new Geom3DPlane(new Geom3DVector(0, 0, 0), new Geom3DVector(0, 0, 1)); // Plane where object movement occurs
        Geom3DVector moveStart = new Geom3DVector(0, 0, 0), moveLast = new Geom3DVector(0, 0, 0), movePos = new Geom3DVector(0, 0, 0);

        public uint lastDepth = 0;
        public Geom3DLine viewLine = null; // Direction of view
        public Geom3DVector pickPoint = new Geom3DVector(0, 0, 0); // Coordinates of last pick

        public Bitmap bitmap = new Bitmap("images/place_texture.bmp");

        public int textureId;
        public int textureSkyBoxId;

        public List<string> faces = new List<string>();
        public List<int> TextureList = new List<int>();
        public int skyboxVAO, skyboxVBO;

//        public ThreeDView preView = null;
//        public ThreeDView sliceView = null;

//        public GCodeVisual jobVisual = new GCodeVisual();


        private WriteableBitmap backbuffer;
        private FrameBufferHandler framebufferHandler;

        private DispatcherTimer timer;

        public float FLOOR_TEXTURE_SIZE = 200.0f;

        private ThreeDView _currentView;
        public ThreeDView CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; }
        }

        public IntPtr _Handler;
        public const int sizeBox = 30000;

    public float[] skyboxVertices = {
    // positions          
    -1.0f*sizeBox,  1.0f*sizeBox, -1.0f*sizeBox,
    -1.0f*sizeBox, -1.0f*sizeBox, -1.0f*sizeBox,
     1.0f*sizeBox, -1.0f*sizeBox, -1.0f*sizeBox,
     1.0f*sizeBox, -1.0f*sizeBox, -1.0f*sizeBox,
     1.0f*sizeBox,  1.0f*sizeBox, -1.0f*sizeBox,
    -1.0f*sizeBox,  1.0f*sizeBox, -1.0f*sizeBox,

    -1.0f*sizeBox, -1.0f*sizeBox,  1.0f*sizeBox,
    -1.0f*sizeBox, -1.0f*sizeBox, -1.0f*sizeBox,
    -1.0f*sizeBox,  1.0f*sizeBox, -1.0f*sizeBox,
    -1.0f*sizeBox,  1.0f*sizeBox, -1.0f*sizeBox,
    -1.0f*sizeBox,  1.0f*sizeBox,  1.0f*sizeBox,
    -1.0f*sizeBox, -1.0f*sizeBox,  1.0f*sizeBox,

     1.0f*sizeBox, -1.0f*sizeBox, -1.0f*sizeBox,
     1.0f*sizeBox, -1.0f*sizeBox,  1.0f*sizeBox,
     1.0f*sizeBox,  1.0f*sizeBox,  1.0f*sizeBox,
     1.0f*sizeBox,  1.0f*sizeBox,  1.0f*sizeBox,
     1.0f*sizeBox,  1.0f*sizeBox, -1.0f*sizeBox,
     1.0f*sizeBox, -1.0f*sizeBox, -1.0f*sizeBox,

    -1.0f*sizeBox, -1.0f*sizeBox,  1.0f*sizeBox,
    -1.0f*sizeBox,  1.0f*sizeBox,  1.0f*sizeBox,
     1.0f*sizeBox,  1.0f*sizeBox,  1.0f*sizeBox,
     1.0f*sizeBox,  1.0f*sizeBox,  1.0f*sizeBox,
     1.0f*sizeBox, -1.0f*sizeBox,  1.0f*sizeBox,
    -1.0f*sizeBox, -1.0f*sizeBox,  1.0f*sizeBox,

    -1.0f*sizeBox,  1.0f*sizeBox, -1.0f*sizeBox,
     1.0f*sizeBox,  1.0f*sizeBox, -1.0f*sizeBox,
     1.0f*sizeBox,  1.0f*sizeBox,  1.0f*sizeBox,
     1.0f*sizeBox,  1.0f*sizeBox,  1.0f*sizeBox,
    -1.0f*sizeBox,  1.0f*sizeBox,  1.0f*sizeBox,
    -1.0f*sizeBox,  1.0f*sizeBox, -1.0f*sizeBox,

    -1.0f*sizeBox, -1.0f*sizeBox, -1.0f*sizeBox,
    -1.0f*sizeBox, -1.0f*sizeBox,  1.0f*sizeBox,
     1.0f*sizeBox, -1.0f*sizeBox, -1.0f*sizeBox,
     1.0f*sizeBox, -1.0f*sizeBox, -1.0f*sizeBox,
    -1.0f*sizeBox, -1.0f*sizeBox,  1.0f*sizeBox,
     1.0f*sizeBox, -1.0f*sizeBox,  1.0f*sizeBox
};

        public ThreeDControl()
        {
            InitializeComponent();



//            preView = new ThreeDView();

            CurrentView = ProjectManager.Instance.CurrentProject.PreView;

//            sliceView = new ThreeDView();
            //   jobPreview.Dock = DockStyle.Fill;
            //   splitJob.Panel2.Controls.Add(jobPreview);
//            sliceView.SetEditor(false);
//            jobVisual.OwnerControl = this;
//            sliceView.models.AddLast(jobVisual);


            this.framebufferHandler = new FrameBufferHandler();
            SettingsProvider.Instance.ThreeDSettings.PropertyChanged += ThreeDSettingsOnPropertyChanged;

//            this.gl = new RHOpenGL();
//                        this.gl.Paint += this.GlcontrolOnPaint;
//                        this.gl.Dock = DockStyle.Fill;
//            this.hostGL.Child = this.gl;

            //cam = new ThreeDCamera(this);
            //SetCameraDefaults();
            //cam.OrientIsometric();

//                        gl.MouseWheel += GlOnMouseWheel;
//                        gl.MouseMove += GlOnMouseMove;
//                        gl.MouseDown += GlOnMouseDown;
            faces.Clear();
            faces.Add("images/skybox/right.jpg");
            faces.Add("images/skybox/left.jpg");
            faces.Add("images/skybox/top.jpg");
            faces.Add("images/skybox/bottom.jpg");
            faces.Add("images/skybox/back.jpg");
            faces.Add("images/skybox/front.jpg");
            faces.Add("images/skyline.jpg");
            faces.Add("images/testback.jpg");
            faces.Add("images/testtop.jpg");
            faces.Add("images/place_texture3.png");


            TextureList.Add(initTexture(initBitMap(faces[5])));
            TextureList.Add(initTexture(initBitMap(faces[1])));
            TextureList.Add(initTexture(initBitMap(faces[4])));
            TextureList.Add(initTexture(initBitMap(faces[0])));
            TextureList.Add(initTexture(initBitMap(faces[2])));
            TextureList.Add(initTexture(initBitMap(faces[3])));
            TextureList.Add(initTexture(initBitMap(faces[6])));
            TextureList.Add(initTexture(initBitMap(faces[7])));
            TextureList.Add(initTexture(initBitMap(faces[8])));
            TextureList.Add(initTexture(initBitMap(faces[9])));


           // textureSkyBoxId = LoadCubeMap(faces);

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(20);
            timer.Tick += this.TimerOnTick;
//            timer.Start();

        }

        private void ThreeDSettingsOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            render = true;
        }



        private void SetCameraDefaults()
        {
            cam.viewCenter = new Vector3(0, 0, 0);
            cam.defaultDistance = 1.6f * (float)Math.Sqrt(ps.PrintAreaDepth * ps.PrintAreaDepth + ps.PrintAreaWidth * ps.PrintAreaWidth + ps.PrintAreaHeight * ps.PrintAreaHeight);
            cam.minDistance = 0.0001 * cam.defaultDistance;
        }

        private void GlcontrolOnPaint() // (object sender, PaintEventArgs e)
        {
          //  this.gl.MakeCurrent();

            //            GL.MatrixMode(MatrixMode.Projection);
            //            GL.LoadIdentity();
            //            float halfWidth = (float)(this.gl.Width / 2);
            //            float halfHeight = (float)(this.gl.Height / 2);
            //            GL.Ortho(-halfWidth, halfWidth, halfHeight, -halfHeight, 1000, -1000);

      //      GL.Viewport(this.gl.Size);


            if (this.view_Image.ActualWidth <= 0 || this.view_Image.ActualHeight <= 0)
            {
                return;
            }


            this.framebufferHandler.Prepare(new System.Drawing.Size((int)this.view_Container.ActualWidth, (int)this.view_Container.ActualHeight));
            

            gl_Paint();


            this.framebufferHandler.Cleanup(ref this.backbuffer);

            if (this.backbuffer != null)
            {
                this.view_Image.Source = this.backbuffer;
            }


        //    GL.Finish();

//            this.gl.SwapBuffers();

        }

        /*
        public void SetView(ThreeDView view, int viewType)
        {
            if (viewType == 1)
            {
                this.preView = view;
                CurrentView = view;
            }
            else
            {
                this.sliceView = view;
                CurrentView = view;
            }
            UpdateChanges();
        }
        */

        private int viewMode = 1;
        public void ChangeView(int viewType)
        {
            viewMode = viewType;
            if (viewType == 1)
            {
                 CurrentView = ProjectManager.Instance.CurrentProject.PreView;
            }
            else
            {
                CurrentView = ProjectManager.Instance.CurrentProject.SlicerView;
            }
            UpdateChanges();
        }
        
        private void TimerOnTick(object sender, EventArgs e)
        {
            Application_Idle(sender, e);
            timeCall++;
            foreach (ThreeDModel m in CurrentView.models)
            {
                if (m.Changed || m.hasAnimations)
                {
                    if ((SettingsProvider.Instance.ThreeDSettings.drawMethod == 0 && (timeCall % 9) != 0))
                        return;
                    if (m.hasAnimations && SettingsProvider.Instance.ThreeDSettings.drawMethod != 0)
                        render = true;
                    else if ((timeCall % 3) == 0)
                        render = true;
                    return;
                }
            }
        }

        public bool CheckUniformButtonState(MouseButtonState state)
        {
            switch (state)
            {
                case MouseButtonState.Pressed:
                    return (Mouse.LeftButton == MouseButtonState.Pressed ||
                        Mouse.RightButton == MouseButtonState.Pressed ||
                        Mouse.MiddleButton == MouseButtonState.Pressed ||
                        Mouse.XButton1 == MouseButtonState.Pressed ||
                        Mouse.XButton2 == MouseButtonState.Pressed);
                case MouseButtonState.Released:
                    return (Mouse.LeftButton == MouseButtonState.Released ||
                        Mouse.RightButton == MouseButtonState.Released ||
                        Mouse.MiddleButton == MouseButtonState.Released ||
                        Mouse.XButton1 == MouseButtonState.Released ||
                        Mouse.XButton2 == MouseButtonState.Released);
                default:
                    return false;
            }
        }

        public void hostGL_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {

            zoomSpeed = -e.Delta;

            lastZoomSpeed = 0;
/*
            FLOOR_TEXTURE_SIZE = FLOOR_TEXTURE_SIZE * (1 - filter);
            if (FLOOR_TEXTURE_SIZE < 20)
            {
                FLOOR_TEXTURE_SIZE = 200.0f;
            }
            if (FLOOR_TEXTURE_SIZE > 200)
            {
                FLOOR_TEXTURE_SIZE = 20.0f;
            }
 */

        }

        public void hostGL_MouseMove(object sender, MouseEventArgs e)
        {
            if (CheckUniformButtonState(MouseButtonState.Pressed))
            {
                Point ep = e.GetPosition(this);
                xPos = (int)ep.X;
                yPos = (int)ep.Y;
            }


            //**            ShowPanel(toolStrip1, gl, gl);
        }

        public void hostGL_MouseDown(object sender, MouseButtonEventArgs e)
        {

            Point ep = e.GetPosition(this);
            lastXpos = xPos = (int)ep.X;
            lastYpos = yPos = (int)ep.Y;
            lastRotateX = lastRotateY = 0;
            Geom3DLine pickLine = GetPickLine((int)ep.X, (int)ep.Y);
            if (e.ChangedButton == MouseButton.Left)
            {
                IsLeftButtonMousePressed = true;
            }
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                ThreeDModel sel = Picktest(pickLine, (int)ep.X, (int)ep.Y);
                if ((sel != null) && (sel.GetType() == typeof(PrintModel)))
                {
                    movePlane = new Geom3DPlane(pickPoint, new Geom3DVector(0, 0, 1));
                    moveStart = moveLast = new Geom3DVector(pickPoint);
                    ProjectManager.Instance.CurrentProject.SelectedModel = sel;
                    SetChangeMode(1);
                }
                else
                {
                    movePlane = new Geom3DPlane(new Geom3DVector(0, 0, 0), new Geom3DVector(0, 0, 1));
                    moveStart = moveLast = new Geom3DVector(0, 0, 0);
                    movePlane.intersectLine(pickLine, moveStart);
                    ProjectManager.Instance.CurrentProject.SelectedModel = null;
                    SetChangeMode(0);
                }
                if (sel != null && CurrentView.eventObjectMoved != null)
                    CurrentView.eventObjectSelected(sel);
                //computeRay();
            }
        }

        private void ViewContainer_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                IsLeftButtonMousePressed = false;
            }
        }

        private void hostGL_MouseEnter(object sender, MouseEventArgs e)
        {
        }

        private void hostGL_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void hostGL_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            if (loaded)
            {
                SetupViewport();
                //**            toolStrip1.Size = new Size(toolStrip1.Width, gl.Height);
                render = true;
            }
        }


        private void SetupViewport()
        {
            var s = GL.GetString(StringName.Version);
            int ii = 0;
            try
            {
                int w = (int) this.view_Container.ActualWidth; //gl.Width;
                int h = (int) this.view_Container.ActualHeight; //gl.Height;
                bedRadius =
                    (float)
                        (1.5*
                         Math.Sqrt((ps.PrintAreaDepth*ps.PrintAreaDepth + ps.PrintAreaHeight*ps.PrintAreaHeight +
                                    ps.PrintAreaWidth*ps.PrintAreaWidth)*0.25));
                GL.Viewport(0, 0, w, h); // Use all of the glControl painting area
                GL.MatrixMode(MatrixMode.Projection);
                //GL.LoadIdentity();
                float angle;
                Vector3 camPos = cam.CameraPosition;
                float dist = (float) cam.distance;

                //nearDist = Math.Max(1, 0.5f*(dist - bedRadius));
                nearDist = 1;
                //nearDist = 4000;
                farDist = Math.Max(bedRadius*2, dist + bedRadius);
                farDist = 45000;
                midHeight = 2.0f*(float) Math.Tan(cam.angle)*dist;
                nearHeight = 2.0f*(float) Math.Tan(cam.angle)*nearDist;
                aspectRatio = (float) w/(float) h;
                angle = (float) (cam.angle)*2.0f;


                if (SettingsProvider.Instance.ThreeDSettings.parallelProjection)
                     persp = Matrix4.CreateOrthographic(midHeight * aspectRatio, midHeight, nearDist, farDist);
                else
                persp = Matrix4.CreatePerspectiveFieldOfView((float) (angle), aspectRatio, nearDist, farDist);
                
                GL.LoadMatrix(ref persp);
                // GL.Ortho(0, w, 0, h, -1, 1); // Bottom-left corner pixel has coordinate (0, 0)
                ///InitScene();
             //   InitSkyBox();
            }
            catch
            {
                int i = 0;
                i++;
            }
        }


        public void SetChangeMode(int mode)
        {
            changeMode = mode;
            manipulateMode = mode;
        }

        void Application_Idle(object sender, EventArgs e)
        {
            if (!loaded)//|| !Main.ApplicationIsActivated())
                return;
            // no guard needed -- we hooked into the event in Load handler

                int gl_Width = (int) this.view_Container.ActualWidth;
                int gl_Height = (int)this.view_Container.ActualHeight;

            float d = Math.Min((int)this.view_Container.ActualWidth, (int)this.view_Container.ActualHeight) / 3;

            // rotate

            if (((viewMode == 2) || (viewMode == 1 && changeMode != 1 && mode == 0)) && ((Mouse.LeftButton == MouseButtonState.Pressed) && !((Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift)))))
            {
                rotateX = ((lastXpos - xPos) / d) * (1 - filter) + filter * lastRotateX;
                rotateY = ((lastYpos - yPos) / d) * (1 - filter) + filter * lastRotateY;
                Rotate();
            }
            else if (rotateX != 0 || rotateY != 0)
            {
                rotateX = rotateX * (1 - filter) + filter * lastRotateX;
                rotateY = rotateY * (1 - filter) + filter * lastRotateY;
                Rotate();
            }
            else if (checkMovements(lastRotateX) || checkMovements(lastRotateY))
            {
                rotateX = filter * lastRotateX;
                rotateY = filter * lastRotateY;
                Rotate();
            }

            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                Byte4 Pixel = new Byte4();
                GL.ReadPixels((int)Mouse.GetPosition(this).X, (int)this.view_Container.ActualHeight - (int)Mouse.GetPosition(this).Y, 1, 1,OpenTK.Graphics.OpenGL.PixelFormat.Rgba, PixelType.UnsignedByte, ref Pixel);
                SelectedTriangle = Pixel.ToUInt32();
            }

            //zoom
            if (mode == 3 && Mouse.LeftButton == MouseButtonState.Pressed)
                zoomSpeed = 120 * (yPos - lastYpos) / d;
            if (zoomSpeed != 0 || checkMovements(lastZoomSpeed))
            {
                zoomSpeed = zoomSpeed * (1 - filter) + filter * lastZoomSpeed;
                lastZoomSpeed = zoomSpeed;
              //  if (cam.distance > 1)
                {
                    float cu = 0.005f;

                    if (cam.distance < 100)
                    {
                        cu = 0.01f;
                    }
                    if (cam.distance < 10)
                    {
                        cu = 0.01f;
                    }
                    //                   cam.Zoom(zoomSpeed * ( (cam.distance - cam.minDistance)/(cam.defaultDistance - cam.minDistance)));


                    cam.Zoom(zoomSpeed * cam.distance*cu);
                }
               // else
                {
                 //   Math.Atan()
                  //  cam.Zoom(zoomSpeed * 5 * (cam.distance - cam.minDistance) / (cam.defaultDistance - cam.minDistance));
                }
                zoomSpeed = 0;
                render = true;
            }

            // MoveViewpoint
            if (mode == 2 || (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift)) || Mouse.MiddleButton == MouseButtonState.Pressed)
            {
                float moveViewpointX = ((xPos - lastXpos) / (float)gl_Width) * (1 - filter) + filter * lastMoveViewpointX;
                float moveViewpointY = ((yPos - lastYpos) / (float)gl_Height) * (1 - filter) + filter * lastMoveViewpointY;
                MoveViewpoint(moveViewpointX, moveViewpointY);
            }
            else if (checkMovements(lastMoveViewpointX) || checkMovements(lastMoveViewpointY))
            {
                float moveViewpointX = filter * lastMoveViewpointX;
                float moveViewpointY = filter * lastMoveViewpointY;
                MoveViewpoint(moveViewpointX, moveViewpointY);
            }

            // MoveObject
//            if (CurrentView.eventObjectMoved != null && (mode == 4 || (Keyboard.IsKeyDown(Key.LeftAlt) || Keyboard.IsKeyDown(Key.RightAlt)) || Mouse.RightButton == MouseButtonState.Pressed))
            if (IsLeftButtonMousePressed && ProjectManager.Instance.CurrentProject.SelectedModel != null && changeMode == 1)
            {
                Geom3DLine pickLine = GetPickLine(xPos, yPos);
                movePos = new Geom3DVector(0, 0, 0);
                movePlane.intersectLine(pickLine, movePos);
                Geom3DVector diff = movePos.sub(moveLast);
                float moveBodyX = diff.x * (1 - moveFilter) + moveFilter * lastMoveBodyX;
                float moveBodyY = diff.y * (1 - moveFilter) + moveFilter * lastMoveBodyY;
                MoveObject(moveBodyX, moveBodyY);
            }
            else if (checkMovements(lastMoveBodyX) || checkMovements(lastMoveBodyY))
            {
                float moveBodyX = moveFilter * lastMoveBodyX;
                float moveBodyY = moveFilter * lastMoveBodyY;
                MoveObject(moveBodyX, moveBodyY);
            }

            if (render)
            {
               // gl.Invalidate();
                GlcontrolOnPaint();
//                gl_Paint();
                lastXpos = xPos;
                lastYpos = yPos;
                render = false;
            }
        }


        private void Rotate()
        {
            lastRotateX = rotateX;
            lastRotateY = rotateY;
            cam.Rotate(rotateX, rotateY);
            rotateX = rotateY = 0;
            render = true;
        }

        private void MoveViewpoint(float moveViewpointX, float moveViewpointY)
        {
            lastMoveViewpointX = moveViewpointX;
            lastMoveViewpointY = moveViewpointY;
            Vector3 planeVec = Vector3.Subtract(new Vector3(moveStart.x, moveStart.y, moveStart.z), cam.CameraPosition);
            float dot = Vector3.Dot(planeVec, cam.ViewDirection());
            double len = (dot > 0 ? planeVec.Length : -1);
            cam.Pan(-moveViewpointX, -moveViewpointY, len);
            render = true;
        }

        private void MoveObject(float moveBodyX, float moveBodyY)
        {
            lastMoveBodyX = moveBodyX;
            lastMoveBodyY = moveBodyY;
          //  CurrentView.eventObjectMoved(moveBodyX, moveBodyY);

            PrintModel stl = (PrintModel)ProjectManager.Instance.CurrentProject.SelectedModel;
            stl.Position.X += moveBodyX;
            stl.Position.Y += moveBodyY;
            stl.UpdateBoundingBox();
            moveLast = movePos;
            ProjectManager.Instance.CurrentProject.updateSTLState(stl);
            render = true;
        }

        private bool checkMovements(float val)
        {
            return (val < 0 ? -val : val) > 1;
        }

        public void SetMode(int _mode)
        {
            mode = _mode;
            //**            toolRotate.Checked = mode == 0;
            //**            toolMoveViewpoint.Checked = mode == 2;
            //**            toolZoom.Checked = mode == 3;
            //**            toolMoveObject.Checked = mode == 4;
        }

        public Geom3DLine GetPickLine(int x, int y)
        {
            if (CurrentView == null)
                return null;
            // Intersection on bottom plane
            int window_y = ((int)this.view_Image.ActualHeight - y) - (int)(this.view_Image.ActualHeight / 2);
            double norm_y = (double)window_y / (double)(this.view_Image.ActualHeight / 2);
            int window_x = x - (int)(this.view_Image.ActualWidth / 2);
            double norm_x = (double)window_x / (double)(this.view_Image.ActualWidth / 2);
            float fpy = (float)(nearHeight * 0.5 * norm_y) * 1f; //(SettingsProvider.Instance.ThreeDSettings.parallelProjection ? 1f : 1f);
            float fpx = (float)(nearHeight * 0.5 * aspectRatio * norm_x) * 1f;//(SettingsProvider.Instance.ThreeDSettings.parallelProjection ? 1f : 1f);

            Vector4 frontPointN = (SettingsProvider.Instance.ThreeDSettings.parallelProjection ? new Vector4(fpx, fpy, 0, 1) : new Vector4(0, 0, 0, 1));
            Vector4 dirN = (SettingsProvider.Instance.ThreeDSettings.parallelProjection ? new Vector4(0, 0, -nearDist, 0) : new Vector4(fpx, fpy, -nearDist, 0));
            Matrix4 ntrans;
            Vector3 camPos = cam.CameraPosition;
            ntrans = Matrix4.LookAt(camPos.X, camPos.Y, camPos.Z, cam.viewCenter.X, cam.viewCenter.Y, cam.viewCenter.Z, 0, 0, 1.0f);
            ntrans = Matrix4.Invert(ntrans);
            Vector4 frontPoint = (SettingsProvider.Instance.ThreeDSettings.parallelProjection ? Vector4.Transform(frontPointN, ntrans) : ntrans.Row3);
            
            //Vector4 frontPoint = (SettingsProvider.Instance.ThreeDSettings.parallelProjection ? frontPointN : ntrans.Row3);
            Vector4 dirVec = Vector4.Transform(dirN, ntrans);
            Geom3DLine pickLine = new Geom3DLine(new Geom3DVector(frontPoint.X / frontPoint.W, frontPoint.Y / frontPoint.W, frontPoint.Z / frontPoint.W),
                new Geom3DVector(dirVec.X, dirVec.Y, dirVec.Z), true);
            pickLine.dir.normalize();
            return pickLine;
            /*Geom3DPlane plane = new Geom3DPlane(new Geom3DVector(0, 0, 0), new Geom3DVector(0, 0, 1));
            Geom3DVector cross = new Geom3DVector(0, 0, 0);
            plane.intersectLine(pickLine, cross);
            */
        }

        public void SetObjectSelected(bool sel)
        {
            //**            toolMoveObject.Enabled = sel;
            //**            toolStripClear.Enabled = sel;
            CurrentView.objectsSelected = sel;
        }

        public void UpdateChanges()
        {
            render = true;
        }

        public OpenTK.Graphics.Color4 convertColor(Color col)
        {
            return new OpenTK.Graphics.Color4(col.R, col.G, col.B, col.A);
        }

        private void AddLights()
        {
            //Enable lighting
            GL.Light(LightName.Light0, LightParameter.Ambient, new float[] { 0.2f, 0.2f, 0.2f, 1f });
            GL.Light(LightName.Light0, LightParameter.Diffuse, new float[] { 1, 1, 1, 0 });
            GL.Light(LightName.Light0, LightParameter.Specular, new float[] { 1, 1, 1, 0 });
            GL.Enable(EnableCap.Light0);
            if (SettingsProvider.Instance.ThreeDSettings.enableLight1)
            {
                GL.Light(LightName.Light1, LightParameter.Ambient, SettingsProvider.Instance.ThreeDSettings.Ambient1());
                GL.Light(LightName.Light1, LightParameter.Diffuse, SettingsProvider.Instance.ThreeDSettings.Diffuse1());
                GL.Light(LightName.Light1, LightParameter.Specular, SettingsProvider.Instance.ThreeDSettings.Specular1());
                GL.Light(LightName.Light1, LightParameter.Position, SettingsProvider.Instance.ThreeDSettings.Dir1());
                //  GL.Light(LightName.Light1, LightParameter.SpotExponent, new float[] { 1.0f, 1.0f, 1.0f, 1.0f });
                GL.Enable(EnableCap.Light1);
            }
            else GL.Disable(EnableCap.Light1);
            if (SettingsProvider.Instance.ThreeDSettings.enableLight2)
            {
                GL.Light(LightName.Light2, LightParameter.Ambient, SettingsProvider.Instance.ThreeDSettings.Ambient2());
                GL.Light(LightName.Light2, LightParameter.Diffuse, SettingsProvider.Instance.ThreeDSettings.Diffuse2());
                GL.Light(LightName.Light2, LightParameter.Specular, SettingsProvider.Instance.ThreeDSettings.Specular2());
                GL.Light(LightName.Light2, LightParameter.Position, SettingsProvider.Instance.ThreeDSettings.Dir2());
                /*  GL.Light(LightName.Light2, LightParameter.Diffuse, new float[] { 0.7f, 0.7f, 0.7f, 1f });
                  GL.Light(LightName.Light2, LightParameter.Specular, new float[] { 1.0f, 1.0f, 1.0f, 1.0f });
                  GL.Light(LightName.Light2, LightParameter.Position, (new Vector4(100f, 200f, 300f, 0)));*/
                GL.Light(LightName.Light2, LightParameter.SpotExponent, new float[] { 1.0f, 1.0f, 1.0f, 1.0f });
                GL.Enable(EnableCap.Light2);
            }
            else GL.Disable(EnableCap.Light2);
            if (SettingsProvider.Instance.ThreeDSettings.enableLight3)
            {
                GL.Light(LightName.Light3, LightParameter.Ambient, SettingsProvider.Instance.ThreeDSettings.Ambient3());
                GL.Light(LightName.Light3, LightParameter.Diffuse, SettingsProvider.Instance.ThreeDSettings.Diffuse3());
                GL.Light(LightName.Light3, LightParameter.Specular, SettingsProvider.Instance.ThreeDSettings.Specular3());
                GL.Light(LightName.Light3, LightParameter.Position, SettingsProvider.Instance.ThreeDSettings.Dir3());
                /*  GL.Light(LightName.Light3, LightParameter.Diffuse, new float[] { 0.8f, 0.8f, 0.8f, 1f });
                  GL.Light(LightName.Light3, LightParameter.Specular, new float[] { 1.0f, 1.0f, 1.0f, 1.0f });
                  GL.Light(LightName.Light3, LightParameter.Position, (new Vector4(100f, -200f, 200f, 0)));*/
                GL.Light(LightName.Light3, LightParameter.SpotExponent, new float[] { 1.0f, 1.0f, 1.0f, 1.0f });
                GL.Enable(EnableCap.Light3);
            }
            else GL.Disable(EnableCap.Light3);
            if (SettingsProvider.Instance.ThreeDSettings.enableLight4)
            {
                GL.Light(LightName.Light4, LightParameter.Ambient, SettingsProvider.Instance.ThreeDSettings.Ambient4());
                GL.Light(LightName.Light4, LightParameter.Diffuse, SettingsProvider.Instance.ThreeDSettings.Diffuse4());
                GL.Light(LightName.Light4, LightParameter.Specular, SettingsProvider.Instance.ThreeDSettings.Specular4());
                GL.Light(LightName.Light4, LightParameter.Position, SettingsProvider.Instance.ThreeDSettings.Dir4());
                /* GL.Light(LightName.Light4, LightParameter.Diffuse, new float[] { 0.7f, 0.7f, 0.7f, 1f });
                 GL.Light(LightName.Light4, LightParameter.Specular, new float[] { 1.0f, 1.0f, 1.0f, 1.0f });
                 GL.Light(LightName.Light4, LightParameter.Position, (new Vector4(170f, -100f, -250f, 0)));*/
                GL.Light(LightName.Light4, LightParameter.SpotExponent, new float[] { 1.0f, 1.0f, 1.0f, 1.0f });
                GL.Enable(EnableCap.Light4);
            }
            else GL.Disable(EnableCap.Light4);

            GL.Enable(EnableCap.Lighting);
        }
        private void DetectDrawingMethod()
        {
            // Check drawing method
            int om = SettingsProvider.Instance.ThreeDSettings.drawMethod;
            switch (SettingsProvider.Instance.ThreeDSettings.comboDrawMethod)
            {
                case 0: // Autodetect;
                    if (SettingsProvider.Instance.ThreeDSettings.useVBOs && SettingsProvider.Instance.ThreeDSettings.openGLVersion >= 1.499)
                        SettingsProvider.Instance.ThreeDSettings.drawMethod = 2;
                    else if (SettingsProvider.Instance.ThreeDSettings.openGLVersion >= 1.099)
                        SettingsProvider.Instance.ThreeDSettings.drawMethod = 1;
                    else
                        SettingsProvider.Instance.ThreeDSettings.drawMethod = 0;
                    break;
                case 1: // VBOs
                    SettingsProvider.Instance.ThreeDSettings.drawMethod = 2;
                    break;
                case 2: // drawElements
                    SettingsProvider.Instance.ThreeDSettings.drawMethod = 1;
                    break;
                case 3: // elements
                    SettingsProvider.Instance.ThreeDSettings.drawMethod = 0;
                    break;
            }
            //if (om != SettingsProvider.Instance.ThreeDSettings.drawMethod)
            //Main.main.updateTravelMoves();
        }
        private void DrawViewpoint()
        {

            GL.Color4(convertColor(SettingsProvider.Instance.ThreeDSettings.printerFrame));
            GL.Disable(EnableCap.Lighting);
            GL.Disable(EnableCap.DepthTest);
            GL.Begin(BeginMode.Lines);
            GL.Vertex3(cam.viewCenter.X - 2, cam.viewCenter.Y, cam.viewCenter.Z);
            GL.Vertex3(cam.viewCenter.X + 2, cam.viewCenter.Y, cam.viewCenter.Z);
            GL.Vertex3(cam.viewCenter.X, cam.viewCenter.Y - 2, cam.viewCenter.Z);
            GL.Vertex3(cam.viewCenter.X, cam.viewCenter.Y + 2, cam.viewCenter.Z);
            GL.Vertex3(cam.viewCenter.X, cam.viewCenter.Y, cam.viewCenter.Z - 2);
            GL.Vertex3(cam.viewCenter.X, cam.viewCenter.Y, cam.viewCenter.Z + 2);
            GL.End();
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.DepthTest);

        }
        private void DrawModels()
        {
            GL.Disable(EnableCap.LineSmooth);

            //            if (Main.main.tab.SelectedIndex > 1)
            //                GL.Enable(EnableCap.CullFace);
            //            else

            GL.Disable(EnableCap.CullFace);
            foreach (ThreeDModel model in CurrentView.models) //**view.models)
            {
                GL.PushMatrix();
                model.AnimationBefore();
                GL.Translate(model.Position.X, model.Position.Y, model.Position.Z);
                GL.Rotate(model.Rotation.Z, Vector3.UnitZ);
                GL.Rotate(model.Rotation.Y, Vector3.UnitY);
                GL.Rotate(model.Rotation.X, Vector3.UnitX);
                GL.Scale(model.Scale.X, model.Scale.Y, model.Scale.Z);
                model.Paint();
                model.AnimationAfter();
                GL.PopMatrix();
                if (model.Selected)
                {
                    GL.PushMatrix();
                    model.AnimationBefore();
                    Color col = SettingsProvider.Instance.ThreeDSettings.selectionBox;
                    GL.Material(MaterialFace.FrontAndBack, MaterialParameter.AmbientAndDiffuse, new OpenTK.Graphics.Color4(0, 0, 0, 255));
                    GL.Material(MaterialFace.Front, MaterialParameter.Emission, new OpenTK.Graphics.Color4(0, 0, 0, 0));
                    GL.Material(MaterialFace.Front, MaterialParameter.Specular, new float[] { 0.0f, 0.0f, 0.0f, 1.0f });
                    GL.Material(
                        MaterialFace.Front,
                        MaterialParameter.Emission,
                        new OpenTK.Graphics.Color4(col.R, col.G, col.B, col.A));
                    GL.Begin(BeginMode.Lines);
                    GL.Vertex3(model.xMin, model.yMin, model.zMin);
                    GL.Vertex3(model.xMax, model.yMin, model.zMin);

                    GL.Vertex3(model.xMin, model.yMin, model.zMin);
                    GL.Vertex3(model.xMin, model.yMax, model.zMin);

                    GL.Vertex3(model.xMin, model.yMin, model.zMin);
                    GL.Vertex3(model.xMin, model.yMin, model.zMax);

                    GL.Vertex3(model.xMax, model.yMax, model.zMax);
                    GL.Vertex3(model.xMin, model.yMax, model.zMax);

                    GL.Vertex3(model.xMax, model.yMax, model.zMax);
                    GL.Vertex3(model.xMax, model.yMin, model.zMax);

                    GL.Vertex3(model.xMax, model.yMax, model.zMax);
                    GL.Vertex3(model.xMax, model.yMax, model.zMin);

                    GL.Vertex3(model.xMin, model.yMax, model.zMax);
                    GL.Vertex3(model.xMin, model.yMax, model.zMin);

                    GL.Vertex3(model.xMin, model.yMax, model.zMax);
                    GL.Vertex3(model.xMin, model.yMin, model.zMax);

                    GL.Vertex3(model.xMax, model.yMax, model.zMin);
                    GL.Vertex3(model.xMax, model.yMin, model.zMin);

                    GL.Vertex3(model.xMax, model.yMax, model.zMin);
                    GL.Vertex3(model.xMin, model.yMax, model.zMin);

                    GL.Vertex3(model.xMax, model.yMin, model.zMax);
                    GL.Vertex3(model.xMin, model.yMin, model.zMax);

                    GL.Vertex3(model.xMax, model.yMin, model.zMax);
                    GL.Vertex3(model.xMax, model.yMin, model.zMin);

                    GL.End();
                    model.AnimationAfter();
                    GL.PopMatrix();
                }
            }
        }
        private void DrawPrintbedFrame()
        {

            float dx1 = ps.DumpAreaLeft;
            float dx2 = dx1 + ps.DumpAreaWidth;
            float dy1 = ps.DumpAreaFront;
            float dy2 = dy1 + ps.DumpAreaDepth;
            GL.LineWidth(2f);
            GL.Hint(HintTarget.LineSmoothHint, HintMode.Nicest);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            GL.Enable(EnableCap.Blend);
            GL.Disable(EnableCap.LineSmooth);
            GL.DepthFunc(DepthFunction.Lequal);
            Color col = SettingsProvider.Instance.ThreeDSettings.printerFrame;
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.AmbientAndDiffuse, new OpenTK.Graphics.Color4(0, 0, 0, 255));
            GL.Material(MaterialFace.Front, MaterialParameter.Emission, new OpenTK.Graphics.Color4(0, 0, 0, 0));
            GL.Material(MaterialFace.Front, MaterialParameter.Specular, new float[] { 0.0f, 0.0f, 0.0f, 1.0f });
            GL.Material(
                MaterialFace.Front,
                MaterialParameter.Emission,
                new OpenTK.Graphics.Color4(col.R, col.G, col.B, col.A));
            if (SettingsProvider.Instance.ThreeDSettings.showPrintbed)
            {
                int i;
                // Draw origin
                GL.Disable(EnableCap.CullFace);
                GL.Begin(BeginMode.Triangles);
                GL.Normal3(0, 0, 1);
                double delta = Math.PI / 8;
                double rad = 2.5;
                for (i = 0; i < 16; i++)
                {
                    GL.Vertex3(0, 0, 0);
                    GL.Vertex3(rad * Math.Sin(i * delta), rad * Math.Cos(i * delta), 0);
                    GL.Vertex3(rad * Math.Sin((i + 1) * delta), rad * Math.Cos((i + 1) * delta), 0);
                }
                GL.End();
                GL.Begin(BeginMode.Lines);
                if (ps.printerType < 2)
                {
                    // Print cube
                    GL.Vertex3(ps.BedLeft, ps.BedFront, 0);
                    GL.Vertex3(ps.BedLeft, ps.BedFront, ps.PrintAreaHeight);
                    GL.Vertex3(ps.BedLeft + ps.PrintAreaWidth, ps.BedFront, 0);
                    GL.Vertex3(ps.BedLeft + ps.PrintAreaWidth, ps.BedFront, ps.PrintAreaHeight);
                    GL.Vertex3(ps.BedLeft, ps.BedFront + ps.PrintAreaDepth, 0);
                    GL.Vertex3(ps.BedLeft, ps.BedFront + ps.PrintAreaDepth, ps.PrintAreaHeight);
                    GL.Vertex3(ps.BedLeft + ps.PrintAreaWidth, ps.BedFront + ps.PrintAreaDepth, 0);
                    GL.Vertex3(ps.BedLeft + ps.PrintAreaWidth, ps.BedFront + ps.PrintAreaDepth, ps.PrintAreaHeight);
                    GL.Vertex3(ps.BedLeft, ps.BedFront, ps.PrintAreaHeight);
                    GL.Vertex3(ps.BedLeft + ps.PrintAreaWidth, ps.BedFront, ps.PrintAreaHeight);
                    GL.Vertex3(ps.BedLeft + ps.PrintAreaWidth, ps.BedFront, ps.PrintAreaHeight);
                    GL.Vertex3(ps.BedLeft + ps.PrintAreaWidth, ps.BedFront + ps.PrintAreaDepth, ps.PrintAreaHeight);
                    GL.Vertex3(ps.BedLeft + ps.PrintAreaWidth, ps.BedFront + ps.PrintAreaDepth, ps.PrintAreaHeight);
                    GL.Vertex3(ps.BedLeft, ps.BedFront + ps.PrintAreaDepth, ps.PrintAreaHeight);
                    GL.Vertex3(ps.BedLeft, ps.BedFront + ps.PrintAreaDepth, ps.PrintAreaHeight);
                    GL.Vertex3(ps.BedLeft, ps.BedFront, ps.PrintAreaHeight);

                    GL.Vertex3(ps.BedLeft, ps.BedFront, 0);
                    GL.Vertex3(ps.BedLeft + ps.PrintAreaWidth, ps.BedFront, 0);
                    GL.Vertex3(ps.BedLeft + ps.PrintAreaWidth, ps.BedFront, 0);
                    GL.Vertex3(ps.BedLeft + ps.PrintAreaWidth, ps.BedFront + ps.PrintAreaDepth, 0);
                    GL.Vertex3(ps.BedLeft + ps.PrintAreaWidth, ps.BedFront + ps.PrintAreaDepth, 0);
                    GL.Vertex3(ps.BedLeft, ps.BedFront + ps.PrintAreaDepth, 0);
                    GL.Vertex3(ps.BedLeft, ps.BedFront + ps.PrintAreaDepth, 0);
                    GL.Vertex3(ps.BedLeft, ps.BedFront, 0);


                    
                    if (ps.printerType == 1)
                    {
                        if (dy1 != 0)
                        {
                            GL.Vertex3(ps.BedLeft + dx1, ps.BedFront + dy1, 0);
                            GL.Vertex3(ps.BedLeft + dx2, ps.BedFront + dy1, 0);
                        }
                        GL.Vertex3(ps.BedLeft + dx2, ps.BedFront + dy1, 0);
                        GL.Vertex3(ps.BedLeft + dx2, ps.BedFront + dy2, 0);
                        GL.Vertex3(ps.BedLeft + dx2, ps.BedFront + dy2, 0);
                        GL.Vertex3(ps.BedLeft + dx1, ps.BedFront + dy2, 0);
                        GL.Vertex3(ps.BedLeft + dx1, ps.BedFront + dy2, 0);
                        GL.Vertex3(ps.BedLeft + dx1, ps.BedFront + dy1, 0);
                    }
                    float dx = 10; // ps.PrintAreaWidth / 20f;
                    float dy = 10; // ps.PrintAreaDepth / 20f;
                    float x, y;





                    /*
                    for (i = 0; i < 200; i++)
                    {
                        x = (float)i * dx;
                        if (x >= ps.PrintAreaWidth)
                            x = ps.PrintAreaWidth;
                        if (ps.printerType == 1 && x >= dx1 && x <= dx2)
                        {
                            GL.Vertex3(ps.BedLeft + x, ps.BedFront, 0);
                            GL.Vertex3(ps.BedLeft + x, ps.BedFront + dy1, 0);
                            GL.Vertex3(ps.BedLeft + x, ps.BedFront + dy2, 0);
                            GL.Vertex3(ps.BedLeft + x, ps.BedFront + ps.PrintAreaDepth, 0);
                        }
                        else
                        {
                            GL.Vertex3(ps.BedLeft + x, ps.BedFront, 0);
                            GL.Vertex3(ps.BedLeft + x, ps.BedFront + ps.PrintAreaDepth, 0);
                        }
                        if (x >= ps.PrintAreaWidth) break;
                    }
                    for (i = 0; i < 200; i++)
                    {
                        y = (float)i * dy;
                        if (y > ps.PrintAreaDepth)
                            y = ps.PrintAreaDepth;
                        if (ps.printerType == 1 && y >= dy1 && y <= dy2)
                        {
                            GL.Vertex3(ps.BedLeft, ps.BedFront + y, 0);
                            GL.Vertex3(ps.BedLeft + dx1, ps.BedFront + y, 0);
                            GL.Vertex3(ps.BedLeft + dx2, ps.BedFront + y, 0);
                            GL.Vertex3(ps.BedLeft + ps.PrintAreaWidth, ps.BedFront + y, 0);
                        }
                        else
                        {
                            GL.Vertex3(ps.BedLeft, ps.BedFront + y, 0);
                            GL.Vertex3(ps.BedLeft + ps.PrintAreaWidth, ps.BedFront + y, 0);
                        }
                        if (y >= ps.PrintAreaDepth)
                            break;
                    }
                    */
                }
                else if (ps.printerType == 2) // Cylinder shape
                {
                    int ncirc = 32;
                    int vertexevery = 4;
                    delta = (float)(Math.PI * 2 / ncirc);
                    float alpha = 0;
                    for (i = 0; i < ncirc; i++)
                    {
                        float alpha2 = (float)(alpha + delta);
                        float x1 = (float)(ps.rostockRadius * Math.Sin(alpha));
                        float y1 = (float)(ps.rostockRadius * Math.Cos(alpha));
                        float x2 = (float)(ps.rostockRadius * Math.Sin(alpha2));
                        float y2 = (float)(ps.rostockRadius * Math.Cos(alpha2));
                        GL.Vertex3(x1, y1, 0);
                        GL.Vertex3(x2, y2, 0);
                        GL.Vertex3(x1, y1, ps.rostockHeight);
                        GL.Vertex3(x2, y2, ps.rostockHeight);
                        if ((i % vertexevery) == 0)
                        {
                            GL.Vertex3(x1, y1, 0);
                            GL.Vertex3(x1, y1, ps.rostockHeight);
                        }
                        alpha = alpha2;
                    }
                    delta = 10;
                    float x = (float)(Math.Floor(ps.rostockRadius / delta) * delta);
                    while (x > -ps.rostockRadius)
                    {
                        alpha = (float)Math.Acos(x / ps.rostockRadius);
                        float y = (float)(ps.rostockRadius * Math.Sin(alpha));
                        GL.Vertex3(x, -y, 0);
                        GL.Vertex3(x, y, 0);
                        GL.Vertex3(y, x, 0);
                        GL.Vertex3(-y, x, 0);
                        x -= (float)delta;
                    }
                }
                else if (ps.printerType == 3)
                {
                    float dx = 10; // ps.PrintAreaWidth / 20f;
                    float dy = 10; // ps.PrintAreaDepth / 20f;
                    float x, y;
                    for (i = 0; i < 200; i++)
                    {
                        x = (float)i * dx;
                        if (x >= ps.PrintAreaWidth)
                            x = ps.PrintAreaWidth;
                        if (ps.printerType == 1 && x >= dx1 && x <= dx2)
                        {
                            GL.Vertex3(ps.BedLeft + x, ps.BedFront, 0);
                            GL.Vertex3(ps.BedLeft + x, ps.BedFront + dy1, 0);
                            GL.Vertex3(ps.BedLeft + x, ps.BedFront + dy2, 0);
                            GL.Vertex3(ps.BedLeft + x, ps.BedFront + ps.PrintAreaDepth, 0);
                        }
                        else
                        {
                            GL.Vertex3(ps.BedLeft + x, ps.BedFront, 0);
                            GL.Vertex3(ps.BedLeft + x, ps.BedFront + ps.PrintAreaDepth, 0);
                        }
                        if (x >= ps.PrintAreaWidth) break;
                    }
                    for (i = 0; i < 200; i++)
                    {
                        y = (float)i * dy;
                        if (y > ps.PrintAreaDepth)
                            y = ps.PrintAreaDepth;
                        if (ps.printerType == 1 && y >= dy1 && y <= dy2)
                        {
                            GL.Vertex3(ps.BedLeft, ps.BedFront + y, 0);
                            GL.Vertex3(ps.BedLeft + dx1, ps.BedFront + y, 0);
                            GL.Vertex3(ps.BedLeft + dx2, ps.BedFront + y, 0);
                            GL.Vertex3(ps.BedLeft + ps.PrintAreaWidth, ps.BedFront + y, 0);
                        }
                        else
                        {
                            GL.Vertex3(ps.BedLeft, ps.BedFront + y, 0);
                            GL.Vertex3(ps.BedLeft + ps.PrintAreaWidth, ps.BedFront + y, 0);
                        }
                        if (y >= ps.PrintAreaDepth)
                            break;
                    }
                }
                GL.End();
                GL.DepthFunc(DepthFunction.Less);
            }
        }

        private void DrawPrintbedBase()
        {
            float dx1 = ps.DumpAreaLeft;
            float dx2 = dx1 + ps.DumpAreaWidth;
            float dy1 = ps.DumpAreaFront;
            float dy2 = dy1 + ps.DumpAreaDepth;

            if (SettingsProvider.Instance.ThreeDSettings.showPrintbed)
            {
                GL.Disable(EnableCap.CullFace);
                GL.Enable(EnableCap.Blend);	// Turn Blending On
                GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
                GL.DepthFunc(DepthFunction.Less);
                //GL.Disable(EnableCap.Lighting);
                // Draw bottom
                Color col = SettingsProvider.Instance.ThreeDSettings.printerBase;
                float[] transblack = new float[] { 0, 0, 0, 0 };
                GL.Material(MaterialFace.FrontAndBack, MaterialParameter.AmbientAndDiffuse, new OpenTK.Graphics.Color4(col.R, col.G, col.B, 130));
                GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Specular, transblack);
                GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Emission, transblack);
                GL.PushMatrix();
                if (cam.phi < Math.PI / 2)
                    GL.Translate(0, 0, -0.04);
                else
                    GL.Translate(0, 0, +0.04);
                if (ps.printerType < 2 || ps.printerType == 3)
                {
                    GL.Begin(BeginMode.Quads);
                    GL.Normal3(0, 0, 1);

                    if (ps.printerType == 1)
                    {
                        if (dy1 > 0)
                        {
                            GL.Vertex3(ps.BedLeft, ps.BedFront, 0);
                            GL.Vertex3(ps.BedLeft + ps.PrintAreaWidth, ps.BedFront, 0);
                            GL.Vertex3(ps.BedLeft + ps.PrintAreaWidth, ps.BedFront + dy1, 0);
                            GL.Vertex3(ps.BedLeft, ps.BedFront + dy1, 0);
                        }
                        if (dy2 < ps.PrintAreaDepth)
                        {
                            GL.Vertex3(ps.BedLeft, ps.BedFront + dy2, 0);
                            GL.Vertex3(ps.BedLeft + ps.PrintAreaWidth, ps.BedFront + dy2, 0);
                            GL.Vertex3(ps.BedLeft + ps.PrintAreaWidth, ps.BedFront + ps.PrintAreaDepth, 0);
                            GL.Vertex3(ps.BedLeft, ps.BedFront + ps.PrintAreaDepth, 0);
                        }
                        if (dx1 > 0)
                        {
                            GL.Vertex3(ps.BedLeft, ps.BedFront + dy1, 0);
                            GL.Vertex3(ps.BedLeft + dx1, ps.BedFront + dy1, 0);
                            GL.Vertex3(ps.BedLeft + dx1, ps.BedFront + dy2, 0);
                            GL.Vertex3(ps.BedLeft, ps.BedFront + dy2, 0);
                        }
                        if (dx2 < ps.PrintAreaWidth)
                        {
                            GL.Vertex3(ps.BedLeft + dx2, ps.BedFront + dy1, 0);
                            GL.Vertex3(ps.BedLeft + ps.PrintAreaWidth, ps.BedFront + dy1, 0);
                            GL.Vertex3(ps.BedLeft + ps.PrintAreaWidth, ps.BedFront + dy2, 0);
                            GL.Vertex3(ps.BedLeft + dx2, ps.BedFront + dy2, 0);
                        }
                    }
                    else
                    {
                        GL.Vertex3(ps.BedLeft, ps.BedFront, 0);
                        GL.Vertex3(ps.BedLeft + ps.PrintAreaWidth, ps.BedFront, 0);
                        GL.Vertex3(ps.BedLeft + ps.PrintAreaWidth, ps.BedFront + ps.PrintAreaDepth, 0);
                        GL.Vertex3(ps.BedLeft + 0, ps.BedFront + ps.PrintAreaDepth, 0);
                    }

                    GL.End();
                }
                else if (ps.printerType == 2)
                {
                    int ncirc = 32;
                    float delta = (float)(Math.PI * 2 / ncirc);
                    float alpha = 0;
                    GL.Begin(BeginMode.Quads);
                    GL.Normal3(0, 0, 1);
                    for (int i = 0; i < ncirc / 4; i++)
                    {
                        float alpha2 = (float)(alpha + delta);
                        float x1 = (float)(ps.rostockRadius * Math.Sin(alpha));
                        float y1 = (float)(ps.rostockRadius * Math.Cos(alpha));
                        float x2 = (float)(ps.rostockRadius * Math.Sin(alpha2));
                        float y2 = (float)(ps.rostockRadius * Math.Cos(alpha2));
                        GL.Vertex3(x1, y1, 0);
                        GL.Vertex3(x2, y2, 0);
                        GL.Vertex3(-x2, y2, 0);
                        GL.Vertex3(-x1, y1, 0);
                        GL.Vertex3(x1, -y1, 0);
                        GL.Vertex3(x2, -y2, 0);
                        GL.Vertex3(-x2, -y2, 0);
                        GL.Vertex3(-x1, -y1, 0);
                        alpha = alpha2;
                    }
                    GL.End();
                }
                GL.PopMatrix();
                GL.Disable(EnableCap.Blend);

            }

        }

        private ThreeDModel Picktest(Geom3DLine pickLine, int x, int y)
        {
            if (CurrentView == null)
                return null;
            // int x = Mouse.X;
            // int y = Mouse.Y;
            // Console.WriteLine("X:" + x + " Y:" + y);
//            gl.MakeCurrent();
            framebufferHandler.MakeCurrent();
            uint[] selectBuffer = new uint[128];
            GL.SelectBuffer(128, selectBuffer);
            GL.RenderMode(RenderingMode.Select);
            SetupViewport();

            GL.MatrixMode(MatrixMode.Projection);
            GL.PushMatrix();
            GL.LoadIdentity();

            int[] viewport = new int[4];
            GL.GetInteger(GetPName.Viewport, viewport);

            Matrix4 m = GluPickMatrix(x, viewport[3] - y, 1, 1, viewport);
            GL.MultMatrix(ref m);


            //GluPerspective(45, 32 / 24, 0.1f, 100.0f);
            //Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, 1, 0.1f, 100.0f);
            GL.MultMatrix(ref persp);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.ClearColor(SettingsProvider.Instance.ThreeDSettings.backgroundTop);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.DepthTest);
            Vector3 camPos = cam.CameraPosition;
            lookAt = Matrix4.LookAt(camPos.X, camPos.Y, camPos.Z, cam.viewCenter.X, cam.viewCenter.Y, cam.viewCenter.Z, 0, 0, 1.0f);

            // Intersection on bottom plane

            int window_y = (viewport[3] - y) - viewport[3] / 2;
            double norm_y = (double)window_y / (double)(viewport[3] / 2);
            int window_x = x - viewport[2] / 2;
            double norm_x = (double)window_x / (double)(viewport[2] / 2);
            float fpy = (float)(nearHeight * 0.5 * norm_y) * 1f;//(SettingsProvider.Instance.ThreeDSettings.parallelProjection ? 1f : 1f);
            float fpx = (float)(nearHeight * 0.5 * aspectRatio * norm_x) * 1f;//(SettingsProvider.Instance.ThreeDSettings.parallelProjection ? 1f : 1f);


            Vector4 frontPointN = (SettingsProvider.Instance.ThreeDSettings.parallelProjection ? new Vector4(fpx, fpy, 0, 1) : new Vector4(0, 0, 0, 1));
            Vector4 dirN = (SettingsProvider.Instance.ThreeDSettings.parallelProjection ? new Vector4(0, 0, -nearDist, 0) : new Vector4(fpx, fpy, -nearDist, 0));
            //Matrix4 trans = Matrix4.CreateTranslation(-ps.BedLeft - ps.PrintAreaWidth * 0.5f, -ps.BedFront - ps.PrintAreaDepth * 0.5f, -0.5f * ps.PrintAreaHeight);
            Matrix4 ntrans = lookAt;
            // ntrans = Matrix4.Mult(trans, ntrans);
            ntrans = Matrix4.Invert(ntrans);
            Vector4 frontPoint = (SettingsProvider.Instance.ThreeDSettings.parallelProjection ? Vector4.Transform(frontPointN, ntrans) : ntrans.Row3);
            Vector4 dirVec = Vector4.Transform(dirN, ntrans);

            dirN = new Vector4(0, 0, -nearDist, 0);
            dirVec = Vector4.Transform(dirN, ntrans);
            viewLine = new Geom3DLine(new Geom3DVector(frontPoint.X / frontPoint.W, frontPoint.Y / frontPoint.W, frontPoint.Z / frontPoint.W),
                new Geom3DVector(dirVec.X, dirVec.Y, dirVec.Z), true);
            viewLine.dir.normalize();
            /* Geom3DPlane plane = new Geom3DPlane(new Geom3DVector(0, 0, 0), new Geom3DVector(0, 0, 1));
             Geom3DVector cross = new Geom3DVector(0, 0, 0);
             plane.intersectLine(pickLine, cross);
             Main.conn.log("Linie: " + pickLine, false, 3);
             Main.conn.log("Schnittpunkt: " + cross, false, 3);
             */
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookAt);
            //GL.Translate(-ps.BedLeft - ps.PrintAreaWidth * 0.5f, -ps.BedFront - ps.PrintAreaDepth * 0.5f, -0.5f * ps.PrintAreaHeight);

            GL.InitNames();
            int pos = 0;
            foreach (ThreeDModel model in CurrentView.models)
            {
                GL.PushName(pos++);
                GL.PushMatrix();
                model.AnimationBefore();
                GL.Translate(model.Position.X, model.Position.Y, model.Position.Z);
                GL.Rotate(model.Rotation.Z, Vector3.UnitZ);
                GL.Rotate(model.Rotation.Y, Vector3.UnitY);
                GL.Rotate(model.Rotation.X, Vector3.UnitX);
                GL.Scale(model.Scale.X, model.Scale.Y, model.Scale.Z);
                model.Paint();
                model.AnimationAfter();
                GL.PopMatrix();
                GL.PopName();
            }
            GL.MatrixMode(MatrixMode.Projection);
            GL.PopMatrix();
            GL.MatrixMode(MatrixMode.Modelview);

            int hits = GL.RenderMode(RenderingMode.Render);
            ThreeDModel selected = null;
            if (hits > 0)
            {
                selected = CurrentView.models.ElementAt((int)selectBuffer[3]);
                lastDepth = selectBuffer[1];
                for (int i = 1; i < hits; i++)
                {
                    if (selectBuffer[4 * i + 1] < lastDepth)
                    {
                        lastDepth = selectBuffer[i * 4 + 1];
                        selected = CurrentView.models.ElementAt((int)selectBuffer[i * 4 + 3]);
                    }
                }
                double dfac = (double)lastDepth / uint.MaxValue;
                dfac = -(farDist * nearDist) / (dfac * (farDist - nearDist) - farDist);
                Geom3DVector crossPlanePoint = new Geom3DVector(viewLine.dir).scale((float)dfac).add(viewLine.point);
                Geom3DPlane objplane = new Geom3DPlane(crossPlanePoint, viewLine.dir);
                objplane.intersectLine(pickLine, pickPoint);
                //Main.conn.log("Objekttreffer: " + pickPoint, false, 3);

            }
            //PrinterConnection.logInfo("Hits: " + hits);
            return selected;
        }

        private Matrix4 GluPickMatrix(float x, float y, float width, float height, int[] viewport)
        {
            Matrix4 result = Matrix4.Identity;
            if ((width <= 0.0f) || (height <= 0.0f))
            {
                return result;
            }

            float translateX = (viewport[2] - (2.0f * (x - viewport[0]))) / width;
            float translateY = (viewport[3] - (2.0f * (y - viewport[1]))) / height;
            result = Matrix4.Mult(Matrix4.CreateTranslation(translateX, translateY, 0.0f), result);
            float scaleX = viewport[2] / width;
            float scaleY = viewport[3] / height;
            result = Matrix4.Mult(Matrix4.Scale(scaleX, scaleY, 1.0f), result);
            return result;
        }


        private void gl_Paint()
        {
            if (CurrentView == null) return;
            try
            {
                if (!loaded) return;
                DetectDrawingMethod();
//**                if (oldCut != Main.main.objectPlacement.checkCutFaces.Checked || oldPosition != Main.main.objectPlacement.cutPositionSlider.Value
//**                    || oldInclination != Main.main.objectPlacement.cutInclinationSlider.Value || oldAzimuth != Main.main.objectPlacement.cutAzimuthSlider.Value || updateCuts)
                {
  //                  UpdateCutData();
                }
                
                fpsTimer.Reset();
                fpsTimer.Start();
                framebufferHandler.MakeCurrent();
//**                gl.MakeCurrent();
                //GL.Enable(EnableCap.Multisample);
           //     GL.ClearColor(SettingsProvider.Instance.ThreeDSettings.backgroundTop);

                GL.ClearColor(new Color4(0.254f, 0.254f, 0.254f, 1.0f)); // (100, 100, 100, 255));

                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                // Draw gradient background
                GL.MatrixMode(MatrixMode.Projection);
                GL.LoadIdentity();
                GL.MatrixMode(MatrixMode.Modelview);
                GL.LoadIdentity();
                GL.Disable(EnableCap.DepthTest);
                GL.Disable(EnableCap.Lighting);
                /*
                
                GL.Begin(BeginMode.Quads);
                GL.Color4(convertColor(SettingsProvider.Instance.ThreeDSettings.backgroundBottom));
                GL.Vertex2(-1.0, -1.0);
                GL.Vertex2(1.0, -1.0);
                GL.Color4(convertColor(SettingsProvider.Instance.ThreeDSettings.backgroundTop));
                GL.Vertex2(1.0, 1.0);
                GL.Vertex2(-1.0, 1.0);
                GL.End();
                */
                

                GL.Enable(EnableCap.DepthTest);
                SetupViewport();
                Vector3 camPos = cam.CameraPosition;
                lookAt = Matrix4.LookAt(camPos.X, camPos.Y, camPos.Z, cam.viewCenter.X, cam.viewCenter.Y, cam.viewCenter.Z, 0, 0, 1.0f);
                GL.MatrixMode(MatrixMode.Modelview);
                GL.LoadMatrix(ref lookAt);
                GL.ShadeModel(ShadingModel.Smooth);

                AddLights();
                
                GL.Enable(EnableCap.CullFace);
                GL.Enable(EnableCap.Blend);
                GL.LineWidth(2f);
                GL.Hint(HintTarget.LineSmoothHint, HintMode.Nicest);
                GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
                Color col = SettingsProvider.Instance.ThreeDSettings.printerBase;
                GL.GetFloat(GetPName.ModelviewMatrix, out modelView);
                GL.Material(MaterialFace.Front,MaterialParameter.Specular,new OpenTK.Graphics.Color4(255, 255, 255, 255));

                DrawModels();
                DrawPrintbedFrame();
              //  DrawPrintbedBase();
               // DrawSkyBox();
                DrawCube();
                //DrawScene();
                if (!CheckUniformButtonState(MouseButtonState.Released)) // Control.MouseButtons != MouseButtons.None)
                     DrawViewpoint();
                DrawCoordinate();
             //   DrawManipulate();

//*                gl.SwapBuffers();
                framebufferHandler.SwapBuffers();
                fpsTimer.Stop();
                
                 double time = fpsTimer.Elapsed.Milliseconds / 1000.0;
                // PrinterConnection.logInfo("OpenGL update time:" + time.ToString());
                double framerate = 1.0 / fpsTimer.Elapsed.TotalSeconds;
                //Main.main.fpsLabel.Text = framerate.ToString("0") + " FPS";
                if (framerate < 30 && SettingsProvider.Instance.Global_Settings.DisableQualityReduction == false)
                {
                    slowCounter++;
                    if (slowCounter >= 10)
                    {
                        slowCounter = 0;
                        foreach (ThreeDModel model in CurrentView.models)
                        {
                            model.ReduceQuality();
                        }
                    }
                }
                else if (slowCounter > 0)
                    slowCounter--;

            }
            catch { }
            updateCuts = false;
            oldCut = false; //Main.main.objectPlacement.checkCutFaces.Checked;
        }


        private int LoadCubeMap(List<string> faces)
        {
            int textureId;
            GL.GenTextures(1,out textureId);
            GL.BindTexture(TextureTarget.TextureCubeMap,textureId);
            int width, height, nrChannels;


            for (int i = 0; i < faces.Count; i++)
            {
                string face = faces[i];
                Bitmap bitmap = new Bitmap(face);

                BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                GL.TexImage2D(TextureTarget.TextureCubeMapPositiveX + i, 0, PixelInternalFormat.Rgb, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Rgb, PixelType.UnsignedByte, data.Scan0);
            }

            GL.Color3(1.0f, 1.0f, 1.0f);

            GL.TexParameterI(TextureTarget.TextureCubeMap,TextureParameterName.TextureMinFilter,new int[] { (int) TextureMinFilter.Linear });
            GL.TexParameterI(TextureTarget.TextureCubeMap,TextureParameterName.TextureMagFilter,new int[] { (int) TextureMinFilter.Linear });

            GL.TexParameterI(TextureTarget.TextureCubeMap,TextureParameterName.TextureWrapS,new int[] { (int) TextureWrapMode.ClampToEdge });
            GL.TexParameterI(TextureTarget.TextureCubeMap,TextureParameterName.TextureWrapT,new int[] { (int) TextureWrapMode.ClampToEdge });
            GL.TexParameterI(TextureTarget.TextureCubeMap,TextureParameterName.TextureWrapR,new int[] { (int) TextureWrapMode.ClampToEdge });

            return textureId;
        }


        void UpdateCutData()
        {
            updateCuts = true;
            cutBBox.Clear();
            foreach (PrintModel model in CurrentView.models) //Main.main.objectPlacement.ListObjects(false))
            {
                cutBBox.Add(model.bbox);
            }
            oldPosition = 500;//Main.main.objectPlacement.cutPositionSlider.Value;
            oldInclination = 0; //Main.main.objectPlacement.cutInclinationSlider.Value;
            oldAzimuth = 0;//Main.main.objectPlacement.cutAzimuthSlider.Value;
            double inclination = (double)oldInclination * Math.PI / 1800.0;
            double azimuth = (double)oldAzimuth * Math.PI / 1800.0;
            cutDirection.x = Math.Sin(inclination) * Math.Cos(azimuth);
            cutDirection.y = Math.Sin(inclination) * Math.Sin(azimuth);
            cutDirection.z = Math.Cos(inclination);
            RHVector3 center = cutBBox.Center;
            TopoPlane plane = new TopoPlane(cutDirection, center);
            double min = 0, max = 0, dist;
            RHVector3 p = new RHVector3(cutBBox.xMin, cutBBox.yMin, cutBBox.zMin);
            dist = plane.VertexDistance(p);
            max = Math.Max(dist, max);
            min = Math.Min(dist, min);
            p = new RHVector3(cutBBox.xMax, cutBBox.yMin, cutBBox.zMin);
            dist = plane.VertexDistance(p);
            max = Math.Max(dist, max);
            min = Math.Min(dist, min);
            p = new RHVector3(cutBBox.xMin, cutBBox.yMax, cutBBox.zMin);
            dist = plane.VertexDistance(p);
            max = Math.Max(dist, max);
            min = Math.Min(dist, min);
            p = new RHVector3(cutBBox.xMax, cutBBox.yMax, cutBBox.zMin);
            dist = plane.VertexDistance(p);
            max = Math.Max(dist, max);
            min = Math.Min(dist, min);
            p = new RHVector3(cutBBox.xMin, cutBBox.yMin, cutBBox.zMax);
            dist = plane.VertexDistance(p);
            max = Math.Max(dist, max);
            min = Math.Min(dist, min);
            p = new RHVector3(cutBBox.xMax, cutBBox.yMin, cutBBox.zMax);
            dist = plane.VertexDistance(p);
            max = Math.Max(dist, max);
            min = Math.Min(dist, min);
            p = new RHVector3(cutBBox.xMin, cutBBox.yMax, cutBBox.zMax);
            dist = plane.VertexDistance(p);
            max = Math.Max(dist, max);
            min = Math.Min(dist, min);
            p = new RHVector3(cutBBox.xMax, cutBBox.yMax, cutBBox.zMax);
            dist = plane.VertexDistance(p);
            max = Math.Max(dist, max);
            min = Math.Min(dist, min);
            double spos = min + 1.001 * (max - min) * (double)oldPosition / 1000.0;
            cutPos.x = center.x + spos * cutDirection.x;
            cutPos.y = center.y + spos * cutDirection.y;
            cutPos.z = center.z + spos * cutDirection.z;
        }

        private void InitScene()
        {
            GL.Enable(EnableCap.Texture2D);
            GL.GenTextures(1, out textureId);
            GL.BindTexture(TextureTarget.Texture2D, textureId);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            bitmap.UnlockBits(data);

        }


        public Bitmap initBitMap(string path)
        {
            Bitmap bitmap = new Bitmap(path);

            return bitmap;
        }


        public int initTexture(Bitmap bitmap)
        {
            int textur;

            GL.Enable(EnableCap.Texture2D);
            GL.GenTextures(1, out textur);
            GL.BindTexture(TextureTarget.Texture2D, textur);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            bitmap.UnlockBits(data);
            return textur;
        }


        private float _pos = 0;
        private void DrawScene()
        {

            GL.Enable(EnableCap.Texture2D);


            GL.BindTexture(TextureTarget.Texture2D, textureId);
//            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
//            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);


            GL.Begin(BeginMode.Quads);
           
       //     GL.Normal3(0.0f, 1.0f, 0.0f);

            GL.TexCoord2(4000 / FLOOR_TEXTURE_SIZE, _pos / FLOOR_TEXTURE_SIZE);
            GL.Vertex3(-2000.0f, -2000.0f, 0.0f);

            GL.TexCoord2(4000 / FLOOR_TEXTURE_SIZE, (4000 + _pos) / FLOOR_TEXTURE_SIZE);
            GL.Vertex3(-2000.0f, 2000.0f, 0.0f);

            GL.TexCoord2(0.0f, (4000 + _pos) / FLOOR_TEXTURE_SIZE);
            GL.Vertex3(2000.0f, 2000.0f, 0.0f);

            GL.TexCoord2(0.0f, _pos / FLOOR_TEXTURE_SIZE);
            GL.Vertex3(2000.0f, -2000.0f, 0.0f);

            GL.End();
        }

        public void InitSkyBox()
        {


            //glGenVertexArrays(1, &skyboxVAO);
            GL.GenVertexArrays(1, out skyboxVAO);
            
            //glGenBuffers(1, &skyboxVBO);
            GL.GenBuffers(1, out skyboxVBO);

            //glBindVertexArray(skyboxVAO);
            GL.BindVertexArray(skyboxVAO);
            
            //glBindBuffer(GL_ARRAY_BUFFER, skyboxVBO);
            GL.BindBuffer(BufferTarget.ArrayBuffer,skyboxVBO);

            //glBufferData(GL_ARRAY_BUFFER, sizeof(skyboxVertices), &skyboxVertices, GL_STATIC_DRAW);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(skyboxVertices.Length * sizeof(float)), skyboxVertices,BufferUsageHint.StaticDraw);

            //glEnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(0);

            //glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 3 * sizeof(float), (void*)0);
            GCHandle handle = GCHandle.Alloc(0, GCHandleType.Pinned);
            IntPtr address = handle.AddrOfPinnedObject();
            Gl.glVertexAttribPointer(0, 3, (int)VertexPointerType.Float, 0, 3 * sizeof(float), address);



        }

        public void DrawSkyBox()
        {

            GL.DepthFunc(DepthFunction.Lequal);
            GL.Enable(EnableCap.Texture2D);

            InitSkyBox();


            GL.BindVertexArray(skyboxVAO);
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.TextureCubeMap, textureSkyBoxId);
            GL.DrawArrays(PrimitiveType.Triangles,0,36);

            GL.BindVertexArray(0);


            GL.DepthFunc(DepthFunction.Less);

        }

        private int DrawCube()
        {
            Gl.glPushMatrix();

         //   Gl.glRotatef(90, 0, 1, 0);

            int width = 30000;
            int height = 30000;
            int length = 30000;


            int x = 0;
            int y = 0;
            int z = 0;


            x = x - width / 2;
            y = y - height / 2;
            z = z - length / 2;

            Gl.glShadeModel(Gl.GL_SMOOTH);									// Enable Smooth Shading
            Gl.glClearColor(0.254f, 0.254f, 0.254f, 1.0f);						// Black Background
//            Gl.glClearDepth(1.0f);											// Depth Buffer Setup
//            Gl.glEnable(Gl.GL_DEPTH_TEST);									// Enables Depth Testing
//            Gl.glDepthFunc(Gl.GL_LEQUAL);									// The Type Of Depth Testing To Do
//            Gl.glHint(Gl.GL_PERSPECTIVE_CORRECTION_HINT, Gl.GL_NICEST);		// Really Nice Perspective Calculations

//            GL.Disable(EnableCap.Blend);
//            Gl.glClearColor(0.402f, 0.402f, 0.402f, 1.0f);


//            Gl.glFogfv(Gl.GL_FOG_COLOR, new float[] { 1.0f, 0.412f, 0.412f, 1.0f });            // Set Fog Color

            Gl.glFogfv(Gl.GL_FOG_COLOR, new float[] { 0.254f, 0.254f, 0.254f, 1.0f });            // Set Fog Color
            Gl.glFogi(Gl.GL_FOG_MODE, Gl.GL_EXP2);        // Fog Mode
            Gl.glFogf(Gl.GL_FOG_DENSITY, 0.0025f);              // How Dense Will The Fog Be
            Gl.glHint(Gl.GL_FOG_HINT, Gl.GL_DONT_CARE);          // Fog Hint Value
            Gl.glFogf(Gl.GL_FOG_START, 100.0f);             // Fog Start Depth
            Gl.glFogf(Gl.GL_FOG_END, 17000.0f);               // Fog End Depth
            Gl.glEnable(Gl.GL_FOG);


            //Gl.glEnable(Gl.GL_LINE_SMOOTH);
            //Gl.glEnable(Gl.GL_POLYGON_SMOOTH);
            //Gl.glHint(Gl.GL_LINE_SMOOTH_HINT, Gl.GL_NICEST);
            //Gl.glHint(Gl.GL_POLYGON_SMOOTH_HINT, Gl.GL_NICEST);
            //Gl.glShadeModel(Gl.GL_SMOOTH);




            GL.Disable(EnableCap.Texture2D);


            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.AmbientAndDiffuse, new OpenTK.Graphics.Color4(0, 0, 0, 255));
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Emission, new OpenTK.Graphics.Color4(0, 0, 0, 0));
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Specular, new float[] { 0.0f, 0.0f, 0.0f, 1.0f });

        //    GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Emission, new OpenTK.Graphics.Color4(140, 190, 228, 255));

            //  Gl.glBindTexture(Gl.GL_TEXTURE_2D,TextureList[1]);


            //            GL.Enable(EnableCap.Texture2D);
            //            Gl.glBindTexture(Gl.GL_TEXTURE_2D, TextureList[3]);

            GL.Material(MaterialFace.Front, MaterialParameter.Emission, new OpenTK.Graphics.Color4(65, 65, 65, 255));

            GL.Begin(PrimitiveType.Quads);
            GL.Normal3(-1, 1, 1);
            GL.TexCoord2(1.0f, 0.0f);
            GL.Vertex3(x + width+1, y-1, z);
            GL.Normal3(-1, -1, 1);
            GL.TexCoord2(1.0f, 1.0f);
            GL.Vertex3(x + width+1, y + height+1, z);
            GL.Normal3(1, -1, 1);
            GL.TexCoord2(0.0f, 1.0f);
            GL.Vertex3(x-1, y + height+1, z);
            GL.Normal3(1, 1, 1);
            GL.TexCoord2(0.0f, 0.0f);
            GL.Vertex3(x-1, y-1, z);
            GL.End();

            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, TextureList[8]);


//            Gl.glEnable(Gl.GL_TEXTURE_2D);
//            Gl.glBindTexture(Gl.GL_TEXTURE_2D, TextureList[2]);

//            GL.Disable(EnableCap.Blend);
//            GL.Disable(EnableCap.Lighting);
//            GL.TexEnv(TextureEnvTarget.TextureEnv,TextureEnvParameter.TextureEnvMode,);
            Gl.glTexEnvi(Gl.GL_TEXTURE_ENV, Gl.GL_TEXTURE_ENV_MODE, Gl.GL_REPLACE);
            GL.Color3(1.0f, 1.0f, 1.0f);




            Gl.glBegin(Gl.GL_QUADS);
            Gl.glNormal3d(1, 1, -1);
            Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3d(x - 1, y - 1, z + length);
            Gl.glNormal3d(1, -1, -1);
            Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3d(x - 1, y + height+1, z + length);
            Gl.glNormal3d(-1, -1, -1);
            Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3d(x + width+1, y + height+1, z + length);
            Gl.glNormal3d(-1, 1, -1);
            Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3d(x + width+1, y - 1, z + length);
            Gl.glEnd();



            //*****





            GL.Color3(1.0f, 1.0f, 1.0f);

            GL.BindTexture(TextureTarget.Texture2D, TextureList[7]);

           Gl.glBegin(Gl.GL_QUADS);
           Gl.glNormal3d(-1, 1, 1);
           Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3d(x + width, y-1, z-1);
           Gl.glNormal3d(-1, 1, -1);
           Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3d(x + width, y-1, z + length+1);
           Gl.glNormal3d(-1, -1, -1);
           Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3d(x + width, y + height+1, z + length+1);
           Gl.glNormal3d(-1, -1, 1);
           Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3d(x + width, y + height+1, z-1);
           Gl.glEnd();


//            GL.Material(MaterialFace.Front,MaterialParameter.Emission,new OpenTK.Graphics.Color4(50, 50, 255, 255));
             Gl.glBindTexture(Gl.GL_TEXTURE_2D, TextureList[7]);

             Gl.glBegin(Gl.GL_QUADS);
             Gl.glNormal3d(1, -1, 1);
             Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3d(x, y + height+1, z-1);
             Gl.glNormal3d(1, -1, -1);
             Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3d(x, y + height+1, z + length+1);
             Gl.glNormal3d(1, 1, -1);
             Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3d(x, y-1, z + length+1);
             Gl.glNormal3d(1, 1, 1);
             Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3d(x, y-1, z-1);
             Gl.glEnd();


            GL.Enable(EnableCap.Texture2D);

            Gl.glBindTexture(Gl.GL_TEXTURE_2D, TextureList[7]);
//            GL.Material(MaterialFace.Front,MaterialParameter.Emission,new OpenTK.Graphics.Color4(50, 255, 50, 255));


            Gl.glBegin(Gl.GL_QUADS);
            Gl.glNormal3d(-1, -1, 1);
            Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3d(x + width+1, y + height, z-1);
            Gl.glNormal3d(-1, -1, -1);
            Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3d(x + width+1, y + height, z + length+1);
            Gl.glNormal3d(1, -1, -1);
            Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3d(x-1, y + height, z + length+1);
            Gl.glNormal3d(1, -1, 1);
            Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3d(x-1, y + height, z-1);
            Gl.glEnd();


            Gl.glBindTexture(Gl.GL_TEXTURE_2D, TextureList[7]);
           // GL.Material(MaterialFace.Front, MaterialParameter.Emission, new OpenTK.Graphics.Color4(255, 50, 255, 255));

            Gl.glBegin(Gl.GL_QUADS);
            Gl.glNormal3d(1, 1, 1);
            Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3d(x-1, y, z-1);
            Gl.glNormal3d(1, 1, -1);
            Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3d(x-1, y, z + length+1);
            Gl.glNormal3d(-1, 1, -1);
            Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3d(x + width+1, y, z + length+1);
            Gl.glNormal3d(-1, 1, 1);
            Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3d(x + width+1, y, z-1);
            Gl.glEnd();




//            Gl.glDepthMask(Gl.GL_FALSE);
//            Gl.glEnable(Gl.GL_BLEND);
//            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);

          //  Gl.glTexParameterf(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAX_ANISOTROPY_EXT, 16);
   //         GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            GL.BindTexture(TextureTarget.Texture2D, TextureList[9]);

            //            GL.Color4(1.0f, 1.0f, 1.0f,0.0f);

            int partSize = (int)(width / FLOOR_TEXTURE_SIZE);

            Gl.glBegin(Gl.GL_QUADS);

            Gl.glNormal3d(1, 1, -1);
            Gl.glTexCoord2f(partSize, 0.0f);
            Gl.glVertex3d(x - 1, y - 1, 0);

            Gl.glNormal3d(1, -1, -1);
            Gl.glTexCoord2f(partSize, partSize);
            Gl.glVertex3d(x - 1, y + height + 1, 0);

            Gl.glNormal3d(-1, -1, -1);
            Gl.glTexCoord2f(0.0f, partSize);
            Gl.glVertex3d(x + width + 1, y + height + 1, 0);

            Gl.glNormal3d(-1, 1, -1);
            Gl.glTexCoord2f(0.0f, 0.0f);
            Gl.glVertex3d(x + width + 1, y - 1, 0);

            Gl.glEnd();

//            Gl.glDisable(Gl.GL_BLEND);
//            Gl.glDepthMask(Gl.GL_TRUE);



            GL.Disable(EnableCap.Texture2D);
            GL.Color3(1.0f, 1.0f, 1.0f);


            Gl.glPopMatrix();

            Gl.glDisable(Gl.GL_FOG);
            return 6;
        }


        private void DrawCoordinate()
        {
            if (SettingsProvider.Instance.ThreeDSettings.ShowCompass)
            {
                bool firstchance = false;

                if (coord == null)
                {
                    coord = new Coordinate(this);
                    firstchance = true;
                }

                coord.Draw((int)this.view_Container.ActualWidth, (int)this.view_Container.ActualHeight, cam.phi, cam.theta, 50);

                if (firstchance)
                    render = true;
            }
            else
                coord = null;
        }

        private int manipulateMode = 0;

        private void DrawManipulate()
        {
            if (manipulateMode != 0)
            {

                if (manipulateMode == 1)
                {
                    DrawTranslateArrows();
                }
                else if (manipulateMode == 2)
                {
                    DrawScaleArrows();
                }
                else if (manipulateMode == 3)
                {
                    DrawRotateArrows();
                }

            }
        }

        private void DrawRotateArrows()
        {

        }

        private void DrawScaleArrows()
        {

        }


        private TranslateArrow tArrows = null;
        private void DrawTranslateArrows()
        {
            if (tArrows == null)
                tArrows = new TranslateArrow(this);
            tArrows.Draw((int)this.view_Container.ActualWidth, (int)this.view_Container.ActualHeight, cam.phi, cam.theta, xPos,yPos);

        }

        private void ThreeDControl_OnLoaded(object sender, RoutedEventArgs e)
        {

            cam = new ThreeDCamera(this);
            SetCameraDefaults();
            cam.OrientIsometric();

            
            loaded = true;
            render = true;
            SetupViewport();

            timer.Start();

        }

        private void ViewContainer_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
          
        }

        private void ViewContainer_OnMouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void ViewContainer_OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            zoomSpeed = -e.Delta;
            lastZoomSpeed = 0;
        }

        public void frontView()
        {
            SetCameraDefaults();
            cam.OrientFront();
            render = true;
        }
        public void backView()
        {
            SetCameraDefaults();
            cam.OrientBack();
            render = true;
        }
        public void leftView()
        {
            SetCameraDefaults();
            cam.OrientLeft();
            render = true;
        }
        public void rightView()
        {
            SetCameraDefaults();
            cam.OrientRight();
            render = true;
        }
        public void topView()
        {
            SetCameraDefaults();
            cam.OrientTop();
            render = true;
        }
        public void bottomView()
        {
            SetCameraDefaults();
            cam.OrientBottom();
            render = true;
        }
        public void isometricView()
        {
            SetCameraDefaults();
            cam.OrientIsometric();
            render = true;
        }

        public void FitPrinter()
        {
            cam.FitPrinter();
            render = true;
        }
        public void FitObjects()
        {
            cam.FitObjects();
            render = true;
        }

        public void FitSelectedObject()
        {
            if (ProjectManager.Instance.CurrentProject.SelectedModel != null)
            {
                cam.FitSelectedObject((PrintModel)ProjectManager.Instance.CurrentProject.SelectedModel);
                render = true;
            }
        }
    }

    struct Byte4
    {
        public byte R, G, B, A;

        public Byte4(byte[] input)
        {
            R = input[0];
            G = input[1];
            B = input[2];
            A = input[3];
        }

        public uint ToUInt32()
        {
            byte[] temp = new byte[] { this.R, this.G, this.B, this.A };
            return BitConverter.ToUInt32(temp, 0);
        }

        public override string ToString()
        {
            return this.R + ", " + this.G + ", " + this.B + ", " + this.A;
        }
    }
}
