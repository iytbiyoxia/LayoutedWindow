//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Drawing;
//using System.Drawing.Drawing2D;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace LayoutedWindow  
//{
//    public interface IGraphicsCache : IDisposable
//    {
//        Graphics Graphics { get; }
//        Point Offset { get; }
//        Matrix TransformMatrix { get; }

//        Rectangle CalcClipRectangle(Rectangle r);
//        Rectangle CalcRectangle(Rectangle r);
//        SizeF CalcTextSize(string text, Font font, StringFormat strFormat, int maxWidth);
//        SizeF CalcTextSize(string text, Font font, StringFormat strFormat, int maxWidth, int maxHeight);
//        SizeF CalcTextSize(string text, Font font, StringFormat strFormat, int maxWidth, int maxHeight, out bool isCropped);
//        void Clear();
//        void DrawRectangle(Pen pen, Rectangle r);
//        void DrawString(string text, Font font, Brush foreBrush, Rectangle bounds, StringFormat strFormat);
//        void DrawVString(string text, Font font, Brush foreBrush, Rectangle bounds, StringFormat strFormat, int angle);
//        void FillRectangle(Brush brush, Rectangle rect);
//        void FillRectangle(Brush brush, RectangleF rect);
//        void FillRectangle(Color color, Rectangle rect);
//        Font GetFont(Font font, FontStyle fontStyle);
//        Brush GetGradientBrush(Rectangle rect, Color startColor, Color endColor, LinearGradientMode mode);
//        Brush GetGradientBrush(Rectangle rect, Color startColor, Color endColor, LinearGradientMode mode, int blendCount);
//        Pen GetPen(Color color);
//        Pen GetPen(Color color, int width);
//        Brush GetSolidBrush(Color color);
//        bool IsNeedDrawRect(Rectangle r);
//        void ResetMatrix();
//    }

//    public class GraphicsCache : IGraphicsCache
//    {
//        XPaint paint;
//        DXPaintEventArgs paintArgs;
//        GraphicsClip clipInfo;
//        ResourceCache cache;
//        Matrix matrix;
//        Point offset, offsetEx;
//        bool matrixReady;
//        public GraphicsCache(Graphics g) : this(new DXPaintEventArgs(g)) { }
//        public GraphicsCache(PaintEventArgs e, XPaint paint) : this(new DXPaintEventArgs(e), paint) { }
//        public GraphicsCache(DXPaintEventArgs e, XPaint paint)
//        {
//            this.paint = paint;
//            this.clipInfo = new GraphicsClip();
//            SetPaintArgs(e);
//        } 
//        public ResourceCache Cache
//        {
//            get
//            {
//                if (cache == null) return ResourceCache.DefaultCache;
//                return cache;
//            }
//            set
//            {
//                cache = value;
//            }
//        }
//        public GraphicsCache(PaintEventArgs e) : this(new DXPaintEventArgs(e)) { }
//        public GraphicsCache(DXPaintEventArgs e) : this(e, XPaint.Graphics) { }
//        public virtual void Dispose()
//        {
//            Clear();
//            if (this.cache != null) cache.Dispose();
//            this.cache = null;
//            if (this.clipInfo != null) this.clipInfo.Dispose();
//        }
//        public virtual void Clear()
//        {
//            this.paintArgs = null;
//            this.matrix = null;
//            this.matrixReady = false;
//            this.offset = Point.Empty;
//            this.offsetEx = Point.Empty;
//        } 
//        public virtual Point Offset
//        {
//            get
//            {
//                if (!IsMatrixReady) UpdateMatrix();
//                return offset;
//            }
//        } 
//        public virtual Point OffsetEx
//        {
//            get
//            {
//                if (!IsMatrixReady) UpdateMatrix();
//                return offsetEx;
//            }
//        }
//        public virtual Rectangle CalcRectangle(Rectangle r)
//        {
//            r.Offset(Offset);
//            return r;
//        }
//        public virtual Rectangle CalcClipRectangle(Rectangle r)
//        {
//            r.Offset(Offset);
//#if DXWhidbey
//            r.Offset(OffsetEx); 
//#endif
//            return r;
//        }
//        public void ResetMatrix()
//        {
//            this.matrixReady = false;
//        }
//        protected virtual bool IsMatrixReady { get { return matrixReady; } }
//        [System.Security.SecuritySafeCritical]
//        protected virtual void UpdateMatrix()
//        {
//            this.matrix = null;
//            this.offset = Point.Empty;
//            this.offsetEx = Point.Empty;
//            if (Graphics != null)
//            {
//                this.matrix = Graphics.Transform;
//                IntPtr hdc = Graphics.GetHdc();
//                try
//                {
//                    NativeMethods.POINT pt = new NativeMethods.POINT();
//                    bool res = NativeMethods.GetViewportOrgEx(hdc, ref pt);
//                    this.offsetEx = new Point(pt.X, pt.Y);
//                }
//                finally
//                {
//                    Graphics.ReleaseHdc(hdc);
//                }
//            }
//            if (TransformMatrix != null)
//            {
//                offset = new Point((int)TransformMatrix.OffsetX, (int)TransformMatrix.OffsetY);
//            }
//            this.matrixReady = true;
//        }
//        internal void SetGraphics(Graphics g) { SetPaintArgs(new DXPaintEventArgs(g, Rectangle.Empty)); }
//        internal void SetPaintArgs(DXPaintEventArgs e)
//        {
//            this.matrixReady = false;
//            this.paintArgs = e;
//            if (this.paintArgs == null) return;
//            this.clipInfo.ReleaseGraphics();
//            this.clipInfo.MaximumBounds = e.ClipRectangle;
//        }
//        public bool IsNeedDrawRectEx(Rectangle r)
//        {
//            if (!IsNeedDrawRect(r)) return false;
//            if (PaintArgs == null) return true;
//            if (PaintArgs.clipRegion == null) PrepareClipRegion();
//            for (int n = 0; n < PaintArgs.clipRegion.Length; n++)
//            {
//                if (PaintArgs.clipRegion[n].IntersectsWith(r)) return true;
//            }
//            return false;
//        }
//        public void PrepareClipRegion()
//        {
//            PaintArgs.clipRegion = new Rectangle[0];
//            if (PaintArgs.WindowHandle == IntPtr.Zero) return;
//            IntPtr hdc = PaintArgs.Graphics.GetHdc();
//            try
//            {
//                Rectangle[] rects = NativeMethods.GetClipRectsFromHDC(PaintArgs.WindowHandle, hdc, PaintArgs.IsNativeDC);
//                if (rects != null) PaintArgs.clipRegion = rects;
//            }
//            finally
//            {
//                PaintArgs.Graphics.ReleaseHdc(hdc);
//            }
//        }
//        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
//        public bool AllowDrawInvisibleRect { get; set; }
//        public bool IsNeedDrawRect(Rectangle r)
//        {
//            if (r.IsEmpty) return false;
//            if (PaintArgs == null) return true;
//            if (AllowDrawInvisibleRect) return true;
//            if (PaintArgs.ClipRectangle.IsEmpty) return true;
//            if (PaintArgs.ClipRectangle.IntersectsWith(r))
//                return Graphics.IsVisible(CalcRectangle(r));
//            return false;
//        } 
//        public GraphicsClip ClipInfo
//        {
//            get
//            {
//                if (clipInfo.Graphics != Graphics) clipInfo.Initialize(this);
//                return clipInfo;
//            }
//        } 
//        public XPaint Paint
//        {
//            get { return paint; }
//            set
//            {
//                if (value == null) value = XPaint.Graphics;
//                paint = value;
//            }
//        } 
//        public DXPaintEventArgs PaintArgs
//        {
//            get { return paintArgs; }
//            set
//            {
//                if (PaintArgs == value) return;
//                SetPaintArgs(value);
//            }
//        } 
//        public Graphics Graphics { get { return PaintArgs == null ? null : PaintArgs.Graphics; } }
//        public Font GetFont(Font font, FontStyle fontStyle) { return Cache.GetFont(font, fontStyle); }
//        public Brush GetSolidBrush(Color color) { return Cache.GetSolidBrush(color); }
//        public Pen GetPen(Color color) { return Cache.GetPen(color); }
//        public Pen GetPen(Color color, int width) { return Cache.GetPen(color, width); }
//        public Brush GetGradientBrush(Rectangle rect, Color startColor, Color endColor, LinearGradientMode mode)
//        {
//            return Cache.GetGradientBrush(rect, startColor, endColor, mode);
//        }
//        public Brush GetGradientBrush(Rectangle rect, Color startColor, Color endColor, LinearGradientMode mode, int blendCount)
//        {
//            return Cache.GetGradientBrush(rect, startColor, endColor, mode, blendCount);
//        }
//        public void FillRectangle(Brush brush, Rectangle rect)
//        {
//            Paint.FillRectangle(Graphics, brush, rect);
//        }
//        public void FillRectangle(Brush brush, RectangleF rect)
//        {
//            Paint.FillRectangle(Graphics, brush, rect);
//        }
//        public void FillRectangle(Color color, Rectangle rect)
//        {
//            FillRectangle(GetSolidBrush(color), rect);
//        }
//        public void DrawVString(string text, Font font, Brush foreBrush, Rectangle bounds, StringFormat strFormat, int angle)
//        {
//            Paint.DrawVString(this, text, font, foreBrush, bounds, strFormat, angle);
//        }
//        public void DrawString(string text, Font font, Brush foreBrush, Rectangle bounds, StringFormat strFormat)
//        {
//            Paint.DrawString(this, text, font, foreBrush, bounds, strFormat);
//        }
//        public SizeF CalcTextSize(string text, Font font, StringFormat strFormat, int maxWidth)
//        {
//            return Paint.CalcTextSize(Graphics, text, font, strFormat, maxWidth);
//        }
//        public SizeF CalcTextSize(string text, Font font, StringFormat strFormat, int maxWidth, int maxHeight)
//        {
//            return Paint.CalcTextSize(Graphics, text, font, strFormat, maxWidth, maxHeight);
//        }
//        public SizeF CalcTextSize(string text, Font font, StringFormat strFormat, int maxWidth, int maxHeight, out bool isCropped)
//        {
//            return Paint.CalcTextSize(Graphics, text, font, strFormat, maxWidth, maxHeight, out isCropped);
//        }
//        public void DrawRectangle(Pen pen, Rectangle r)
//        {
//            Paint.DrawRectangle(Graphics, pen, r);
//        }
//    }
//}
