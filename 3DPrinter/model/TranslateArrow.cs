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
    public class TranslateArrow:TransformArrows
    {
        public TranslateArrow(ThreeDControl ctrl)
            :base(ctrl)
        {

        }

        public override void DrawArrow()
        {


            GL.LineWidth(2f);
            GL.Hint(HintTarget.LineSmoothHint, HintMode.Nicest);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            GL.Enable(EnableCap.Blend);
            GL.Disable(EnableCap.LineSmooth);
            GL.DepthFunc(DepthFunction.Lequal);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.AmbientAndDiffuse, new OpenTK.Graphics.Color4(0, 0, 0, 255));
            GL.Material(MaterialFace.Front, MaterialParameter.Emission, new OpenTK.Graphics.Color4(0, 0, 0, 0));
            GL.Material(MaterialFace.Front, MaterialParameter.Specular, new float[] { 0.0f, 0.0f, 0.0f, 1.0f });
            GL.Material(MaterialFace.Front,MaterialParameter.Emission,new OpenTK.Graphics.Color4(255 ,0, 0, 255));

            float xCenter = (ProjectManager.Instance.CurrentProject.SelectedModel.xMax + ProjectManager.Instance.CurrentProject.SelectedModel.xMin) / 2;
            float yCenter = (ProjectManager.Instance.CurrentProject.SelectedModel.yMax + ProjectManager.Instance.CurrentProject.SelectedModel.yMin) / 2;
            float zCenter = (ProjectManager.Instance.CurrentProject.SelectedModel.zMax + ProjectManager.Instance.CurrentProject.SelectedModel.zMin) / 2;
            
            GL.LineWidth(3.0f);
            GL.Color3(1.0f, 0.0f, 0.0f);
            GL.Begin(PrimitiveType.Lines);
            GL.Vertex3(xCenter, yCenter, zCenter);
            GL.Vertex3(xCenter+200, yCenter, zCenter);
            GL.End();

            GL.Material(MaterialFace.Front, MaterialParameter.Emission, new OpenTK.Graphics.Color4(0, 255, 0, 255));

            GL.Begin(PrimitiveType.Lines);
            GL.Vertex3(xCenter, yCenter, zCenter);
            GL.Vertex3(xCenter, yCenter + 200, zCenter);
            GL.End();

            GL.Material(MaterialFace.Front, MaterialParameter.Emission, new OpenTK.Graphics.Color4(0, 0, 255, 255));

            GL.Begin(PrimitiveType.Lines);
                GL.Vertex3(xCenter, yCenter, zCenter);
                GL.Vertex3(xCenter, yCenter, zCenter + 200);
            GL.End();

        }



    }
}
