using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics;
using OpenTK.Platform;

namespace OpenTK.WPF
{
    internal interface IGLControl
    {
        IGraphicsContext CreateContext(int major, int minor, GraphicsContextFlags flags);
        bool IsIdle { get; }
        IWindowInfo WindowInfo { get; }
    }
}
