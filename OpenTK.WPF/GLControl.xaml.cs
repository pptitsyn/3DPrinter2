using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using OpenTK.Graphics;
using OpenTK.Platform;

namespace OpenTK.WPF
{
    /// <summary>
    /// Interaction logic for GLControl.xaml
    /// </summary>
    public partial class GLControl : UserControl
    {

        IGraphicsContext context;
        IGLControl implementation;
        GraphicsMode format;
        int major, minor;
        GraphicsContextFlags flags;
        bool? initial_vsync_value;

        bool resize_event_suppressed;
        
        readonly bool design_mode;

        DispatcherTimer timer = null;



        public GLControl()
            : this(GraphicsMode.Default)
        {

        }

        public GLControl(GraphicsMode mode)
            : this(mode, 1, 0, GraphicsContextFlags.Default)
        { }


        public GLControl(GraphicsMode mode, int major, int minor, GraphicsContextFlags flags)
        {
            if (mode == null)
                throw new ArgumentNullException("mode");

//            SetStyle(ControlStyles.Opaque, true);
//            SetStyle(ControlStyles.UserPaint, true);
//            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
//            DoubleBuffered = false;

            this.format = mode;
            this.major = major;
            this.minor = minor;
            this.flags = flags;

            // Note: the DesignMode property may be incorrect when nesting controls.
            // We use LicenseManager.UsageMode as a workaround (this only works in
            // the constructor).
            design_mode =
                DesignMode ||
                LicenseManager.UsageMode == LicenseUsageMode.Designtime;

            InitializeComponent();

            timer = new DispatcherTimer();

            Unloaded += OpenGLControl_Unloaded;
            Loaded += OpenGLControl_Loaded;

        }

        /// <summary>
        /// This method converts the output from the OpenGL render context provider to a 
        /// FormatConvertedBitmap in order to show it in the image.
        /// </summary>
        /// <param name="hBitmap">The handle of the bitmap from the OpenGL render context.</param>
        /// <returns>Returns the new format converted bitmap.</returns>
        private static FormatConvertedBitmap GetFormatedBitmapSource(IntPtr hBitmap)
        {
            //  TODO: We have to remove the alpha channel - for some reason it comes out as 0.0 
            //  meaning the drawing comes out transparent.

            FormatConvertedBitmap newFormatedBitmapSource = new FormatConvertedBitmap();
            newFormatedBitmapSource.BeginInit();
            newFormatedBitmapSource.Source = BitmapConversion.HBitmapToBitmapSource(hBitmap);
            newFormatedBitmapSource.DestinationFormat = PixelFormats.Rgb24;
            newFormatedBitmapSource.EndInit();

            return newFormatedBitmapSource;
        }

        /// <summary>
        /// Handles the Loaded event of the OpenGLControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> Instance containing the event data.</param>
        private void OpenGLControl_Loaded(object sender, RoutedEventArgs routedEventArgs)
        {
            SizeChanged += OpenGLControl_SizeChanged;


            UpdateOpenGLControl((int)RenderSize.Width, (int)RenderSize.Height);

            //  DispatcherTimer setup
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        /// <summary>
        /// Handles the Unloaded event of the OpenGLControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> Instance containing the event data.</param>
        private void OpenGLControl_Unloaded(object sender, RoutedEventArgs routedEventArgs)
        {
            SizeChanged -= OpenGLControl_SizeChanged;

            timer.Stop();
            timer.Tick -= timer_Tick;
        }


        /// <summary>
        /// Handles the SizeChanged event of the OpenGLControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.SizeChangedEventArgs"/> Instance containing the event data.</param>
        void OpenGLControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateOpenGLControl((int)e.NewSize.Width, (int)e.NewSize.Height);
        }

        private void UpdateOpenGLControl(int width, int height)
        {
            SizeChangedEventArgs e;
            // Lock on OpenGL.
            /*
            lock (GL)
            {
                GL. .SetDimensions(width, height);

                //	Set the viewport.
                gl.Viewport(0, 0, width, height);

                //  If we have a project handler, call it...
                if (width != -1 && height != -1)
                {
                    var handler = Resized;
                    if (handler != null)
                        handler(this, eventArgsFast);
                    else
                    {
                        //  Otherwise we do our own projection.
                        gl.MatrixMode(OpenGL.GL_PROJECTION);
                        gl.LoadIdentity();

                        // Calculate The Aspect Ratio Of The Window
                        gl.Perspective(45.0f, (float) width/(float) height, 0.1f, 100.0f);

                        gl.MatrixMode(OpenGL.GL_MODELVIEW);
                        gl.LoadIdentity();
                    }
                }
            }
            */
        }


        /// <summary>
        /// Handles the Tick event of the timer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void timer_Tick(object sender, EventArgs e)
        {
            //  Lock on OpenGL.
            lock (gl)
            {
                //  Start the stopwatch so that we can time the rendering.
                stopwatch.Restart();

                //  Make GL current.
                gl.MakeCurrent();

                //	If there is a draw handler, then call it.
                var handler = OpenGLDraw;
                if (handler != null)
                    handler(this, eventArgsFast);
                else
                    gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT);

                //  Draw the FPS.
                if (DrawFPS)
                {
                    gl.DrawText(5, 5, 1.0f, 0.0f, 0.0f, "Courier New", 12.0f, string.Format("Draw Time: {0:0.0000} ms ~ {1:0.0} FPS", frameTime, 1000.0 / frameTime));
                    gl.Flush();
                }

                //  Render.
                gl.Blit(IntPtr.Zero);

                switch (RenderContextType)
                {
                    case RenderContextType.DIBSection:
                        {
                            var provider = gl.RenderContextProvider as DIBSectionRenderContextProvider;
                            var hBitmap = provider.DIBSection.HBitmap;

                            if (hBitmap != IntPtr.Zero)
                            {
                                var newFormatedBitmapSource = GetFormatedBitmapSource(hBitmap);

                                //  Copy the pixels over.
                                image.Source = newFormatedBitmapSource;
                            }
                        }
                        break;
                    case RenderContextType.NativeWindow:
                        break;
                    case RenderContextType.HiddenWindow:
                        break;
                    case RenderContextType.FBO:
                        {
                            var provider = gl.RenderContextProvider as FBORenderContextProvider;
                            var hBitmap = provider.InternalDIBSection.HBitmap;

                            if (hBitmap != IntPtr.Zero)
                            {
                                var newFormatedBitmapSource = GetFormatedBitmapSource(hBitmap);

                                //  Copy the pixels over.
                                image.Source = newFormatedBitmapSource;
                            }
                        }
                        break;
                    default:
                        break;
                }

                //  Stop the stopwatch.
                stopwatch.Stop();

                //  Store the frame time.
                frameTime = stopwatch.Elapsed.TotalMilliseconds;
            }
        }


        protected Stopwatch stopwatch = new Stopwatch();

        protected double frameTime = 0;


        IGLControl Implementation
        {
            get
            {
                ValidateState();

                return implementation;
            }
        }

        void ValidateState()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().Name);

            if (!IsHandleCreated)
                CreateControl();

            if (implementation == null || context == null || context.IsDisposed)
                RecreateHandle();
        }


        #region --- Public Methods ---

        #region public void SwapBuffers()

        /// <summary>
        /// Swaps the front and back buffers, presenting the rendered scene to the screen.
        /// </summary>
        public void SwapBuffers()
        {
            ValidateState();
            Context.SwapBuffers();
        }

        #endregion

        #region public void MakeCurrent()

        /// <summary>
        /// Makes the underlying this GLControl current in the calling thread.
        /// All OpenGL commands issued are hereafter interpreted by this GLControl.
        /// </summary>
        public void MakeCurrent()
        {
            ValidateState();
            Context.MakeCurrent(Implementation.WindowInfo);
        }

        #endregion

        #region public bool IsIdle

        /// <summary>
        /// Gets a value indicating whether the current thread contains pending system messages.
        /// </summary>
        [Browsable(false)]
        public bool IsIdle
        {
            get
            {
                ValidateState();
                return Implementation.IsIdle;
            }
        }

        #endregion

        #region public IGraphicsContext Context

        /// <summary>
        /// Gets an interface to the underlying GraphicsContext used by this GLControl.
        /// </summary>
        [Browsable(false)]
        public IGraphicsContext Context
        {
            get
            {
                ValidateState();
                return context;
            }
            private set { context = value; }
        }

        #endregion

        #region public float AspectRatio

        /// <summary>
        /// Gets the aspect ratio of this GLControl.
        /// </summary>
        [Description("The aspect ratio of the client area of this GLControl.")]
        public float AspectRatio
        {
            get
            {
                ValidateState();
                return (float)Width / (float)Height;
            }
        }

        #endregion

        #region public bool VSync

        /// <summary>
        /// Gets or sets a value indicating whether vsync is active for this GLControl.
        /// </summary>
        [Description("Indicates whether GLControl updates are synced to the monitor's refresh rate.")]
        public bool VSync
        {
            get
            {
                if (!IsHandleCreated)
                    return false;

                ValidateState();
                return Context.VSync;
            }
            set
            {
                // The winforms designer sets this to false by default which forces control creation.
                // However, events are typically connected after the VSync = false assignment, which
                // can lead to "event xyz is not fired" issues.
                // Work around this issue by deferring VSync mode setting to the HandleCreated event.
                if (!IsHandleCreated)
                {
                    initial_vsync_value = value;
                    return;
                }

                ValidateState();
                Context.VSync = value;
            }
        }

        #endregion

        #region public GraphicsMode GraphicsMode

        /// <summary>
        /// Gets the GraphicsMode of the GraphicsContext attached to this GLControl.
        /// </summary>
        /// <remarks>
        /// To change the GraphicsMode, you must destroy and recreate the GLControl.
        /// </remarks>
        public GraphicsMode GraphicsMode
        {
            get
            {
                ValidateState();
                return Context.GraphicsMode;
            }
        }

        #endregion

        #region WindowInfo

        /// <summary>
        /// Gets the <see cref="OpenTK.Platform.IWindowInfo"/> for this instance.
        /// </summary>
        public IWindowInfo WindowInfo
        {
            get { return implementation.WindowInfo; }
        }

        #endregion

        #endregion


    }

}
