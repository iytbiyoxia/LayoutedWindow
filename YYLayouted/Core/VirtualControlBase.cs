using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YYLayouted
{
    public class VirtualControlBase
    {
        #region 属性

        public string Text { get; set; }

        Size _controlSize;
        public Size ControlSize
        {
            get { return _controlSize; }
            set
            {
                if (_controlSize != value)
                {
                    _controlSize = value;
                    _bounds = new Rectangle(_location, value);
                }
            }
        }

        private Point _location;
        public Point Location
        {
            get { return _location; }
            set
            {
                if (_location != value)
                {
                    _location = value;
                    _bounds = new Rectangle(value, _controlSize);
                }
            }
        }

        Rectangle _bounds;
        public Rectangle Bounds
        {
            get { return _bounds; }
            set
            {
                if (_bounds != value)
                {
                    _bounds = value;
                    _controlSize = value.Size;
                    _location = value.Location;
                }
            }
        }

        public ControlStates ControlState { get; set; }

        public bool Visible { get; set; }

        public bool Enabled { get; set; }

        public object Parent { get; set; }

        /// <summary>
        /// 表示捕获了鼠标焦点的控件，即鼠标在这个控件上点击且未松开
        /// </summary>
        private VirtualControlBase capturedControl;

        /// <summary>
        /// 表示最近一次鼠标在上面正常移动（按钮未按下）的控件
        /// </summary>
        private VirtualControlBase lastMouseMoveControl;

        private bool isMouseDown = false;

        List<VirtualControlBase> _realControls;

        VirtualCollection _virtualControls;
        public VirtualCollection VirtualControls
        {
            get
            {
                if (_virtualControls == null)
                {
                    _virtualControls = new VirtualCollection();
                    _virtualControls.CollectionChange += _virtualControls_CollectionChange;
                }
                return _virtualControls;
            }
        }
        #endregion

        public VirtualControlBase(object parent)
        {
            this.Parent = parent;
            _realControls = new List<VirtualControlBase>();
        }

        /// <summary>
        /// 子控件集合变化事件
        /// </summary>
        /// <param name="e"></param>
        void _virtualControls_CollectionChange(VirtualCollectionChangeArgs e)
        {
            switch (e.Action)
            {
                case VirtualCollectionChangeAction.AfterClear:
                    _realControls.Clear();
                    break;
                case VirtualCollectionChangeAction.AfterInsert:
                    {
                        VirtualControlBase ctl = e.Value as VirtualControlBase;
                        if (ctl != null)
                            _realControls.Add(ctl);
                    }
                    break;
                case VirtualCollectionChangeAction.AfterRemove:
                    {
                        VirtualControlBase ctl = e.Value as VirtualControlBase;
                        if (ctl != null)
                            _realControls.Remove(ctl);
                    }
                    break;
                case VirtualCollectionChangeAction.BeforeInsert:
                    break;
                case VirtualCollectionChangeAction.BeforeSet:
                    break;
                case VirtualCollectionChangeAction.AfterSet:
                    break;
                case VirtualCollectionChangeAction.BeforeRemove:
                    break;
                case VirtualCollectionChangeAction.BeforeClear:
                    break;
            }
        }

        public virtual Bitmap GetDisplayImage()
        {
            return null;
        }

        public void MouseOperation(MouseEventArgs e, MouseOperationType operType)
        {
            if (!Enabled || !Visible)
                return;
            switch (operType)
            {
                case MouseOperationType.Move:
                    OnMouseMove(e);
                    break;
                case MouseOperationType.Down:
                    OnMouseDown(e);
                    break;
                case MouseOperationType.Up:
                    OnMouseUp(e);
                    break;
                case MouseOperationType.Leave:
                    OnMouseLeave(e);
                    break;
                case MouseOperationType.Wheel:
                    OnMouseWheel(e);
                    break;
            }
        }

        public void MouseOperation(Point location, MouseOperationType type)
        {
            MouseOperation(new MouseEventArgs(MouseButtons.None, 0, location.X, location.Y, 0), type);
        }

        #region mouse operation

        protected virtual void OnMouseMove(MouseEventArgs e)
        {
            if (isMouseDown)
            {
                if (capturedControl != null)
                    capturedControl.MouseOperation(e, MouseOperationType.Move);
            }
            else
            {
                for (int i = _realControls.Count - 1; i >= 0; i--)
                {
                    VirtualControlBase ctl = _realControls[i];

                    if (!ctl.Visible)
                        continue;

                    if (ctl.Bounds.Contains(e.Location))
                    {
                        if (lastMouseMoveControl != null && lastMouseMoveControl != ctl)
                            lastMouseMoveControl.MouseOperation(Point.Empty, MouseOperationType.Leave);
                        ctl.MouseOperation(e, MouseOperationType.Move);
                        lastMouseMoveControl = ctl;
                        return;
                    }
                }
                if (lastMouseMoveControl != null)
                    lastMouseMoveControl.MouseOperation(Point.Empty, MouseOperationType.Leave);
                lastMouseMoveControl = null;
            }
        }

        protected virtual void OnMouseDown(MouseEventArgs e)
        {
            // 是否点击了本控件
            isMouseDown = this.Bounds.Contains(e.Location);

            for (int i = _realControls.Count - 1; i >= 0; i--)
            {
                VirtualControlBase ctl = _realControls[i];
                if (!ctl.Visible)
                    continue;
                if (ctl.Bounds.Contains(e.Location))
                {
                    if (ctl.Enabled)
                    {
                        capturedControl = ctl;
                        ctl.MouseOperation(e, MouseOperationType.Down);
                    }
                    break;
                }
            }
        }

        protected virtual void OnMouseUp(MouseEventArgs e)
        {
            isMouseDown = false;
            if (capturedControl != null)
                capturedControl.MouseOperation(e, MouseOperationType.Up);
            capturedControl = null;
        }

        protected virtual void OnMouseLeave(MouseEventArgs e)
        {
            if (lastMouseMoveControl != null)
                lastMouseMoveControl.MouseOperation(Point.Empty, MouseOperationType.Leave);
            lastMouseMoveControl = null;
            isMouseDown = false;
        }

        protected virtual void OnMouseWheel(MouseEventArgs e)
        {

        }

        #endregion

    }

}
