using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace Azelea.WPF.Controls
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct POINT
    {
        public int X;
        public int Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct MINMAXINFO
    {
        public POINT Reserved;
        public POINT MaxSize;
        public POINT MaxPosition;
        public POINT MinTrackSize;
        public POINT MaxTrackSize;
    };

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal class MONITORINFO
    {
        public int Size = Marshal.SizeOf(typeof(MONITORINFO));
        public RECT Monitor;
        public RECT Work;
        public int Flags;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    public static class WindowHelper
    {
        public const int MONITOR_DEFAULTTONEAREST = 0x00000002;
        public const int WM_SYSCOMMAND = 0x0112;
        public const int WM_GETMINMAXINFO = 0x0024;
        public const int SC_MOVEHEADER = 0xF012;
        public const int SC_CLOSE = 61536;
        public const int SC_SIZE_LEFT = 0xF001;
        public const int SC_SIZE_RIGHT = 0xF002;
        public const int SC_SIZE_TOP = 0xF003;
        public const int SC_SIZE_TOPLEFT = 0xF004;
        public const int SC_SIZE_TOPRIGHT = 0xF005;
        public const int SC_SIZE_BOTTOM = 0xF006;
        public const int SC_SIZE_BOTTOMLEFT = 0xF007;
        public const int SC_SIZE_BOTTOMRIGHT = 0xF008;

        public static IntPtr Handle = IntPtr.Zero;

        public static void RepairWindow(Window window)
        {
            window.SourceInitialized += delegate
            {
                Handle = (new WindowInteropHelper(window)).Handle;
                HwndSource source = HwndSource.FromHwnd(Handle);
                if (source != null)
                {
                    source.AddHook(WindowHelper.WindowProc);
                }
            };
        }

        private static IntPtr WindowProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case WM_GETMINMAXINFO:
                    WmGetMinMaxInfo(hWnd, lParam);
                    handled = true;
                    break;
            }

            return IntPtr.Zero;
        }

        private static void WmGetMinMaxInfo(IntPtr hWnd, IntPtr lParam)
        {
            var mmi = (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));
            IntPtr pMonitor = MonitorFromWindow(hWnd, MONITOR_DEFAULTTONEAREST);
            if (pMonitor != IntPtr.Zero)
            {
                var mi = new MONITORINFO();
                GetMonitorInfo(pMonitor, mi);
                RECT rcWorkArea = mi.Work;
                RECT rcMonitorArea = mi.Monitor;
                mmi.MaxPosition.X = Math.Abs(rcWorkArea.Left - rcMonitorArea.Left);
                mmi.MaxPosition.Y = Math.Abs(rcWorkArea.Top - rcMonitorArea.Top);
                mmi.MaxSize.X = Math.Abs(rcWorkArea.Right - rcWorkArea.Left);
                mmi.MaxSize.Y = Math.Abs(rcWorkArea.Bottom - rcWorkArea.Top);
            }

            Marshal.StructureToPtr(mmi, lParam, true);
        }

        [DllImport("user32")]
        internal static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32")]
        internal static extern bool GetMonitorInfo(IntPtr hMonitor, MONITORINFO lpmi);
        [DllImport("user32")]
        internal static extern IntPtr MonitorFromWindow(IntPtr handle, int flags);
        [DllImport("user32")]
        internal static extern bool GetCursorPos(ref POINT point);
    }
}
