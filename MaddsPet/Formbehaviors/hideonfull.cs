using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace MaddsPet.Formbehaviors
{
    internal class hideonfull
    {
        private readonly Form form;
        private readonly System.Windows.Forms.Timer timer;

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(
            IntPtr hWnd,
            out RECT lpRect
        );

        [DllImport("user32.dll")]
        private static extern IntPtr MonitorFromWindow(
            IntPtr hwnd,
            uint dwFlags
        );

        [DllImport("user32.dll")]
        private static extern bool GetMonitorInfo(
            IntPtr hMonitor,
            ref MONITORINFO lpmi
        );

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int GetClassName(
            IntPtr hWnd,
            StringBuilder lpClassName,
            int nMaxCount
        );

        private const uint MONITOR_DEFAULTTONEAREST = 2;

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MONITORINFO
        {
            public int cbSize;
            public RECT rcMonitor;
            public RECT rcWork;
            public uint dwFlags;
        }

        public hideonfull(Form form)
        {
            this.form = form;

            timer = new System.Windows.Forms.Timer
            {
                Interval = 500
            };

            timer.Tick += CheckFullscreen;
            timer.Start();
        }

        private void CheckFullscreen(object sender, EventArgs e)
        {
            IntPtr window = GetForegroundWindow();

            if (window == IntPtr.Zero || window == form.Handle)
                return;
            if (IsDesktopOrShellWindow(window))
                return;

            if (!GetWindowRect(window, out RECT windowRect))
                return;
            IntPtr monitor = MonitorFromWindow(
                window,
                MONITOR_DEFAULTTONEAREST
            );
            MONITORINFO monitorInfo = new MONITORINFO
            {
                cbSize = Marshal.SizeOf<MONITORINFO>()
            };
            if (!GetMonitorInfo(monitor, ref monitorInfo))
                return;

            RECT screen = monitorInfo.rcMonitor;
            bool fullscreen =
                windowRect.Left <= screen.Left &&
                windowRect.Top <= screen.Top &&
                windowRect.Right >= screen.Right &&
                windowRect.Bottom >= screen.Bottom;
            if (fullscreen)
            {
                if (form.Visible)
                    form.Hide();
            }
            else
            {
                if (!form.Visible)
                    form.Show();
            }
        }
        private static bool IsDesktopOrShellWindow(IntPtr hWnd)
        {
            StringBuilder className = new StringBuilder(256);
            GetClassName(hWnd, className, className.Capacity);

            string cls = className.ToString();

            // Masaüstü ve kabuk pencereleri
            return cls.Equals("Progman", StringComparison.OrdinalIgnoreCase)
                || cls.Equals("WorkerW", StringComparison.OrdinalIgnoreCase)
                || cls.Equals("Shell_TrayWnd", StringComparison.OrdinalIgnoreCase)
                || cls.Equals("Shell_SecondaryTrayWnd", StringComparison.OrdinalIgnoreCase)
                || cls.Equals("Windows.UI.Core.CoreWindow", StringComparison.OrdinalIgnoreCase)
                || cls.Equals("ApplicationFrameWindow", StringComparison.OrdinalIgnoreCase) == false && false;
        }
    }
}