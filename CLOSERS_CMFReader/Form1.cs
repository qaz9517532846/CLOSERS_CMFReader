using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Leayal.Closers.CMF;
using CLOSERS_CMFReader.Classes;
using System.IO;

namespace CLOSERS_CMFReader
{
    public partial class CMFReader : Form
    {
        public CMFReader()
        {
            InitializeComponent();
        }

        private CMFFile archive;
        public List<string> CMF_files = new List<string>();
        public string Unpacking_file;

        private void ReadCMF_btn_Click(object sender, EventArgs e)
        {
            Output_btn.Enabled = true;
            record_btn.Enabled = true;
            OpenFileDialog ReadCMF = new OpenFileDialog();
            ReadCMF.Title = "Select a file to open";
            ReadCMF.RestoreDirectory = true;
            ReadCMF.DefaultExt = "cmf";
            ReadCMF.CheckPathExists = true;
            ReadCMF.CheckFileExists = true;
            ReadCMF.Filter = "Closers CMF|*.cmf";
            ReadCMF.Multiselect = true;
            if (ReadCMF.ShowDialog() == DialogResult.OK)
            {
                progressbar progress_win = new progressbar(ReadCMF.FileNames.Length);
                progress_win.Show();
                foreach (string strFilename in ReadCMF.FileNames)
                {
                    CMF_files.Add(strFilename);
                    Unpacking_file = strFilename;
                    OpenArchive(strFilename);
                    progress_win.progress_Finish("Reading CMF File.");
                    System.Threading.Thread.Sleep(10);
                }
                progress_win.Close();
            }
        }

        private void OpenArchive(string file_path)
        {
            CloseArchive();
            archive = new CMFFile(file_path);
            archive.Ready += Archive_Ready;
            archive.Closed += Archive_Closed;
            try
            {
                archive.BeginRead();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Error while opening '{archive.Filename}'\n" + ex.ToString(), "Error");
                archive = null;
            }
        }

        private void CloseArchive()
        {
            if (archive != null)
            {
                archive.Dispose();
                archive = null;
            }
        }

        private void CMFReader_Load(object sender, EventArgs e)
        {

        }

        private void Archive_Ready(object sender, EventArgs e)
        {
            for (int i = 0; i < archive.FileCount; i++)
            {
                var result_item = new ListViewItem(Unpacking_file);
                result_item.SubItems.Add(new Classes.File(archive.Entries[i]).Name);
                result_item.SubItems.Add(new Classes.File(archive.Entries[i]).SizeInString);
                result_item.SubItems.Add(new Classes.File(archive.Entries[i]).Type);
                resultview.Items.Add(result_item);
            }
        }

        private void Archive_Closed(object sender, EventArgs e)
        {
            archive = null;
        }

        private void Clear_btn_Click(object sender, EventArgs e)
        {
            archive = null;
            Output_btn.Enabled = false;
            record_btn.Enabled = false;
            CMF_files.Clear();
            resultview.Items.Clear();
            Unpacking_file = "";
        }

        private void Output_btn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog UnpackedSaveFolder = new FolderBrowserDialog();
            UnpackedSaveFolder.ShowDialog();
            progressbar progress_win = new progressbar(CMF_files.Count + 1);
            progress_win.Show();
            foreach (string output_file in CMF_files)
            {
                CloseArchive();
                archive = new CMFFile(output_file);
                archive.Closed += Archive_Closed;
                archive.BeginRead();
                //extractArchive(UnpackedSaveFolder.SelectedPath);
                for (int i = 0; i < archive.FileCount; i++)
                {
                    archive.ExtractEntry(archive.Entries[i], UnpackedSaveFolder.SelectedPath + "/" + new Classes.File(archive.Entries[i]).Name);
                }
                System.Threading.Thread.Sleep(10);
                progress_win.progress_Finish("Unpacking output CMF File.");
            }
            progress_win.Close();
        }

        public void extractArchive(string destination)
        {   
            archive.ExtractAllEntries(destination);
        }

        private void record_btn_Click(object sender, EventArgs e)
        {
            saveRecordDialog.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            if (saveRecordDialog.ShowDialog() == DialogResult.OK)
            {
                string name = saveRecordDialog.FileName;

                TextWriter sw = new StreamWriter(saveRecordDialog.FileName.ToString());

                for (int i = 0; i < resultview.Items.Count; i++)
                {
                    sw.Write(resultview.Items[i].SubItems[0].Text + " | " + resultview.Items[i].SubItems[1].Text);
                    sw.WriteLine("");
                }
                sw.Close();
            }
        }
    }
}
