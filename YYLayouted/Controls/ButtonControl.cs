using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YYLayouted
{
    public class ButtonControl : BaseControl
    {
        public ControlStates ControlState { get; set; }


        ControlRenderBase _render;
        public override ControlRenderBase Render
        {
            get
            {
                if (_render == null)
                    _render = new ButtonRender();
                return _render;
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            ControlState = ControlStates.Hover;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            ControlState = ControlStates.Normal;
        }

        protected override void OnMouseClick(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                ControlState = ControlStates.Clicked;
            }
        }
    }
}
