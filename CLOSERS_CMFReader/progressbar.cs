using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CLOSERS_CMFReader
{
    public partial class progressbar : Form
    {
        public progressbar(int fileCount)
        {
            InitializeComponent();
            label1.Visible = true;
            label2.Visible = true;
            progressBar1.Visible = true;
            progressBar1.Minimum = 1;
            progressBar1.Maximum = fileCount;
            progressBar1.Value = 1;// 設置進度條初始值
            progressBar1.Step = 1;// 設置每次增加的步長
        }

        public void progress_Finish(string progressMsg)
        {
            label2.Text = progressMsg;
            progressBar1.PerformStep();
        }

        private void progressbar_Load(object sender, EventArgs e)
        {

        }
    }
}
