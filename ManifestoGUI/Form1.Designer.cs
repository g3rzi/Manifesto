using System.Windows.Forms;

namespace ManifestoGUI
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.FileColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LevelColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uiAccessColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.autoElevateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dpiAwareColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelTotalRows = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.button1 = new System.Windows.Forms.Button();
            this.searchButton1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.checkBoxRecursive = new System.Windows.Forms.CheckBox();
            this.groupBoxLevel = new System.Windows.Forms.GroupBox();
            this.checkBoxLevelAsInvoker = new System.Windows.Forms.CheckBox();
            this.checkBoxhHighestAvailable = new System.Windows.Forms.CheckBox();
            this.checkBoxRequireAdministrator = new System.Windows.Forms.CheckBox();
            this.groupBoxuiAccess = new System.Windows.Forms.GroupBox();
            this.checkBoxUiAccessTrue = new System.Windows.Forms.CheckBox();
            this.checkBoxUiAccessFalse = new System.Windows.Forms.CheckBox();
            this.groupBoxAutoElevate = new System.Windows.Forms.GroupBox();
            this.checkBoxAutoElevateTrue = new System.Windows.Forms.CheckBox();
            this.checkBoxAutoElevateFalse = new System.Windows.Forms.CheckBox();
            this.groupBoxdpiAware = new System.Windows.Forms.GroupBox();
            this.checkBoxDpiAwareTrue = new System.Windows.Forms.CheckBox();
            this.checkBoxDpiAwareFalse = new System.Windows.Forms.CheckBox();
            this.buttonSelectAll = new System.Windows.Forms.Button();
            this.buttonClearResults = new System.Windows.Forms.Button();
            this.buttonSaveResults = new System.Windows.Forms.Button();
            this.comboBoxExtensions = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBoxLevel.SuspendLayout();
            this.groupBoxuiAccess.SuspendLayout();
            this.groupBoxAutoElevate.SuspendLayout();
            this.groupBoxdpiAware.SuspendLayout();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FileColumn,
            this.LevelColumn,
            this.uiAccessColumn,
            this.autoElevateColumn,
            this.dpiAwareColumn});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(890, 343);
            this.dataGridView1.TabIndex = 0;
            // 
            // FileColumn
            // 
            this.FileColumn.HeaderText = "File path";
            this.FileColumn.Name = "FileColumn";
            // 
            // LevelColumn
            // 
            this.LevelColumn.HeaderText = "Level";
            this.LevelColumn.Name = "LevelColumn";
            // 
            // uiAccessColumn
            // 
            this.uiAccessColumn.HeaderText = "uiAccess";
            this.uiAccessColumn.Name = "uiAccessColumn";
            // 
            // autoElevateColumn
            // 
            this.autoElevateColumn.HeaderText = "autoElevate";
            this.autoElevateColumn.Name = "autoElevateColumn";
            // 
            // dpiAwareColumn
            // 
            this.dpiAwareColumn.HeaderText = "dpiAware";
            this.dpiAwareColumn.Name = "dpiAwareColumn";
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 210);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(890, 10);
            this.splitter1.TabIndex = 7;
            this.splitter1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.statusStrip1);
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 220);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(890, 343);
            this.panel2.TabIndex = 8;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelTotalRows,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 321);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(890, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelTotalRows
            // 
            this.toolStripStatusLabelTotalRows.Name = "toolStripStatusLabelTotalRows";
            this.toolStripStatusLabelTotalRows.Size = new System.Drawing.Size(67, 17);
            this.toolStripStatusLabelTotalRows.Text = "Total rows: ";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(42, 17);
            this.toolStripStatusLabel1.Text = "Status:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(24, 51);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Browse...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.buttonBrowse_Click_1);
            // 
            // searchButton1
            // 
            this.searchButton1.Image = global::ManifestoGUI.Properties.Resources.search__1_;
            this.searchButton1.Location = new System.Drawing.Point(105, 51);
            this.searchButton1.Name = "searchButton1";
            this.searchButton1.Size = new System.Drawing.Size(75, 23);
            this.searchButton1.TabIndex = 2;
            this.searchButton1.UseVisualStyleBackColor = true;
            this.searchButton1.Click += new System.EventHandler(this.searchButton1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Folder name: ";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(98, 25);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(272, 20);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = "C:\\Windows";
            // 
            // checkBoxRecursive
            // 
            this.checkBoxRecursive.AutoSize = true;
            this.checkBoxRecursive.Location = new System.Drawing.Point(204, 56);
            this.checkBoxRecursive.Name = "checkBoxRecursive";
            this.checkBoxRecursive.Size = new System.Drawing.Size(74, 17);
            this.checkBoxRecursive.TabIndex = 6;
            this.checkBoxRecursive.Text = "Recursive";
            this.checkBoxRecursive.UseVisualStyleBackColor = true;
            // 
            // groupBoxLevel
            // 
            this.groupBoxLevel.Controls.Add(this.checkBoxLevelAsInvoker);
            this.groupBoxLevel.Controls.Add(this.checkBoxhHighestAvailable);
            this.groupBoxLevel.Controls.Add(this.checkBoxRequireAdministrator);
            this.groupBoxLevel.Location = new System.Drawing.Point(127, 89);
            this.groupBoxLevel.Name = "groupBoxLevel";
            this.groupBoxLevel.Size = new System.Drawing.Size(200, 100);
            this.groupBoxLevel.TabIndex = 13;
            this.groupBoxLevel.TabStop = false;
            this.groupBoxLevel.Text = "Level";
            // 
            // checkBoxLevelAsInvoker
            // 
            this.checkBoxLevelAsInvoker.AutoSize = true;
            this.checkBoxLevelAsInvoker.Location = new System.Drawing.Point(14, 28);
            this.checkBoxLevelAsInvoker.Name = "checkBoxLevelAsInvoker";
            this.checkBoxLevelAsInvoker.Size = new System.Drawing.Size(73, 17);
            this.checkBoxLevelAsInvoker.TabIndex = 11;
            this.checkBoxLevelAsInvoker.Text = "asInvoker";
            this.checkBoxLevelAsInvoker.UseVisualStyleBackColor = true;
            // 
            // checkBoxhHighestAvailable
            // 
            this.checkBoxhHighestAvailable.AutoSize = true;
            this.checkBoxhHighestAvailable.Location = new System.Drawing.Point(14, 74);
            this.checkBoxhHighestAvailable.Name = "checkBoxhHighestAvailable";
            this.checkBoxhHighestAvailable.Size = new System.Drawing.Size(103, 17);
            this.checkBoxhHighestAvailable.TabIndex = 12;
            this.checkBoxhHighestAvailable.Text = "highestAvailable";
            this.checkBoxhHighestAvailable.UseVisualStyleBackColor = true;
            // 
            // checkBoxRequireAdministrator
            // 
            this.checkBoxRequireAdministrator.AutoSize = true;
            this.checkBoxRequireAdministrator.Location = new System.Drawing.Point(14, 51);
            this.checkBoxRequireAdministrator.Name = "checkBoxRequireAdministrator";
            this.checkBoxRequireAdministrator.Size = new System.Drawing.Size(118, 17);
            this.checkBoxRequireAdministrator.TabIndex = 10;
            this.checkBoxRequireAdministrator.Text = "requireAdministrator";
            this.checkBoxRequireAdministrator.UseVisualStyleBackColor = true;
            // 
            // groupBoxuiAccess
            // 
            this.groupBoxuiAccess.Controls.Add(this.checkBoxUiAccessTrue);
            this.groupBoxuiAccess.Controls.Add(this.checkBoxUiAccessFalse);
            this.groupBoxuiAccess.Location = new System.Drawing.Point(352, 89);
            this.groupBoxuiAccess.Name = "groupBoxuiAccess";
            this.groupBoxuiAccess.Size = new System.Drawing.Size(97, 80);
            this.groupBoxuiAccess.TabIndex = 14;
            this.groupBoxuiAccess.TabStop = false;
            this.groupBoxuiAccess.Text = "uiAccess";
            // 
            // checkBoxUiAccessTrue
            // 
            this.checkBoxUiAccessTrue.AutoSize = true;
            this.checkBoxUiAccessTrue.Location = new System.Drawing.Point(14, 28);
            this.checkBoxUiAccessTrue.Name = "checkBoxUiAccessTrue";
            this.checkBoxUiAccessTrue.Size = new System.Drawing.Size(44, 17);
            this.checkBoxUiAccessTrue.TabIndex = 11;
            this.checkBoxUiAccessTrue.Text = "true";
            this.checkBoxUiAccessTrue.UseVisualStyleBackColor = true;
            // 
            // checkBoxUiAccessFalse
            // 
            this.checkBoxUiAccessFalse.AutoSize = true;
            this.checkBoxUiAccessFalse.Location = new System.Drawing.Point(14, 51);
            this.checkBoxUiAccessFalse.Name = "checkBoxUiAccessFalse";
            this.checkBoxUiAccessFalse.Size = new System.Drawing.Size(48, 17);
            this.checkBoxUiAccessFalse.TabIndex = 10;
            this.checkBoxUiAccessFalse.Text = "false";
            this.checkBoxUiAccessFalse.UseVisualStyleBackColor = true;
            // 
            // groupBoxAutoElevate
            // 
            this.groupBoxAutoElevate.Controls.Add(this.checkBoxAutoElevateTrue);
            this.groupBoxAutoElevate.Controls.Add(this.checkBoxAutoElevateFalse);
            this.groupBoxAutoElevate.Location = new System.Drawing.Point(474, 89);
            this.groupBoxAutoElevate.Name = "groupBoxAutoElevate";
            this.groupBoxAutoElevate.Size = new System.Drawing.Size(93, 80);
            this.groupBoxAutoElevate.TabIndex = 15;
            this.groupBoxAutoElevate.TabStop = false;
            this.groupBoxAutoElevate.Text = "autoElevate";
            // 
            // checkBoxAutoElevateTrue
            // 
            this.checkBoxAutoElevateTrue.AutoSize = true;
            this.checkBoxAutoElevateTrue.Location = new System.Drawing.Point(14, 28);
            this.checkBoxAutoElevateTrue.Name = "checkBoxAutoElevateTrue";
            this.checkBoxAutoElevateTrue.Size = new System.Drawing.Size(44, 17);
            this.checkBoxAutoElevateTrue.TabIndex = 11;
            this.checkBoxAutoElevateTrue.Text = "true";
            this.checkBoxAutoElevateTrue.UseVisualStyleBackColor = true;
            // 
            // checkBoxAutoElevateFalse
            // 
            this.checkBoxAutoElevateFalse.AutoSize = true;
            this.checkBoxAutoElevateFalse.Location = new System.Drawing.Point(14, 51);
            this.checkBoxAutoElevateFalse.Name = "checkBoxAutoElevateFalse";
            this.checkBoxAutoElevateFalse.Size = new System.Drawing.Size(48, 17);
            this.checkBoxAutoElevateFalse.TabIndex = 10;
            this.checkBoxAutoElevateFalse.Text = "false";
            this.checkBoxAutoElevateFalse.UseVisualStyleBackColor = true;
            // 
            // groupBoxdpiAware
            // 
            this.groupBoxdpiAware.Controls.Add(this.checkBoxDpiAwareTrue);
            this.groupBoxdpiAware.Controls.Add(this.checkBoxDpiAwareFalse);
            this.groupBoxdpiAware.Location = new System.Drawing.Point(586, 89);
            this.groupBoxdpiAware.Name = "groupBoxdpiAware";
            this.groupBoxdpiAware.Size = new System.Drawing.Size(93, 80);
            this.groupBoxdpiAware.TabIndex = 16;
            this.groupBoxdpiAware.TabStop = false;
            this.groupBoxdpiAware.Text = "dpiAware";
            // 
            // checkBoxDpiAwareTrue
            // 
            this.checkBoxDpiAwareTrue.AutoSize = true;
            this.checkBoxDpiAwareTrue.Location = new System.Drawing.Point(14, 28);
            this.checkBoxDpiAwareTrue.Name = "checkBoxDpiAwareTrue";
            this.checkBoxDpiAwareTrue.Size = new System.Drawing.Size(44, 17);
            this.checkBoxDpiAwareTrue.TabIndex = 11;
            this.checkBoxDpiAwareTrue.Text = "true";
            this.checkBoxDpiAwareTrue.UseVisualStyleBackColor = true;
            // 
            // checkBoxDpiAwareFalse
            // 
            this.checkBoxDpiAwareFalse.AutoSize = true;
            this.checkBoxDpiAwareFalse.Location = new System.Drawing.Point(14, 51);
            this.checkBoxDpiAwareFalse.Name = "checkBoxDpiAwareFalse";
            this.checkBoxDpiAwareFalse.Size = new System.Drawing.Size(48, 17);
            this.checkBoxDpiAwareFalse.TabIndex = 10;
            this.checkBoxDpiAwareFalse.Text = "false";
            this.checkBoxDpiAwareFalse.UseVisualStyleBackColor = true;
            // 
            // buttonSelectAll
            // 
            this.buttonSelectAll.Location = new System.Drawing.Point(24, 89);
            this.buttonSelectAll.Name = "buttonSelectAll";
            this.buttonSelectAll.Size = new System.Drawing.Size(82, 23);
            this.buttonSelectAll.TabIndex = 17;
            this.buttonSelectAll.Text = "Un\\Select All";
            this.buttonSelectAll.UseVisualStyleBackColor = true;
            this.buttonSelectAll.Click += new System.EventHandler(this.buttonSelectAll_Click);
            // 
            // buttonClearResults
            // 
            this.buttonClearResults.Location = new System.Drawing.Point(24, 118);
            this.buttonClearResults.Name = "buttonClearResults";
            this.buttonClearResults.Size = new System.Drawing.Size(82, 23);
            this.buttonClearResults.TabIndex = 18;
            this.buttonClearResults.Text = "Clear results";
            this.buttonClearResults.UseVisualStyleBackColor = true;
            this.buttonClearResults.Click += new System.EventHandler(this.buttonClearResults_Click);
            // 
            // buttonSaveResults
            // 
            this.buttonSaveResults.Location = new System.Drawing.Point(24, 147);
            this.buttonSaveResults.Name = "buttonSaveResults";
            this.buttonSaveResults.Size = new System.Drawing.Size(82, 23);
            this.buttonSaveResults.TabIndex = 19;
            this.buttonSaveResults.Text = "Save results";
            this.buttonSaveResults.UseVisualStyleBackColor = true;
            this.buttonSaveResults.Click += new System.EventHandler(this.buttonSaveResults_Click);
            // 
            // comboBoxExtensions
            // 
            this.comboBoxExtensions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxExtensions.FormattingEnabled = true;
            this.comboBoxExtensions.Items.AddRange(new object[] {
            "All Files",
            "*.exe",
            "*.com",
            "*.dll"});
            this.comboBoxExtensions.Location = new System.Drawing.Point(385, 24);
            this.comboBoxExtensions.Name = "comboBoxExtensions";
            this.comboBoxExtensions.Size = new System.Drawing.Size(121, 21);
            this.comboBoxExtensions.TabIndex = 21;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.comboBoxExtensions);
            this.panel1.Controls.Add(this.buttonSaveResults);
            this.panel1.Controls.Add(this.buttonClearResults);
            this.panel1.Controls.Add(this.buttonSelectAll);
            this.panel1.Controls.Add(this.groupBoxdpiAware);
            this.panel1.Controls.Add(this.groupBoxAutoElevate);
            this.panel1.Controls.Add(this.groupBoxuiAccess);
            this.panel1.Controls.Add(this.groupBoxLevel);
            this.panel1.Controls.Add(this.checkBoxRecursive);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.searchButton1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(890, 210);
            this.panel1.TabIndex = 6;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(890, 24);
            this.menuStrip1.TabIndex = 22;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 563);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Manifesto";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBoxLevel.ResumeLayout(false);
            this.groupBoxLevel.PerformLayout();
            this.groupBoxuiAccess.ResumeLayout(false);
            this.groupBoxuiAccess.PerformLayout();
            this.groupBoxAutoElevate.ResumeLayout(false);
            this.groupBoxAutoElevate.PerformLayout();
            this.groupBoxdpiAware.ResumeLayout(false);
            this.groupBoxdpiAware.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridViewTextBoxColumn FileColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn LevelColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn uiAccessColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn autoElevateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dpiAwareColumn;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelTotalRows;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private Button button1;
        private Button searchButton1;
        private Label label1;
        private TextBox textBox1;
        private CheckBox checkBoxRecursive;
        private GroupBox groupBoxLevel;
        private CheckBox checkBoxLevelAsInvoker;
        private CheckBox checkBoxhHighestAvailable;
        private CheckBox checkBoxRequireAdministrator;
        private GroupBox groupBoxuiAccess;
        private CheckBox checkBoxUiAccessTrue;
        private CheckBox checkBoxUiAccessFalse;
        private GroupBox groupBoxAutoElevate;
        private CheckBox checkBoxAutoElevateTrue;
        private CheckBox checkBoxAutoElevateFalse;
        private GroupBox groupBoxdpiAware;
        private CheckBox checkBoxDpiAwareTrue;
        private CheckBox checkBoxDpiAwareFalse;
        private Button buttonSelectAll;
        private Button buttonClearResults;
        private Button buttonSaveResults;
        private ComboBox comboBoxExtensions;
        private Panel panel1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem aboutToolStripMenuItem;
    }
}

