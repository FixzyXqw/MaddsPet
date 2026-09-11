using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
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
        private readonly List<PictureBox> cats = new();
        private readonly List<Point> velocities = new();
        private readonly System.Windows.Forms.Timer moveTimer;
        private readonly System.Windows.Forms.Timer lifeTimer;
        private readonly Random rng = new();

        private static readonly Image[] CatImages =
        {
            CleanEdges(Resources.blackcat),
            CleanEdges(Resources.whitecat),
            CleanEdges(Resources.graycat)
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
                BackColor = Color.Lime,
                TransparencyKey = Color.Lime
            };

            for (int i = 0; i < CatCount; i++)
            {
                PictureBox pb = new PictureBox
                {
                    Size = new Size(CatSize, CatSize),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Image = CatImages[rng.Next(CatImages.Length)],
                    Location = new Point(
                        rng.Next(0, overlay.Width - CatSize),
                        rng.Next(0, overlay.Height - CatSize)
                    ),
                    BackColor = Color.Transparent
                };

                overlay.Controls.Add(pb);
                cats.Add(pb);

                double angle = rng.NextDouble() * Math.PI * 2;
                int speed = rng.Next(10, 19);

                velocities.Add(new Point(
                    (int)(Math.Cos(angle) * speed),
                    (int)(Math.Sin(angle) * speed)
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
            for (int i = 0; i < cats.Count; i++)
            {
                PictureBox cat = cats[i];
                Point velocity = velocities[i];

                int newX = cat.Left + velocity.X;
                int newY = cat.Top + velocity.Y;

                if (newX <= 0 || newX >= overlay.Width - cat.Width)
                {
                    velocity.X = -velocity.X;
                    newX = Math.Clamp(newX, 0, overlay.Width - cat.Width);
                }

                if (newY <= 0 || newY >= overlay.Height - cat.Height)
                {
                    velocity.Y = -velocity.Y;
                    newY = Math.Clamp(newY, 0, overlay.Height - cat.Height);
                }

                velocities[i] = velocity;
                cat.Location = new Point(newX, newY);
            }
        }

        private void LifeTimer_Tick(object sender, EventArgs e)
        {
            moveTimer.Stop();
            lifeTimer.Stop();

            moveTimer.Dispose();
            lifeTimer.Dispose();

            overlay.Close();
            overlay.Dispose();
        }
        private static Image CleanEdges(Image source)
        {
            Bitmap bmp = new Bitmap(source);

            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color px = bmp.GetPixel(x, y);

                    bool looksLikePurpleFringe =
                        px.A < 255 &&
                        px.R > 80 &&
                        px.B > 80 &&
                        px.G < px.R - 20 &&
                        px.G < px.B - 20;

                    if (looksLikePurpleFringe)
                    {
                        bmp.SetPixel(x, y, Color.Transparent);
                    }
                }
            }

            return bmp;
        }
    }
}