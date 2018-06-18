using Manifesto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManifestoGUI
{
    public partial class Form1 : Form
    {
        private uint numberOfUpdatedFiles;
        public Form1()
        {
            InitializeComponent();
        }

        private void searchButton1_Click(object sender, EventArgs e)
        {
            bool recursive = false;
            if (checkBoxRecursive.Checked) recursive = true;

            ThreadPool.QueueUserWorkItem(o => search(textBox1.Text, recursive));
            //search(textBox1.Text, recursive);
        }

        // Original: https://stackoverflow.com/questions/172544/ignore-folders-files-when-directory-getfiles-is-denied-access
        // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/file-system/how-to-iterate-through-a-directory-tree
        private void search(string root, bool isRecursive)
        {
            // Data structure to hold names of subfolders to be
            // examined for files.
            Stack<string> dirs = new Stack<string>(20);

            if (!System.IO.Directory.Exists(root))
            {
                throw new ArgumentException();
            }
            dirs.Push(root);

            while (dirs.Count > 0)
            {
                string currentDir = dirs.Pop();
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
                    files = System.IO.Directory.GetFiles(currentDir, "*.exe");
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
                            updateTable(info, file);
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

                if(!isRecursive)
                {
                    break;
                }

                // Push the subdirectories onto the stack for traversal.
                // This could also be done before handing the files.
                foreach (string str in subDirs)
                    dirs.Push(str);
            }
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
                numberOfUpdatedFiles += 1;
                toolStripStatusLabelTotalRows.Text = "Total Rows: " + numberOfUpdatedFiles;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browser = new FolderBrowserDialog();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (browser.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = browser.SelectedPath;
            }
        }

        private void buttonSaveStats_Click(object sender, EventArgs e)
        {
            //DataGridViewRowCollection rows = dataGridView1.Rows;

            //DataView dv = new DataView(ds.Tables[0], "type = 'business' ", "type Desc", DataViewRowState.CurrentRows);
            //dataGridView1.DataSource = dv;

            //StringBuilder sb = new StringBuilder();
            //foreach(DataGridViewRow row in rows)
            //{
            //    string key = "";
            //    for (int i=1; i < 5; i++)
            //    {
            //       key += (string)row.Cells[i].Value;
            //    }
                
            //    ////row.Cells[0].Value = path;
            //    ////row.Cells[1].Value = info.Level;
            //    ////row.Cells[2].Value = info.uiAccess;
            //    ////row.Cells[3].Value = info.autoElevate;
            //    ////row.Cells[4].Value = info.dpiAware;
            //}
        }
    }
}
