﻿namespace CLOSERS_CMFReader
{
    partial class CMFReader
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.ReadCMF_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Clear_btn = new System.Windows.Forms.Button();
            this.Output_btn = new System.Windows.Forms.Button();
            this.OpenCMFDialog = new System.Windows.Forms.OpenFileDialog();
            this.resultview = new System.Windows.Forms.ListView();
            this.CMF_File = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.UnPacked_File = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Byte = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.UnpackedSaveFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.Processing_text = new System.Windows.Forms.Label();
            this.record_btn = new System.Windows.Forms.Button();
            this.saveRecordDialog = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // ReadCMF_btn
            // 
            this.ReadCMF_btn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReadCMF_btn.Location = new System.Drawing.Point(22, 55);
            this.ReadCMF_btn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ReadCMF_btn.Name = "ReadCMF_btn";
            this.ReadCMF_btn.Size = new System.Drawing.Size(124, 49);
            this.ReadCMF_btn.TabIndex = 1;
            this.ReadCMF_btn.Text = "Read CMF";
            this.ReadCMF_btn.UseVisualStyleBackColor = true;
            this.ReadCMF_btn.Click += new System.EventHandler(this.ReadCMF_btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(378, 38);
            this.label1.TabIndex = 3;
            this.label1.Text = "CLOSERS CMF Reader";
            // 
            // Clear_btn
            // 
            this.Clear_btn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Clear_btn.Location = new System.Drawing.Point(154, 55);
            this.Clear_btn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Clear_btn.Name = "Clear_btn";
            this.Clear_btn.Size = new System.Drawing.Size(132, 49);
            this.Clear_btn.TabIndex = 4;
            this.Clear_btn.Text = "Clear";
            this.Clear_btn.UseVisualStyleBackColor = true;
            this.Clear_btn.Click += new System.EventHandler(this.Clear_btn_Click);
            // 
            // Output_btn
            // 
            this.Output_btn.Enabled = false;
            this.Output_btn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Output_btn.Location = new System.Drawing.Point(292, 55);
            this.Output_btn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Output_btn.Name = "Output_btn";
            this.Output_btn.Size = new System.Drawing.Size(134, 49);
            this.Output_btn.TabIndex = 5;
            this.Output_btn.Text = "Output";
            this.Output_btn.UseVisualStyleBackColor = true;
            this.Output_btn.Click += new System.EventHandler(this.Output_btn_Click);
            // 
            // OpenCMFDialog
            // 
            this.OpenCMFDialog.FileName = "openFileDialog1";
            // 
            // resultview
            // 
            this.resultview.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.resultview.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.CMF_File,
            this.UnPacked_File,
            this.Byte,
            this.Type});
            this.resultview.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.resultview.GridLines = true;
            this.resultview.HideSelection = false;
            this.resultview.HoverSelection = true;
            this.resultview.Location = new System.Drawing.Point(22, 113);
            this.resultview.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.resultview.Name = "resultview";
            this.resultview.Size = new System.Drawing.Size(938, 440);
            this.resultview.TabIndex = 6;
            this.resultview.UseCompatibleStateImageBehavior = false;
            this.resultview.View = System.Windows.Forms.View.Details;
            // 
            // CMF_File
            // 
            this.CMF_File.Text = "CMF File";
            this.CMF_File.Width = 350;
            // 
            // UnPacked_File
            // 
            this.UnPacked_File.Text = "UnPacked File";
            this.UnPacked_File.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.UnPacked_File.Width = 350;
            // 
            // Byte
            // 
            this.Byte.Text = "Byte";
            this.Byte.Width = 75;
            // 
            // Type
            // 
            this.Type.Text = "Type";
            this.Type.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Type.Width = 100;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(583, 70);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(375, 34);
            this.progressBar1.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(579, 35);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(116, 22);
            this.label2.TabIndex = 10;
            this.label2.Text = "Processing：";
            // 
            // Processing_text
            // 
            this.Processing_text.AutoSize = true;
            this.Processing_text.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Processing_text.Location = new System.Drawing.Point(701, 35);
            this.Processing_text.Name = "Processing_text";
            this.Processing_text.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Processing_text.Size = new System.Drawing.Size(28, 22);
            this.Processing_text.TabIndex = 12;
            this.Processing_text.Text = "??";
            // 
            // record_btn
            // 
            this.record_btn.Enabled = false;
            this.record_btn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.record_btn.Location = new System.Drawing.Point(432, 55);
            this.record_btn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.record_btn.Name = "record_btn";
            this.record_btn.Size = new System.Drawing.Size(134, 49);
            this.record_btn.TabIndex = 13;
            this.record_btn.Text = "Record";
            this.record_btn.UseVisualStyleBackColor = true;
            this.record_btn.Click += new System.EventHandler(this.record_btn_Click);
            // 
            // CMFReader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(970, 566);
            this.Controls.Add(this.record_btn);
            this.Controls.Add(this.Processing_text);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.resultview);
            this.Controls.Add(this.Output_btn);
            this.Controls.Add(this.Clear_btn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ReadCMF_btn);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(988, 613);
            this.MinimumSize = new System.Drawing.Size(988, 613);
            this.Name = "CMFReader";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CLOSERS CMFReader";
            this.Load += new System.EventHandler(this.CMFReader_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button ReadCMF_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Clear_btn;
        private System.Windows.Forms.Button Output_btn;
        private System.Windows.Forms.OpenFileDialog OpenCMFDialog;
        private System.Windows.Forms.ListView resultview;
        private System.Windows.Forms.ColumnHeader CMF_File;
        private System.Windows.Forms.ColumnHeader UnPacked_File;
        private System.Windows.Forms.ColumnHeader Byte;
        private System.Windows.Forms.ColumnHeader Type;
        private System.Windows.Forms.FolderBrowserDialog UnpackedSaveFolder;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Processing_text;
        private System.Windows.Forms.Button record_btn;
        private System.Windows.Forms.SaveFileDialog saveRecordDialog;
    }
}

