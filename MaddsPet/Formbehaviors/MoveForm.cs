using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using MaddsPet.Settings;

public class MoveForm
{
    private readonly Form form;
    private readonly Control control;
    private readonly System.Windows.Forms.Timer timer;
    private readonly Settingsmanager settings;
    private Point mouseStart;
    private Point formStart;
    private bool holding;
    private bool moving;

    private const int GWL_EXSTYLE = -20;
    private const int WS_EX_APPWINDOW = 0x00040000;
    private const int WS_EX_TOOLWINDOW = 0x00000080;

    [DllImport("user32.dll")]
    private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

    public MoveForm(Form form, Control control, Settingsmanager settings)
    {
        this.form = form;
        this.control = control;
        this.settings = settings;

        HideFromAltTab();

        form.ShowInTaskbar = false;
        form.TopMost = false;
        form.FormBorderStyle = FormBorderStyle.None;
        form.BackColor = settings.IdleColor;
        form.TransparencyKey = settings.IdleColor;

        timer = new System.Windows.Forms.Timer
        {
            Interval = 500
        };

        timer.Tick += Timer_Tick;
        control.MouseDown += MouseDown;
        control.MouseMove += MouseMove;
        control.MouseUp += MouseUp;
    }

    private void MouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Left)
            return;

        holding = true;
        moving = false;

        mouseStart = Cursor.Position;
        formStart = form.Location;

        timer.Stop();
        timer.Start();
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
        timer.Stop();

        if (!holding)
            return;

        moving = true;

        form.BackColor = settings.MovingColor;
        form.TransparencyKey = settings.MovingColor;
    }

    private void MouseMove(object sender, MouseEventArgs e)
    {
        if (!holding || !moving)
            return;

        Point current = Cursor.Position;

        form.Location = new Point(
            formStart.X + current.X - mouseStart.X,
            formStart.Y + current.Y - mouseStart.Y
        );
    }
    private void MouseUp(object sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Left)
            return;

        holding = false;
        moving = false;

        timer.Stop();

        form.BackColor = settings.IdleColor;
        form.TransparencyKey = settings.IdleColor;
    }
    private void HideFromAltTab()
    {
        int style = GetWindowLong(form.Handle, GWL_EXSTYLE);

        style &= ~WS_EX_APPWINDOW;
        style |= WS_EX_TOOLWINDOW;

        SetWindowLong(form.Handle, GWL_EXSTYLE, style);
    }
}