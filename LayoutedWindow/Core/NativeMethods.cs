using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LayoutedWindow
{
    public class MSG
    {
        public const int
            WM_DWMCOMPOSITIONCHANGED = 0x031E,
            SIZE_RESTORED = 0,
            WM_DESTROY = 0x02,
            VK_LBUTTON = 0x01,
            VK_RBUTTON = 0x02,
            MK_LBUTTON = 0x0001, MK_RBUTTON = 0x0002,
            WA_INACTIVE = 0,
            WM_SETCURSOR = 0x0020,
            WM_MOUSEACTIVATE = 0x0021,
            WM_ACTIVATE = 0x0006,
            WM_SETFOCUS = 0x0007,
            WM_KILLFOCUS = 0x0008,
            WM_MDICREATE = 0x0220,
            WM_MDIDESTROY = 0x0221,
            WM_MDIACTIVATE = 0x0222,
            WM_MDIRESTORE = 0x0223,
            WM_MDINEXT = 0x0224,
            WM_MDIMAXIMIZE = 0x0225,
            WM_MDISETMENU = 0x0230,
            WM_CONTEXTMENU = 0x007B,
            WM_SYSCOLORCHANGE = 0x15,
            WM_EXITMENULOOP = 530,
            WM_MENUCHAR = 288,
            WM_SYSCHAR = 0x0106,
            WM_VSCROLL = 0x115,
            WM_HSCROLL = 0x114,
            WM_COMMAND = 273,
            WM_MENUSELECT = 0x011F,
            WM_CHILDACTIVATE = 0x0022,
            WM_NCCALCSIZE = 0x0083,
            WM_GETMINMAXINFO = 0x24,
            WM_ENABLE = 0x000A,
            WM_NCHITTEST = 0x0084,
            WM_NCRBUTTONDOWN = 0x00A4,
            WM_NCRBUTTONUP = 0x00A5,
            WM_NCRBUTTONDBLCLK = 0x00A6,
            WM_NCLBUTTONDOWN = 0x00A1,
            WM_NCLBUTTONDBLCLK = 0x00A3,
            WM_NCLBUTTONUP = 0x00A2,
            WM_NCMBUTTONDOWN = 0x00A7,
            WM_NCMBUTTONUP = 0x00A8,
            WM_NCMBUTTONDBLCLK = 0x00A9,
            WM_NCMOUSEMOVE = 0x00A0,
            WM_NCMOUSELEAVE = 0x02A2,
            WM_MOUSELEAVE = 675,
            WM_MOUSEHOVER = 673,
            WM_NCMOUSEHOVER = 0x02A0,
            WM_NCPAINT = 0x0085,
            WM_NCACTIVATE = 0x0086,
            WM_SETICON = 0x0080,
            WM_SETTEXT = 0x000C,
            WM_LBUTTONDOWN = 0x0201,
            WM_LBUTTONUP = 0x0202,
            WM_LBUTTONDBLCLK = 0x0203,
            WM_RBUTTONDOWN = 0x0204,
            WM_RBUTTONUP = 0x0205,
            WM_RBUTTONDBLCLK = 0x0206,
            WM_MBUTTONDOWN = 0x0207,
            WM_MBUTTONUP = 0x0208,
            WM_MBUTTONDBLCLK = 0x0209,
            WM_MOUSEHWHEEL = 0x20e,
            WM_MOUSEWHEEL = 0x020A,
            WM_MOUSEMOVE = 0x0200,
            WM_PRINTCLIENT = 0x0318,
            WM_IME_NOTIFY = 642,
            WM_DEADCHAR = 0x103,
            WM_SYSKEYDOWN = 0x104,
            WM_KEYUP = 257,
            WM_KEYDOWN = 256,
            WM_CAPTURECHANGED = 0x215,
            WM_SYSCOMMAND = 0x0112,
            WM_SYSKEYUP = 0x0105,
            WM_CHAR = 0x0102,
            WM_SIZE = 5,
            WM_SIZING = 0x0214,
            WM_EXITSIZEMOVE = 0x0232,
            WM_SYNCPAINT = 0x0088,
            WM_PAINT = 0x000F,
            WM_PRINT = 0x0317,
            WM_ERASEBKGND = 0x0014,
            WM_SHOWWINDOW = 0x18,
            WM_NCCREATE = 0x0081,
            WM_MOVE = 0x0003,
            WM_ACTIVATEAPP = 28,
            WM_APP = 0x8000,
            WM_CREATE = 0x0001,
            WM_WINDOWPOSCHANGING = 0x0046,
            WM_WINDOWPOSCHANGED = 0x0047,
            WM_USER = 0x0400,
            WM_NCUAHDRAWCAPTION = 0x00AE,
            WM_NCUAHDRAWFRAME = 0x00AF,
            WM_IME_STARTCOMPOSITION = 0x010D,
            WM_IME_ENDCOMPOSITION = 0x010E,
            WM_IME_COMPOSITION = 0x010F,
            WM_IME_KEYLAST = 0x010F,
            WM_XREDRAW = WM_USER + 100,
            WM_XREDRAWC = WM_USER + 101,
            WM_USER7441 = WM_USER + 7441;
    }
    [System.Security.SecuritySafeCritical]
    public class NativeMethods
    {
        #region Structs&Enums
        [StructLayout(LayoutKind.Sequential)]
        internal struct APPBARDATA
        {
            public int cbSize;
            public IntPtr hWnd;
            public uint uCallbackMessage;
            public uint uEdge;
            public NativeMethods.RECT rc;
            public IntPtr lParam;
        }
        public struct WNDCLASS
        {
            public Int32 style;
            public Delegate lpfnWndProc;
            public int cbClsExtra;
            public int cbWndExtra;
            public IntPtr hInstance;
            public IntPtr hIcon;
            public IntPtr hCursor;
            public IntPtr hbrBackground;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpszMenuName;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpszClassName;
        }
        public enum ShowWindowCommands : int
        {
            Hide = 0,
            Normal = 1,
            ShowMinimized = 2,
            Maximize = 3,
            ShowMaximized = 3,
            ShowNoActivate = 4,
            Show = 5,
            Minimize = 6,
            ShowMinNoActive = 7,
            ShowNA = 8,
            Restore = 9,
            ShowDefault = 10,
            ForceMinimize = 11
        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWPLACEMENT
        {
            public int Length;
            public int Flags;
            public ShowWindowCommands ShowCmd;
            public NativeMethods.POINT MinPosition;
            public NativeMethods.POINT MaxPosition;
            public NativeMethods.RECT NormalPosition;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }
        public enum ScrollWindowExFlags
        {
            SW_SCROLLCHILDREN = 0x01,
            SW_INVALIDATE = 0x02,
            SW_ERASE = 0x04,
            SW_SMOOTHSCROLL = 0x10
        };
        [StructLayout(LayoutKind.Sequential)]
        public struct Margins
        {
            public int Left, Right, Top, Bottom;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct BITMAPINFO_SMALL
        {
            public int biSize;
            public int biWidth;
            public int biHeight;
            public short biPlanes;
            public short biBitCount;
            public int biCompression;
            public int biSizeImage;
            public int biXPelsPerMeter;
            public int biYPelsPerMeter;
            public int biClrUsed;
            public int biClrImportant;
            public byte bmiColors_rgbBlue;
            public byte bmiColors_rgbGreen;
            public byte bmiColors_rgbRed;
            public byte bmiColors_rgbReserved;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct BITMAPINFO_FLAT
        {
            public int biSize;
            public int biWidth;
            public int biHeight;
            public short biPlanes;
            public short biBitCount;
            public int biCompression;
            public int bmiHeader_biSizeImage;
            public int bmiHeader_biXPelsPerMeter;
            public int bmiHeader_biYPelsPerMeter;
            public int bmiHeader_biClrUsed;
            public int bmiHeader_biClrImportant;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x400)]
            public byte[] bmiColors;
        }
        [StructLayout(LayoutKind.Sequential)]
        public class BITMAPINFOHEADER
        {
            public int biSize;
            public int biWidth;
            public int biHeight;
            public short biPlanes;
            public short biBitCount;
            public int biCompression;
            public int biSizeImage;
            public int biXPelsPerMeter;
            public int biYPelsPerMeter;
            public int biClrUsed;
            public int biClrImportant;
            public BITMAPINFOHEADER()
            {
                this.biSize = 40;
            }
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct HWND : IWin32Window
        {
            IntPtr _Handle;
            public static readonly HWND Empty = new HWND(IntPtr.Zero);
            public static HWND Desktop
            {
                get { return NativeMethods.GetDesktopWindow(); }
            }
            public HWND(IntPtr aValue)
            {
                _Handle = aValue;
            }
            #region Overrides
            public override bool Equals(object obj)
            {
                if (obj == null)
                    return false;
                if (obj is HWND)
                    return Equals((HWND)obj);
                if (obj is IntPtr)
                    return Equals((IntPtr)obj);
                return false;
            }
            public bool Equals(IntPtr ptr)
            {
                if (!_Handle.ToInt32().Equals(ptr.ToInt32()))
                    return false;
                return true;
            }
            public bool Equals(HWND hwnd)
            {
                return Equals(hwnd._Handle);
            }
            public bool Equals(IWin32Window window)
            {
                return Equals(window.Handle);
            }
            public override int GetHashCode()
            {
                return _Handle.GetHashCode();
            }
            public override string ToString()
            {
                return "{" + "Handle=0x" + _Handle.ToInt32().ToString("X8") + "}";
            }
            #endregion
            public bool IsEmpty
            {
                get { return _Handle == IntPtr.Zero; }
            }
            public bool IsVisible
            {
                get { return NativeMethods.IsWindowVisible(_Handle); }
            }
            public IntPtr Handle
            {
                get { return _Handle; }
            }
            #region Operators
            public static bool operator ==(HWND aHwnd1, HWND aHwnd2)
            {
                if ((object)aHwnd1 == null)
                    return ((object)aHwnd2 == null);
                return aHwnd1.Equals(aHwnd2);
            }
            public static bool operator ==(IntPtr aIntPtr, HWND aHwnd)
            {
                if ((object)aIntPtr == null)
                    return ((object)aHwnd == null);
                return aHwnd.Equals(aIntPtr);
            }
            public static bool operator ==(HWND aHwnd, IntPtr aIntPtr)
            {
                if ((object)aHwnd == null)
                    return ((object)aIntPtr == null);
                return aHwnd.Equals(aIntPtr);
            }
            public static bool operator !=(HWND aHwnd1, HWND aHwnd2)
            {
                return !(aHwnd1 == aHwnd2);
            }
            public static bool operator !=(IntPtr aIntPtr, HWND aHwnd)
            {
                return !(aIntPtr == aHwnd);
            }
            public static bool operator !=(HWND aHwnd, IntPtr aIntPtr)
            {
                return !(aHwnd == aIntPtr);
            }
            public static implicit operator IntPtr(HWND aHwnd)
            {
                return aHwnd.Handle;
            }
            public static implicit operator HWND(IntPtr aIntPtr)
            {
                return new HWND(aIntPtr);
            }
            #endregion
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct HDC
        {
            IntPtr _Handle;
            public static readonly HDC Empty = new HDC(0);
            public HDC(IntPtr aValue)
            {
                _Handle = aValue;
            }
            public HDC(int aValue)
            {
                _Handle = new IntPtr(aValue);
            }
            #region Overrides
            public override bool Equals(object aObj)
            {
                if (aObj == null)
                    return false;
                if (aObj is HDC)
                    return Equals((HDC)aObj);
                if (aObj is IntPtr)
                    return Equals((IntPtr)aObj);
                return false;
            }
            public bool Equals(HDC aHDC)
            {
                if (!_Handle.Equals(aHDC._Handle))
                    return false;
                return true;
            }
            public bool Equals(IntPtr aIntPtr)
            {
                if (!_Handle.Equals(aIntPtr))
                    return false;
                return true;
            }
            public override int GetHashCode()
            {
                return _Handle.GetHashCode();
            }
            public override string ToString()
            {
                return "{" + "Handle=0x" + _Handle.ToInt32().ToString("X8") + "}";
            }
            #endregion
            public void Release(HWND window)
            {
                NativeMethods.ReleaseDC(window, this);
            }
            public IntPtr SelectObject(IntPtr aGDIObj)
            {
                return NativeMethods.SelectObject(this, aGDIObj);
            }
            public HDC CreateCompatible()
            {
                return NativeMethods.CreateCompatibleDC(_Handle);
            }
            public IntPtr CreateCompatibleBitmap(int width, int height)
            {
                return NativeMethods.CreateCompatibleBitmap(_Handle, width, height);
            }
            public IntPtr CreateCompatibleBitmap(Rectangle rectangle)
            {
                return CreateCompatibleBitmap(rectangle.Width, rectangle.Height);
            }
            public void Delete()
            {
                NativeMethods.DeleteDC(_Handle);
            }
            public IntPtr Handle
            {
                get
                {
                    return _Handle;
                }
            }
            public bool IsEmpty
            {
                get
                {
                    return _Handle == IntPtr.Zero;
                }
            }
            #region Operators
            public static bool operator ==(HDC aHdc1, HDC aHdc2)
            {
                if ((object)aHdc1 == null)
                    return ((object)aHdc2 == null);
                return aHdc1.Equals(aHdc2);
            }
            public static bool operator ==(IntPtr aIntPtr, HDC aHdc)
            {
                if ((object)aIntPtr == null)
                    return ((object)aHdc == null);
                return aHdc.Equals(aIntPtr);
            }
            public static bool operator ==(HDC aHdc, IntPtr aIntPtr)
            {
                if ((object)aHdc == null)
                    return ((object)aIntPtr == null);
                return aHdc.Equals(aIntPtr);
            }
            public static bool operator !=(HDC aHdc1, HDC aHdc2)
            {
                return !(aHdc1 == aHdc2);
            }
            public static bool operator !=(IntPtr aIntPtr, HDC aHdc)
            {
                return !(aIntPtr == aHdc);
            }
            public static bool operator !=(HDC aHdc, IntPtr aIntPtr)
            {
                return !(aHdc == aIntPtr);
            }
            public static implicit operator IntPtr(HDC aHdc)
            {
                return aHdc.Handle;
            }
            public static implicit operator HDC(IntPtr aIntPtr)
            {
                return new HDC(aIntPtr);
            }
            #endregion
        }
        [StructLayout(LayoutKind.Sequential), CLSCompliant(false)]
        public struct COLORREF
        {
            uint _ColorRef;
            public COLORREF(Color aValue)
            {
                int lRGB = aValue.ToArgb();
                int n0 = (lRGB & 0xff) << 16;
                lRGB = lRGB & 0xffff00;
                lRGB = (lRGB | (lRGB >> 16 & 0xff));
                lRGB = (lRGB & 0xffff);
                lRGB = (lRGB | n0);
                _ColorRef = (uint)lRGB;
            }
            public COLORREF(int lRGB)
            {
                _ColorRef = (uint)lRGB;
            }
            public Color ToColor()
            {
                int r = (int)_ColorRef & 0xff;
                int g = ((int)_ColorRef >> 8) & 0xff;
                int b = ((int)_ColorRef >> 16) & 0xff;
                return Color.FromArgb(r, g, b);
            }
            public static COLORREF FromColor(System.Drawing.Color aColor)
            {
                return new COLORREF(aColor);
            }
            public static System.Drawing.Color ToColor(COLORREF aColorRef)
            {
                return aColorRef.ToColor();
            }
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct HHOOK
        {
            IntPtr _Handle;
            public static readonly HHOOK Empty = new HHOOK(0);
            public HHOOK(IntPtr aValue)
            {
                _Handle = aValue;
            }
            public HHOOK(int aValue)
            {
                _Handle = new IntPtr(aValue);
            }
            #region Overrides
            public override bool Equals(object aObj)
            {
                if (aObj == null)
                    return false;
                if (aObj is HHOOK)
                    return Equals((HHOOK)aObj);
                if (aObj is IntPtr)
                    return Equals((IntPtr)aObj);
                return false;
            }
            public bool Equals(HHOOK aHHOOK)
            {
                if (!_Handle.Equals(aHHOOK._Handle))
                    return false;
                return true;
            }
            public bool Equals(IntPtr aIntPtr)
            {
                if (!_Handle.Equals(aIntPtr))
                    return false;
                return true;
            }
            public override int GetHashCode()
            {
                return _Handle.GetHashCode();
            }
            public override string ToString()
            {
                return "{" + "Handle=0x" + _Handle.ToInt32().ToString("X8") + "}";
            }
            #endregion
            public IntPtr Handle
            {
                get { return _Handle; }
            }
            public bool IsEmpty
            {
                get { return _Handle == IntPtr.Zero; }
            }
            #region Operators
            public static bool operator ==(HHOOK aHHook1, HHOOK aHHook2)
            {
                if ((object)aHHook1 == null)
                    return ((object)aHHook2 == null);
                return aHHook1.Equals(aHHook2);
            }
            public static bool operator ==(IntPtr aIntPtr, HHOOK aHHook)
            {
                if ((object)aIntPtr == null)
                    return ((object)aHHook == null);
                return aHHook.Equals(aIntPtr);
            }
            public static bool operator ==(HHOOK aHHook, IntPtr aIntPtr)
            {
                if ((object)aHHook == null)
                    return ((object)aIntPtr == null);
                return aHHook.Equals(aIntPtr);
            }
            public static bool operator !=(HHOOK aHHook1, HHOOK aHHook2)
            {
                return !(aHHook1 == aHHook2);
            }
            public static bool operator !=(IntPtr aIntPtr, HHOOK aHHook)
            {
                return !(aIntPtr == aHHook);
            }
            public static bool operator !=(HHOOK aHHook, IntPtr aIntPtr)
            {
                return !(aHHook == aIntPtr);
            }
            public static implicit operator IntPtr(HHOOK aHHook)
            {
                return aHHook.Handle;
            }
            public static implicit operator HHOOK(IntPtr aIntPtr)
            {
                return new HHOOK(aIntPtr);
            }
            #endregion
        }
        public enum SystemCursors
        {
            OCR_NORMAL = 32512,
            OCR_IBEAM = 32513,
            OCR_WAIT = 32514,
            OCR_CROSS = 32515,
            OCR_UP = 32516,
            OCR_SIZE = 32640,
            OCR_ICON = 32641,
            OCR_SIZENWSE = 32642,
            OCR_SIZENESW = 32643,
            OCR_SIZEWE = 32644,
            OCR_SIZENS = 32645,
            OCR_SIZEALL = 32646,
            OCR_ICOCUR = 32647,
            OCR_NO = 32648,
            OCR_HAND = 32649,
            OCR_APPSTARTING = 32650
        }
        [Flags]
        public enum RasterOperations
        {
            SRCCOPY = 0x00CC0020,
            SRCPAINT = 0x00EE0086,
            SRCAND = 0x008800C6,
            SRCINVERT = 0x00660046,
            SRCERASE = 0x00440328,
            NOTSRCCOPY = 0x00330008,
            NOTSRCERASE = 0x001100A6,
            MERGECOPY = 0x00C000CA,
            MERGEPAINT = 0x00BB0226,
            PATCOPY = 0x00F00021,
            PATPAINT = 0x00FB0A09,
            PATINVERT = 0x005A0049,
            DSTINVERT = 0x00550009,
            BLACKNESS = 0x00000042,
            WHITENESS = 0x00FF0062
        }
        public enum MouseMessages
        {
            WM_LBUTTONDOWN = 0x0201,
            WM_LBUTTONUP = 0x0202,
            WM_MOUSEMOVE = 0x0200,
            WM_MOUSEWHEEL = 0x020A,
            WM_RBUTTONDOWN = 0x0204,
            WM_RBUTTONUP = 0x0205
        }
        [StructLayout(LayoutKind.Sequential)]
        public class TRACKMOUSEEVENT
        {
            public int cbSize;
            public int dwFlags;
            public IntPtr hwndTrack;
            public int dwHoverTime = 0;
            public TRACKMOUSEEVENT()
            {
                this.cbSize = Marshal.SizeOf(typeof(TRACKMOUSEEVENT));
            }
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct NCCALCSIZE_PARAMS
        {
            public RECT rgrc0, rgrc1, rgrc2;
            public IntPtr lppos;
            [System.Security.SecuritySafeCritical]
            public static NCCALCSIZE_PARAMS GetFrom(IntPtr lParam)
            {
                return (NCCALCSIZE_PARAMS)Marshal.PtrToStructure(lParam, typeof(NCCALCSIZE_PARAMS));
            }
            [System.Security.SecuritySafeCritical]
            public void SetTo(IntPtr lParam)
            {
                Marshal.StructureToPtr(this, lParam, false);
            }
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct PAINTSTRUCT
        {
            public IntPtr hdc;
            public bool fErase;
            public RECT rcPaint;
            public bool fRestore;
            public bool fIncUpdate;
            public int reserved1;
            public int reserved2;
            public int reserved3;
            public int reserved4;
            public int reserved5;
            public int reserved6;
            public int reserved7;
            public int reserved8;
        }
        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct POINT
        {
            public int X, Y;
            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
            public POINT(Point pt)
            {
                this.X = pt.X;
                this.Y = pt.Y;
            }
            public Point ToPoint() { return new Point(X, Y); }
        }
        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct SIZE
        {
            public int Width, Height;
            public SIZE(int w, int h)
            {
                this.Width = w;
                this.Height = h;
            }
            public SIZE(Size size)
            {
                this.Width = size.Width;
                this.Height = size.Height;
            }
            public Size ToSize() { return new Size(Width, Height); }
        }
        public enum RegionDataHeaderTypes : int
        {
            Rectangles = 1
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct RGNDATAHEADER
        {
            public int dwSize;
            public int iType;
            public int nCount;
            public int nRgnSize;
            public RECT rcBound;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left, Top, Right, Bottom;
            public RECT(int l, int t, int r, int b)
            {
                Left = l; Top = t; Right = r; Bottom = b;
            }
            public RECT(Rectangle r)
            {
                Left = r.Left; Top = r.Top; Right = r.Right; Bottom = r.Bottom;
            }
            public Rectangle ToRectangle()
            {
                return Rectangle.FromLTRB(Left, Top, Right, Bottom);
            }
            public void Inflate(int width, int height)
            {
                Left -= width;
                Top -= height;
                Right += width;
                Bottom += height;
            }
            public override string ToString()
            {
                return string.Format("x:{0},y:{1},width:{2},height:{3}", Left, Top, Right - Left, Bottom - Top);
            }
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct MINMAXINFO
        {
            public POINT ptReserved,
            ptMaxSize, ptMaxPosition, ptMinTrackSize, ptMaxTrackSize;
            [System.Security.SecuritySafeCritical]
            public static MINMAXINFO GetFrom(IntPtr lParam)
            {
                return (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));
            }
            [System.Security.SecuritySafeCritical]
            public void SetTo(IntPtr lParam)
            {
                Marshal.StructureToPtr(this, lParam, false);
            }
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct GESTUREINFO
        {
            public int cbSize;
            public int dwFlags;
            public int dwID;
            public IntPtr hwndTarget;
            [MarshalAs(UnmanagedType.Struct)]
            internal POINTS ptsLocation;
            public int dwInstanceID;
            public int dwSequenceID;
            public Int64 ullArguments;
            public int cbExtraArgs;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct GESTURECONFIG
        {
            public GESTURECONFIG(int dwID, int dwWant, int dwBlock)
            {
                this.dwID = dwID;
                this.dwWant = dwWant;
                this.dwBlock = dwBlock;
            }
            public int dwID;
            public int dwWant;
            public int dwBlock;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct POINTS
        {
            public short x;
            public short y;
        }
        [StructLayout(LayoutKind.Sequential)]
        public class GESTURENOTIFYSTRUCT
        {
            public int cbSize;
            public int dwFlags;
            public IntPtr hwndTarget;
            [MarshalAs(UnmanagedType.Struct)]
            public POINTS ptsLocation;
            public int dwInstanceID;
        }
        public class WMSZ
        {
            public const int
                WMSZ_LEFT = 1,
                WMSZ_RIGHT = 2,
                WMSZ_TOP = 3,
                WMSZ_TOPLEFT = 4,
                WMSZ_TOPRIGHT = 5,
                WMSZ_BOTTOM = 6,
                WMSZ_BOTTOMLEFT = 7,
                WMSZ_BOTTOMRIGHT = 8;
        }
        public class SWP
        {
            public const int
                SWP_NOSIZE = 0x0001,
                SWP_NOMOVE = 0x0002,
                SWP_NOZORDER = 0x0004,
                SWP_NOREDRAW = 0x0008,
                SWP_NOACTIVATE = 0x0010,
                SWP_FRAMECHANGED = 0x0020,
                SWP_DRAWFRAME = SWP_FRAMECHANGED,
                SWP_SHOWWINDOW = 0x0040,
                SWP_HIDEWINDOW = 0x0080,
                SWP_NOCOPYBITS = 0x0100,
                SWP_NOOWNERZORDER = 0x0200,
                SWP_NOREPOSITION = SWP_NOOWNERZORDER,
                SWP_NOSENDCHANGING = 0x0400;
        }
        public class DC
        {
            public const int
                DCX_WINDOW = 0x00000001,
                DCX_CACHE = 0x00000002,
                DCX_NORESETATTRS = 0x00000004,
                DCX_CLIPCHILDREN = 0x00000008,
                DCX_CLIPSIBLINGS = 0x00000010,
                DCX_PARENTCLIP = 0x00000020,
                DCX_EXCLUDERGN = 0x00000040,
                DCX_INTERSECTRGN = 0x00000080,
                DCX_EXCLUDEUPDATE = 0x00000100,
                DCX_INTERSECTUPDATE = 0x00000200,
                DCX_LOCKWINDOWUPDATE = 0x00000400,
                DCX_VALIDATE = 0x00200000;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWPOS
        {
            public IntPtr hWnd;
            public IntPtr hHndInsertAfter;
            public int x;
            public int y;
            public int cx;
            public int cy;
            public int flags;
        }
        public class SC
        {
            public const int
                SC_SIZE = 0xf000,
                SC_MOVE = 0xf010,
                SC_MINIMIZE = 0xf020,
                SC_MAXIMIZE = 0xf030,
                SC_NEXTWINDOW = 0xf040,
                SC_PREVWINDOW = 0xf050,
                SC_CLOSE = 0xf060,
                SC_VSCROLL = 0xf070,
                SC_HSCROLL = 0xf080,
                SC_MOUSEMENU = 0xf090,
                SC_KEYMENU = 0xf100,
                SC_ARRANGE = 0xf110,
                SC_RESTORE = 0xf120,
                SC_TASKLIST = 0xf130,
                SC_SCREENSAVE = 0xf140,
                SC_HOTKEY = 0xf150,
                SC_CONTEXTHELP = 0xf180,
                SC_DRAGMOVE = 0xf012,
                SC_SYSMENU = 0xf093;
        }
        public class HT
        {
            public const int HTERROR = (-2);
            public const int HTTRANSPARENT = (-1);
            public const int HTNOWHERE = 0, HTCLIENT = 1, HTCAPTION = 2, HTSYSMENU = 3,
                HTGROWBOX = 4, HTSIZE = HTGROWBOX, HTMENU = 5, HTHSCROLL = 6, HTVSCROLL = 7, HTMINBUTTON = 8, HTMAXBUTTON = 9,
                HTLEFT = 10, HTRIGHT = 11, HTTOP = 12, HTTOPLEFT = 13, HTTOPRIGHT = 14, HTBOTTOM = 15, HTBOTTOMLEFT = 16,
                HTBOTTOMRIGHT = 17, HTBORDER = 18, HTREDUCE = HTMINBUTTON, HTZOOM = HTMAXBUTTON, HTSIZEFIRST = HTLEFT,
                HTSIZELAST = HTBOTTOMRIGHT, HTOBJECT = 19, HTCLOSE = 20, HTHELP = 21;
        }
        [StructLayout(LayoutKind.Sequential)]
        public class NONCLIENTMETRICS
        {
            public int cbSize = Marshal.SizeOf(typeof(NONCLIENTMETRICS));
            public int iBorderWidth = 0;
            public int iScrollWidth = 0;
            public int iScrollHeight = 0;
            public int iCaptionWidth = 0;
            public int iCaptionHeight = 0;
            [MarshalAs(UnmanagedType.Struct)]
            public LOGFONT lfCaptionFont = null;
            public int iSmCaptionWidth = 0;
            public int iSmCaptionHeight = 0;
            [MarshalAs(UnmanagedType.Struct)]
            public LOGFONT lfSmCaptionFont = null;
            public int iMenuWidth = 0;
            public int iMenuHeight = 0;
            [MarshalAs(UnmanagedType.Struct)]
            public LOGFONT lfMenuFont = null;
            [MarshalAs(UnmanagedType.Struct)]
            public LOGFONT lfStatusFont = null;
            [MarshalAs(UnmanagedType.Struct)]
            public LOGFONT lfMessageFont = null;
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class LOGFONT
        {
            public int lfHeight = 0;
            public int lfWidth = 0;
            public int lfEscapement = 0;
            public int lfOrientation = 0;
            public int lfWeight = 0;
            public byte lfItalic = 0;
            public byte lfUnderline = 0;
            public byte lfStrikeOut = 0;
            public byte lfCharSet = 0;
            public byte lfOutPrecision = 0;
            public byte lfClipPrecision = 0;
            public byte lfQuality = 0;
            public byte lfPitchAndFamily = 0;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string lfFaceName = null;
        }
        public const int SPI_GETNONCLIENTMETRICS = 0x0029;
        #endregion Structs&Enums
        #region SecurityCritical
        static class UnsafeNativeMethods
        {
            [DllImport("user32.dll")]
            public static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);
            [DllImport("SHELL32", CallingConvention = CallingConvention.StdCall)]
            internal static extern int SHAppBarMessage(int dwMessage, ref APPBARDATA pData);
            [DllImport("user32.dll")]
            internal static extern bool DrawMenuBar(IntPtr hWnd);
            [DllImport("user32.dll")]
            internal static extern IntPtr LoadImage(IntPtr hinst, int iconId, uint uType, int cxDesired, int cyDesired, uint fuLoad);
            [DllImport("user32.dll")]
            internal static extern int DestroyIcon(IntPtr hIcon);
            [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            internal static extern IntPtr CreateWindowEx(int dwExStyle, IntPtr classAtom, string lpWindowName, int dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam);
            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool DestroyWindow(IntPtr hwnd);
            [DllImport("user32.dll", CharSet = CharSet.Unicode)]
            internal static extern IntPtr DefWindowProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
            [DllImport("user32.dll", CharSet = CharSet.Unicode)]
            internal static extern Int32 RegisterClass(ref WNDCLASS lpWndClass);
            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool UnregisterClass(IntPtr classAtom, IntPtr hInstance);
            [DllImport("GDI32.dll")]
            internal static extern int RestoreDC(IntPtr hdc, int savedDC);
            [DllImport("GDI32.dll")]
            internal static extern int SaveDC(IntPtr hdc);
            [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
            internal static extern int BitBlt(HandleRef hDC, int x, int y, int nWidth, int nHeight, HandleRef hSrcDC, int xSrc, int ySrc, int dwRop);
            [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
            internal static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);
            [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
            internal static extern IntPtr SelectObject(HandleRef hdc, HandleRef obj);
            [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
            internal static extern IntPtr SelectObject(IntPtr hdc, IntPtr obj);
            [DllImport("gdi32.dll")]
            internal extern static IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
            [DllImport("gdi32.dll")]
            internal static extern int GetDIBits(HandleRef hdc, HandleRef hbm, int arg1, int arg2, IntPtr arg3, ref NativeMethods.BITMAPINFO_FLAT bmi, int arg5);
            [DllImport("gdi32.dll")]
            internal static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int width, int height);
            [DllImport("gdi32.dll")]
            internal static extern IntPtr CreateCompatibleBitmap(HandleRef hDC, int width, int height);
            [DllImport("gdi32.dll")]
            internal static extern IntPtr CreateCompatibleDC(HandleRef hDC);
            [DllImport("gdi32.dll")]
            internal static extern IntPtr CreateCompatibleDC(IntPtr hDC);
            [DllImport("gdi32.dll")]
            public static extern bool DeleteDC(HandleRef hDC);
            [DllImport("gdi32.dll")]
            internal static extern bool DeleteDC(IntPtr hDC);
            [DllImport("gdi32.dll")]
            internal static extern bool DeleteObject(HandleRef hObject);
            [DllImport("gdi32.dll")]
            internal static extern bool DeleteObject(IntPtr hObject);
            [DllImport("gdi32.dll", SetLastError = true)]
            internal static extern IntPtr CreateDIBSection(HandleRef hdc, ref BITMAPINFO_FLAT bmi, int iUsage, ref IntPtr ppvBits, IntPtr hSection, int dwOffset);
            [DllImport("gdi32.dll", SetLastError = true)]
            internal static extern IntPtr CreateDIBSection(IntPtr hdc, ref BITMAPINFO_SMALL bmi, int iUsage, int pvvBits, IntPtr hSection, int dwOffset);
            [DllImport("gdi32.dll")]
            internal static extern int GetPaletteEntries(IntPtr hPal, int startIndex, int entries, byte[] palette);
            [DllImport("comctl32.dll", ExactSpelling = true)]
            internal static extern bool _TrackMouseEvent(TRACKMOUSEEVENT tme);
            [DllImport("user32.dll")]
            internal static extern IntPtr TrackPopupMenu(IntPtr menuHandle, int uFlags, int x, int y, int nReserved, IntPtr hwnd, IntPtr par);
            [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
            internal static extern bool GetViewportOrgEx(IntPtr hDC, ref POINT point);
            [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
            internal static extern bool ScrollWindowEx(IntPtr hWnd, int nXAmount, int nYAmount, RECT rectScrollRegion, ref RECT rectClip, IntPtr hrgnUpdate, ref RECT prcUpdate, int flags);
            [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
            internal static extern bool ScrollWindowEx(IntPtr hWnd, int nXAmount, int nYAmount, IntPtr rectScrollRegion, ref RECT rectClip, IntPtr hrgnUpdate, ref RECT prcUpdate, int flags);
            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            internal static extern int ScrollWindowEx(IntPtr hWnd, int dx, int dy, ref NativeMethods.RECT scrollRect, ref NativeMethods.RECT clipRect, IntPtr hrgnUpdate, IntPtr updateRect, int flags);
            [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
            internal static extern bool ScrollWindow(IntPtr hWnd, int nXAmount, int nYAmount, ref NativeMethods.RECT rectScrollRegion, ref NativeMethods.RECT rectClip);
            [DllImport("USER32.dll")]
            internal static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
            [DllImport("USER32.dll")]
            internal static extern IntPtr GetDCEx(IntPtr hWnd, IntPtr hrgnClip, int flags);
            [DllImport("USER32.dll")]
            internal static extern IntPtr GetWindowDC(IntPtr hwnd);
            [DllImport("USER32.dll")]
            internal static extern int GetClassLong(IntPtr hwnd, int flags);
            [DllImport("USER32.dll")]
            internal static extern int GetWindowLong(IntPtr hwnd, int flags);
            [DllImport("USER32.dll")]
            internal static extern int SetWindowLong(IntPtr hwnd, int flags, int val);
            [DllImport("user32.dll")]
            internal static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);
            [DllImport("USER32.dll")]
            internal static extern IntPtr GetDesktopWindow();
            [DllImport("USER32.dll")]
            internal static extern bool RedrawWindow(IntPtr hwnd, IntPtr rcUpdate, IntPtr hrgnUpdate, int flags);
            [DllImport("USER32.dll")]
            internal static extern short GetAsyncKeyState(int vKey);
            [DllImport("USER32.dll")]
            internal static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
                int X, int Y, int cx, int cy, int uFlags);
            [DllImport("USER32.dll")]
            internal static extern int SetCapture(IntPtr hWnd);
            [DllImport("USER32.dll")]
            internal static extern bool ReleaseCapture();
            [DllImport("USER32.dll")]
            internal static extern bool IsWindowVisible(IntPtr hWnd);
            [DllImport("USER32.dll", CharSet = CharSet.Auto)]
            internal static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, int lParam);
            [DllImport("USER32.dll", CharSet = CharSet.Auto)]
            internal static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, IntPtr lParam);
            [DllImport("USER32.dll")]
            internal static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
            [DllImport("USER32.dll")]
            internal static extern int PostMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
            [DllImport("USER32.dll")]
            internal static extern bool IsZoomed(IntPtr hwnd);
            [DllImport("USER32.dll")]
            internal static extern bool IsIconic(IntPtr hwnd);
            [DllImport("USER32.dll")]
            internal static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);
            [DllImport("USER32.dll")]
            internal static extern bool ValidateRect(IntPtr hwnd, ref RECT lpRect);
            [DllImport("User32.dll")]
            internal static extern int GetUpdateRect(IntPtr hwnd, ref RECT rect, bool erase);
            [DllImport("USER32.dll")]
            internal static extern IntPtr BeginPaint(IntPtr hWnd, [In, Out] ref PAINTSTRUCT lpPaint);
            [DllImport("USER32.dll")]
            internal static extern bool EndPaint(IntPtr hWnd, ref PAINTSTRUCT lpPaint);
            [DllImport("USER32.dll")]
            internal static extern bool LockWindowUpdate(IntPtr hWndLock);
            [DllImport("USER32.dll")]
            internal static extern int SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool redraw);
            [DllImport("GDI32.dll")]
            internal static extern int CombineRgn(IntPtr hrgnDest, IntPtr hrgnSrc1, IntPtr hrgnSrc2, int fnCombineMode);
            [DllImport("GDI32.dll")]
            internal static extern int ExcludeClipRect(IntPtr hdc, int left, int top, int right, int bottom);
            [DllImport("GDI32.dll")]
            internal static extern int GetClipRgn(IntPtr hdc, IntPtr hrgn);
            [DllImport("GDI32.dll")]
            internal static extern int SelectClipRgn(IntPtr hdc, IntPtr hrgn);
            [DllImport("GDI32.dll")]
            internal static extern int ExtSelectClipRgn(IntPtr hdc, IntPtr hrgn, int mode);
            [DllImport("gdi32.dll")]
            internal static extern bool LPtoDP(IntPtr hdc, [In, Out] POINT[] lpPoints, int nCount);
            [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
            internal static extern bool GetUpdateRgn(IntPtr hwnd, IntPtr hrgn, bool fErase);
            [DllImport("gdi32.dll")]
            internal static extern int GetRegionData(IntPtr hRgn, int dwCount, IntPtr lpRgnData);
            [DllImport("gdi32.dll")]
            internal static extern int OffsetRgn(IntPtr hrgn, int nXOffset, int nYOffset);
            [DllImport("user32.dll", SetLastError = true)]
            internal static extern int MapWindowPoints(IntPtr hwndFrom, IntPtr hwndTo, ref POINT lpPoints, [MarshalAs(UnmanagedType.U4)] int cPoints);
            [DllImport("gdi32.dll")]
            internal static extern int GetRandomRgn(IntPtr hdc, IntPtr hrgn, int iNum);
            [DllImport("GDI32.dll")]
            internal static extern IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);
            [DllImport("GDI32.dll")]
            internal static extern bool RectVisible(IntPtr hdc, ref NativeMethods.RECT rect);
            [DllImport("User32.dll", CharSet = CharSet.Auto)]
            internal static extern bool DragDetect(IntPtr hwnd, POINT pt);
            [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
            internal static extern int GetObject(IntPtr hObject, int nSize, [In, Out] LOGFONT lf);
            [DllImport("gdi32.dll")]
            internal static extern IntPtr SelectPalette(IntPtr hdc, IntPtr hpal, bool bForceBackground);
            [DllImport("gdi32.dll")]
            internal static extern int RealizePalette(IntPtr hdc);
            [DllImport("User32.dll")]
            internal static extern HDC GetDC(HWND handle);
            [DllImport("User32.dll")]
            internal static extern IntPtr GetCursor();
            [DllImport("User32.dll")]
            internal static extern bool SetSystemCursor(IntPtr hCursor, int id);
            [DllImport("Gdi32.dll")]
            internal static extern IntPtr CreateSolidBrush(COLORREF aColorRef);
            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool FillRect(HDC hdc, ref RECT rect, IntPtr hbrush);
            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool FillRect(IntPtr hdc, ref RECT rect, IntPtr hbrush);
            [DllImport("GDI32.dll")]
            internal static extern bool FillRgn(IntPtr hdc, IntPtr hrgn, IntPtr brush);
            [DllImport("gdi32.dll")]
            internal static extern int GetPixel(IntPtr hdc, int nXPos, int nYPos);
            [DllImport("gdi32.dll")]
            internal static extern bool StretchBlt(IntPtr hdcDest, int nXOriginDest, int nYOriginDest, int nWidthDest, int nHeightDest, IntPtr hdcSrc, int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc, int dwRop);
            [DllImport("User32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool UnhookWindowsHookEx(HHOOK aHook);
            [DllImport("User32.dll")]
            internal static extern IntPtr CopyIcon(IntPtr hCursor);
            [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, int dwThreadId);
            [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);
            [DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal static extern IntPtr GetModuleHandle(string lpModuleName);
            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
            internal static extern bool UpdateLayeredWindow(IntPtr hwnd,
                IntPtr hdcDst, ref NativeMethods.POINT pptDst, ref NativeMethods.SIZE pSizeDst,
                IntPtr hdcSrc, ref NativeMethods.POINT pptSrc,
                int crKey, ref BLENDFUNCTION pBlend, int dwFlags);
            [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
            internal static extern bool InvalidateRgn(IntPtr hWnd, IntPtr hrgn, bool erase);
            [DllImport("user32.dll")]
            internal static extern bool SetLayeredWindowAttributes(IntPtr hwnd, int crKey, byte bAlpha, uint dwFlags);
            [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
            internal static extern bool AdjustWindowRectEx(ref NativeMethods.RECT lpRect, int dwStyle, bool bMenu, int dwExStyle);
            [DllImport("user32.dll")]
            internal static extern IntPtr FindWindow(string className, string windowText);
            [DllImport("user32.dll")]
            internal static extern int ShowWindow(IntPtr hWnd, int command);
            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool ShowWindow(IntPtr hWnd, ShowWindowCommands nCmdShow);
            [DllImport("user32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool GetWindowPlacement(IntPtr hWnd, out WINDOWPLACEMENT lpwndpl);
            [DllImport("User32.dll")]
            internal static extern IntPtr WindowFromPoint(Point pt);
            [DllImport("User32.dll")]
            internal static extern IntPtr GetWindow(IntPtr hWnd, uint wCmd);
            [DllImport("User32.dll")]
            internal static extern bool SetForegroundWindow(IntPtr hWnd);
            [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
            internal static extern bool EnableWindow(IntPtr hWnd, bool enable);
            [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
            internal static extern bool IsWindowEnabled(IntPtr hWnd);
            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal static extern bool SystemParametersInfo(int uiAction, int uiParam, [In, Out] NONCLIENTMETRICS pvParam, int fWinIni);
            [DllImport("user32")]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool SetGestureConfig(IntPtr hWnd, int dwReserved, int cIDs, [In, Out] GESTURECONFIG[] pGestureConfig, int cbSize);
            [DllImport("UxTheme")]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool BeginPanningFeedback(IntPtr hWnd);
            [DllImport("UxTheme")]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool EndPanningFeedback(IntPtr hWnd, bool fAnimateBack);
            [DllImport("UxTheme")]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool UpdatePanningFeedback(IntPtr hwnd, int lTotalOverpanOffsetX, int lTotalOverpanOffsetY, bool fInInertia);
            [DllImport("user32")]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool CloseGestureInfoHandle(IntPtr hGestureInfo);
            [DllImport("user32")]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool GetGestureInfo(IntPtr hGestureInfo, ref GESTUREINFO pGestureInfo);
        }
        #endregion SecurityCritical
        internal static int SHAppBarMessage(int dwMessage, ref APPBARDATA pData)
        {
            return UnsafeNativeMethods.SHAppBarMessage(dwMessage, ref pData);
        }
        public static bool DrawMenuBar(IntPtr hWnd)
        {
            return UnsafeNativeMethods.DrawMenuBar(hWnd);
        }
        public static IntPtr LoadImage(IntPtr hinst, int iconId, int uType, int cxDesired, int cyDesired, int fuLoad)
        {
            return UnsafeNativeMethods.LoadImage(hinst, iconId, (uint)uType, cxDesired, cyDesired, (uint)fuLoad);
        }
        public static int DestroyIcon(IntPtr hIcon)
        {
            return UnsafeNativeMethods.DestroyIcon(hIcon);
        }
        public static IntPtr CreateWindowEx(int dwExStyle, IntPtr classAtom, string lpWindowName, int dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam)
        {
            return UnsafeNativeMethods.CreateWindowEx(dwExStyle, classAtom, lpWindowName, dwStyle, x, y, nWidth, nHeight, hWndParent, hMenu, hInstance, lpParam);
        }
        public static bool DestroyWindow(IntPtr hWnd)
        {
            return UnsafeNativeMethods.DestroyWindow(hWnd);
        }
        public static IntPtr DefWindowProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam)
        {
            return UnsafeNativeMethods.DefWindowProc(hWnd, msg, wParam, lParam);
        }
        public static Int32 RegisterClass(ref WNDCLASS lpWndClass)
        {
            return UnsafeNativeMethods.RegisterClass(ref lpWndClass);
        }
        public static bool UnregisterClass(IntPtr classAtom, IntPtr hInstance)
        {
            return UnsafeNativeMethods.UnregisterClass(classAtom, hInstance);
        }
        public static int RestoreDC(IntPtr hdc, int savedDC)
        {
            return UnsafeNativeMethods.RestoreDC(hdc, savedDC);
        }
        public static int SaveDC(IntPtr hdc)
        {
            return UnsafeNativeMethods.SaveDC(hdc);
        }
        public static int BitBlt(HandleRef hDC, int x, int y, int nWidth, int nHeight, HandleRef hSrcDC, int xSrc, int ySrc, int dwRop)
        {
            return UnsafeNativeMethods.BitBlt(hDC, x, y, nWidth, nHeight, hSrcDC, xSrc, ySrc, dwRop);
        }
        public static int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop)
        {
            return UnsafeNativeMethods.BitBlt(hDC, x, y, nWidth, nHeight, hSrcDC, xSrc, ySrc, dwRop);
        }
        public static IntPtr SelectObject(HandleRef hdc, HandleRef obj)
        {
            return UnsafeNativeMethods.SelectObject(hdc, obj);
        }
        public static bool GetClientRect(IntPtr hWnd, out NativeMethods.RECT rect)
        {
            return UnsafeNativeMethods.GetClientRect(hWnd, out rect);
        }
        public static IntPtr SelectObject(IntPtr hdc, IntPtr obj)
        {
            return UnsafeNativeMethods.SelectObject(hdc, obj);
        }
        public static IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse)
        {
            return UnsafeNativeMethods.CreateRoundRectRgn(nLeftRect, nTopRect, nRightRect, nBottomRect, nWidthEllipse, nHeightEllipse);
        }
        public static int GetDIBits(HandleRef hdc, HandleRef hbm, int arg1, int arg2, IntPtr arg3, ref NativeMethods.BITMAPINFO_FLAT bmi, int arg5)
        {
            return UnsafeNativeMethods.GetDIBits(hdc, hbm, arg1, arg2, arg3, ref bmi, arg5);
        }
        public static IntPtr CreateCompatibleBitmap(IntPtr hDC, int width, int height)
        {
            return UnsafeNativeMethods.CreateCompatibleBitmap(hDC, width, height);
        }
        public static IntPtr CreateCompatibleBitmap(HandleRef hDC, int width, int height)
        {
            return UnsafeNativeMethods.CreateCompatibleBitmap(hDC, width, height);
        }
        public static IntPtr CreateCompatibleDC(HandleRef hDC)
        {
            return UnsafeNativeMethods.CreateCompatibleDC(hDC);
        }
        public static IntPtr CreateCompatibleDC(IntPtr hDC)
        {
            return UnsafeNativeMethods.CreateCompatibleDC(hDC);
        }
        public static bool DeleteDC(HandleRef hDC)
        {
            return UnsafeNativeMethods.DeleteDC(hDC);
        }
        public static bool DeleteDC(IntPtr hDC)
        {
            return UnsafeNativeMethods.DeleteDC(hDC);
        }
        public static bool DeleteObject(HandleRef hObject)
        {
            return UnsafeNativeMethods.DeleteObject(hObject);
        }
        public static bool DeleteObject(IntPtr hObject)
        {
            return UnsafeNativeMethods.DeleteObject(hObject);
        }
        public static IntPtr CreateDIBSection(HandleRef hdc, ref BITMAPINFO_FLAT bmi, int iUsage, ref IntPtr ppvBits, IntPtr hSection, int dwOffset)
        {
            return UnsafeNativeMethods.CreateDIBSection(hdc, ref bmi, iUsage, ref ppvBits, hSection, dwOffset);
        }
        public static IntPtr CreateDIBSection(IntPtr hdc, ref BITMAPINFO_SMALL bmi, int iUsage, int pvvBits, IntPtr hSection, int dwOffset)
        {
            return UnsafeNativeMethods.CreateDIBSection(hdc, ref bmi, iUsage, pvvBits, hSection, dwOffset);
        }
        public static int GetPaletteEntries(IntPtr hPal, int startIndex, int entries, byte[] palette)
        {
            return UnsafeNativeMethods.GetPaletteEntries(hPal, startIndex, entries, palette);
        }
        internal static bool _TrackMouseEvent(TRACKMOUSEEVENT tme)
        {
            return UnsafeNativeMethods._TrackMouseEvent(tme);
        }
        public static IntPtr TrackPopupMenu(IntPtr menuHandle, int uFlags, int x, int y, int nReserved, IntPtr hwnd, IntPtr par)
        {
            return UnsafeNativeMethods.TrackPopupMenu(menuHandle, uFlags, x, y, nReserved, hwnd, par);
        }
        public static bool GetViewportOrgEx(IntPtr hDC, ref POINT point)
        {
            return UnsafeNativeMethods.GetViewportOrgEx(hDC, ref point);
        }
        public static bool ScrollWindowEx(IntPtr hWnd, int nXAmount, int nYAmount, RECT rectScrollRegion, ref RECT rectClip, IntPtr hrgnUpdate, ref RECT prcUpdate, int flags)
        {
            return UnsafeNativeMethods.ScrollWindowEx(hWnd, nXAmount, nYAmount, rectScrollRegion, ref rectClip, hrgnUpdate, ref prcUpdate, flags);
        }
        public static bool ScrollWindowEx(IntPtr hWnd, int nXAmount, int nYAmount, IntPtr rectScrollRegion, ref RECT rectClip, IntPtr hrgnUpdate, ref RECT prcUpdate, int flags)
        {
            return UnsafeNativeMethods.ScrollWindowEx(hWnd, nXAmount, nYAmount, rectScrollRegion, ref rectClip, hrgnUpdate, ref prcUpdate, flags);
        }
        public static int ScrollWindowEx(IntPtr hWnd, int dx, int dy, ref NativeMethods.RECT scrollRect, ref NativeMethods.RECT clipRect, IntPtr hrgnUpdate, IntPtr updateRect, int flags)
        {
            return UnsafeNativeMethods.ScrollWindowEx(hWnd, dx, dy, ref scrollRect, ref clipRect, hrgnUpdate, updateRect, flags);
        }
        public static bool ScrollWindow(IntPtr hWnd, int nXAmount, int nYAmount, ref NativeMethods.RECT rectScrollRegion, ref NativeMethods.RECT rectClip)
        {
            return UnsafeNativeMethods.ScrollWindow(hWnd, nXAmount, nYAmount, ref rectScrollRegion, ref rectClip);
        }
        public static int ReleaseDC(IntPtr hWnd, IntPtr hDC)
        {
            return UnsafeNativeMethods.ReleaseDC(hWnd, hDC);
        }
        public static IntPtr GetDCEx(IntPtr hWnd, IntPtr hrgnClip, int flags)
        {
            return UnsafeNativeMethods.GetDCEx(hWnd, hrgnClip, flags);
        }
        public static IntPtr GetWindowDC(IntPtr hWnd)
        {
            return UnsafeNativeMethods.GetWindowDC(hWnd);
        }
        public static int GetClassLong(IntPtr hWnd, int flags)
        {
            return UnsafeNativeMethods.GetClassLong(hWnd, flags);
        }
        public static int GetWindowLong(IntPtr hWnd, int flags)
        {
            return UnsafeNativeMethods.GetWindowLong(hWnd, flags);
        }
        public static int SetWindowLong(IntPtr hWnd, int flags, int val)
        {
            return UnsafeNativeMethods.SetWindowLong(hWnd, flags, val);
        }
        public static IntPtr SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
        {
            return UnsafeNativeMethods.SetWindowLong(hWnd, nIndex, dwNewLong);
        }
        public static IntPtr GetDesktopWindow()
        {
            return UnsafeNativeMethods.GetDesktopWindow();
        }
        public static bool RedrawWindow(IntPtr hWnd, IntPtr rcUpdate, IntPtr hrgnUpdate, int flags)
        {
            return UnsafeNativeMethods.RedrawWindow(hWnd, rcUpdate, hrgnUpdate, flags);
        }
        public static short GetAsyncKeyState(int vKey)
        {
            return UnsafeNativeMethods.GetAsyncKeyState(vKey);
        }
        public static bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, int uFlags)
        {
            return UnsafeNativeMethods.SetWindowPos(hWnd, hWndInsertAfter, x, y, cx, cy, uFlags);
        }
        public static int SetCapture(IntPtr hWnd)
        {
            return UnsafeNativeMethods.SetCapture(hWnd);
        }
        public static bool ReleaseCapture()
        {
            return UnsafeNativeMethods.ReleaseCapture();
        }
        public static bool IsWindowVisible(IntPtr hWnd)
        {
            return UnsafeNativeMethods.IsWindowVisible(hWnd);
        }
        public static IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, int lParam)
        {
            return UnsafeNativeMethods.SendMessage(hWnd, Msg, wParam, lParam);
        }
        public static IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, IntPtr lParam)
        {
            return UnsafeNativeMethods.SendMessage(hWnd, Msg, wParam, lParam);
        }
        public static int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam)
        {
            return UnsafeNativeMethods.SendMessage(hWnd, Msg, wParam, lParam);
        }
        public static int PostMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam)
        {
            return UnsafeNativeMethods.PostMessage(hWnd, Msg, wParam, lParam);
        }
        public static bool IsZoomed(IntPtr hWnd)
        {
            return UnsafeNativeMethods.IsZoomed(hWnd);
        }
        public static bool IsIconic(IntPtr hWnd)
        {
            return UnsafeNativeMethods.IsIconic(hWnd);
        }
        public static bool GetWindowRect(IntPtr hWnd, ref RECT lpRect)
        {
            return UnsafeNativeMethods.GetWindowRect(hWnd, ref lpRect);
        }
        public static bool ValidateRect(IntPtr hWnd, ref RECT lpRect)
        {
            return UnsafeNativeMethods.ValidateRect(hWnd, ref lpRect);
        }
        public static IntPtr BeginPaint(IntPtr hWnd, [In, Out] ref PAINTSTRUCT lpPaint)
        {
            return UnsafeNativeMethods.BeginPaint(hWnd, ref lpPaint);
        }
        public static bool EndPaint(IntPtr hWnd, ref PAINTSTRUCT lpPaint)
        {
            return UnsafeNativeMethods.EndPaint(hWnd, ref lpPaint);
        }
        public static bool LockWindowUpdate(IntPtr hWnd)
        {
            return UnsafeNativeMethods.LockWindowUpdate(hWnd);
        }
        public static int SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool redraw)
        {
            return UnsafeNativeMethods.SetWindowRgn(hWnd, hRgn, redraw);
        }
        public static int CombineRgn(IntPtr hrgnDest, IntPtr hrgnSrc1, IntPtr hrgnSrc2, int fnCombineMode)
        {
            return UnsafeNativeMethods.CombineRgn(hrgnDest, hrgnSrc1, hrgnSrc2, fnCombineMode);
        }
        public static int ExcludeClipRect(IntPtr hdc, int left, int top, int right, int bottom)
        {
            return UnsafeNativeMethods.ExcludeClipRect(hdc, left, top, right, bottom);
        }
        public static int GetClipRgn(IntPtr hdc, IntPtr hrgn)
        {
            return UnsafeNativeMethods.GetClipRgn(hdc, hrgn);
        }
        public static int SelectClipRgn(IntPtr hdc, IntPtr hrgn)
        {
            return UnsafeNativeMethods.SelectClipRgn(hdc, hrgn);
        }
        public static int ExtSelectClipRgn(IntPtr hdc, IntPtr hrgn, int mode)
        {
            return UnsafeNativeMethods.ExtSelectClipRgn(hdc, hrgn, mode);
        }
        public static bool LPtoDP(IntPtr hdc, [In, Out] POINT[] lpPoints, int nCount)
        {
            return UnsafeNativeMethods.LPtoDP(hdc, lpPoints, nCount);
        }
        public static int GetUpdateRect(IntPtr hWnd, ref RECT lpRect, bool erase)
        {
            return UnsafeNativeMethods.GetUpdateRect(hWnd, ref lpRect, erase);
        }
        public static bool GetUpdateRgn(IntPtr hWnd, IntPtr hrgn, bool erase)
        {
            return UnsafeNativeMethods.GetUpdateRgn(hWnd, hrgn, erase);
        }
        public static int GetRegionData(IntPtr hRgn, int dwCount, IntPtr lpRgnData)
        {
            return UnsafeNativeMethods.GetRegionData(hRgn, dwCount, lpRgnData);
        }
        public static int OffsetRgn(IntPtr hRgn, int nXOffset, int nYOffset)
        {
            return UnsafeNativeMethods.OffsetRgn(hRgn, nXOffset, nYOffset);
        }
        public static int MapWindowPoints(IntPtr hwndFrom, IntPtr hwndTo, ref POINT lpPoints, [MarshalAs(UnmanagedType.U4)] int cPoints)
        {
            return UnsafeNativeMethods.MapWindowPoints(hwndFrom, hwndTo, ref lpPoints, cPoints);
        }
        public static int GetRandomRgn(IntPtr hdc, IntPtr hrgn, int iNum)
        {
            return UnsafeNativeMethods.GetRandomRgn(hdc, hrgn, iNum);
        }
        public static IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect)
        {
            return UnsafeNativeMethods.CreateRectRgn(nLeftRect, nTopRect, nRightRect, nBottomRect);
        }
        public static bool RectVisible(IntPtr hdc, ref NativeMethods.RECT rect)
        {
            return UnsafeNativeMethods.RectVisible(hdc, ref rect);
        }
        public static bool DragDetect(IntPtr hWnd, POINT pt)
        {
            return UnsafeNativeMethods.DragDetect(hWnd, pt);
        }
        public static int GetObject(IntPtr hObject, int nSize, [In, Out] LOGFONT lf)
        {
            return UnsafeNativeMethods.GetObject(hObject, nSize, lf);
        }
        public static IntPtr SelectPalette(IntPtr hdc, IntPtr hpal, bool bForceBackground)
        {
            return UnsafeNativeMethods.SelectPalette(hdc, hpal, bForceBackground);
        }
        public static int RealizePalette(IntPtr hdc)
        {
            return UnsafeNativeMethods.RealizePalette(hdc);
        }
        public static HDC GetDC(HWND handle)
        {
            return UnsafeNativeMethods.GetDC(handle);
        }
        public static IntPtr GetCursor()
        {
            return UnsafeNativeMethods.GetCursor();
        }
        public static bool SetSystemCursor(IntPtr hCursor, int id)
        {
            return UnsafeNativeMethods.SetSystemCursor(hCursor, id);
        }
        internal static IntPtr CreateSolidBrush(COLORREF aColorRef)
        {
            return UnsafeNativeMethods.CreateSolidBrush(aColorRef);
        }
        public static bool FillRect(IntPtr hdc, ref RECT rect, IntPtr hbrush)
        {
            return UnsafeNativeMethods.FillRect(hdc, ref rect, hbrush);
        }
        public static bool FillRect(HDC hdc, ref RECT rect, IntPtr hbrush)
        {
            return UnsafeNativeMethods.FillRect(hdc, ref rect, hbrush);
        }
        public static bool FillRgn(IntPtr hdc, IntPtr hrgn, IntPtr hbrush)
        {
            return UnsafeNativeMethods.FillRgn(hdc, hrgn, hbrush);
        }
        public static int GetPixel(IntPtr hdc, int nXPos, int nYPos)
        {
            return UnsafeNativeMethods.GetPixel(hdc, nXPos, nYPos);
        }
        public static bool StretchBlt(IntPtr hdcDest, int nXOriginDest, int nYOriginDest, int nWidthDest, int nHeightDest, IntPtr hdcSrc, int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc, int dwRop)
        {
            return UnsafeNativeMethods.StretchBlt(hdcDest, nXOriginDest, nYOriginDest, nWidthDest, nHeightDest, hdcSrc, nXOriginSrc, nYOriginSrc, nWidthSrc, nHeightSrc, dwRop);
        }
        public static bool UnhookWindowsHookEx(HHOOK aHook)
        {
            return UnsafeNativeMethods.UnhookWindowsHookEx(aHook);
        }
        public static IntPtr CopyIcon(IntPtr hCursor)
        {
            return UnsafeNativeMethods.CopyIcon(hCursor);
        }
        public static IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, int dwThreadId)
        {
            return UnsafeNativeMethods.SetWindowsHookEx(idHook, lpfn, hMod, dwThreadId);
        }
        public static IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam)
        {
            return UnsafeNativeMethods.CallNextHookEx(hhk, nCode, wParam, lParam);
        }
        public static IntPtr GetModuleHandle(string lpModuleName)
        {
            return UnsafeNativeMethods.GetModuleHandle(lpModuleName);
        }
        public static bool IsKeyboardContextMenuMessage(Message msg)
        {
            if (msg.Msg != MSG.WM_CONTEXTMENU) return false;
            Point pt = new Point(msg.LParam.ToInt32());
            if (pt.X == -1 && pt.Y == -1) return true;
            return false;
        }
        public static void ExcludeClipRect(IntPtr hdc, Rectangle rect)
        {
            UnsafeNativeMethods.ExcludeClipRect(hdc, rect.X, rect.Y, rect.Right, rect.Bottom);
        }
        public static Rectangle[] GetClipRectsFromHDC(IntPtr hWnd, IntPtr hdc, bool offsetPoints)
        {
            IntPtr rgn = NativeMethods.CreateRectRgn(0, 0, 0, 0);
            try
            {
                if (UnsafeNativeMethods.GetRandomRgn(hdc, rgn, SYSRGN) != 1) return null;
                if (offsetPoints)
                {
                    POINT pt = new POINT();
                    UnsafeNativeMethods.MapWindowPoints(IntPtr.Zero, hWnd, ref pt, 1);
                    UnsafeNativeMethods.OffsetRgn(rgn, pt.X, pt.Y);
                }
                RECT[] apirects = RectsFromRegion(rgn);
                if (apirects == null || apirects.Length == 0) return null;
                Rectangle[] res = new Rectangle[apirects.Length];
                for (int n = 0; n < apirects.Length; n++)
                {
                    res[n] = apirects[n].ToRectangle();
                }
                return res;
            }
            finally
            {
                UnsafeNativeMethods.DeleteObject(rgn);
            }
        }
        public static NativeMethods.RECT[] RectsFromRegion(IntPtr hRgn)
        {
            NativeMethods.RECT[] rects = null;
            int dataSize = UnsafeNativeMethods.GetRegionData(hRgn, 0, IntPtr.Zero);
            if (dataSize != 0)
            {
                IntPtr bytes = IntPtr.Zero;
                bytes = Marshal.AllocCoTaskMem(dataSize);
                int retValue = UnsafeNativeMethods.GetRegionData(hRgn, dataSize, bytes);
                RGNDATAHEADER header = (RGNDATAHEADER)Marshal.PtrToStructure(bytes, typeof(RGNDATAHEADER));
                if (header.iType == (int)NativeMethods.RegionDataHeaderTypes.Rectangles)
                {
                    rects = new NativeMethods.RECT[header.nCount];
                    int rectOffset = header.dwSize;
                    for (int i = 0; i < header.nCount; i++)
                    {
                        IntPtr offset = new IntPtr(bytes.ToInt64() + rectOffset + (Marshal.SizeOf(typeof(NativeMethods.RECT)) * i));
                        rects[i] = (NativeMethods.RECT)Marshal.PtrToStructure(offset, typeof(NativeMethods.RECT));
                    }
                }
            }
            return rects;
        }
        public static IntPtr CreateSolidBrush(Color aColor)
        {
            return UnsafeNativeMethods.CreateSolidBrush(new COLORREF(aColor));
        }
        public static IntPtr CreateSolidBrush(int argb)
        {
            return UnsafeNativeMethods.CreateSolidBrush(new COLORREF(argb));
        }
        public static Region CreateRoundRegion(Rectangle windowBounds, int ellipseSize)
        {
            IntPtr rgn = UnsafeNativeMethods.CreateRoundRectRgn(windowBounds.X, windowBounds.Y, windowBounds.Width + 1, windowBounds.Height + 1, ellipseSize, ellipseSize);
            Region res = Region.FromHrgn(rgn);
            DeleteObject(rgn);
            return res;
        }
        public static bool UpdateLayeredWindow(IntPtr hwnd,
            IntPtr hdcDst, ref NativeMethods.POINT pptDst, ref NativeMethods.SIZE pSizeDst,
            IntPtr hdcSrc, ref NativeMethods.POINT pptSrc,
            int crKey, ref BLENDFUNCTION pBlend, int dwFlags)
        {
            return UnsafeNativeMethods.UpdateLayeredWindow(hwnd, hdcDst, ref pptDst, ref pSizeDst, hdcSrc, ref pptSrc, crKey, ref pBlend, dwFlags);
        }
        public static bool InvalidateRgn(IntPtr hWnd, IntPtr hrgn, bool erase)
        {
            return UnsafeNativeMethods.InvalidateRgn(hWnd, hrgn, erase);
        }
        public static bool SetLayeredWindowAttributes(IntPtr hwnd, int crKey, byte bAlpha, int dwFlags)
        {
            return UnsafeNativeMethods.SetLayeredWindowAttributes(hwnd, crKey, bAlpha, (uint)dwFlags);
        }
        public static bool AdjustWindowRectEx(ref NativeMethods.RECT lpRect, int dwStyle, bool bMenu, int dwExStyle)
        {
            return UnsafeNativeMethods.AdjustWindowRectEx(ref lpRect, dwStyle, bMenu, dwExStyle);
        }
        public static IntPtr FindWindow(string className, string windowText)
        {
            return UnsafeNativeMethods.FindWindow(className, windowText);
        }
        public static int ShowWindow(IntPtr hWnd, int command)
        {
            return UnsafeNativeMethods.ShowWindow(hWnd, command);
        }
        public static bool ShowWindow(IntPtr hWnd, ShowWindowCommands command)
        {
            return UnsafeNativeMethods.ShowWindow(hWnd, command);
        }
        public static bool GetWindowPlacement(IntPtr hWnd, out WINDOWPLACEMENT lpwndpl)
        {
            return UnsafeNativeMethods.GetWindowPlacement(hWnd, out lpwndpl);
        }
        public static IntPtr WindowFromPoint(Point pt)
        {
            return UnsafeNativeMethods.WindowFromPoint(pt);
        }
        public static IntPtr GetWindow(IntPtr hWnd, int wCmd)
        {
            return UnsafeNativeMethods.GetWindow(hWnd, (uint)wCmd);
        }
        public static bool SetForegroundWindow(IntPtr hWnd)
        {
            return UnsafeNativeMethods.SetForegroundWindow(hWnd);
        }
        public static bool EnableWindow(IntPtr hWnd, bool enable)
        {
            return UnsafeNativeMethods.EnableWindow(hWnd, enable);
        }
        public static bool IsWindowEnabled(IntPtr hWnd)
        {
            return UnsafeNativeMethods.IsWindowEnabled(hWnd);
        }
        public static bool SystemParametersInfo(int uiAction, int uiParam, NONCLIENTMETRICS pvParam, int fWinIni)
        {
            return UnsafeNativeMethods.SystemParametersInfo(uiAction, uiParam, pvParam, fWinIni);
        }
        public static bool SetGestureConfig(IntPtr hWnd, int dwReserved, int cIDs, [In, Out] GESTURECONFIG[] pGestureConfig, int cbSize)
        {
            return UnsafeNativeMethods.SetGestureConfig(hWnd, dwReserved, cIDs, pGestureConfig, cbSize);
        }
        public static bool BeginPanningFeedback(IntPtr hWnd)
        {
            return UnsafeNativeMethods.BeginPanningFeedback(hWnd);
        }
        public static bool EndPanningFeedback(IntPtr hWnd, bool fAnimateBack)
        {
            return UnsafeNativeMethods.EndPanningFeedback(hWnd, fAnimateBack);
        }
        public static bool UpdatePanningFeedback(IntPtr hwnd, int lTotalOverpanOffsetX, int lTotalOverpanOffsetY, bool fInInertia)
        {
            return UnsafeNativeMethods.UpdatePanningFeedback(hwnd, lTotalOverpanOffsetX, lTotalOverpanOffsetY, fInInertia);
        }
        public static bool CloseGestureInfoHandle(IntPtr hGestureInfo)
        {
            return UnsafeNativeMethods.CloseGestureInfoHandle(hGestureInfo);
        }
        public static bool GetGestureInfo(IntPtr hGestureInfo, ref GESTUREINFO pGestureInfo)
        {
            return UnsafeNativeMethods.GetGestureInfo(hGestureInfo, ref pGestureInfo);
        }
        public delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);
        public delegate IntPtr WndProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        public static int SYSRGN = 4;
        public const int RGN_AND = 1, RGN_OR = 2, RGN_XOR = 3, RGN_DIFF = 4, RGN_COPY = 5;
        public const int MAX_PATH = 260;
        public const int GW_HWNDFIRST = 0;
        public const int GW_HWNDLAST = 1;
        public const int GW_HWNDNEXT = 2;
        public const int GW_HWNDPREV = 3;
        public const int GW_OWNER = 4;
        public const int GW_CHILD = 5;
    }
}
