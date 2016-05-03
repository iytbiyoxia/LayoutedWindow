using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YYLayouted
{
    public abstract class ControlRenderBase
    {
        public Size Size { get; set; }
        public Point Location { get; set; }

        public object Parent { get; set; }


        public abstract Bitmap GetDisplayImage();

        public abstract void OnMouseMove(MouseEventArgs e);

        public abstract void OnMouseClick(MouseEventArgs e);

        public abstract void OnSizeChanged(Size size);

        public abstract void OnLocationChanged(Point location);

        public abstract void OnMouseEnter(EventArgs e);

        public abstract void OnMouseLeave(EventArgs e);

    }

}
