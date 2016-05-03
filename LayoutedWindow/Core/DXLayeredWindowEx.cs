using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LayoutedWindow 
{ 
        public abstract class DXLayeredWindowEx : NativeWindow, IDisposable
        {
            const int
                WS_POPUP = unchecked((int)0x80000000),
                WS_EX_LAYERED = 0x80000,
                ULW_ALPHA = 0x02;
            byte AC_SRC_OVER = 0x00;
            byte AC_SRC_ALPHA = 0x01;
            byte alphaCore = 255;
            Rectangle bounds = Rectangle.Empty;
            bool isVisibleCore = false;
            Control ownerCore;
            bool isDisposing;
            public void Dispose()
            {
                if (!isDisposing)
                {
                    isDisposing = true;
                    OnDisposing();
                }
            }
            protected virtual void OnDisposing()
            {
                if (IsCreated)
                    DestroyHandle();
            }
            public byte Alpha
            {
                get { return alphaCore; }
                set { alphaCore = value; }
            }
            public void Create(IWin32Window topLevel)
            {
                ownerCore = topLevel as Control;
                Create(topLevel.Handle);
            }
            IntPtr handleCore = IntPtr.Zero;
            public void Create(IntPtr handle)
            {
                if (IsCreated) return;
                this.handleCore = handle;
                CreateParams cp = new CreateParams();
                cp.Parent = handle;
                cp.Style = WS_POPUP;
                cp.ExStyle = WS_EX_LAYERED;
                cp.Caption = null;
                CreateHandle(cp);
            }
            public bool IsCreated
            {
                get { return Handle != IntPtr.Zero; }
            }
            public bool IsVisible
            {
                get { return isVisibleCore; }
            }
            public Rectangle Bounds
            {
                get { return bounds; }
            }
            public void Show(Point location)
            {
                if (IsCreated && IsVisible && location == Bounds.Location) return;
                this.bounds.Location = location;
                ShowCore();
            }
            protected virtual IntPtr hWndInsertAfter { get { return new IntPtr(-1); } }
            Size lastSize = Size.Empty;
            [SecuritySafeCritical]
            protected void ShowCore()
            {
                if (!IsCreated) return;
                this.isVisibleCore = true;
                int flags = NativeMethods.SWP.SWP_NOACTIVATE | NativeMethods.SWP.SWP_SHOWWINDOW | NativeMethods.SWP.SWP_DRAWFRAME |
                    NativeMethods.SWP.SWP_NOOWNERZORDER;
                if (this.lastSize == Size)
                {
                    flags |= NativeMethods.SWP.SWP_NOSIZE | NativeMethods.SWP.SWP_NOZORDER;
                }
                this.lastSize = Size;
                NativeMethods.SetWindowPos(Handle, hWndInsertAfter, Bounds.X, Bounds.Y, Size.Width, Size.Height, flags);
            }
            [SecuritySafeCritical]
            public void Hide()
            {
                if (!IsCreated || !IsVisible) return;
                NativeMethods.SetWindowPos(Handle, hWndInsertAfter, 0, 0, 0, 0,
                    NativeMethods.SWP.SWP_NOACTIVATE | NativeMethods.SWP.SWP_HIDEWINDOW | NativeMethods.SWP.SWP_NOSIZE | NativeMethods.SWP.SWP_NOMOVE
                    | NativeMethods.SWP.SWP_NOZORDER | NativeMethods.SWP.SWP_NOOWNERZORDER);
                this.isVisibleCore = false;
            }
            protected override void WndProc(ref Message m)
            {
                switch (m.Msg)
                {
                    case MSG.WM_NCHITTEST:
                        NCHitTest(ref m);
                        return;
                }
                base.WndProc(ref m);
            }
            void NCHitTest(ref Message m)
            {
                m.Result = new IntPtr(NativeMethods.HT.HTTRANSPARENT);
            }
            public virtual Size Size
            {
                get { return bounds.Size; }
                set
                {
                    if (Size == value) return;
                    bounds.Size = value;
                    OnWindowChanged();
                }
            }
            protected virtual void OnWindowChanged()
            {
                if (!IsCreated || !IsVisible) return;
                ShowCore();
            }
            protected virtual Point GetPaintOffset()
            {
                return new Point(0, 0);
            }
            protected virtual Control CheckOwner()
            {
                return ownerCore;
            }
            volatile int updateRequested = 0;
            public void Invalidate()
            {
                if (updateRequested > 0) return;
                Control owner = CheckOwner();
                if (owner == null)
                {
                    Update();
                    return;
                }
                updateRequested++;
                owner.BeginInvoke(new MethodInvoker(UpdateAsync));
            }
            void UpdateAsync()
            {
                Update();
                updateRequested = 0;
            }
            public void Update()
            {
                if (CheckBounds()) return;
                UpdateLayeredWindowCore(DrawToBackBuffer);
            }
            public void Clear()
            {
                if (CheckBounds()) return;
                UpdateLayeredWindowCore(null);
            }
            protected void UpdateLayeredWindowCore(Action<IntPtr> updateBackBufferCallback)
            {
                IntPtr screenDC = NativeMethods.GetDC(IntPtr.Zero);
                IntPtr backBufferDC = NativeMethods.CreateCompatibleDC(screenDC);
                IntPtr hBufferBitmap = Create32bppDIBSection(screenDC, Bounds.Width, Bounds.Height);
                IntPtr tmp = IntPtr.Zero;
                try
                {
                    tmp = NativeMethods.SelectObject(backBufferDC, hBufferBitmap);
                    if (updateBackBufferCallback != null)
                        updateBackBufferCallback(backBufferDC);
                    NativeMethods.POINT newLocation = new NativeMethods.POINT(Bounds.Location);
                    NativeMethods.SIZE newSize = new NativeMethods.SIZE(Bounds.Size);
                    NativeMethods.POINT sourceLocation = new NativeMethods.POINT(0, 0);
                    NativeMethods.BLENDFUNCTION blend = new NativeMethods.BLENDFUNCTION();
                    blend.BlendOp = AC_SRC_OVER;
                    blend.BlendFlags = 0;
                    blend.SourceConstantAlpha = Alpha;
                    blend.AlphaFormat = AC_SRC_ALPHA;
                    NativeMethods.UpdateLayeredWindow(Handle, screenDC, ref newLocation, ref newSize, backBufferDC, ref sourceLocation, 0, ref blend, ULW_ALPHA);
                }
                finally
                {
                    NativeMethods.SelectObject(backBufferDC, tmp);
                    NativeMethods.DeleteObject(hBufferBitmap);
                    NativeMethods.DeleteDC(backBufferDC);
                    NativeMethods.ReleaseDC(IntPtr.Zero, screenDC);
                }
            }
            [SecuritySafeCritical]
            protected void DrawToBackBuffer(IntPtr backBufferDC)
            {
                Rectangle rect = new Rectangle(0, 0, Bounds.Width, Bounds.Height);
                using (XtraBufferedGraphics bg = XtraBufferedGraphicsManager.Current.Allocate(backBufferDC, rect))
                {
                    Point paintOffset = GetPaintOffset();
                    bg.Graphics.TranslateTransform(paintOffset.X, paintOffset.Y);

                    DrawCore(bg.Graphics);
                    bg.Render();
                }
            }
            bool CheckBounds()
            {
                if (Bounds.Width <= 0 || Bounds.Height <= 0) return true;
                return false;
            }
            static IntPtr Create32bppDIBSection(IntPtr hDC, int w, int h)
            {
                NativeMethods.BITMAPINFO_SMALL bitmapInfo = new NativeMethods.BITMAPINFO_SMALL();
                bitmapInfo.biSize = Marshal.SizeOf(bitmapInfo);
                bitmapInfo.biBitCount = 0x20;
                bitmapInfo.biPlanes = 1;
                bitmapInfo.biWidth = w;
                bitmapInfo.biHeight = h;
                return NativeMethods.CreateDIBSection(hDC, ref bitmapInfo, 0, 0, IntPtr.Zero, 0);
            }
            protected abstract void DrawCore(Graphics g);
        }

        public class DXLayeredImageWindow : DXLayeredWindowEx
        {
            bool isActive;
            Image image;
            Timer delayedClosingTimer;
            Control parent;
            public DXLayeredImageWindow(Image image, Control parent)
            {
                this.image = image;
                this.parent = parent;
                this.Size = image.Size;
                this.delayedClosingTimer = CreateDelayedClosingTimer();
            }
            protected virtual Timer CreateDelayedClosingTimer()
            {
                Timer timer = new Timer();
                timer.Tick += OnTimerTick;
                return timer;
            }
            bool tickPerformed = true;
            void OnTimerTick(object sender, EventArgs e)
            {
                if (tickPerformed) return;
                tickPerformed = true;
                Close();
            }
            public new void Show(Point pos)
            {
                isActive = true;
                base.Create(ParentHandle);
                base.Show(pos);
                Update();
            }
            protected IntPtr ParentHandle
            {
                get
                {
                    if (parent != null && parent.IsHandleCreated)
                        return parent.Handle;
                    return IntPtr.Zero;
                }
            }
            public void Close()
            {
                isActive = false;
                Dispose();
            }
            public void Close(int delay)
            {
                tickPerformed = false;
                delayedClosingTimer.Interval = delay;
                delayedClosingTimer.Start();
            }
            protected override void DrawCore(Graphics g)
            {
                if (image != null)g.DrawImage(image, Point.Empty);
            }
            protected override void OnDisposing()
            {
                this.image = null;
                this.parent = null;
                if (this.delayedClosingTimer != null) this.delayedClosingTimer.Dispose();
                this.delayedClosingTimer = null;
                base.OnDisposing();
            }
            public bool IsActive { get { return isActive; } }
        } 
}
