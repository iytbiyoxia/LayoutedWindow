using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YYLayouted
{
    public class ButtonRender : ControlRenderBase
    {
        ControlStates ControlState;

        public override void OnMouseMove(MouseEventArgs e)
        {
        }
        private void DuiButton_MouseEnter(object sender, EventArgs e)
        {

        }

        private void DuiButton_MouseLeave(object sender, EventArgs e)
        {

        }

        public override void OnMouseClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.ControlState = ControlStates.Clicked;
            }
        }

        public override void OnMouseEnter(EventArgs e)
        {
            this.ControlState = ControlStates.Hover;
        }

        public override void OnMouseLeave(EventArgs e)
        {
            this.ControlState = ControlStates.Normal;
        }

        public override void OnSizeChanged(Size size)
        {
            base.Size = size;
        }
        public override void OnLocationChanged(Point location)
        {
            base.Location = location;
        }

        public override Bitmap GetDisplayImage()
        {
            var bmp = new Bitmap(base.Size.Width, base.Size.Height);
            using (var g = Graphics.FromImage(bmp))
            {
                var boundRect = new Rectangle(Point.Empty, base.Size);
                g.DrawRectangle(new Pen(Color.Red, 3), boundRect);
                boundRect.Inflate(-3, -3);
                boundRect.Offset(1, 1);
                if (ControlState == ControlStates.Normal)
                    g.FillRectangle(new SolidBrush(Color.Green), boundRect);
                else if (ControlState == ControlStates.Hover)
                    g.FillRectangle(new SolidBrush(Color.HotPink), boundRect);
                else if (ControlState == ControlStates.Clicked)
                    g.FillRectangle(new SolidBrush(Color.LightSalmon), boundRect);
            }
            return bmp;
        }
    }
}
