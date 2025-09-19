namespace Image_Processing
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            cqwc = new TabControl();
            tabPage1 = new TabPage();
            menuStrip2 = new MenuStrip();
            toolStripMenuItem1 = new ToolStripMenuItem();
            loadToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem3 = new ToolStripMenuItem();
            copyToolStripMenuItem4 = new ToolStripMenuItem();
            toolStripMenuItem5 = new ToolStripMenuItem();
            toolStripMenuItem6 = new ToolStripMenuItem();
            toolStripMenuItem7 = new ToolStripMenuItem();
            toolStripMenuItem8 = new ToolStripMenuItem();
            toolStripMenuItem9 = new ToolStripMenuItem();
            toolStripMenuItem10 = new ToolStripMenuItem();
            label2 = new Label();
            label1 = new Label();
            pictureBox2 = new PictureBox();
            pictureBox1 = new PictureBox();
            tabPage2 = new TabPage();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem1 = new ToolStripMenuItem();
            loadGreenScreenToolStripMenuItem = new ToolStripMenuItem();
            loadBackgroundToolStripMenuItem = new ToolStripMenuItem();
            saveImageToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem1 = new ToolStripMenuItem();
            fileToolStripMenuItem = new ToolStripMenuItem();
            applySubtractionToolStripMenuItem = new ToolStripMenuItem();
            webCamToolStripMenuItem = new ToolStripMenuItem();
            startWebcamToolStripMenuItem = new ToolStripMenuItem();
            stopWebcamToolStripMenuItem = new ToolStripMenuItem();
            captureFrameToolStripMenuItem = new ToolStripMenuItem();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            pictureBox5 = new PictureBox();
            pictureBox4 = new PictureBox();
            pictureBox3 = new PictureBox();
            exitToolStripMenuItem = new ToolStripMenuItem();
            openFileDialog1 = new OpenFileDialog();
            cqwc.SuspendLayout();
            tabPage1.SuspendLayout();
            menuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            tabPage2.SuspendLayout();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // cqwc
            // 
            cqwc.Controls.Add(tabPage1);
            cqwc.Controls.Add(tabPage2);
            cqwc.Location = new Point(0, 0);
            cqwc.Name = "cqwc";
            cqwc.SelectedIndex = 0;
            cqwc.Size = new Size(1178, 718);
            cqwc.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(menuStrip2);
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(label1);
            tabPage1.Controls.Add(pictureBox2);
            tabPage1.Controls.Add(pictureBox1);
            tabPage1.Location = new Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1170, 685);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Image Processing";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // menuStrip2
            // 
            menuStrip2.ImageScalingSize = new Size(20, 20);
            menuStrip2.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1, toolStripMenuItem6 });
            menuStrip2.Location = new Point(3, 3);
            menuStrip2.Name = "menuStrip2";
            menuStrip2.Size = new Size(1164, 28);
            menuStrip2.TabIndex = 4;
            menuStrip2.Text = "menuStrip2";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { loadToolStripMenuItem, saveToolStripMenuItem3, copyToolStripMenuItem4, toolStripMenuItem5 });
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(46, 24);
            toolStripMenuItem1.Text = "File";
            // 
            // loadToolStripMenuItem
            // 
            loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            loadToolStripMenuItem.Size = new Size(172, 26);
            loadToolStripMenuItem.Text = "Load Image";
            loadToolStripMenuItem.Click += loadToolStripMenuItem_Click;
            // 
            // saveToolStripMenuItem3
            // 
            saveToolStripMenuItem3.Name = "saveToolStripMenuItem3";
            saveToolStripMenuItem3.Size = new Size(172, 26);
            saveToolStripMenuItem3.Text = "Save Image";
            saveToolStripMenuItem3.Click += saveToolStripMenuItem3_Click;
            // 
            // copyToolStripMenuItem4
            // 
            copyToolStripMenuItem4.Name = "copyToolStripMenuItem4";
            copyToolStripMenuItem4.Size = new Size(172, 26);
            copyToolStripMenuItem4.Text = "Copy Image";
            copyToolStripMenuItem4.Click += copyToolStripMenuItem_Click;
            // 
            // toolStripMenuItem5
            // 
            toolStripMenuItem5.Name = "toolStripMenuItem5";
            toolStripMenuItem5.Size = new Size(172, 26);
            toolStripMenuItem5.Text = "Exit";
            toolStripMenuItem5.Click += exitToolStripMenuItem1_Click;
            // 
            // toolStripMenuItem6
            // 
            toolStripMenuItem6.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem7, toolStripMenuItem8, toolStripMenuItem9, toolStripMenuItem10 });
            toolStripMenuItem6.Name = "toolStripMenuItem6";
            toolStripMenuItem6.Size = new Size(93, 24);
            toolStripMenuItem6.Text = "Processing";
            // 
            // toolStripMenuItem7
            // 
            toolStripMenuItem7.Name = "toolStripMenuItem7";
            toolStripMenuItem7.Size = new Size(175, 26);
            toolStripMenuItem7.Text = "Grayscale";
            toolStripMenuItem7.Click += grayscaleToolStripMenuItem_Click;
            // 
            // toolStripMenuItem8
            // 
            toolStripMenuItem8.Name = "toolStripMenuItem8";
            toolStripMenuItem8.Size = new Size(175, 26);
            toolStripMenuItem8.Text = "Invert Colors";
            toolStripMenuItem8.Click += invertcolorToolStripMenuItem_Click;
            // 
            // toolStripMenuItem9
            // 
            toolStripMenuItem9.Name = "toolStripMenuItem9";
            toolStripMenuItem9.Size = new Size(175, 26);
            toolStripMenuItem9.Text = "Histogram";
            toolStripMenuItem9.Click += histogramToolStripMenuItem_Click;
            // 
            // toolStripMenuItem10
            // 
            toolStripMenuItem10.Name = "toolStripMenuItem10";
            toolStripMenuItem10.Size = new Size(175, 26);
            toolStripMenuItem10.Text = "Sepia";
            toolStripMenuItem10.Click += sepiaToolStripMenuItem_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Verdana", 10.8F, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(64, 64, 64);
            label2.Location = new Point(762, 533);
            label2.Name = "label2";
            label2.Size = new Size(178, 22);
            label2.TabIndex = 3;
            label2.Text = "Resulting Image";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Verdana", 10.8F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(64, 64, 64);
            label1.Location = new Point(233, 533);
            label1.Name = "label1";
            label1.Size = new Size(155, 22);
            label1.TabIndex = 2;
            label1.Text = "Loaded Image";
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Gainsboro;
            pictureBox2.BorderStyle = BorderStyle.Fixed3D;
            pictureBox2.Location = new Point(648, 107);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(400, 400);
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Gainsboro;
            pictureBox1.BorderStyle = BorderStyle.Fixed3D;
            pictureBox1.Location = new Point(119, 107);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(400, 400);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(menuStrip1);
            tabPage2.Controls.Add(label5);
            tabPage2.Controls.Add(label4);
            tabPage2.Controls.Add(label3);
            tabPage2.Controls.Add(pictureBox5);
            tabPage2.Controls.Add(pictureBox4);
            tabPage2.Controls.Add(pictureBox3);
            tabPage2.Location = new Point(4, 29);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1170, 685);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Green Screen";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem1, fileToolStripMenuItem, applySubtractionToolStripMenuItem, webCamToolStripMenuItem });
            menuStrip1.Location = new Point(3, 3);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1164, 28);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem1
            // 
            fileToolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { loadGreenScreenToolStripMenuItem, loadBackgroundToolStripMenuItem, saveImageToolStripMenuItem, exitToolStripMenuItem1 });
            fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            fileToolStripMenuItem1.Size = new Size(46, 24);
            fileToolStripMenuItem1.Text = "File";
            // 
            // loadGreenScreenToolStripMenuItem
            // 
            loadGreenScreenToolStripMenuItem.Name = "loadGreenScreenToolStripMenuItem";
            loadGreenScreenToolStripMenuItem.Size = new Size(216, 26);
            loadGreenScreenToolStripMenuItem.Text = "Load Green Screen";
            loadGreenScreenToolStripMenuItem.Click += loadGreenScreenToolStripMenuItem_Click_1;
            // 
            // loadBackgroundToolStripMenuItem
            // 
            loadBackgroundToolStripMenuItem.Name = "loadBackgroundToolStripMenuItem";
            loadBackgroundToolStripMenuItem.Size = new Size(216, 26);
            loadBackgroundToolStripMenuItem.Text = "Load Background";
            loadBackgroundToolStripMenuItem.Click += loadBackgroundToolStripMenuItem_Click_1;
            // 
            // saveImageToolStripMenuItem
            // 
            saveImageToolStripMenuItem.Name = "saveImageToolStripMenuItem";
            saveImageToolStripMenuItem.Size = new Size(216, 26);
            saveImageToolStripMenuItem.Text = "Save Image";
            saveImageToolStripMenuItem.Click += saveGreenScreenImageToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem1
            // 
            exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            exitToolStripMenuItem1.Size = new Size(216, 26);
            exitToolStripMenuItem1.Text = "Exit";
            exitToolStripMenuItem1.Click += exitToolStripMenuItem1_Click;
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(14, 24);
            // 
            // applySubtractionToolStripMenuItem
            // 
            applySubtractionToolStripMenuItem.Name = "applySubtractionToolStripMenuItem";
            applySubtractionToolStripMenuItem.Size = new Size(142, 24);
            applySubtractionToolStripMenuItem.Text = "Apply Subtraction";
            applySubtractionToolStripMenuItem.Click += applySubtractionToolStripMenuItem_Click;
            // 
            // webCamToolStripMenuItem
            // 
            webCamToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { startWebcamToolStripMenuItem, stopWebcamToolStripMenuItem, captureFrameToolStripMenuItem });
            webCamToolStripMenuItem.Name = "webCamToolStripMenuItem";
            webCamToolStripMenuItem.Size = new Size(83, 24);
            webCamToolStripMenuItem.Text = "WebCam";
            // 
            // startWebcamToolStripMenuItem
            // 
            startWebcamToolStripMenuItem.Name = "startWebcamToolStripMenuItem";
            startWebcamToolStripMenuItem.Size = new Size(224, 26);
            startWebcamToolStripMenuItem.Text = "Start Webcam";
            startWebcamToolStripMenuItem.Click += startWebcamToolStripMenuItem_Click;
            // 
            // stopWebcamToolStripMenuItem
            // 
            stopWebcamToolStripMenuItem.Name = "stopWebcamToolStripMenuItem";
            stopWebcamToolStripMenuItem.Size = new Size(224, 26);
            stopWebcamToolStripMenuItem.Text = "Stop Webcam";
            // 
            // captureFrameToolStripMenuItem
            // 
            captureFrameToolStripMenuItem.Name = "captureFrameToolStripMenuItem";
            captureFrameToolStripMenuItem.Size = new Size(224, 26);
            captureFrameToolStripMenuItem.Text = "Capture Frame";
            captureFrameToolStripMenuItem.Click += captureFrameToolStripMenuItem_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Verdana", 9F, FontStyle.Bold);
            label5.ForeColor = Color.FromArgb(64, 64, 64);
            label5.Location = new Point(874, 484);
            label5.Name = "label5";
            label5.Size = new Size(143, 18);
            label5.TabIndex = 5;
            label5.Text = "Resulting Image";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Verdana", 9F, FontStyle.Bold);
            label4.ForeColor = Color.FromArgb(64, 64, 64);
            label4.Location = new Point(464, 484);
            label4.Name = "label4";
            label4.Size = new Size(228, 18);
            label4.TabIndex = 4;
            label4.Text = "Loaded Background Image";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Verdana", 9F, FontStyle.Bold);
            label3.ForeColor = Color.FromArgb(64, 64, 64);
            label3.Location = new Point(92, 484);
            label3.Name = "label3";
            label3.Size = new Size(242, 18);
            label3.TabIndex = 3;
            label3.Text = "Loaded Green Screen Image";
            // 
            // pictureBox5
            // 
            pictureBox5.BackColor = Color.Gainsboro;
            pictureBox5.BorderStyle = BorderStyle.Fixed3D;
            pictureBox5.Location = new Point(795, 158);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(300, 300);
            pictureBox5.TabIndex = 2;
            pictureBox5.TabStop = false;
            // 
            // pictureBox4
            // 
            pictureBox4.BackColor = Color.Gainsboro;
            pictureBox4.BorderStyle = BorderStyle.Fixed3D;
            pictureBox4.Location = new Point(431, 158);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(300, 300);
            pictureBox4.TabIndex = 1;
            pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.Gainsboro;
            pictureBox3.BorderStyle = BorderStyle.Fixed3D;
            pictureBox3.Location = new Point(67, 158);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(300, 300);
            pictureBox3.TabIndex = 0;
            pictureBox3.TabStop = false;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(224, 26);
            exitToolStripMenuItem.Text = "Exit";
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1177, 717);
            Controls.Add(cqwc);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form1";
            Text = "Form1";
            cqwc.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            menuStrip2.ResumeLayout(false);
            menuStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl cqwc;
        private TabPage tabPage2;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem loadGreenScreenToolStripMenuItem_Click;
        private ToolStripMenuItem loadBackgroundToolStripMenuItem_Click;
        private ToolStripMenuItem saveToolStripMenuItem_Click;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem webCamToolStripMenuItem;
        private ToolStripMenuItem startWebcamToolStripMenuItem;
        private ToolStripMenuItem stopWebcamToolStripMenuItem;
        private ToolStripMenuItem captureFrameToolStripMenuItem;
        private TabPage tabPage1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox1;
        private OpenFileDialog openFileDialog1;
        private Label label1;
        private PictureBox pictureBox5;
        private PictureBox pictureBox4;
        private PictureBox pictureBox3;
        private Label label2;
        private Label label3;
        private Label label5;
        private Label label4;
        private MenuStrip menuStrip2;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem loadToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem3;
        private ToolStripMenuItem copyToolStripMenuItem4;
        private ToolStripMenuItem toolStripMenuItem5;
        private ToolStripMenuItem toolStripMenuItem6;
        private ToolStripMenuItem toolStripMenuItem7;
        private ToolStripMenuItem toolStripMenuItem8;
        private ToolStripMenuItem toolStripMenuItem9;
        private ToolStripMenuItem toolStripMenuItem10;
        private ToolStripMenuItem applySubtractionToolStripMenuItem;
        private ToolStripMenuItem fileToolStripMenuItem1;
        private ToolStripMenuItem loadGreenScreenToolStripMenuItem;
        private ToolStripMenuItem loadBackgroundToolStripMenuItem;
        private ToolStripMenuItem saveImageToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem1;
    }
}
