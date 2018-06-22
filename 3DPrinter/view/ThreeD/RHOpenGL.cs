using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;

namespace _3DPrinter.view.ThreeD
{
    public class RHOpenGL : OpenTK.GLControl
    {
        public RHOpenGL()
            : base(new GraphicsMode(DisplayDevice.Default.BitsPerPixel, 24, 0, 4, 0, 2, false))
        //    : base(new GraphicsMode(32, 24, 8, RHOpenGL.MaxAntiAlias()))
                //    : base(new GraphicsMode(32, 24, 8, 4), 2, 0, GraphicsContextFlags.ForwardCompatible)
        {

        }
        public static int MaxAntiAlias()
        {
            var aa_modes = new List<int>();
            int aa = 0;
            do
            {
                GraphicsMode mode = new GraphicsMode(32, 8, 0, aa);
                Console.WriteLine("Samples:" + mode.Samples);
                if (!aa_modes.Contains(mode.Samples))
                    aa_modes.Add(aa);
                aa += 2;
            } while (aa <= 32);
            int best = aa_modes.Last();
            Console.WriteLine("Best AntiAlias:" + best);
            return best;
        }
        // Returns a System.Drawing.Bitmap with the contents of the current framebuffer
        // Call Dispose on bitmap when done
        public void SaveScreenshot(string file)
        {
            if (GraphicsContext.CurrentContext == null)
                throw new GraphicsContextMissingException();

            Bitmap bmp = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            System.Drawing.Imaging.BitmapData data =
                bmp.LockBits(this.ClientRectangle, System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            OpenTK.Graphics.OpenGL.GL.ReadPixels(0, 0, this.ClientSize.Width, this.ClientSize.Height, OpenTK.Graphics.OpenGL.PixelFormat.Bgr, OpenTK.Graphics.OpenGL.PixelType.UnsignedByte, data.Scan0);
            bmp.UnlockBits(data);

            bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            bmp.Save(file, ImageFormat.Png);
            bmp.Dispose();
        }
        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Right:
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                    return true;
                case Keys.Shift | Keys.Right:
                case Keys.Shift | Keys.Left:
                case Keys.Shift | Keys.Up:
                case Keys.Shift | Keys.Down:
                    return true;
            }
            return base.IsInputKey(keyData);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // RHOpenGL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "RHOpenGL";
            this.Load += new System.EventHandler(this.RHOpenGL_Load);
            this.ResumeLayout(false);

        }

        private void RHOpenGL_Load(object sender, EventArgs e)
        {

        }
    }
}
