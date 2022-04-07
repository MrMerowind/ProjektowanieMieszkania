namespace ProjektowanieMieszkaniaCSharp
{
    partial class Prekonfiguracja
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.numericUpDownWidth = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownHeight = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBoxSize = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.buttonCreateNewProject = new System.Windows.Forms.Button();
            this.buttonLoadSaveFile = new System.Windows.Forms.Button();
            this.buttonCloseApp = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeight)).BeginInit();
            this.groupBoxSize.SuspendLayout();
            this.SuspendLayout();
            // 
            // numericUpDownWidth
            // 
            this.numericUpDownWidth.Location = new System.Drawing.Point(7, 47);
            this.numericUpDownWidth.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownWidth.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownWidth.Name = "numericUpDownWidth";
            this.numericUpDownWidth.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownWidth.TabIndex = 0;
            this.numericUpDownWidth.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDownWidth.ValueChanged += new System.EventHandler(this.numericUpDownWidth_ValueChanged);
            // 
            // numericUpDownHeight
            // 
            this.numericUpDownHeight.Location = new System.Drawing.Point(7, 73);
            this.numericUpDownHeight.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownHeight.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownHeight.Name = "numericUpDownHeight";
            this.numericUpDownHeight.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownHeight.TabIndex = 1;
            this.numericUpDownHeight.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numericUpDownHeight.ValueChanged += new System.EventHandler(this.numericUpDownHeight_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(133, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Szerokość";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(133, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Wysokość";
            // 
            // groupBoxSize
            // 
            this.groupBoxSize.Controls.Add(this.label3);
            this.groupBoxSize.Controls.Add(this.comboBox1);
            this.groupBoxSize.Controls.Add(this.label1);
            this.groupBoxSize.Controls.Add(this.label2);
            this.groupBoxSize.Controls.Add(this.numericUpDownWidth);
            this.groupBoxSize.Controls.Add(this.numericUpDownHeight);
            this.groupBoxSize.Location = new System.Drawing.Point(12, 12);
            this.groupBoxSize.Name = "groupBoxSize";
            this.groupBoxSize.Size = new System.Drawing.Size(204, 266);
            this.groupBoxSize.TabIndex = 4;
            this.groupBoxSize.TabStop = false;
            this.groupBoxSize.Text = "Wymiary";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(106, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Jednostka miary";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Metry (m)",
            "Stopy (ft)"});
            this.comboBox1.Location = new System.Drawing.Point(7, 20);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(93, 21);
            this.comboBox1.TabIndex = 4;
            this.comboBox1.Text = "Metry (m)";
            this.comboBox1.SelectedValueChanged += new System.EventHandler(this.comboBox1_SelectedValueChanged);
            // 
            // buttonCreateNewProject
            // 
            this.buttonCreateNewProject.Font = new System.Drawing.Font("Microsoft Sans Serif", 33F);
            this.buttonCreateNewProject.Location = new System.Drawing.Point(518, 315);
            this.buttonCreateNewProject.Name = "buttonCreateNewProject";
            this.buttonCreateNewProject.Size = new System.Drawing.Size(270, 123);
            this.buttonCreateNewProject.TabIndex = 5;
            this.buttonCreateNewProject.Text = "Stwórz nowy projekt";
            this.buttonCreateNewProject.UseVisualStyleBackColor = true;
            this.buttonCreateNewProject.Click += new System.EventHandler(this.buttonCreateNewProject_Click);
            // 
            // buttonLoadSaveFile
            // 
            this.buttonLoadSaveFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 33F);
            this.buttonLoadSaveFile.Location = new System.Drawing.Point(518, 164);
            this.buttonLoadSaveFile.Name = "buttonLoadSaveFile";
            this.buttonLoadSaveFile.Size = new System.Drawing.Size(270, 123);
            this.buttonLoadSaveFile.TabIndex = 6;
            this.buttonLoadSaveFile.Text = "Wczytaj projekt";
            this.buttonLoadSaveFile.UseVisualStyleBackColor = true;
            this.buttonLoadSaveFile.Click += new System.EventHandler(this.buttonLoadSaveFile_Click);
            // 
            // buttonCloseApp
            // 
            this.buttonCloseApp.Font = new System.Drawing.Font("Microsoft Sans Serif", 32F);
            this.buttonCloseApp.Location = new System.Drawing.Point(518, 13);
            this.buttonCloseApp.Name = "buttonCloseApp";
            this.buttonCloseApp.Size = new System.Drawing.Size(270, 123);
            this.buttonCloseApp.TabIndex = 7;
            this.buttonCloseApp.Text = "Zamknij program";
            this.buttonCloseApp.UseVisualStyleBackColor = true;
            this.buttonCloseApp.Click += new System.EventHandler(this.buttonCloseApp_Click);
            // 
            // Prekonfiguracja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSlateGray;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonCloseApp);
            this.Controls.Add(this.buttonLoadSaveFile);
            this.Controls.Add(this.buttonCreateNewProject);
            this.Controls.Add(this.groupBoxSize);
            this.Name = "Prekonfiguracja";
            this.Text = "Konfiguracja wstępna";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeight)).EndInit();
            this.groupBoxSize.ResumeLayout(false);
            this.groupBoxSize.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDownWidth;
        private System.Windows.Forms.NumericUpDown numericUpDownHeight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBoxSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button buttonCreateNewProject;
        private System.Windows.Forms.Button buttonLoadSaveFile;
        private System.Windows.Forms.Button buttonCloseApp;
    }
}