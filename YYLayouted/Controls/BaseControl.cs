using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YYLayouted
{
    public abstract class BaseControl : Control, IYYLayout
    {
        ControlStates ControlState;


        public VirtualCollection VirtualControls
        {
            get
            {
                return this.InnerControl.VirtualControls;
            }
        }

        public string Text
        {
            get
            {
                return InnerControl.Text;
            }
            set
            {
                InnerControl.Text = value;
            }
        }

        protected VirtualControlBase _innerControl;

        protected abstract VirtualControlBase InnerControl { get; }

        public Bitmap DisplayImage
        {
            get { return this.InnerControl.GetDisplayImage(); }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            // SetBestSize();
            InnerControl.Bounds = ClientRectangle;
        }

        protected override void OnMouseEnter(EventArgs eventargs)
        {
            base.OnMouseEnter(eventargs);
            ControlState = ControlStates.Hover;
            InnerControl.ControlState = ControlStates.Hover;
            //InnerControl.MouseOperation(Location, MouseOperationType.Hover);
            base.Invalidate();
        }

        protected override void OnMouseLeave(EventArgs eventargs)
        {
            base.OnMouseLeave(eventargs);
            ControlState = ControlStates.Normal;
            InnerControl.ControlState = ControlStates.Normal;
            base.Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            ControlState = ControlStates.Pressed;
            InnerControl.ControlState = ControlStates.Pressed;
            base.Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            if (ClientRectangle.Contains(mevent.Location))
                ControlState = ControlStates.Hover;
            else
                ControlState = ControlStates.Normal;

            InnerControl.ControlState = ControlState;
            base.Invalidate();
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            InnerControl.Enabled = base.Enabled;
            base.Invalidate();
        }

    }

}
