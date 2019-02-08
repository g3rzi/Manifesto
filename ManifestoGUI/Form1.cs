using Manifesto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace ManifestoGUI
{
    public partial class Form1 : Form
    {
        private uint numberOfUpdatedFiles;
        private List<GroupBox> groupBoxesList;
        
        public Form1()
        {
            InitializeComponent();
            this.groupBoxesList = getGroupBoxesList();
            this.comboBoxExtensions.Text = "All Files";
        }
        
        private List<GroupBox> getGroupBoxesList()
        {
            List<GroupBox> groupBoxes = new List<GroupBox>();
            foreach (Control groupControl in panel1.Controls)
            {
                if (groupControl is GroupBox)
                {
                    groupBoxes.Add((GroupBox)groupControl);
                }
            }
            return groupBoxes;
        }

        private void searchButton1_Click(object sender, EventArgs e)
        {
            bool recursive = false;
            if (checkBoxRecursive.Checked) recursive = true;

            string extension = "*.*";
            if (this.comboBoxExtensions.Text != "All Files")
            {
                extension = comboBoxExtensions.Text;
            }
            ThreadPool.QueueUserWorkItem(o => search(textBox1.Text, recursive, extension));
            //search(textBox1.Text, recursive);
        }

        // Original: https://stackoverflow.com/questions/172544/ignore-folders-files-when-directory-getfiles-is-denied-access
        // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/file-system/how-to-iterate-through-a-directory-tree
        private void search(string root, bool isRecursive, string extension)
        {
            // Data structure to hold names of subfolders to be
            // examined for files.
            Stack<string> dirs = new Stack<string>(10);

            if (!System.IO.Directory.Exists(root))
            {
                throw new ArgumentException();
            }
            dirs.Push(root);

            while (dirs.Count > 0)
            {
                string currentDir = dirs.Pop();
                updateStatusToolStrip(currentDir);
                string[] subDirs;
                try
                {
                    subDirs = System.IO.Directory.GetDirectories(currentDir);
                }
                // An UnauthorizedAccessException exception will be thrown if we do not have
                // discovery permission on a folder or file. It may or may not be acceptable 
                // to ignore the exception and continue enumerating the remaining files and 
                // folders. It is also possible (but unlikely) that a DirectoryNotFound exception 
                // will be raised. This will happen if currentDir has been deleted by
                // another application or thread after our call to Directory.Exists. The 
                // choice of which exceptions to catch depends entirely on the specific task 
                // you are intending to perform and also on how much you know with certainty 
                // about the systems on which this code will run.
                catch (UnauthorizedAccessException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                catch (System.IO.DirectoryNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }

                string[] files = null;
                try
                {
                    files = System.IO.Directory.GetFiles(currentDir, extension);
                }

                catch (UnauthorizedAccessException e)
                {

                    Console.WriteLine(e.Message);
                    continue;
                }

                catch (System.IO.DirectoryNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                // Perform the required action on each file here.
                // Modify this block to perform your required task.
                foreach (string file in files)
                {
                    try
                    {
                        ManifestInfo info = Engine.GetManifestInfo(file);
                        if (info != null && (String.Empty != info.Level + info.uiAccess + info.autoElevate + info.dpiAware))
                        {
                            if(UserMatchesFilters(info)) { 
                            //if(isFilteredByCheckboxes(info)){
                                updateTable(info, file);
                            }
                        }
                    }
                    catch (System.IO.FileNotFoundException e)
                    {
                        // If file was deleted by a separate application
                        //  or thread since the call to TraverseTree()
                        // then just continue.
                        Console.WriteLine(e.Message);
                        continue;
                    }
                }

                if (!isRecursive)
                {
                    break;
                }

                // Push the subdirectories onto the stack for traversal.
                // This could also be done before handing the files.
                foreach (string str in subDirs)
                    dirs.Push(str);
            }

            this.toolStripStatusLabel1.Text = "Done";
        }

        private delegate void updateStatusToolStripCallBack(string path);

        void updateStatusToolStrip(string path)
        {
            if (this.InvokeRequired)
            {
                updateStatusToolStripCallBack s = new updateStatusToolStripCallBack(updateStatusToolStrip);
                this.Invoke(s, new object[] { path });
            }
            else
            {
                this.toolStripStatusLabel1.Text = "Status: " + path;
            }
        }

        bool FieldMatchesFilterGroup(string field, params CheckBox[] group)
        {
            return group.All(box => !box.Checked)
                || group.Any(box => box.Checked && field != null && box.Text.ToLower() == field.ToLower());
        }

        bool UserMatchesFilters(ManifestInfo info)
        {
            return FieldMatchesFilterGroup(info.Level, checkBoxLevelAsInvoker, checkBoxRequireAdministrator, checkBoxhHighestAvailable)
                && FieldMatchesFilterGroup(info.autoElevate, checkBoxAutoElevateFalse, checkBoxAutoElevateTrue)
                && FieldMatchesFilterGroup(info.dpiAware, checkBoxDpiAwareFalse, checkBoxDpiAwareTrue)  // there are other types like "Explorer", "per monitor", ...
                                                                                                        // Can be classified to "others"
                && FieldMatchesFilterGroup(info.uiAccess, checkBoxUiAccessFalse, checkBoxUiAccessTrue);
        }

        private delegate void updateTableCallBack(ManifestInfo info, string path);

        void updateTable(ManifestInfo info, string path)
        {
            if (this.InvokeRequired)
            {
                updateTableCallBack s = new updateTableCallBack(updateTable);
                this.Invoke(s, new object[] { info, path });
            }
            else
            {
                DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                row.Cells[0].Value = path;
                row.Cells[1].Value = info.Level;
                row.Cells[2].Value = info.uiAccess;
                row.Cells[3].Value = info.autoElevate;
                row.Cells[4].Value = info.dpiAware;
                dataGridView1.Rows.Add(row);
                this.numberOfUpdatedFiles += 1;
                this.toolStripStatusLabelTotalRows.Text = "Total Rows: " + this.numberOfUpdatedFiles;
            }
        }

        private bool isAllCheckboxesAreChecked()
        {
            bool allAreChecked = false;
            int numOfCheckboxes = 0;
            int checkedCheckboxes = 0;
            foreach (GroupBox groupBox in this.groupBoxesList)
            {
                foreach (Control c in groupBox.Controls)
                {
                    if (c is CheckBox)
                    {
                        numOfCheckboxes++;
                        if (((CheckBox)c).Checked)
                        {
                            checkedCheckboxes++;
                        }
                    }
                }
            }

            if (numOfCheckboxes == checkedCheckboxes)
            {
                allAreChecked = true;
            }

            return allAreChecked;
        }

        private void checkOrUncheckAll(bool checkAll)
        {
            foreach (GroupBox groupBox in this.groupBoxesList)
            {
                foreach (Control c in groupBox.Controls)
                {
                    if (c is CheckBox)
                    {
                        ((CheckBox)c).Checked = checkAll;
                    }
                }
            }
        }

        private void buttonSelectAll_Click(object sender, EventArgs e)
        {
            bool checkAll = true;
            if (isAllCheckboxesAreChecked())
            {
                checkAll = false;
            }

            checkOrUncheckAll(checkAll);
        }

        private void buttonClearResults_Click(object sender, EventArgs e)
        {
            //this.dataGridView1.DataSource = null;
            this.dataGridView1.Rows.Clear();
            this.toolStripStatusLabelTotalRows.Text = "Total Rows: 0";
            this.numberOfUpdatedFiles = 0;
        }

        // Taken from https://stackoverflow.com/a/26259909/2153777
        private void saveDataGridViewToCSV(string filename)
        {
            // Choose whether to write header. Use EnableWithoutHeaderText instead to omit header.
            dataGridView1.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            // Select all the cells
            dataGridView1.SelectAll();
            // Copy selected cells to DataObject
            DataObject dataObject = dataGridView1.GetClipboardContent();
            // Get the text of the DataObject, and serialize it to a file
            File.WriteAllText(filename, dataObject.GetText(TextDataFormat.CommaSeparatedValue));
        }

        private void buttonSaveResults_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Title = "Save results as CSV";
            saveDialog.InitialDirectory = @"c:\";
            saveDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            saveDialog.FilterIndex = 2;
            saveDialog.RestoreDirectory = true;
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                saveDataGridViewToCSV(saveDialog.FileName);
            }
        }

        private void buttonBrowse_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog browser = new FolderBrowserDialog();
            browser.SelectedPath = @"C:\Windows\System32";
            if (browser.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = browser.SelectedPath;
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Author: Eviatar Gerzi\nVersion: 1.0", "About");
        }
    }
}
