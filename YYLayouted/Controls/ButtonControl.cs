using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YYLayouted
{
    public class ButtonControl : BaseControl
    {
         
        //public ControlStates ControlState { get; set; }


        //VirtualControlBase _render;
        //public override VirtualControlBase VirtualControl
        //{
        //    get
        //    {
        //        if (_render == null)
        //            _render = new VirtualButton();
        //        return _render;
        //    }
        //}

        //protected override void OnMouseEnter(EventArgs e)
        //{
        //    base.OnMouseEnter(e);
        //    ControlState = ControlStates.Hover;
        //}

        //protected override void OnMouseLeave(EventArgs e)
        //{
        //    base.OnMouseLeave(e);
        //    ControlState = ControlStates.Normal;
        //}

        //protected override void OnMouseClick(System.Windows.Forms.MouseEventArgs e)
        //{
        //    base.OnMouseClick(e);
        //    if (e.Button == System.Windows.Forms.MouseButtons.Left)
        //    {
        //        ControlState = ControlStates.Clicked;
        //    }
        //}

        protected override VirtualControlBase InnerControl
        {
            get
            {
                if (_innerControl == null)
                {
                    _innerControl = new VirtualButton(this);
                    _innerControl.Bounds = ClientRectangle;
                }
                return _innerControl;
            }
        }
    }
}
