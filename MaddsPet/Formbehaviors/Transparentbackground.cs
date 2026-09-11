using System;
using System.Drawing;
using System.Windows.Forms;

namespace MaddsPet.Formbehaviors
{
    internal class Transparentbackground
    {
        public static void settrans(Form form, Color transparentColor)
        {
            form.FormBorderStyle = FormBorderStyle.None;
            form.BackColor = transparentColor;
            form.TransparencyKey = transparentColor;
        }

        public static void AnimateToColor(Form form, Color targetColor, int duration = 500)
        {
            Color startColor = form.BackColor;

            int steps = 30;
            int interval = duration / steps;

            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer
            {
                Interval = interval
            };

            int step = 0;

            timer.Tick += (s, e) =>
            {
                step++;

                float progress = (float)step / steps;

                int r = (int)(startColor.R + (targetColor.R - startColor.R) * progress);
                int g = (int)(startColor.G + (targetColor.G - startColor.G) * progress);
                int b = (int)(startColor.B + (targetColor.B - startColor.B) * progress);

                Color color = Color.FromArgb(r, g, b);

                form.BackColor = color;
                form.TransparencyKey = color;

                if (step >= steps)
                {
                    timer.Stop();
                    timer.Dispose();
                }
            };

            timer.Start();
        }
    }
}