using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using Leayal.Closers.CMF;
using CLOSERS_CMFReader.Classes;
using System.IO;

namespace CLOSERS_CMFReader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public CMFFile archive;
        public List<string> CMF_files = new List<string>();
        public string Unpacking_file;
        public List<UnpackList> items = new List<UnpackList>();
        public List<UnpackList> SelectCMF_file = new List<UnpackList>();
        private string motion;

        public MainWindow()
        {
            InitializeComponent();
            UnpackCMF_btn.IsEnabled = false;
            UnpackCMFRecord_btn.IsEnabled = false;
            CMFListView.ItemsSource = items;
        }

        private void UnpackCMF_btn_Click(object sender, RoutedEventArgs e)
        {
            motion = "Unpacking";
            archive = null;
            ShowOutputWin OutputWindows = new ShowOutputWin(motion, CMF_files, CMF_files.Count);
            OutputWindows.Show();
            /*
            if (CMFListView.SelectedItems.Count > 0)
            {
                for (int i = 0; i < CMFListView.SelectedItems.Count; i++)
                {
                    UnpackList SelectCMF = (UnpackList)CMFListView.SelectedItems[i];
                    SelectCMF_file.Add(SelectCMF);
                }
                ShowOutputWin OutputWindows = new ShowOutputWin(motion, CMF_files, items.Count);
                OutputWindows.Show();
            }
            else
            {
                ShowOutputWin OutputWindows = new ShowOutputWin(motion, CMF_files, items.Count);
                OutputWindows.Show();
            }
            */
        }

        private void PackFile_btn_Click(object sender, RoutedEventArgs e)
        {
            //ShowOutputWin OutputWindows = new ShowOutputWin();
            //OutputWindows.Show();
        }

        private void ReadCMF_btn_Click(object sender, RoutedEventArgs e)
        {
            UnpackCMF_btn.IsEnabled = true;
            UnpackCMFRecord_btn.IsEnabled = true;
            OpenFileDialog ReadCMF = new OpenFileDialog();
            ReadCMF.Title = "Select a file to open";
            ReadCMF.RestoreDirectory = true;
            ReadCMF.DefaultExt = "cmf";
            ReadCMF.CheckPathExists = true;
            ReadCMF.CheckFileExists = true;
            ReadCMF.Filter = "Closers CMF|*.cmf";
            ReadCMF.Multiselect = true;
            if (ReadCMF.ShowDialog() == true)
            {
                ShowOutputWin OutputWindows = new ShowOutputWin(motion, CMF_files, ReadCMF.FileNames.Count());
                OutputWindows.Show();
                foreach (string strFilename in ReadCMF.FileNames)
                {
                    CMF_files.Add(strFilename);
                    Unpacking_file = strFilename;
                    OpenArchive(strFilename);
                    System.Threading.Thread.Sleep(10);
                    OutputWindows.processbar_processing();
                }
                OutputWindows.Close();
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

        private void Archive_Ready(object sender, EventArgs e)
        {
            for (int i = 0; i < archive.FileCount; i++)
            {
                items.Add(new UnpackList() { File = Unpacking_file,
                                             Name = new Classes.File(archive.Entries[i]).Name,
                                             Size = new Classes.File(archive.Entries[i]).SizeInString,
                                             Type = new Classes.File(archive.Entries[i]).Type});
            }
            CMFList_Update();
        }

        private void Archive_Closed(object sender, EventArgs e)
        {
            archive = null;
        }

        void CMFList_Update()
        {
            CMFListView.ItemsSource = null;
            CMFListView.ItemsSource = items;
        }

        private void UnpackClear_btn_Click(object sender, RoutedEventArgs e)
        {
            archive = null;
            CMF_files.Clear();
            UnpackCMF_btn.IsEnabled = false;
            UnpackCMFRecord_btn.IsEnabled = false;
            CMFListView.ItemsSource = null;
            Unpacking_file = "";
        }

        private void UnpackCMFRecord_btn_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveRecordDialog = new SaveFileDialog();
            saveRecordDialog.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            if (saveRecordDialog.ShowDialog() == true)
            {
                string name = saveRecordDialog.FileName;

                TextWriter sw = new StreamWriter(saveRecordDialog.FileName.ToString());

                for (int i = 0; i < items.Count; i++)
                {
                    sw.Write(items[i].File + " | " + items[i].Name);
                    sw.WriteLine("");
                }
                sw.Close();
            }
        }
    }
}
