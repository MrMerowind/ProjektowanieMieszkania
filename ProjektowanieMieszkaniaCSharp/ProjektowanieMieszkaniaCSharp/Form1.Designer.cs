namespace ProjektowanieMieszkaniaCSharp
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.artboard = new System.Windows.Forms.PictureBox();
            this.wallLengthText = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.zapiszToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wczytajToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oddalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.przybliżToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupTools = new System.Windows.Forms.GroupBox();
            this.doorButton = new System.Windows.Forms.RadioButton();
            this.radioButtonFurniture = new System.Windows.Forms.RadioButton();
            this.radioEditWindow = new System.Windows.Forms.RadioButton();
            this.radioPlaceWindow = new System.Windows.Forms.RadioButton();
            this.radioPlaceWall = new System.Windows.Forms.RadioButton();
            this.radioEdit = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.listBoxRooms = new System.Windows.Forms.ListBox();
            this.listViewFurniture = new System.Windows.Forms.ListView();
            this.contextMenuStripFurnitureViewList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dodajWłasnyMebelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteFurnitureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zmieńNazwęZaznaczonegoMeblaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.artboard)).BeginInit();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupTools.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.contextMenuStripFurnitureViewList.SuspendLayout();
            this.SuspendLayout();
            // 
            // artboard
            // 
            this.artboard.BackColor = System.Drawing.Color.Teal;
            this.artboard.Cursor = System.Windows.Forms.Cursors.Cross;
            this.artboard.Location = new System.Drawing.Point(0, 0);
            this.artboard.Name = "artboard";
            this.artboard.Size = new System.Drawing.Size(637, 455);
            this.artboard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.artboard.TabIndex = 4;
            this.artboard.TabStop = false;
            this.artboard.SizeChanged += new System.EventHandler(this.artboard_SizeChanged);
            this.artboard.MouseDown += new System.Windows.Forms.MouseEventHandler(this.artboard_MouseDown);
            this.artboard.MouseHover += new System.EventHandler(this.artboard_MouseHover);
            this.artboard.MouseMove += new System.Windows.Forms.MouseEventHandler(this.artboard_MouseMove);
            this.artboard.MouseUp += new System.Windows.Forms.MouseEventHandler(this.artboard_MouseUp);
            // 
            // wallLengthText
            // 
            this.wallLengthText.AutoSize = true;
            this.wallLengthText.Location = new System.Drawing.Point(686, 157);
            this.wallLengthText.Name = "wallLengthText";
            this.wallLengthText.Size = new System.Drawing.Size(0, 13);
            this.wallLengthText.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.artboard);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 29);
            this.panel1.Name = "panel1";
            this.tableLayoutPanel1.SetRowSpan(this.panel1, 4);
            this.panel1.Size = new System.Drawing.Size(845, 663);
            this.panel1.TabIndex = 14;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zapiszToolStripMenuItem,
            this.wczytajToolStripMenuItem,
            this.oddalToolStripMenuItem,
            this.przybliżToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(851, 26);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // zapiszToolStripMenuItem
            // 
            this.zapiszToolStripMenuItem.Name = "zapiszToolStripMenuItem";
            this.zapiszToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.zapiszToolStripMenuItem.Size = new System.Drawing.Size(52, 22);
            this.zapiszToolStripMenuItem.Text = "Zapisz";
            this.zapiszToolStripMenuItem.Click += new System.EventHandler(this.zapiszToolStripMenuItem_Click);
            // 
            // wczytajToolStripMenuItem
            // 
            this.wczytajToolStripMenuItem.Name = "wczytajToolStripMenuItem";
            this.wczytajToolStripMenuItem.Size = new System.Drawing.Size(60, 22);
            this.wczytajToolStripMenuItem.Text = "Wczytaj";
            this.wczytajToolStripMenuItem.Click += new System.EventHandler(this.wczytajToolStripMenuItem_Click);
            // 
            // oddalToolStripMenuItem
            // 
            this.oddalToolStripMenuItem.Name = "oddalToolStripMenuItem";
            this.oddalToolStripMenuItem.Size = new System.Drawing.Size(51, 22);
            this.oddalToolStripMenuItem.Text = "Oddal";
            this.oddalToolStripMenuItem.Click += new System.EventHandler(this.oddalToolStripMenuItem_Click);
            // 
            // przybliżToolStripMenuItem
            // 
            this.przybliżToolStripMenuItem.Name = "przybliżToolStripMenuItem";
            this.przybliżToolStripMenuItem.Size = new System.Drawing.Size(59, 22);
            this.przybliżToolStripMenuItem.Text = "Przybliż";
            this.przybliżToolStripMenuItem.Click += new System.EventHandler(this.przybliżToolStripMenuItem_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BackColor = System.Drawing.Color.Crimson;
            this.textBox1.Location = new System.Drawing.Point(854, 196);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(151, 20);
            this.textBox1.TabIndex = 9;
            this.textBox1.Text = "Długość ściany";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupTools
            // 
            this.groupTools.AutoSize = true;
            this.groupTools.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupTools.Controls.Add(this.doorButton);
            this.groupTools.Controls.Add(this.radioButtonFurniture);
            this.groupTools.Controls.Add(this.radioEditWindow);
            this.groupTools.Controls.Add(this.radioPlaceWindow);
            this.groupTools.Controls.Add(this.radioPlaceWall);
            this.groupTools.Controls.Add(this.radioEdit);
            this.groupTools.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupTools.Location = new System.Drawing.Point(854, 3);
            this.groupTools.MinimumSize = new System.Drawing.Size(150, 0);
            this.groupTools.Name = "groupTools";
            this.tableLayoutPanel1.SetRowSpan(this.groupTools, 2);
            this.groupTools.Size = new System.Drawing.Size(151, 187);
            this.groupTools.TabIndex = 8;
            this.groupTools.TabStop = false;
            this.groupTools.Text = "Narzędzia";
            this.groupTools.Enter += new System.EventHandler(this.groupTools_Enter);
            // 
            // doorButton
            // 
            this.doorButton.AutoSize = true;
            this.doorButton.Location = new System.Drawing.Point(6, 112);
            this.doorButton.Name = "doorButton";
            this.doorButton.Size = new System.Drawing.Size(110, 17);
            this.doorButton.TabIndex = 11;
            this.doorButton.TabStop = true;
            this.doorButton.Text = "Dodaj/Usuń drzwi";
            this.doorButton.UseVisualStyleBackColor = true;
            this.doorButton.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButtonFurniture
            // 
            this.radioButtonFurniture.AutoSize = true;
            this.radioButtonFurniture.Location = new System.Drawing.Point(6, 135);
            this.radioButtonFurniture.Name = "radioButtonFurniture";
            this.radioButtonFurniture.Size = new System.Drawing.Size(98, 17);
            this.radioButtonFurniture.TabIndex = 10;
            this.radioButtonFurniture.TabStop = true;
            this.radioButtonFurniture.Text = "Biblioteka mebli";
            this.radioButtonFurniture.UseVisualStyleBackColor = true;
            this.radioButtonFurniture.CheckedChanged += new System.EventHandler(this.radioButtonFurniture_CheckedChanged);
            // 
            // radioEditWindow
            // 
            this.radioEditWindow.AutoSize = true;
            this.radioEditWindow.Location = new System.Drawing.Point(6, 89);
            this.radioEditWindow.Name = "radioEditWindow";
            this.radioEditWindow.Size = new System.Drawing.Size(90, 17);
            this.radioEditWindow.TabIndex = 9;
            this.radioEditWindow.TabStop = true;
            this.radioEditWindow.Text = "Przesuń okno";
            this.radioEditWindow.UseVisualStyleBackColor = true;
            // 
            // radioPlaceWindow
            // 
            this.radioPlaceWindow.AutoSize = true;
            this.radioPlaceWindow.Location = new System.Drawing.Point(6, 66);
            this.radioPlaceWindow.Name = "radioPlaceWindow";
            this.radioPlaceWindow.Size = new System.Drawing.Size(110, 17);
            this.radioPlaceWindow.TabIndex = 8;
            this.radioPlaceWindow.TabStop = true;
            this.radioPlaceWindow.Text = "Dodaj/Usuń okno";
            this.radioPlaceWindow.UseVisualStyleBackColor = true;
            // 
            // radioPlaceWall
            // 
            this.radioPlaceWall.AutoSize = true;
            this.radioPlaceWall.Checked = true;
            this.radioPlaceWall.Location = new System.Drawing.Point(6, 19);
            this.radioPlaceWall.Name = "radioPlaceWall";
            this.radioPlaceWall.Size = new System.Drawing.Size(117, 17);
            this.radioPlaceWall.TabIndex = 6;
            this.radioPlaceWall.TabStop = true;
            this.radioPlaceWall.Text = "Dodaj/Usuń ściane";
            this.radioPlaceWall.UseVisualStyleBackColor = true;
            // 
            // radioEdit
            // 
            this.radioEdit.AutoSize = true;
            this.radioEdit.Location = new System.Drawing.Point(6, 42);
            this.radioEdit.Name = "radioEdit";
            this.radioEdit.Size = new System.Drawing.Size(97, 17);
            this.radioEdit.TabIndex = 7;
            this.radioEdit.TabStop = true;
            this.radioEdit.Text = "Przesuń ściane";
            this.radioEdit.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 157F));
            this.tableLayoutPanel1.Controls.Add(this.menuStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.listBoxRooms, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.listViewFurniture, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.groupTools, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.MinimumSize = new System.Drawing.Size(200, 400);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 167F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1008, 695);
            this.tableLayoutPanel1.TabIndex = 15;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // listBoxRooms
            // 
            this.listBoxRooms.BackColor = System.Drawing.SystemColors.ControlDark;
            this.listBoxRooms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxRooms.FormattingEnabled = true;
            this.listBoxRooms.Location = new System.Drawing.Point(854, 548);
            this.listBoxRooms.Name = "listBoxRooms";
            this.listBoxRooms.Size = new System.Drawing.Size(151, 144);
            this.listBoxRooms.TabIndex = 15;
            this.listBoxRooms.Click += new System.EventHandler(this.listBoxRooms_Click);
            this.listBoxRooms.DoubleClick += new System.EventHandler(this.listBoxRooms_DoubleClick);
            this.listBoxRooms.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listBoxRooms_KeyUp);
            // 
            // listViewFurniture
            // 
            this.listViewFurniture.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewFurniture.BackColor = System.Drawing.SystemColors.ControlDark;
            this.listViewFurniture.ContextMenuStrip = this.contextMenuStripFurnitureViewList;
            this.listViewFurniture.Location = new System.Drawing.Point(854, 220);
            this.listViewFurniture.Name = "listViewFurniture";
            this.listViewFurniture.Size = new System.Drawing.Size(151, 322);
            this.listViewFurniture.TabIndex = 16;
            this.listViewFurniture.UseCompatibleStateImageBehavior = false;
            this.listViewFurniture.Visible = false;
            this.listViewFurniture.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.listViewFurniture_AfterLabelEdit);
            this.listViewFurniture.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.listViewFurniture_ItemDrag);
            this.listViewFurniture.DoubleClick += new System.EventHandler(this.listViewFurniture_DoubleClick);
            this.listViewFurniture.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listViewFurniture_MouseClick);
            this.listViewFurniture.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listViewFurniture_MouseUp);
            // 
            // contextMenuStripFurnitureViewList
            // 
            this.contextMenuStripFurnitureViewList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dodajWłasnyMebelToolStripMenuItem,
            this.deleteFurnitureToolStripMenuItem,
            this.zmieńNazwęZaznaczonegoMeblaToolStripMenuItem});
            this.contextMenuStripFurnitureViewList.Name = "contextMenuStripFurnitureViewList";
            this.contextMenuStripFurnitureViewList.Size = new System.Drawing.Size(258, 70);
            this.contextMenuStripFurnitureViewList.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripFurnitureViewList_Opening);
            this.contextMenuStripFurnitureViewList.Click += new System.EventHandler(this.contextMenuStripFurnitureViewList_Click);
            // 
            // dodajWłasnyMebelToolStripMenuItem
            // 
            this.dodajWłasnyMebelToolStripMenuItem.Name = "dodajWłasnyMebelToolStripMenuItem";
            this.dodajWłasnyMebelToolStripMenuItem.Size = new System.Drawing.Size(257, 22);
            this.dodajWłasnyMebelToolStripMenuItem.Text = "Dodaj własny mebel";
            this.dodajWłasnyMebelToolStripMenuItem.Click += new System.EventHandler(this.dodajWłasnyMebelToolStripMenuItem_Click);
            // 
            // deleteFurnitureToolStripMenuItem
            // 
            this.deleteFurnitureToolStripMenuItem.Name = "deleteFurnitureToolStripMenuItem";
            this.deleteFurnitureToolStripMenuItem.Size = new System.Drawing.Size(257, 22);
            this.deleteFurnitureToolStripMenuItem.Text = "Usuń zaznaczony mebel";
            this.deleteFurnitureToolStripMenuItem.Click += new System.EventHandler(this.deleteFurnitureToolStripMenuItem_Click);
            // 
            // zmieńNazwęZaznaczonegoMeblaToolStripMenuItem
            // 
            this.zmieńNazwęZaznaczonegoMeblaToolStripMenuItem.Name = "zmieńNazwęZaznaczonegoMeblaToolStripMenuItem";
            this.zmieńNazwęZaznaczonegoMeblaToolStripMenuItem.Size = new System.Drawing.Size(257, 22);
            this.zmieńNazwęZaznaczonegoMeblaToolStripMenuItem.Text = "Zmień nazwę zaznaczonego mebla";
            this.zmieńNazwęZaznaczonegoMeblaToolStripMenuItem.Click += new System.EventHandler(this.zmieńNazwęZaznaczonegoMeblaToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSlateGray;
            this.ClientSize = new System.Drawing.Size(1008, 695);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.wallLengthText);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(700, 600);
            this.Name = "Form1";
            this.Text = "Projektowanie mieszkania 2D";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.artboard)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupTools.ResumeLayout(false);
            this.groupTools.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.contextMenuStripFurnitureViewList.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox artboard;
        private System.Windows.Forms.Label wallLengthText;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupTools;
        private System.Windows.Forms.RadioButton radioPlaceWall;
        private System.Windows.Forms.RadioButton radioEdit;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.RadioButton radioEditWindow;
        private System.Windows.Forms.RadioButton radioPlaceWindow;
        private System.Windows.Forms.ListView listViewFurniture;
        private System.Windows.Forms.RadioButton radioButtonFurniture;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripFurnitureViewList;
        private System.Windows.Forms.ToolStripMenuItem dodajWłasnyMebelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteFurnitureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zmieńNazwęZaznaczonegoMeblaToolStripMenuItem;
        public System.Windows.Forms.ListBox listBoxRooms;
        private System.Windows.Forms.RadioButton doorButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem zapiszToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wczytajToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oddalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem przybliżToolStripMenuItem;
    }
}

