using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MaddsPet.Formbehaviors;
using MaddsPet.Settings;

namespace MaddsPet
{
    public partial class Hub : Form
    {
        private Settingsmanager settings;

        public Hub()
        {
            InitializeComponent();

            settings = Program.Settingss;
            LoadPetComboItems();
            comboBoxPet.SelectedItem = settings.Pet;
            checkBoxFullscreen.Checked = settings.HideOnFullscreenApps;
            Autostartbox.Checked = settings.Autostart;
            panelMovingColor.BackColor = settings.MovingColor;
            panelIdleColor.BackColor = settings.IdleColor;

            textBoxWidth.Text = settings.WindowWidth.ToString();
            textBoxHeight.Text = settings.WindowHeight.ToString();
        }

        private void LoadPetComboItems()
        {
            comboBoxPet.Items.Clear();

            comboBoxPet.Items.Add("blackcat");
            comboBoxPet.Items.Add("whitecat");
            comboBoxPet.Items.Add("graycat");
            comboBoxPet.Items.Add("custom");

            string customsFolder = Path.Combine(
                Settingsmanager.SettingsFolder,
                "customs"
            );

            if (Directory.Exists(customsFolder))
            {
                foreach (string file in Directory.GetFiles(customsFolder, "*.png"))
                {
                    comboBoxPet.Items.Add(Path.GetFileNameWithoutExtension(file));
                }
            }

            if (comboBoxPet.Items.Contains(settings.Pet))
                comboBoxPet.SelectedItem = settings.Pet;
            else
                comboBoxPet.SelectedIndex = 0;
        }

        private void pickimage()
        {
            using OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "PNG Image|*.png",
                Title = "Select a Pet Image"
            };

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                RestoreComboSelection();
                return;
            }

            string name = PromptForName();
            if (string.IsNullOrWhiteSpace(name))
            {
                RestoreComboSelection();
                return;
            }

            foreach (char c in Path.GetInvalidFileNameChars())
                name = name.Replace(c, '_');

            if (name.Equals("custom", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("'custom' ismi kullanılamaz, farklı bir isim seçin.");
                RestoreComboSelection();
                return;
            }

            string customsFolder = Path.Combine(
                Settingsmanager.SettingsFolder,
                "customs"
            );

            Directory.CreateDirectory(customsFolder);

            string targetPath = Path.Combine(customsFolder, name + ".png");
            File.Copy(dialog.FileName, targetPath, overwrite: true);

            settings.Pet = name;
            settings.Save();

            LoadPetComboItems();

            if (comboBoxPet.Items.Contains(name))
                comboBoxPet.SelectedItem = name;

            Program.ActiveWidget?.LoadPetImage();
        }

        private void RestoreComboSelection()
        {
            if (comboBoxPet.Items.Contains(settings.Pet))
                comboBoxPet.SelectedItem = settings.Pet;
            else if (comboBoxPet.Items.Count > 0)
                comboBoxPet.SelectedIndex = 0;
        }

        private string PromptForName()
        {
            using Form prompt = new Form
            {
                Width = 300,
                Height = 130,
                Text = "Pet Name",
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false
            };

            TextBox input = new TextBox
            {
                Left = 10,
                Top = 15,
                Width = 260
            };

            Button ok = new Button
            {
                Text = "Tamam",
                Left = 190,
                Width = 80,
                Top = 50,
                DialogResult = DialogResult.OK
            };

            prompt.Controls.Add(input);
            prompt.Controls.Add(ok);
            prompt.AcceptButton = ok;

            return prompt.ShowDialog() == DialogResult.OK ? input.Text.Trim() : null;
        }

        private void comboBoxPet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxPet.SelectedItem == null)
                return;

            string selected = comboBoxPet.SelectedItem.ToString();

            if (selected == "custom")
            {
                pickimage();
                return;
            }

            settings.Pet = selected;
            settings.Save();

            Program.ActiveWidget?.LoadPetImage();
        }

        private void checkBoxFullscreen_CheckedChanged(object sender, EventArgs e)
        {
            settings.HideOnFullscreenApps = checkBoxFullscreen.Checked;
            settings.Save();
        }

        private void Autostartbox_CheckedChanged(object sender, EventArgs e)
        {
            settings.Autostart = Autostartbox.Checked;
            settings.Save();

            autostart.Set(settings.Autostart);
        }

        private void buttonMovingColor_Click(object sender, EventArgs e)
        {
            using ColorDialog dialog = new ColorDialog { Color = settings.MovingColor };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                settings.MovingColor = dialog.Color;
                panelMovingColor.BackColor = dialog.Color;
                settings.Save();
            }
        }

        private void buttonIdleColor_Click(object sender, EventArgs e)
        {
            using ColorDialog dialog = new ColorDialog { Color = settings.IdleColor };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                settings.IdleColor = dialog.Color;
                panelIdleColor.BackColor = dialog.Color;
                settings.Save();
            }
        }

        private void buttonApplySize_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBoxWidth.Text, out int w) || w < 16)
            {
                MessageBox.Show("Enter a valid width (min 16).");
                return;
            }

            if (!int.TryParse(textBoxHeight.Text, out int h) || h < 16)
            {
                MessageBox.Show("Enter a valid height (min 16).");
                return;
            }

            settings.WindowWidth = w;
            settings.WindowHeight = h;
            settings.Save();

            if (Program.ActiveWidget != null)
            {
                Program.ActiveWidget.ClientSize = new Size(w, h);
            }
        }

        private void Hub_FormClosing(object sender, FormClosingEventArgs e)
        {
            settings.Save();
            Program.ActiveWidget?.LoadPetImage();
        }
    }
}