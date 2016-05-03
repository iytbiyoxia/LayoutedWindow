using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YYLayouted
{
    public partial class BaseForm : Form
    {
        public BaseForm()
        {
            InitializeComponent();
        }

        Bitmap _mainImg;
        Graphics _mainGraphics;
        Timer _paintTimer;
        Rectangle _lastInvalidate;

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            _mainImg = new Bitmap(this.Width, this.Height);
            _mainGraphics = Graphics.FromImage(_mainImg);

            _paintTimer = new Timer();
            _paintTimer.Tick += _paintTimer_Tick;
            _paintTimer.Interval = 15;
            _paintTimer.Start();

            ReDraw(this.Bounds);

            this.buttonControl1.VirtualControls.Add(new VirtualButton(this.buttonControl1) { ControlSize = new Size(20, 10), Text = "AA" });
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            e.Control.VisibleChanged += Control_VisibleChanged;
            e.Control.Invalidated += Control_Invalidated;
        }

        void Control_Invalidated(object sender, InvalidateEventArgs e)
        {
            RePaint(e.InvalidRect);
        }

        void Control_VisibleChanged(object sender, EventArgs e)
        {
        }

        void RePaint(Rectangle rect)
        {
            if (rect.IsEmpty)
                return;
            if (_lastInvalidate.IsEmpty)
            {
                _lastInvalidate = rect;
            }
            else
            {
                if (rect.Contains(_lastInvalidate))// 新的完全包住旧的
                {
                    _lastInvalidate = rect;
                }
                else if (_lastInvalidate.Contains(rect))// 旧的完全包住新的
                {

                }
                else
                {
                    int minX = rect.X < _lastInvalidate.X ? rect.X : _lastInvalidate.X;
                    int minY = rect.X < _lastInvalidate.Y ? rect.Y : _lastInvalidate.Y;
                    int maxRigth = rect.Right > _lastInvalidate.Right ? rect.Right : _lastInvalidate.Right;
                    int maxBottom = rect.Bottom > _lastInvalidate.Bottom ? rect.Bottom : _lastInvalidate.Bottom;

                    _lastInvalidate = new Rectangle(minX, minY, maxRigth - minX, maxBottom - minY);
                }
            }
        }

        void _paintTimer_Tick(object sender, EventArgs e)
        {
            var oldInv = _paintTimer.Interval;
            _paintTimer.Interval = int.MaxValue;
            if (!_lastInvalidate.IsEmpty)
            {
                ReDraw(_lastInvalidate);
            }
            _paintTimer.Interval = oldInv;
        }

        void RefreshBmp(Rectangle rect)
        {
            this.FindForm().Text = string.Format("{0},{1}", rect.Width, rect.Height);

            _mainGraphics.Clear(Color.Transparent);
            foreach (Control item in this.Controls)
            {
                var tmpRect = new Rectangle(item.Location, rect.Size);
                if (tmpRect.IntersectsWith(item.Bounds))
                {
                    // 当前控件与要重绘的区域有交集
                    if (item is IYYLayout)
                    {
                        var yyI = item as IYYLayout;
                        _mainGraphics.DrawImage(yyI.DisplayImage, item.Location);
                        //_mainGraphics.DrawString("TTT", new Font("宋体", 20), Brushes.Red, item.Location);
                    }
                    else if (item is FlowLayoutPanel)
                    {

                    }
                    else
                    {
                        item.DrawToBitmap(_mainImg, item.Bounds);
                        //_mainGraphics.DrawImage(img, item.Location);
                    }
                }
            }
        }

        /// <summary>
        /// 更新窗体
        /// </summary>
        /// <param name="bitmap"></param>
        public void ReDraw(Rectangle rect)
        {
            RefreshBmp(rect);

            var bitmap = this._mainImg;

            IntPtr oldBits = IntPtr.Zero;
            IntPtr screenDC = Win32.GetDC(IntPtr.Zero);
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr memDc = Win32.CreateCompatibleDC(screenDC);
            try
            {
                Win32.Point topLoc = new Win32.Point(Left, Top);
                Win32.Size bitMapSize = new Win32.Size(bitmap.Width, bitmap.Height);
                Win32.BLENDFUNCTION blendFunc = new Win32.BLENDFUNCTION();
                Win32.Point srcLoc = new Win32.Point(0, 0);
                hBitmap = bitmap.GetHbitmap(Color.FromArgb(0)); //创建GDI位图
                oldBits = Win32.SelectObject(memDc, hBitmap);
                blendFunc.BlendOp = Win32.AC_SRC_OVER;
                blendFunc.SourceConstantAlpha = 255;
                blendFunc.AlphaFormat = Win32.AC_SRC_ALPHA;
                blendFunc.BlendFlags = 0;

                Win32.UpdateLayeredWindow(Handle, screenDC, ref topLoc, ref bitMapSize, memDc, ref srcLoc, 0, ref blendFunc, Win32.ULW_ALPHA);  //更新窗体
            }
            finally
            {
                if (hBitmap != IntPtr.Zero)
                {
                    Win32.SelectObject(memDc, oldBits);
                    Win32.DeleteObject(hBitmap);
                }
                Win32.ReleaseDC(IntPtr.Zero, screenDC);
                Win32.DeleteDC(memDc);
            }
            // 清空
            _lastInvalidate = Rectangle.Empty;

            //   bitmap.Dispose(); //释放
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var pms = base.CreateParams;
                pms.ExStyle |= 0x80000;
                return pms;
            }
        }
    }
}
