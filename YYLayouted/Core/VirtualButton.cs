using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YYLayouted
{
    public class VirtualButton : VirtualControlBase
    {
        public VirtualButton(object parent)
            : base(parent)
        {

        }

        //ControlStates ControlState;

        ////private void DuiButton_MouseEnter(object sender, EventArgs e)
        ////{

        ////}

        ////private void DuiButton_MouseLeave(object sender, EventArgs e)
        ////{

        ////}

        //protected override void OnMouseDown(MouseEventArgs e)
        //{
        //    base.OnMouseDown(e);
        //    if (e.Button == MouseButtons.Left)
        //    {
        //        this.ControlState = ControlStates.Pressed;
        //    }
        //}

        //protected override void OnMouseUp(MouseEventArgs e)
        //{
        //    base.OnMouseUp(e);
        //    this.ControlState = ControlStates.Hover;
        //}

        //protected override void OnMouseLeave(MouseEventArgs e)
        //{
        //    base.OnMouseLeave(e);
        //    this.ControlState = ControlStates.Normal;
        //}

        //protected override void OnMouseMove(MouseEventArgs e)
        //{
        //    base.OnMouseMove(e);
        //    this.ControlState = ControlStates.Hover;
        //}

        //protected override void OnMouseWheel(MouseEventArgs e)
        //{
        //    base.OnMouseWheel(e);
        //} 

        public override Bitmap GetDisplayImage()
        {
            var bmp = new Bitmap(base.ControlSize.Width, base.ControlSize.Height);
            using (var g = Graphics.FromImage(bmp))
            {
                var boundRect = new Rectangle(Point.Empty, base.ControlSize);
                g.DrawRectangle(new Pen(Color.Red, 3), boundRect);
                boundRect.Inflate(-3, -3);
                boundRect.Offset(1, 1);
                if (ControlState == ControlStates.Normal)
                    g.FillRectangle(new SolidBrush(Color.Green), boundRect);
                else if (ControlState == ControlStates.Hover)
                    g.FillRectangle(new SolidBrush(Color.Coral), boundRect);
                else if (ControlState == ControlStates.Pressed)
                    g.FillRectangle(new SolidBrush(Color.Blue), boundRect);


                g.DrawString(Text, new Font("宋体", 15), new SolidBrush(Color.Red), Location);

            }
            return bmp;
        }
    }
}
