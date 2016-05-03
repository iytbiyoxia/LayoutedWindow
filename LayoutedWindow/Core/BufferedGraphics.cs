using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LayoutedWindow 
{
    public sealed class XtraBufferedGraphics : IDisposable
    {
        static XtraBufferedGraphics()
        {
            XtraBufferedGraphics.rop = 0xcc0020;
        }
        internal XtraBufferedGraphics(Graphics bufferedGraphicsSurface, XtraBufferedGraphicsContext context, Graphics targetGraphics, IntPtr targetDC, Point targetLoc, Size virtualSize)
        {
            this.context = context;
            this.bufferedGraphicsSurface = bufferedGraphicsSurface;
            this.targetDC = targetDC;
            this.targetGraphics = targetGraphics;
            this.targetLoc = targetLoc;
            this.virtualSize = virtualSize;
        }
        public void Dispose()
        {
            this.Dispose(true);
        }
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.context != null)
                {
                    this.context.ReleaseBuffer(this);
                    if (this.DisposeContext)
                    {
                        this.context.Dispose();
                        this.context = null;
                    }
                }
                if (this.bufferedGraphicsSurface != null)
                {
                    this.bufferedGraphicsSurface.Dispose();
                    this.bufferedGraphicsSurface = null;
                }
            }
        }
        public void Render()
        {
            if (this.targetGraphics != null)
            {
                this.Render(this.targetGraphics);
            }
            else
            {
                this.RenderInternal(new HandleRef(this.Graphics, this.targetDC), this, 0);
            }
        }
        public void Render(Graphics target, Point targetPoint, int rop)
        {
            this.targetLoc = targetPoint;
            if (target != null)
            {
                IntPtr ptr1 = target.GetHdc();
                try
                {
                    this.RenderInternal(new HandleRef(target, ptr1), this, rop);
                }
                finally
                {
                    target.ReleaseHdcInternal(ptr1);
                }
            }
        }
        public void Render(Graphics target)
        {
            if (target != null)
            {
                IntPtr ptr1 = target.GetHdc();
                try
                {
                    this.RenderInternal(new HandleRef(target, ptr1), this, 0);
                }
                finally
                {
                    target.ReleaseHdcInternal(ptr1);
                }
            }
        }
        public void Render(IntPtr targetDC)
        {
            this.RenderInternal(new HandleRef(null, targetDC), this, 0);
        }
        private void RenderInternal(HandleRef refTargetDC, XtraBufferedGraphics buffer, int rop)
        {
            if (rop == 0) rop = XtraBufferedGraphics.rop;
            IntPtr ptr1 = buffer.Graphics.GetHdc();
            try
            {
                NativeMethods.BitBlt(refTargetDC, this.targetLoc.X, this.targetLoc.Y, this.virtualSize.Width, this.virtualSize.Height, new HandleRef(buffer.Graphics, ptr1), 0, 0, rop);
            }
            finally
            {
                buffer.Graphics.ReleaseHdcInternal(ptr1);
            }
        }
        internal bool DisposeContext
        {
            get
            {
                return this.disposeContext;
            }
            set
            {
                this.disposeContext = value;
            }
        }
        public Graphics Graphics
        {
            get
            {
                return this.bufferedGraphicsSurface;
            }
        }
        private Graphics bufferedGraphicsSurface;
        private XtraBufferedGraphicsContext context;
        private bool disposeContext;
        private static int rop;
        private IntPtr targetDC;
        private Graphics targetGraphics;
        private Point targetLoc;
        private Size virtualSize;
    }
    public sealed class XtraBufferedGraphicsContext : IDisposable
    {
        public XtraBufferedGraphicsContext()
        {
            this.maximumBuffer.Width = 0xe1;
            this.maximumBuffer.Height = 0x60;
            this.bufferSize = Size.Empty;
        }
        public XtraBufferedGraphics Allocate(Graphics targetGraphics, Rectangle targetRectangle)
        {
            return this.Allocate(targetGraphics, IntPtr.Zero, targetRectangle);
        }
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public XtraBufferedGraphics Allocate(IntPtr targetDC, Rectangle targetRectangle)
        {
            return this.Allocate(null, targetDC, targetRectangle);
        }
        XtraBufferedGraphics Allocate(Graphics targetGraphics, IntPtr targetDC, Rectangle targetRectangle)
        {
            if (targetRectangle.Width <= 0 || targetRectangle.Height <= 0)
                throw new ArgumentException("targetRectangle is empty", "targetRectangle");
            if (this.ShouldUseTempManager(targetRectangle))
            {
                return this.AllocBufferInTempManager(targetGraphics, targetDC, targetRectangle);
            }
            return this.AllocBuffer(targetGraphics, targetDC, targetRectangle);
        }
        private XtraBufferedGraphics AllocBuffer(Graphics targetGraphics, IntPtr targetDC, Rectangle targetRectangle)
        {
            Graphics graphics1;
            int num1 = Interlocked.CompareExchange(ref this.busy, BUFFER_BUSY_PAINTING, BUFFER_FREE);
            if (num1 != 0)
            {
                return this.AllocBufferInTempManager(targetGraphics, targetDC, targetRectangle);
            }
            this.targetLoc = new Point(targetRectangle.X, targetRectangle.Y);
            if (targetGraphics != null)
            {
                IntPtr ptr1 = targetGraphics.GetHdc();
                try
                {
                    graphics1 = this.CreateBuffer(ptr1, -this.targetLoc.X, -this.targetLoc.Y, targetRectangle.Width, targetRectangle.Height);
                    this.buffer = new XtraBufferedGraphics(graphics1, this, targetGraphics, targetDC, this.targetLoc, this.virtualSize);
                    return this.buffer;
                }
                finally
                {
                    targetGraphics.ReleaseHdcInternal(ptr1);
                }
            }
            graphics1 = this.CreateBuffer(targetDC, -this.targetLoc.X, -this.targetLoc.Y, targetRectangle.Width, targetRectangle.Height);
            this.buffer = new XtraBufferedGraphics(graphics1, this, targetGraphics, targetDC, this.targetLoc, this.virtualSize);
            return this.buffer;
        }
        private XtraBufferedGraphics AllocBufferInTempManager(Graphics targetGraphics, IntPtr targetDC, Rectangle targetRectangle)
        {
            XtraBufferedGraphicsContext context1 = null;
            XtraBufferedGraphics graphics1 = null;
            try
            {
                context1 = new XtraBufferedGraphicsContext();
                graphics1 = context1.AllocBuffer(targetGraphics, targetDC, targetRectangle);
                graphics1.DisposeContext = true;
            }
            finally
            {
                if (graphics1 != null && !graphics1.DisposeContext)
                    context1.Dispose();
            }
            return graphics1;
        }
        private bool bFillBitmapInfo(IntPtr hdc, IntPtr hpal, ref NativeMethods.BITMAPINFO_FLAT pbmi)
        {
            IntPtr ptr1 = IntPtr.Zero;
            bool flag1 = false;
            try
            {
                ptr1 = NativeMethods.CreateCompatibleBitmap(new HandleRef(null, hdc), 1, 1);
                if (ptr1 == IntPtr.Zero)
                {
                    throw new OutOfMemoryException();
                }
                pbmi.biSize = Marshal.SizeOf(typeof(NativeMethods.BITMAPINFOHEADER));
                pbmi.bmiColors = new byte[0x400];
                NativeMethods.GetDIBits(new HandleRef(null, hdc), new HandleRef(null, ptr1), 0, 0, IntPtr.Zero, ref pbmi, 0);
                if (pbmi.biBitCount <= 8)
                {
                    return this.bFillColorTable(hdc, hpal, ref pbmi);
                }
                if (pbmi.biCompression == 3)
                {
                    NativeMethods.GetDIBits(new HandleRef(null, hdc), new HandleRef(null, ptr1), 0, pbmi.biHeight, IntPtr.Zero, ref pbmi, 0);
                }
                flag1 = true;
            }
            finally
            {
                if (ptr1 != IntPtr.Zero)
                {
                    NativeMethods.DeleteObject(new HandleRef(null, ptr1));
                    ptr1 = IntPtr.Zero;
                }
            }
            return flag1;
        }
        private bool bFillColorTable(IntPtr hdc, IntPtr hpal, ref NativeMethods.BITMAPINFO_FLAT pbmi)
        {
            bool flag1 = false;
            int colorsToRetrieve = 1 << (pbmi.biBitCount & 0x1f);
            if (colorsToRetrieve <= 0x100)
            {
                if (hpal == IntPtr.Zero)
                    hpal = Graphics.GetHalftonePalette();
                byte[] palette = new byte[0x400];
                int retrievedColors = NativeMethods.GetPaletteEntries(hpal, 0, colorsToRetrieve, palette);
                if (retrievedColors != 0)
                {
                    for (int i = 0; i < colorsToRetrieve * 4; i += 4)
                    {
                        pbmi.bmiColors[i + 0] = palette[i + 2];
                        pbmi.bmiColors[i + 1] = palette[i + 1];
                        pbmi.bmiColors[i + 2] = palette[i + 0];
                        pbmi.bmiColors[i + 0] = 0;
                    }
                    flag1 = true;
                }
            }
            return flag1;
        }
        private Graphics CreateBuffer(IntPtr src, int offsetX, int offsetY, int width, int height)
        {
            this.busy = BUFFER_BUSY_DISPOSING;
            this.DisposeDC();
            this.busy = BUFFER_BUSY_PAINTING;
            this.compatDC = NativeMethods.CreateCompatibleDC(new HandleRef(null, src));
            if ((width > this.bufferSize.Width) || (height > this.bufferSize.Height))
            {
                int num1 = Math.Max(width, this.bufferSize.Width);
                int num2 = Math.Max(height, this.bufferSize.Height);
                this.busy = BUFFER_BUSY_DISPOSING;
                this.DisposeBitmap();
                this.busy = BUFFER_BUSY_PAINTING;
                IntPtr ptr1 = IntPtr.Zero;
                this.dib = this.CreateCompatibleDIB(src, IntPtr.Zero, num1, num2, ref ptr1);
                this.bufferSize = new Size(num1, num2);
            }
            this.oldBitmap = NativeMethods.SelectObject(new HandleRef(this, this.compatDC), new HandleRef(this, this.dib));
            this.compatGraphics = Graphics.FromHdcInternal(this.compatDC);
            this.compatGraphics.TranslateTransform((float)-this.targetLoc.X, (float)-this.targetLoc.Y);
            this.virtualSize = new Size(width, height);
            return this.compatGraphics;
        }
        private IntPtr CreateCompatibleDIB(IntPtr hdc, IntPtr hpal, int ulWidth, int ulHeight, ref IntPtr ppvBits)
        {
            NativeMethods.BITMAPINFO_FLAT bitmapinfo_flat1;
            if (hdc == IntPtr.Zero)
            {
                throw new ArgumentNullException("hdc");
            }
            IntPtr ptr1 = IntPtr.Zero;
            bitmapinfo_flat1 = new NativeMethods.BITMAPINFO_FLAT();
            if (this.bFillBitmapInfo(hdc, hpal, ref bitmapinfo_flat1))
            {
                bitmapinfo_flat1.biWidth = ulWidth;
                bitmapinfo_flat1.biHeight = ulHeight;
                bitmapinfo_flat1.bmiHeader_biSizeImage = 0;
                if (bitmapinfo_flat1.biBitCount < 0x10)
                    bitmapinfo_flat1.biBitCount = 0x20;
                bitmapinfo_flat1.bmiHeader_biClrUsed = 0;
                bitmapinfo_flat1.bmiHeader_biClrImportant = 0;
                ptr1 = NativeMethods.CreateDIBSection(new HandleRef(null, hdc), ref bitmapinfo_flat1, 0, ref ppvBits, IntPtr.Zero, 0);
                if (ptr1 == IntPtr.Zero)
                {
                    throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
                }
            }
            return ptr1;
        }
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            int num1 = Interlocked.CompareExchange(ref this.busy, BUFFER_BUSY_DISPOSING, BUFFER_FREE);
            if (disposing)
            {
                if (num1 == 1)
                {
                    throw new InvalidOperationException();
                }
                if (this.compatGraphics != null)
                {
                    this.compatGraphics.Dispose();
                    this.compatGraphics = null;
                }
            }
            this.DisposeDC();
            this.DisposeBitmap();
            if (this.buffer != null)
            {
                this.buffer.Dispose();
                this.buffer = null;
            }
            this.bufferSize = Size.Empty;
            this.virtualSize = Size.Empty;
            this.busy = BUFFER_FREE;
        }
        private void DisposeBitmap()
        {
            if (this.dib != IntPtr.Zero)
            {
                NativeMethods.DeleteObject(new HandleRef(this, this.dib));
                this.dib = IntPtr.Zero;
            }
        }
        private void DisposeDC()
        {
            if ((this.oldBitmap != IntPtr.Zero) && (this.compatDC != IntPtr.Zero))
            {
                NativeMethods.SelectObject(new HandleRef(this, this.compatDC), new HandleRef(this, this.oldBitmap));
                this.oldBitmap = IntPtr.Zero;
            }
            if (this.compatDC != IntPtr.Zero)
            {
                NativeMethods.DeleteDC(new HandleRef(this, this.compatDC));
                this.compatDC = IntPtr.Zero;
            }
        }
        ~XtraBufferedGraphicsContext()
        {
            this.Dispose(false);
        }
        public void Invalidate()
        {
            int num1 = Interlocked.CompareExchange(ref this.busy, BUFFER_BUSY_DISPOSING, BUFFER_FREE);
            if (num1 == 0)
            {
                this.Dispose();
                this.busy = BUFFER_FREE;
            }
            else
            {
                this.invalidateWhenFree = true;
            }
        }
        internal void ReleaseBuffer(XtraBufferedGraphics buffer)
        {
            this.buffer = null;
            if (this.invalidateWhenFree)
            {
                this.busy = BUFFER_BUSY_DISPOSING;
                this.Dispose();
            }
            else
            {
                this.busy = BUFFER_BUSY_DISPOSING;
                this.DisposeDC();
            }
            this.busy = BUFFER_FREE;
        }
        private bool ShouldUseTempManager(Rectangle targetBounds)
        {
            return ((targetBounds.Width * targetBounds.Height) > (this.MaximumBuffer.Width * this.MaximumBuffer.Height));
        }
        internal static TraceSwitch DoubleBuffering
        {
            get
            {
                if (XtraBufferedGraphicsContext.doubleBuffering == null)
                {
                    XtraBufferedGraphicsContext.doubleBuffering = new TraceSwitch("DoubleBuffering", "Output information about double buffering");
                }
                return XtraBufferedGraphicsContext.doubleBuffering;
            }
        }
        public Size MaximumBuffer
        {
            get
            {
                return this.maximumBuffer;
            }
            [UIPermission(SecurityAction.Demand, Window = UIPermissionWindow.AllWindows)]
            set
            {
                if ((value.Width <= 0) || (value.Height <= 0))
                {
                    object[] objArray1 = new object[2] { "MaximumBuffer", value };
                    throw new ArgumentException();
                }
                if ((value.Width * value.Height) < (this.maximumBuffer.Width * this.maximumBuffer.Height))
                {
                    this.Invalidate();
                }
                this.maximumBuffer = value;
            }
        }
        private XtraBufferedGraphics buffer;
        private const int BUFFER_BUSY_DISPOSING = 2;
        private const int BUFFER_BUSY_PAINTING = 1;
        private const int BUFFER_FREE = 0;
        private Size bufferSize;
        private int busy;
        private IntPtr compatDC;
        private Graphics compatGraphics;
        private IntPtr dib;
        private static TraceSwitch doubleBuffering;
        private bool invalidateWhenFree;
        private Size maximumBuffer;
        private IntPtr oldBitmap;
        private Point targetLoc;
        private Size virtualSize;
    }
    public sealed class XtraBufferedGraphicsManager
    {
        static XtraBufferedGraphicsManager()
        {
            XtraBufferedGraphicsManager.bufferedGraphicsContext = new XtraBufferedGraphicsContext();
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(XtraBufferedGraphicsManager.OnShutdown);
            AppDomain.CurrentDomain.DomainUnload += new EventHandler(XtraBufferedGraphicsManager.OnShutdown);
        }
        private XtraBufferedGraphicsManager()
        {
        }
        private static void OnShutdown(object sender, EventArgs e)
        {
            XtraBufferedGraphicsManager.Current.Invalidate();
        }
        public static XtraBufferedGraphicsContext Current
        {
            get
            {
                return XtraBufferedGraphicsManager.bufferedGraphicsContext;
            }
        }
        private static XtraBufferedGraphicsContext bufferedGraphicsContext;
    }
}
