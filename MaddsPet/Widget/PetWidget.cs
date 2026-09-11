using MaddsPet.Easteregg;
using MaddsPet.Formbehaviors;
using MaddsPet.Properties;
using MaddsPet.Settings;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace MaddsPet
{
    public partial class PetWidget : Form
    {
        private const int ResizeMargin = 8;
        private const int WM_NCHITTEST = 0x84;
        private const int HTLEFT = 10;
        private const int HTRIGHT = 11;
        private const int HTTOP = 12;
        private const int HTTOPLEFT = 13;
        private const int HTTOPRIGHT = 14;
        private const int HTBOTTOM = 15;
        private const int HTBOTTOMLEFT = 16;
        private const int HTBOTTOMRIGHT = 17;

        private bool isHovering;
        private bool isDragging;
        private readonly System.Windows.Forms.Timer hoverPollTimer;

        private bool IsGrabbed => isDragging;

        public PetWidget()
        {
            InitializeComponent();

            MoveForm sa = new MoveForm(
                this,
                pictureBox1,
                Program.Settingss
            );
            LoadPetImage();
            if (Program.Settingss.HideOnFullscreenApps)
            {
                new hideonfull(this);
            }
            pictureBox1.MouseDown += PictureBox1_MouseDown;
            pictureBox1.MouseUp += PictureBox1_MouseUp;
            pictureBox1.Paint += PictureBox1_Paint;
            hoverPollTimer = new System.Windows.Forms.Timer { Interval = 50 };
            hoverPollTimer.Tick += HoverPollTimer_Tick;
            hoverPollTimer.Start();
            if (Program.Settingss.WindowX.HasValue && Program.Settingss.WindowY.HasValue)
            {
                StartPosition = FormStartPosition.Manual;
                Location = new Point(
                    Program.Settingss.WindowX.Value,
                    Program.Settingss.WindowY.Value
                );
            }
            ClientSize = new Size(
                Program.Settingss.WindowWidth,
                Program.Settingss.WindowHeight
            );
            if (Program.Settingss.WindowX.HasValue && Program.Settingss.WindowY.HasValue)
            {
                StartPosition = FormStartPosition.Manual;
                Location = new Point(
                    Program.Settingss.WindowX.Value,
                    Program.Settingss.WindowY.Value
                );
            }
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    base.WndProc(ref m);

                    Point screenPoint = new Point(
                        (int)(m.LParam.ToInt64() & 0xFFFF),
                        (int)(m.LParam.ToInt64() >> 16) & 0xFFFF
                    );

                    Point clientPoint = PointToClient(screenPoint);

                    bool onLeft = clientPoint.X <= ResizeMargin;
                    bool onRight = clientPoint.X >= ClientSize.Width - ResizeMargin;
                    bool onTop = clientPoint.Y <= ResizeMargin;
                    bool onBottom = clientPoint.Y >= ClientSize.Height - ResizeMargin;

                    if (onTop && onLeft) m.Result = (IntPtr)HTTOPLEFT;
                    else if (onTop && onRight) m.Result = (IntPtr)HTTOPRIGHT;
                    else if (onBottom && onLeft) m.Result = (IntPtr)HTBOTTOMLEFT;
                    else if (onBottom && onRight) m.Result = (IntPtr)HTBOTTOMRIGHT;
                    else if (onLeft) m.Result = (IntPtr)HTLEFT;
                    else if (onRight) m.Result = (IntPtr)HTRIGHT;
                    else if (onTop) m.Result = (IntPtr)HTTOP;
                    else if (onBottom) m.Result = (IntPtr)HTBOTTOM;

                    return;
            }

            base.WndProc(ref m);
        }
        private void HoverPollTimer_Tick(object sender, EventArgs e)
        {
            bool wasHovering = isHovering;

            Point localCursor = pictureBox1.PointToClient(Cursor.Position);
            isHovering = pictureBox1.ClientRectangle.Contains(localCursor);

            if (isHovering != wasHovering)
            {
                pictureBox1.Invalidate();
            }
        }
        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            isDragging = true;
            pictureBox1.Invalidate();
        }

        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            isDragging = false;
            pictureBox1.Invalidate();
        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {

        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            hoverPollTimer?.Stop();
            hoverPollTimer?.Dispose();
            base.OnFormClosed(e);
            hoverPollTimer?.Stop();
            hoverPollTimer?.Dispose();

            Program.Settingss.WindowX = Location.X;
            Program.Settingss.WindowY = Location.Y;
            Program.Settingss.Save();

            base.OnFormClosed(e);
        }

        public void LoadPetImage()
        {
            string customsFolder = Path.Combine(
                Settingsmanager.SettingsFolder,
                "customs"
            );
            string customPath = Path.Combine(customsFolder, Program.Settingss.Pet + ".png");
            if (File.Exists(customPath))
            {
                using FileStream fs = new FileStream(customPath, FileMode.Open, FileAccess.Read);
                pictureBox1.Image = Image.FromStream(fs);
                return;
            }

            switch (Program.Settingss.Pet)
            {
                case "blackcat":
                    pictureBox1.Image = Resources.blackcat;
                    break;

                case "whitecat":
                    pictureBox1.Image = Resources.whitecat;
                    break;

                case "graycat":
                    pictureBox1.Image = Resources.graycat;
                    break;

                default:
                    pictureBox1.Image = Resources.blackcat;
                    break;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Hub mainthread = new Hub();
                mainthread.Show();
            }
        }
        private void PetWidget_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            new runningcats().Start();
        }
    }
}