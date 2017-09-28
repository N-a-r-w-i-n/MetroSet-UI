using System;
using System.Runtime.InteropServices;

namespace MetroSet_UI.Native
{
    internal class User32
    {
        public AnimateWindowFlags AW_HIDE { get; internal set; }

        #region Flags

        public const int WM_SETCURSOR = 0x0020;
        public const int IDC_HAND = 32649;

        public enum AnimateWindowFlags : uint
        {
            AW_HOR_POSITIVE = 0x00000001,
            AW_HOR_NEGATIVE = 0x00000002,
            AW_VER_POSITIVE = 0x00000004,
            AW_VER_NEGATIVE = 0x00000008,
            AW_CENTER = 0x00000010,
            AW_HIDE = 0x00010000,
            AW_ACTIVATE = 0x00020000,
            AW_SLIDE = 0x00040000,
            AW_BLEND = 0x00080000
        }

        #endregion

        #region Methods

        [DllImport("user32")]
        public static extern bool AnimateWindow(IntPtr hwnd, int time, AnimateWindowFlags flags);

        internal void AnimateWindow(IntPtr handle, int v, object p)
        {
            throw new NotImplementedException();
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, string lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr LoadCursor(IntPtr hInstance, int lpCursorName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetCursor(IntPtr hCursor);

        #endregion

    }
}