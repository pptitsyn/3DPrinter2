using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using _3DPrinter.model.geom;
using _3DPrinter.projectManager;
using _3DPrinter.setting;
using _3DPrinter.view.ThreeD;

namespace _3DPrinter.model
{
    public class TransformArrows
    {
        #region defines
        const int area_size = 500;
        const int margin = 16;
        const int label_font_size = 16;
        readonly Vector4 z_unit = new Vector4(0.0f, 0.0f, area_size, 0.0f);
        readonly int[] coordinate_colors = 
        {
            Submesh.ColorToRgba32(System.Drawing.Color.Red), //x axis
            Submesh.ColorToRgba32(System.Drawing.Color.Green), //y axis
            Submesh.ColorToRgba32(System.Drawing.Color.Blue) //z axis                                       
        };
        readonly OpenTK.Graphics.Color4[] label_colors = 
        {
            OpenTK.Graphics.Color4.Red, // x axis
            OpenTK.Graphics.Color4.Green, //y axis
            OpenTK.Graphics.Color4.Blue // z axis                                                        
        };
        readonly string[] names = { "data/ArrowX.stl", "data/ArrowY.stl", "data/ArrowZ.stl" };
        #endregion

        public ThreeDControl ctrl;

        public TransformArrows(ThreeDControl ctrl)
        {

            this.ctrl = ctrl;
        }

        internal void Draw(int viewport_x, int viewport_y, double rotX, double rotZ, int offsetX, int offsetY)
        {
            GL.Clear(ClearBufferMask.DepthBufferBit);
            GL.MatrixMode(MatrixMode.Projection);
            GL.PushMatrix(); // push projection
            GL.LoadIdentity();

            GL.MatrixMode(MatrixMode.Modelview);
            GL.PushMatrix(); // push modelview
            GL.LoadIdentity();

            float xCenter = (ProjectManager.Instance.CurrentProject.SelectedModel.xMax + ProjectManager.Instance.CurrentProject.SelectedModel.xMin) / 2;
            float yCenter = (ProjectManager.Instance.CurrentProject.SelectedModel.yMax + ProjectManager.Instance.CurrentProject.SelectedModel.yMin) / 2;
            float zCenter = (ProjectManager.Instance.CurrentProject.SelectedModel.zMax + ProjectManager.Instance.CurrentProject.SelectedModel.zMin) / 2;

            GL.Viewport(offsetX - 250, viewport_y-offsetY+250, area_size, area_size); // change viewport
            float length = 200;//area_size * 0.5f + margin * 2.0f;
            GL.Ortho(-length, length, -length, length, -length, length);

           // GL.Rotate(rotX / Math.PI * 180D, -1D, 0D, 0D);
          //  GL.Rotate(rotZ / Math.PI * 180D, 0D, 0D, -1D);

            DrawArrow();

            GL.Disable(EnableCap.Texture2D); // QFont doesn't restor Texture2D. 
            GL.Disable(EnableCap.Blend); // QFont doesn't restore Blend.
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.DepthTest);

            GL.Viewport(0, 0, viewport_x, viewport_y); // restore viewport

            GL.MatrixMode(MatrixMode.Modelview);
            GL.PopMatrix();
            GL.MatrixMode(MatrixMode.Projection);
            GL.PopMatrix();
        }

        public virtual void DrawArrow() { }


    }
}
