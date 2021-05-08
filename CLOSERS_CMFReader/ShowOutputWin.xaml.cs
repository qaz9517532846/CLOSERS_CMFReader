using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using CLOSERS_CMFReader.WinForms;
using CLOSERS_CMFReader.Classes;

namespace CLOSERS_CMFReader
{
    /// <summary>
    /// ShowOutputWin.xaml 的互動邏輯
    /// </summary>
    public partial class ShowOutputWin : Window
    {
        private CMFFile archive;
        private List<string> Unpack_file;
        private List<string> UnpackSelect_file = new List<string>();
        private string cmf_motion;

        public ShowOutputWin(string motion, List<string> CMF_files, int Datanum)
        {
            InitializeComponent();
            Unpack_file = CMF_files;
            cmf_motion = motion;
            progressbar_motion.Minimum = 1;
            progressbar_motion.Maximum = Datanum;
            if (motion == "Read")
            {
                Cancle_btn.IsEnabled = false;
                OK_btn.IsEnabled = false;
                Close_btn.IsEnabled = false;
                Browse_btn.IsEnabled = false;
                PathTextbox.IsEnabled = false;
            }
            else if (motion == "UnpackingSelect")
            {
                foreach (string output_file in CMF_files)
                {
                    string[] data_file = null;
                    data_file = output_file.Split(',');
                    UnpackSelect_file.Add(data_file[0]);

                }
            }
        }

        private void Close_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Browse_btn_Click(object sender, RoutedEventArgs e)
        {
            using (WpfFolderBrowserDialogEx folderBrowse = new WpfFolderBrowserDialogEx()
            {
                Title = "Select destination folder"
            })
                if (folderBrowse.ShowDialog(this) == true)
                {
                    PathTextbox.Text = folderBrowse.FileName;
                }
        }

        private void Cancle_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OK_btn_Click(object sender, RoutedEventArgs e)
        {
            Cancle_btn.IsEnabled = false;
            OK_btn.IsEnabled = false;
            if (PathTextbox.Text == "")
            {
                MessageBox.Show("Please input destination path.");
            }
            else
            {
                if (cmf_motion == "Unpacking")
                {
                    foreach (string output_file in Unpack_file)
                    {
                        archive = new CMFFile(output_file);
                        archive.Closed += Archive_Closed;
                        archive.BeginRead();
                        for (int i = 0; i < archive.FileCount; i++)
                        {
                            archive.ExtractEntry(archive.Entries[i], PathTextbox.Text + "/" + new Classes.File(archive.Entries[i]).Name);
                            ShowProcessingText.AppendText((new Classes.File(archive.Entries[i]).Name) + "......finished" + System.Environment.NewLine);
                        }
                        processbar_processing();
                        System.Threading.Thread.Sleep(10);
                    }
                }
                else if (cmf_motion == "UnpackingSelect")
                {
                    string predata_file = "";
                    foreach (string output_file in Unpack_file)
                    {
                        string[] data_file = null;
                        data_file = output_file.Split(',');
                        if (data_file[0] == predata_file)
                        {
                            archive.ExtractEntry(this.archive[data_file[1]], PathTextbox.Text + "/" + data_file[1]);
                        }
                        else
                        {
                            archive = new CMFFile(data_file[0]);
                            archive.Closed += Archive_Closed;
                            archive.BeginRead();
                            archive.ExtractEntry(this.archive[data_file[1]], PathTextbox.Text + "/" + data_file[1]);
                        }
                        predata_file = data_file[0];
                        ShowProcessingText.AppendText((data_file[1] + "......finished" + System.Environment.NewLine));
                        processbar_processing();
                        System.Threading.Thread.Sleep(10);                    }
                }
            }
        }

        private void Archive_Closed(object sender, EventArgs e)
        {
            archive = null;
        }

        public void processbar_processing()
        {
            progressbar_motion.Value++;
        }
    }
}