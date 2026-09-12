using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using MaddsPet.Properties;

namespace MaddsPet.Easteregg
{
    internal class runningcats
    {
        private const int CatCount = 40;
        private const int CatSize = 48;
        private const int DurationMs = 5000;
        private const int TickIntervalMs = 16; // ~60fps

        private readonly Form overlay;
        private readonly List<PointF> positions = new();
        private readonly List<PointF> velocities = new();
        private readonly List<int> catImageIndices = new();

        private readonly System.Windows.Forms.Timer moveTimer;
        private readonly System.Windows.Forms.Timer lifeTimer;
        private readonly Random rng = new();


        private static readonly Image[] CatImages =
        {
            CleanEdgesFast(Resources.blackcat),
            CleanEdgesFast(Resources.whitecat),
            CleanEdgesFast(Resources.graycat)
        };

        public runningcats()
        {
            overlay = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                WindowState = FormWindowState.Maximized,
                StartPosition = FormStartPosition.Manual,
                Bounds = Screen.PrimaryScreen.Bounds,
                TopMost = true,
                ShowInTaskbar = false,
                BackColor = Color.Black,
                TransparencyKey = Color.Black,
               
            };
            typeof(Form).GetProperty("DoubleBuffered",
            System.Reflection.BindingFlags.NonPublic |
            System.Reflection.BindingFlags.Instance)
            ?.SetValue(overlay, true);
            overlay.Paint += Overlay_Paint;

            for (int i = 0; i < CatCount; i++)
            {
                positions.Add(new PointF(
                    rng.Next(0, overlay.Width - CatSize),
                    rng.Next(0, overlay.Height - CatSize)
                ));

                catImageIndices.Add(rng.Next(CatImages.Length));

                double angle = rng.NextDouble() * Math.PI * 2;
                float speed = rng.Next(10, 19);

                velocities.Add(new PointF(
                    (float)(Math.Cos(angle) * speed),
                    (float)(Math.Sin(angle) * speed)
                ));
            }

            moveTimer = new System.Windows.Forms.Timer { Interval = TickIntervalMs };
            moveTimer.Tick += MoveTimer_Tick;

            lifeTimer = new System.Windows.Forms.Timer { Interval = DurationMs };
            lifeTimer.Tick += LifeTimer_Tick;
        }

        public void Start()
        {
            overlay.Show();
            moveTimer.Start();
            lifeTimer.Start();
        }

        private void MoveTimer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < CatCount; i++)
            {
                PointF pos = positions[i];
                PointF vel = velocities[i];

                float newX = pos.X + vel.X;
                float newY = pos.Y + vel.Y;

                if (newX <= 0 || newX >= overlay.Width - CatSize)
                {
                    vel.X = -vel.X;
                    newX = Math.Clamp(newX, 0, overlay.Width - CatSize);
                }

                if (newY <= 0 || newY >= overlay.Height - CatSize)
                {
                    vel.Y = -vel.Y;
                    newY = Math.Clamp(newY, 0, overlay.Height - CatSize);
                }

                velocities[i] = vel;
                positions[i] = new PointF(newX, newY);
            }

            overlay.Invalidate();
        }

        private void Overlay_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;

            for (int i = 0; i < CatCount; i++)
            {
                Image img = CatImages[catImageIndices[i]];
                PointF pos = positions[i];
                e.Graphics.DrawImage(img, pos.X, pos.Y, CatSize, CatSize);
            }
        }

        private void LifeTimer_Tick(object sender, EventArgs e)
        {
            moveTimer.Stop();
            lifeTimer.Stop();

            moveTimer.Dispose();
            lifeTimer.Dispose();

            overlay.Paint -= Overlay_Paint;
            overlay.Close();
            overlay.Dispose();
        }
        private static Image CleanEdgesFast(Image source)
        {
            Bitmap bmp = new Bitmap(source);
            BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            int size = data.Stride * data.Height;
            byte[] pixels = new byte[size];
            System.Runtime.InteropServices.Marshal.Copy(data.Scan0, pixels, 0, size);

            for (int i = 0; i < size; i += 4)
            {
                byte b = pixels[i];
                byte g = pixels[i + 1];
                byte r = pixels[i + 2];
                byte a = pixels[i + 3];

                bool looksLikePurpleFringe =
                    a < 255 &&
                    r > 80 &&
                    b > 80 &&
                    g < r - 20 &&
                    g < b - 20;

                if (looksLikePurpleFringe)
                {
                    pixels[i + 3] = 0;
                }
            }

            System.Runtime.InteropServices.Marshal.Copy(pixels, 0, data.Scan0, size);
            bmp.UnlockBits(data);
            return bmp;
        }
    }
}
