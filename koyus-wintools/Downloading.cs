using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace koyus_wintools
{
    public partial class Downloading : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );

        public Downloading()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            #pragma warning disable CA1416
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            #pragma warning restore CA1416
        }
    }
}
