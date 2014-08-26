using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Diagnostics;
using System.Reflection;

namespace DNA_Error_Fix_UI
{
    public partial class DNAErrorFixFrm : Form
    {
        string[] files = new string[]{};
        string _dgv = null;        

        public DNAErrorFixFrm()
        {
            InitializeComponent();
        }

        private void updateReadyProcessing()
        {
            files=files.Distinct().ToArray();            
            output.Text = files.Length+" files ready for processing :\r\n";
            for (int i = 0; i < files.Length;i++ )
                output.Text = output.Text + (i+1)+". "+files[i] + "\r\n";
            output.Text = output.Text+"\r\nIf you wish to remove a file, please use File (menu) and select 'Clear Selection' (sub-menu) and include only the files you wich to fix/correct. ";
            output.Text = output.Text+"FTDNA, 23andMe, Ancestry and Decodeme formats are supported. You can add a file (or multiple files) by either going to File (menu) and 'Select Autosomal Files' (sub-menu) or just drag and drop here. ";
            output.Text = output.Text + "If you have placed multiple files with different builds, please use b36to37 (y-str.org/tools/build-36-to-37-converter) to convert from/to build 36 or 37. ";
            output.Text = output.Text + "For a quick tutorial (or) step by step help, please click Help (menu) and  select Quick Tutorial (sub-menu).\r\n";
            statusStrip1.Visible = true;
            statusLbl.Text = files.Length + " files loaded.";
            output.SelectionStart = 0;
            output.SelectionLength = 0;
            //            
            if (files.Length >= 2)
            {
                clearSelectionToolStripMenuItem.Enabled = true;
                fixErrorsAndNoCallsToolStripMenuItem.Enabled = true;
            }
            else
            {
                clearSelectionToolStripMenuItem.Enabled = false;
                fixErrorsAndNoCallsToolStripMenuItem.Enabled = false;
                //
                saveConsolidatedFileToolStripMenuItem.Enabled = false;
                saveFixedFilesToolStripMenuItem.Enabled = false;
                saveReplacedReportToolStripMenuItem.Enabled = false;
            }
        }

        private void bwMasterLoad_DoWork(object sender, DoWorkEventArgs e)
        {
            DNAFixUtil.MasterSNPlist(files);
        }

        private void bwMasterLoad_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {           
            if (!bwFix.IsBusy)
            {
                statusLbl.Text = "Fixing differences ...";
                bwFix.RunWorkerAsync();
            }
            else
                statusLbl.Text = "Fixing background thread busy ...";
        }

        private void bwFix_DoWork(object sender, DoWorkEventArgs e)
        {
            bwFix.ReportProgress(0, "Please wait: Fixing ...");
            DNAFixUtil.fixSNPlist(files);
            //bwFix.ReportProgress(0, "Loading errors ...");
            _dgv = getFixedValues();            
        }

        private string getFixedValues()
        {            
            Hashtable map = DNAFixUtil.fixed_snps;            
            ArrayList list = null;
            StringBuilder sb = new StringBuilder();            
            //sb.Append("# File Location: "+tmp_file+"\r\n");
            //sb.Append("# The file contains fixed values in CSV format\r\n");
            //sb.Append("# Removing this header, you should be able to load in MS Excel\r\n");
            sb.Append("RSID,Chr,");
            for (int i = 0; i < files.Length;i++ )
            {
                sb.Append(Path.GetFileName(files[i]).Replace(",","_"));
                if(i!=files.Length-1)
                    sb.Append(",");
            }            
            sb.Append("\r\n");
            string[] gt = new string[files.Length];
            string snp = "";
            string f = null;            
            foreach (string key in map.Keys)
            {

                sb.Append(key.Split(":".ToCharArray())[0]);
                sb.Append(",");
                sb.Append(key.Split(":".ToCharArray())[1]);
                sb.Append(",");

                list = (ArrayList)map[key];
                for (int i = 0; i < files.Length; i++)
                {
                    gt[i] = "";
                    foreach (string l in list)
                    {
                        f = l.Split(":".ToCharArray())[0];
                        snp = l.Split(":".ToCharArray())[1];
                        if (f == Path.GetFileName(files[i]))
                        {
                            gt[i] = snp;
                        }
                    }
                }

                for (int i = 0; i < gt.Length; i++)
                {
                    sb.Append(gt[i]);
                    if(i!=gt.Length-1)
                        sb.Append(",");
                }
                sb.Append("\r\n");
            }
            return sb.ToString().Trim() ;
        }

        private void bwFix_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //statusLbl.Text = "Populating UI with errors fixed ...";
            //
            //dataGridView1 = _dgv;
            if (_dgv != null)
                output.Text = "Correcting/Fixing Process completed. Please use Tools (menu) and Save Fixed Files (sub-menu) to save fixed/corrected files.";
            //
            //StringBuilder sb = new StringBuilder();
            //sb.Append("The following files are created (fixing the errors and no-calls):\r\n\r\n");
            //foreach (string file in files)
            //    sb.Append(file + ".fixed\r\n");
            output.Visible = true;
            MessageBox.Show("Autosomal DNA Files successfully fixed. Please use Tools (menu) and Save Fixed Files (sub-menu) to save fixed/corrected files.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            statusLbl.Text = "Done.";
            statusStrip1.Visible = false;
            //
            saveConsolidatedFileToolStripMenuItem.Enabled = true;
            saveFixedFilesToolStripMenuItem.Enabled = true;
            saveReplacedReportToolStripMenuItem.Enabled = true;
        }

        private void btnAutosomalFiles_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files2 = (string[])(e.Data.GetData(DataFormats.FileDrop));
                string[] z = new string[files.Length + files2.Length];
                files.CopyTo(z, 0);
                files2.CopyTo(z, files.Length);
                files = z;
                updateReadyProcessing();
            }
        }

        private void btnAutosomalFiles_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void bwMasterLoad_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            statusLbl.Text = (string)e.UserState;
        }

        private void bwFix_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            statusLbl.Text = (string)e.UserState;
        }

        private void splitContainer1_Panel2_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files2 = (string[])(e.Data.GetData(DataFormats.FileDrop));
                string[] z = new string[files.Length + files2.Length];
                files.CopyTo(z, 0);
                files2.CopyTo(z, files.Length);
                files = z;
                updateReadyProcessing();
            }      
        }

        private void splitContainer1_Panel2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void selectAutosomalFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] files2 = new string[] { };
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                files2 = openFileDialog1.FileNames;

            string[] z = new string[files.Length + files2.Length];
            files.CopyTo(z, 0);
            files2.CopyTo(z, files.Length);
            files = z;
            updateReadyProcessing();
        }

        private void clearSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            files = new string[] { };
            updateReadyProcessing();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Version: "+Assembly.GetExecutingAssembly().GetName().Version.Major.ToString() + "." + Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString()+"\r\nDeveloper: Felix Jeyareuben <i@fc.id.au>\nWebsite: y-str.org/tools/dna-error-fix", "About DNA Error Fix", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void fixErrorsAndNoCallsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (files.Length > 1)
            {
                if (!bwMasterLoad.IsBusy)
                {
                    statusStrip1.Visible = true;
                    statusLbl.Text = "Loading autosomal files ...";
                    bwMasterLoad.RunWorkerAsync();
                }
                else
                    MessageBox.Show("Unable to fix. Background process busy.");
            }
            else
                MessageBox.Show("Please select a minimum of 2 autosomal files first. \r\n[Tip: Use Ctrl key to select multiple files or just drag and drop all files you want to fix]");
        }

        private void websiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.y-str.org/tools/dna-error-fix/");
        }

        private void DNAErrorFixFrm_Load(object sender, EventArgs e)
        {
            this.Text = "DNA Error Fix " + Assembly.GetExecutingAssembly().GetName().Version.Major.ToString() + "." + Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString();
            updateReadyProcessing();
        }

        private void licenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            output.Text = DNA_Error_Fix_UI.Properties.Resources.mit_license;
        }

        private void output_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files2 = (string[])(e.Data.GetData(DataFormats.FileDrop));
                string[] z = new string[files.Length + files2.Length];
                files.CopyTo(z, 0);
                files2.CopyTo(z, files.Length);
                files = z;
                updateReadyProcessing();
            }     
        }

        private void output_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void saveFixedFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {                
                foreach (string file in files)
                {
                    if (File.Exists(folderBrowserDialog1.SelectedPath + Path.DirectorySeparatorChar + Path.GetFileName(file)))
                    {
                        MessageBox.Show("File(s) with same name exists on the selected folder '" + folderBrowserDialog1.SelectedPath+"'. Please selection another folder.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        File.Copy(file + ".fixed", folderBrowserDialog1.SelectedPath + Path.DirectorySeparatorChar + "FIXED_"+Path.GetFileName(file));
                    }
                }
                //
                if (MessageBox.Show("I have saved " + files.Length.ToString() + " into " + folderBrowserDialog1.SelectedPath + "\r\nDo you wish remove *.fixed temporary files found at the same directory as the original input file?"
                , files.Length.ToString() + " files Saved", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    foreach (string file in files)
                    {
                        File.Delete(file + ".fixed");
                    }
                }
            }          
        }

        private void saveReplacedReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "CSV Files|*.csv";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, _dgv);
                MessageBox.Show("Replaced Report file successfully saved", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void saveConsolidatedFileToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            saveFileDialog1.Filter = "FTDNA CSV File|*.csv|23andMe Text File|*.txt|Ancestry DNA File|*.txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                saveConsolidatedFileToolStripMenuItem.Enabled = false;
                if (bwSaveConsolidated.IsBusy)
                {
                    MessageBox.Show("Saving process busy. Previous request for saving is not yet complete. Please try again after some time or restart the application.");
                    return;
                }
                else
                {
                    saveConsolidatedFileToolStripMenuItem.Enabled = false;
                    statusLbl.Text = "Saving ...";
                    statusStrip1.Visible = true;
                    bwSaveConsolidated.RunWorkerAsync(new object[] { saveFileDialog1.FileName, (saveFileDialog1.FilterIndex - 1) });
                }
            }            
        }

        private void bwSaveConsolidated_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] o = (object[])e.Argument;
            File.WriteAllText((string)o[0], DNAFixUtil.getConsolidatedFile(int.Parse(o[1].ToString())));
        }

        private void bwSaveConsolidated_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Consolidated file successfully saved.\r\n\r\nNote: If you have placed multiple files with different builds, please use b36to37 (y-str.org/tools/build-36-to-37-converter) to convert from/to build 36 or 37.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            statusLbl.Text = "Done ...";
            statusStrip1.Visible = false;
            saveConsolidatedFileToolStripMenuItem.Enabled = true;
        }

        private void quickTutorialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            output.Text = DNA_Error_Fix_UI.Properties.Resources.help;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void maleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            femaleToolStripMenuItem.Checked = false;
            maleToolStripMenuItem.Checked = true;
            DNAFixUtil.male = true;
        }

        private void femaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            maleToolStripMenuItem.Checked = false;
            femaleToolStripMenuItem.Checked = true;
            DNAFixUtil.male = false;
        }
    }
}
