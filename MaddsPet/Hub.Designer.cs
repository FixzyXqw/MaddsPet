namespace MaddsPet
{
    partial class Hub
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Hub));
            groupBoxPet = new GroupBox();
            comboBoxPet = new ComboBox();
            groupBoxBehavior = new GroupBox();
            checkBoxFullscreen = new CheckBox();
            Autostartbox = new CheckBox();
            groupBoxColors = new GroupBox();
            buttonMovingColor = new Button();
            panelMovingColor = new Panel();
            buttonIdleColor = new Button();
            panelIdleColor = new Panel();
            groupBoxSize = new GroupBox();
            labelWidth = new Label();
            textBoxWidth = new TextBox();
            labelHeight = new Label();
            textBoxHeight = new TextBox();
            buttonApplySize = new Button();
            groupBoxPet.SuspendLayout();
            groupBoxBehavior.SuspendLayout();
            groupBoxColors.SuspendLayout();
            groupBoxSize.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxPet
            // 
            groupBoxPet.Controls.Add(comboBoxPet);
            groupBoxPet.Location = new Point(15, 15);
            groupBoxPet.Name = "groupBoxPet";
            groupBoxPet.Size = new Size(260, 65);
            groupBoxPet.TabIndex = 0;
            groupBoxPet.TabStop = false;
            groupBoxPet.Text = "Pet";
            // 
            // comboBoxPet
            // 
            comboBoxPet.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxPet.Location = new Point(15, 27);
            comboBoxPet.Name = "comboBoxPet";
            comboBoxPet.Size = new Size(230, 23);
            comboBoxPet.TabIndex = 0;
            comboBoxPet.SelectedIndexChanged += comboBoxPet_SelectedIndexChanged;
            // 
            // groupBoxBehavior
            // 
            groupBoxBehavior.Controls.Add(checkBoxFullscreen);
            groupBoxBehavior.Controls.Add(Autostartbox);
            groupBoxBehavior.Location = new Point(15, 90);
            groupBoxBehavior.Name = "groupBoxBehavior";
            groupBoxBehavior.Size = new Size(260, 80);
            groupBoxBehavior.TabIndex = 1;
            groupBoxBehavior.TabStop = false;
            groupBoxBehavior.Text = "Behavior";
            // 
            // checkBoxFullscreen
            // 
            checkBoxFullscreen.AutoSize = true;
            checkBoxFullscreen.Location = new Point(15, 27);
            checkBoxFullscreen.Name = "checkBoxFullscreen";
            checkBoxFullscreen.Size = new Size(150, 19);
            checkBoxFullscreen.TabIndex = 0;
            checkBoxFullscreen.Text = "Hide on fullscreen apps";
            checkBoxFullscreen.UseVisualStyleBackColor = true;
            checkBoxFullscreen.CheckedChanged += checkBoxFullscreen_CheckedChanged;
            // 
            // Autostartbox
            // 
            Autostartbox.AutoSize = true;
            Autostartbox.Location = new Point(15, 52);
            Autostartbox.Name = "Autostartbox";
            Autostartbox.Size = new Size(128, 19);
            Autostartbox.TabIndex = 1;
            Autostartbox.Text = "Start with Windows";
            Autostartbox.UseVisualStyleBackColor = true;
            Autostartbox.CheckedChanged += Autostartbox_CheckedChanged;
            // 
            // groupBoxColors
            // 
            groupBoxColors.Controls.Add(buttonMovingColor);
            groupBoxColors.Controls.Add(panelMovingColor);
            groupBoxColors.Controls.Add(buttonIdleColor);
            groupBoxColors.Controls.Add(panelIdleColor);
            groupBoxColors.Location = new Point(15, 180);
            groupBoxColors.Name = "groupBoxColors";
            groupBoxColors.Size = new Size(260, 100);
            groupBoxColors.TabIndex = 2;
            groupBoxColors.TabStop = false;
            groupBoxColors.Text = "Colors";
            // 
            // buttonMovingColor
            // 
            buttonMovingColor.Location = new Point(15, 27);
            buttonMovingColor.Name = "buttonMovingColor";
            buttonMovingColor.Size = new Size(150, 27);
            buttonMovingColor.TabIndex = 0;
            buttonMovingColor.Text = "Moving Color";
            buttonMovingColor.UseVisualStyleBackColor = true;
            buttonMovingColor.Click += buttonMovingColor_Click;
            // 
            // panelMovingColor
            // 
            panelMovingColor.BorderStyle = BorderStyle.FixedSingle;
            panelMovingColor.Location = new Point(175, 27);
            panelMovingColor.Name = "panelMovingColor";
            panelMovingColor.Size = new Size(27, 27);
            panelMovingColor.TabIndex = 1;
            // 
            // buttonIdleColor
            // 
            buttonIdleColor.Location = new Point(15, 62);
            buttonIdleColor.Name = "buttonIdleColor";
            buttonIdleColor.Size = new Size(150, 27);
            buttonIdleColor.TabIndex = 2;
            buttonIdleColor.Text = "Idle Color";
            buttonIdleColor.UseVisualStyleBackColor = true;
            buttonIdleColor.Click += buttonIdleColor_Click;
            // 
            // panelIdleColor
            // 
            panelIdleColor.BorderStyle = BorderStyle.FixedSingle;
            panelIdleColor.Location = new Point(175, 62);
            panelIdleColor.Name = "panelIdleColor";
            panelIdleColor.Size = new Size(27, 27);
            panelIdleColor.TabIndex = 3;
            // 
            // groupBoxSize
            // 
            groupBoxSize.Controls.Add(labelWidth);
            groupBoxSize.Controls.Add(textBoxWidth);
            groupBoxSize.Controls.Add(labelHeight);
            groupBoxSize.Controls.Add(textBoxHeight);
            groupBoxSize.Controls.Add(buttonApplySize);
            groupBoxSize.Location = new Point(15, 290);
            groupBoxSize.Name = "groupBoxSize";
            groupBoxSize.Size = new Size(260, 100);
            groupBoxSize.TabIndex = 3;
            groupBoxSize.TabStop = false;
            groupBoxSize.Text = "Size";
            // 
            // labelWidth
            // 
            labelWidth.AutoSize = true;
            labelWidth.Location = new Point(15, 30);
            labelWidth.Name = "labelWidth";
            labelWidth.Size = new Size(42, 15);
            labelWidth.TabIndex = 0;
            labelWidth.Text = "Width:";
            // 
            // textBoxWidth
            // 
            textBoxWidth.Location = new Point(70, 27);
            textBoxWidth.Name = "textBoxWidth";
            textBoxWidth.Size = new Size(80, 23);
            textBoxWidth.TabIndex = 1;
            // 
            // labelHeight
            // 
            labelHeight.AutoSize = true;
            labelHeight.Location = new Point(15, 62);
            labelHeight.Name = "labelHeight";
            labelHeight.Size = new Size(46, 15);
            labelHeight.TabIndex = 2;
            labelHeight.Text = "Height:";
            // 
            // textBoxHeight
            // 
            textBoxHeight.Location = new Point(70, 59);
            textBoxHeight.Name = "textBoxHeight";
            textBoxHeight.Size = new Size(80, 23);
            textBoxHeight.TabIndex = 3;
            // 
            // buttonApplySize
            // 
            buttonApplySize.Location = new Point(160, 27);
            buttonApplySize.Name = "buttonApplySize";
            buttonApplySize.Size = new Size(85, 55);
            buttonApplySize.TabIndex = 4;
            buttonApplySize.Text = "Apply";
            buttonApplySize.UseVisualStyleBackColor = true;
            buttonApplySize.Click += buttonApplySize_Click;
            // 
            // Hub
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(290, 405);
            Controls.Add(groupBoxSize);
            Controls.Add(groupBoxColors);
            Controls.Add(groupBoxBehavior);
            Controls.Add(groupBoxPet);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Hub";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MaddsPet HUB";
            FormClosing += Hub_FormClosing;
            groupBoxPet.ResumeLayout(false);
            groupBoxBehavior.ResumeLayout(false);
            groupBoxBehavior.PerformLayout();
            groupBoxColors.ResumeLayout(false);
            groupBoxSize.ResumeLayout(false);
            groupBoxSize.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBoxPet;
        private ComboBox comboBoxPet;
        private GroupBox groupBoxBehavior;
        private CheckBox checkBoxFullscreen;
        private CheckBox Autostartbox;
        private GroupBox groupBoxColors;
        private Button buttonMovingColor;
        private Panel panelMovingColor;
        private Button buttonIdleColor;
        private Panel panelIdleColor;
        private GroupBox groupBoxSize;
        private Label labelWidth;
        private TextBox textBoxWidth;
        private Label labelHeight;
        private TextBox textBoxHeight;
        private Button buttonApplySize;
    }
}