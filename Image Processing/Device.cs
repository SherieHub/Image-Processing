using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WebCamLib
{
    public class Device
    {
        private const int WM_CAP = 0x400;
        private const int WM_CAP_DRIVER_CONNECT = 0x40a;
        private const int WM_CAP_DRIVER_DISCONNECT = 0x40b;
        private const int WM_CAP_EDIT_COPY = WM_CAP + 30;
        private const int WM_CAP_SET_PREVIEW = 0x432;
        private const int WM_CAP_SET_PREVIEWRATE = 0x434;
        private const int WM_CAP_SET_SCALE = 0x435;
        private const int WS_CHILD = 0x40000000;
        private const int WS_VISIBLE = 0x10000000;
        private const uint SWP_NOZORDER = 0x4;
        private const int SWP_NOMOVE = 0x2;
        private const int SWP_NOSIZE = 0x1;

        [DllImport("avicap32.dll", CharSet = CharSet.Ansi)]
        private static extern IntPtr capCreateCaptureWindowA(
            string lpszWindowName,
            int dwStyle,
            int x, int y, int nWidth, int nHeight,
            IntPtr hwndParent,
            int nID);

        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, [MarshalAs(UnmanagedType.AsAny)] object lParam);

        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll")]
        private static extern bool DestroyWindow(IntPtr hWnd);

        private int index;
        private IntPtr deviceHandle;

        public string Name { get; set; }
        public string Version { get; set; }

        public Device() { }
        public Device(int index) { this.index = index; }

        public override string ToString() => Name;

        /// <summary>
        /// Initializes the webcam and attaches it to a control.
        /// </summary>
        public void Init(int windowHeight, int windowWidth, IntPtr parentHandle)
        {
            string deviceIndex = index.ToString();

            // Create capture window as child of parent control
            deviceHandle = capCreateCaptureWindowA(
                deviceIndex,
                WS_CHILD | WS_VISIBLE,
                0, 0, windowWidth, windowHeight,
                parentHandle,
                0);

            if (deviceHandle == IntPtr.Zero)
            {
                MessageBox.Show("Failed to create capture window!");
                return;
            }

            int connected = SendMessage(deviceHandle, WM_CAP_DRIVER_CONNECT, index, 0);
            if (connected <= 0)
            {
                MessageBox.Show("Failed to connect to webcam driver!");
                return;
            }

            // Enable scaling and preview
            SendMessage(deviceHandle, WM_CAP_SET_SCALE, -1, 0);
            SendMessage(deviceHandle, WM_CAP_SET_PREVIEWRATE, 66, 0); // ~15 FPS
            SendMessage(deviceHandle, WM_CAP_SET_PREVIEW, -1, 0);

            // Position inside panel
            SetWindowPos(deviceHandle, IntPtr.Zero, 0, 0, windowWidth, windowHeight, SWP_NOZORDER);
        }

        /// <summary>
        /// Shows webcam preview inside a control (e.g., Panel)
        /// </summary>
        public void ShowWindow(Control windowsControl)
        {
            Init(windowsControl.Height, windowsControl.Width, windowsControl.Handle);
        }

        /// <summary>
        /// Stops webcam and destroys window
        /// </summary>
        public void Stop()
        {
            if (deviceHandle != IntPtr.Zero)
            {
                SendMessage(deviceHandle, WM_CAP_DRIVER_DISCONNECT, index, 0);
                DestroyWindow(deviceHandle);
                deviceHandle = IntPtr.Zero;
            }
        }

        /// <summary>
        /// Copies current frame to clipboard
        /// </summary>
        public void Sendmessage()
        {
            if (deviceHandle != IntPtr.Zero)
                SendMessage(deviceHandle, WM_CAP_EDIT_COPY, 0, 0);
        }
    }
}
