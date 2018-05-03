namespace CSSPPolSourceSiteInputTool
{
    partial class CSSPPolSourceSiteInputToolForm
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
            this.panelButtonBar = new System.Windows.Forms.Panel();
            this.lblSubsector = new System.Windows.Forms.Label();
            this.comboBoxSubsectorNames = new System.Windows.Forms.ComboBox();
            this.butLogoff = new System.Windows.Forms.Button();
            this.panelStatusBar = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblStatusTxt = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panelPollutionSiteEdit = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.butPolSourceSiteEditCancel = new System.Windows.Forms.Button();
            this.lblPolSourceSiteEdit = new System.Windows.Forms.Label();
            this.panelPollutionSitesList = new System.Windows.Forms.Panel();
            this.panelPSS = new System.Windows.Forms.Panel();
            this.panelSubsectorPollutionSitesTop = new System.Windows.Forms.Panel();
            this.lblPolSourceSiteList = new System.Windows.Forms.Label();
            this.panelPicture = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.butShowMap = new System.Windows.Forms.Button();
            this.lblPolSourceSitePictures = new System.Windows.Forms.Label();
            this.panelMap = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.butShowPictures = new System.Windows.Forms.Button();
            this.lblPolSourceSiteMap = new System.Windows.Forms.Label();
            this.panelPassword = new System.Windows.Forms.Panel();
            this.panelPasswordCenter = new System.Windows.Forms.Panel();
            this.panelAccessCode = new System.Windows.Forms.Panel();
            this.lblEnterAccessCodeTxt = new System.Windows.Forms.Label();
            this.textBoxAccessCode = new System.Windows.Forms.TextBox();
            this.butOpenProvinceFile = new System.Windows.Forms.Button();
            this.lblProvinceFileName = new System.Windows.Forms.Label();
            this.openFileDialogCSSP = new System.Windows.Forms.OpenFileDialog();
            this.panelButtonBar.SuspendLayout();
            this.panelStatusBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panelPollutionSiteEdit.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panelPollutionSitesList.SuspendLayout();
            this.panelSubsectorPollutionSitesTop.SuspendLayout();
            this.panelPicture.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelMap.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelPassword.SuspendLayout();
            this.panelPasswordCenter.SuspendLayout();
            this.panelAccessCode.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelButtonBar
            // 
            this.panelButtonBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelButtonBar.Controls.Add(this.lblSubsector);
            this.panelButtonBar.Controls.Add(this.comboBoxSubsectorNames);
            this.panelButtonBar.Controls.Add(this.butLogoff);
            this.panelButtonBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelButtonBar.Location = new System.Drawing.Point(0, 0);
            this.panelButtonBar.Name = "panelButtonBar";
            this.panelButtonBar.Size = new System.Drawing.Size(1140, 36);
            this.panelButtonBar.TabIndex = 9;
            // 
            // lblSubsector
            // 
            this.lblSubsector.AutoSize = true;
            this.lblSubsector.Location = new System.Drawing.Point(11, 10);
            this.lblSubsector.Name = "lblSubsector";
            this.lblSubsector.Size = new System.Drawing.Size(63, 13);
            this.lblSubsector.TabIndex = 9;
            this.lblSubsector.Text = "Subsectors:";
            // 
            // comboBoxSubsectorNames
            // 
            this.comboBoxSubsectorNames.DropDownWidth = 500;
            this.comboBoxSubsectorNames.FormattingEnabled = true;
            this.comboBoxSubsectorNames.Location = new System.Drawing.Point(96, 6);
            this.comboBoxSubsectorNames.Name = "comboBoxSubsectorNames";
            this.comboBoxSubsectorNames.Size = new System.Drawing.Size(341, 21);
            this.comboBoxSubsectorNames.TabIndex = 6;
            this.comboBoxSubsectorNames.SelectedIndexChanged += new System.EventHandler(this.comboBoxSubsectorNames_SelectedIndexChanged);
            // 
            // butLogoff
            // 
            this.butLogoff.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.butLogoff.Location = new System.Drawing.Point(1047, 4);
            this.butLogoff.Name = "butLogoff";
            this.butLogoff.Size = new System.Drawing.Size(80, 25);
            this.butLogoff.TabIndex = 0;
            this.butLogoff.Text = "Logoff";
            this.butLogoff.UseVisualStyleBackColor = true;
            this.butLogoff.Click += new System.EventHandler(this.butLogoff_Click);
            // 
            // panelStatusBar
            // 
            this.panelStatusBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelStatusBar.Controls.Add(this.lblStatus);
            this.panelStatusBar.Controls.Add(this.lblStatusTxt);
            this.panelStatusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelStatusBar.Location = new System.Drawing.Point(0, 694);
            this.panelStatusBar.Name = "panelStatusBar";
            this.panelStatusBar.Size = new System.Drawing.Size(1140, 29);
            this.panelStatusBar.TabIndex = 10;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblStatus.Location = new System.Drawing.Point(58, 7);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(16, 13);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "...";
            // 
            // lblStatusTxt
            // 
            this.lblStatusTxt.AutoSize = true;
            this.lblStatusTxt.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblStatusTxt.Location = new System.Drawing.Point(12, 7);
            this.lblStatusTxt.Name = "lblStatusTxt";
            this.lblStatusTxt.Size = new System.Drawing.Size(40, 13);
            this.lblStatusTxt.TabIndex = 0;
            this.lblStatusTxt.Text = "Status:";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Location = new System.Drawing.Point(39, 248);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panelPollutionSiteEdit);
            this.splitContainer1.Panel1.Controls.Add(this.panelPollutionSitesList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panelPicture);
            this.splitContainer1.Panel2.Controls.Add(this.panelMap);
            this.splitContainer1.Size = new System.Drawing.Size(1054, 535);
            this.splitContainer1.SplitterDistance = 351;
            this.splitContainer1.TabIndex = 11;
            // 
            // panelPollutionSiteEdit
            // 
            this.panelPollutionSiteEdit.Controls.Add(this.panel3);
            this.panelPollutionSiteEdit.Location = new System.Drawing.Point(76, 257);
            this.panelPollutionSiteEdit.Name = "panelPollutionSiteEdit";
            this.panelPollutionSiteEdit.Size = new System.Drawing.Size(264, 353);
            this.panelPollutionSiteEdit.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.butPolSourceSiteEditCancel);
            this.panel3.Controls.Add(this.lblPolSourceSiteEdit);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(264, 24);
            this.panel3.TabIndex = 4;
            // 
            // butPolSourceSiteEditCancel
            // 
            this.butPolSourceSiteEditCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.butPolSourceSiteEditCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.butPolSourceSiteEditCancel.Location = new System.Drawing.Point(185, 0);
            this.butPolSourceSiteEditCancel.Name = "butPolSourceSiteEditCancel";
            this.butPolSourceSiteEditCancel.Size = new System.Drawing.Size(77, 22);
            this.butPolSourceSiteEditCancel.TabIndex = 7;
            this.butPolSourceSiteEditCancel.Text = "Cancel";
            this.butPolSourceSiteEditCancel.UseVisualStyleBackColor = true;
            // 
            // lblPolSourceSiteEdit
            // 
            this.lblPolSourceSiteEdit.AutoSize = true;
            this.lblPolSourceSiteEdit.Location = new System.Drawing.Point(5, 6);
            this.lblPolSourceSiteEdit.Name = "lblPolSourceSiteEdit";
            this.lblPolSourceSiteEdit.Size = new System.Drawing.Size(126, 13);
            this.lblPolSourceSiteEdit.TabIndex = 2;
            this.lblPolSourceSiteEdit.Text = "Pollution Source Site Edit";
            // 
            // panelPollutionSitesList
            // 
            this.panelPollutionSitesList.Controls.Add(this.panelPSS);
            this.panelPollutionSitesList.Controls.Add(this.panelSubsectorPollutionSitesTop);
            this.panelPollutionSitesList.Location = new System.Drawing.Point(16, 26);
            this.panelPollutionSitesList.Name = "panelPollutionSitesList";
            this.panelPollutionSitesList.Size = new System.Drawing.Size(225, 371);
            this.panelPollutionSitesList.TabIndex = 0;
            // 
            // panelPSS
            // 
            this.panelPSS.AutoScroll = true;
            this.panelPSS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPSS.Location = new System.Drawing.Point(0, 24);
            this.panelPSS.Name = "panelPSS";
            this.panelPSS.Size = new System.Drawing.Size(225, 347);
            this.panelPSS.TabIndex = 4;
            // 
            // panelSubsectorPollutionSitesTop
            // 
            this.panelSubsectorPollutionSitesTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSubsectorPollutionSitesTop.Controls.Add(this.lblPolSourceSiteList);
            this.panelSubsectorPollutionSitesTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSubsectorPollutionSitesTop.Location = new System.Drawing.Point(0, 0);
            this.panelSubsectorPollutionSitesTop.Name = "panelSubsectorPollutionSitesTop";
            this.panelSubsectorPollutionSitesTop.Size = new System.Drawing.Size(225, 24);
            this.panelSubsectorPollutionSitesTop.TabIndex = 3;
            // 
            // lblPolSourceSiteList
            // 
            this.lblPolSourceSiteList.AutoSize = true;
            this.lblPolSourceSiteList.Location = new System.Drawing.Point(5, 6);
            this.lblPolSourceSiteList.Name = "lblPolSourceSiteList";
            this.lblPolSourceSiteList.Size = new System.Drawing.Size(124, 13);
            this.lblPolSourceSiteList.TabIndex = 2;
            this.lblPolSourceSiteList.Text = "Pollution Source Site List";
            // 
            // panelPicture
            // 
            this.panelPicture.Controls.Add(this.panel2);
            this.panelPicture.Location = new System.Drawing.Point(217, 174);
            this.panelPicture.Name = "panelPicture";
            this.panelPicture.Size = new System.Drawing.Size(323, 311);
            this.panelPicture.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.butShowMap);
            this.panel2.Controls.Add(this.lblPolSourceSitePictures);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(323, 24);
            this.panel2.TabIndex = 4;
            // 
            // butShowMap
            // 
            this.butShowMap.Dock = System.Windows.Forms.DockStyle.Right;
            this.butShowMap.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.butShowMap.Location = new System.Drawing.Point(244, 0);
            this.butShowMap.Name = "butShowMap";
            this.butShowMap.Size = new System.Drawing.Size(77, 22);
            this.butShowMap.TabIndex = 8;
            this.butShowMap.Text = "Show Map";
            this.butShowMap.UseVisualStyleBackColor = true;
            // 
            // lblPolSourceSitePictures
            // 
            this.lblPolSourceSitePictures.AutoSize = true;
            this.lblPolSourceSitePictures.Location = new System.Drawing.Point(5, 6);
            this.lblPolSourceSitePictures.Name = "lblPolSourceSitePictures";
            this.lblPolSourceSitePictures.Size = new System.Drawing.Size(146, 13);
            this.lblPolSourceSitePictures.TabIndex = 2;
            this.lblPolSourceSitePictures.Text = "Pollution Source Site Pictures";
            // 
            // panelMap
            // 
            this.panelMap.Controls.Add(this.panel1);
            this.panelMap.Location = new System.Drawing.Point(47, 26);
            this.panelMap.Name = "panelMap";
            this.panelMap.Size = new System.Drawing.Size(323, 311);
            this.panelMap.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.butShowPictures);
            this.panel1.Controls.Add(this.lblPolSourceSiteMap);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(323, 24);
            this.panel1.TabIndex = 4;
            // 
            // butShowPictures
            // 
            this.butShowPictures.Dock = System.Windows.Forms.DockStyle.Right;
            this.butShowPictures.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.butShowPictures.Location = new System.Drawing.Point(198, 0);
            this.butShowPictures.Name = "butShowPictures";
            this.butShowPictures.Size = new System.Drawing.Size(123, 22);
            this.butShowPictures.TabIndex = 8;
            this.butShowPictures.Text = "Show Pictures";
            this.butShowPictures.UseVisualStyleBackColor = true;
            // 
            // lblPolSourceSiteMap
            // 
            this.lblPolSourceSiteMap.AutoSize = true;
            this.lblPolSourceSiteMap.Location = new System.Drawing.Point(5, 6);
            this.lblPolSourceSiteMap.Name = "lblPolSourceSiteMap";
            this.lblPolSourceSiteMap.Size = new System.Drawing.Size(129, 13);
            this.lblPolSourceSiteMap.TabIndex = 2;
            this.lblPolSourceSiteMap.Text = "Pollution Source Site Map";
            // 
            // panelPassword
            // 
            this.panelPassword.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelPassword.Controls.Add(this.panelPasswordCenter);
            this.panelPassword.Location = new System.Drawing.Point(97, 84);
            this.panelPassword.Name = "panelPassword";
            this.panelPassword.Size = new System.Drawing.Size(669, 226);
            this.panelPassword.TabIndex = 12;
            // 
            // panelPasswordCenter
            // 
            this.panelPasswordCenter.Controls.Add(this.panelAccessCode);
            this.panelPasswordCenter.Controls.Add(this.butOpenProvinceFile);
            this.panelPasswordCenter.Controls.Add(this.lblProvinceFileName);
            this.panelPasswordCenter.Location = new System.Drawing.Point(18, 19);
            this.panelPasswordCenter.Name = "panelPasswordCenter";
            this.panelPasswordCenter.Size = new System.Drawing.Size(626, 200);
            this.panelPasswordCenter.TabIndex = 2;
            // 
            // panelAccessCode
            // 
            this.panelAccessCode.Controls.Add(this.lblEnterAccessCodeTxt);
            this.panelAccessCode.Controls.Add(this.textBoxAccessCode);
            this.panelAccessCode.Location = new System.Drawing.Point(158, 78);
            this.panelAccessCode.Name = "panelAccessCode";
            this.panelAccessCode.Size = new System.Drawing.Size(362, 113);
            this.panelAccessCode.TabIndex = 5;
            this.panelAccessCode.Visible = false;
            // 
            // lblEnterAccessCodeTxt
            // 
            this.lblEnterAccessCodeTxt.AutoSize = true;
            this.lblEnterAccessCodeTxt.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblEnterAccessCodeTxt.Location = new System.Drawing.Point(19, 40);
            this.lblEnterAccessCodeTxt.Name = "lblEnterAccessCodeTxt";
            this.lblEnterAccessCodeTxt.Size = new System.Drawing.Size(101, 13);
            this.lblEnterAccessCodeTxt.TabIndex = 0;
            this.lblEnterAccessCodeTxt.Text = "Enter Access Code:";
            // 
            // textBoxAccessCode
            // 
            this.textBoxAccessCode.Location = new System.Drawing.Point(126, 37);
            this.textBoxAccessCode.Name = "textBoxAccessCode";
            this.textBoxAccessCode.Size = new System.Drawing.Size(100, 20);
            this.textBoxAccessCode.TabIndex = 3;
            this.textBoxAccessCode.TextChanged += new System.EventHandler(this.textBoxAccessCode_TextChanged);
            // 
            // butOpenProvinceFile
            // 
            this.butOpenProvinceFile.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.butOpenProvinceFile.Location = new System.Drawing.Point(222, 21);
            this.butOpenProvinceFile.Name = "butOpenProvinceFile";
            this.butOpenProvinceFile.Size = new System.Drawing.Size(183, 23);
            this.butOpenProvinceFile.TabIndex = 6;
            this.butOpenProvinceFile.Text = "Open Province File";
            this.butOpenProvinceFile.UseVisualStyleBackColor = true;
            this.butOpenProvinceFile.Click += new System.EventHandler(this.butOpenProvinceFile_Click);
            // 
            // lblProvinceFileName
            // 
            this.lblProvinceFileName.AutoSize = true;
            this.lblProvinceFileName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblProvinceFileName.Location = new System.Drawing.Point(155, 50);
            this.lblProvinceFileName.Name = "lblProvinceFileName";
            this.lblProvinceFileName.Size = new System.Drawing.Size(99, 13);
            this.lblProvinceFileName.TabIndex = 4;
            this.lblProvinceFileName.Text = "Province File Name";
            this.lblProvinceFileName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // openFileDialogCSSP
            // 
            this.openFileDialogCSSP.FileName = "PollutionSourceSiteFromCSSPWebTools_*.txt";
            // 
            // CSSPPolSourceSiteInputToolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1140, 723);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panelStatusBar);
            this.Controls.Add(this.panelButtonBar);
            this.Controls.Add(this.panelPassword);
            this.Name = "CSSPPolSourceSiteInputToolForm";
            this.Text = "CSSP Pollution Source Site Input Tool";
            this.panelButtonBar.ResumeLayout(false);
            this.panelButtonBar.PerformLayout();
            this.panelStatusBar.ResumeLayout(false);
            this.panelStatusBar.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panelPollutionSiteEdit.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panelPollutionSitesList.ResumeLayout(false);
            this.panelSubsectorPollutionSitesTop.ResumeLayout(false);
            this.panelSubsectorPollutionSitesTop.PerformLayout();
            this.panelPicture.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panelMap.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelPassword.ResumeLayout(false);
            this.panelPasswordCenter.ResumeLayout(false);
            this.panelPasswordCenter.PerformLayout();
            this.panelAccessCode.ResumeLayout(false);
            this.panelAccessCode.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelButtonBar;
        private System.Windows.Forms.Label lblSubsector;
        private System.Windows.Forms.ComboBox comboBoxSubsectorNames;
        private System.Windows.Forms.Button butLogoff;
        private System.Windows.Forms.Panel panelStatusBar;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblStatusTxt;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panelPollutionSiteEdit;
        private System.Windows.Forms.Panel panelPollutionSitesList;
        private System.Windows.Forms.Label lblPolSourceSiteList;
        private System.Windows.Forms.Panel panelPicture;
        private System.Windows.Forms.Panel panelMap;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblPolSourceSiteEdit;
        private System.Windows.Forms.Panel panelSubsectorPollutionSitesTop;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblPolSourceSitePictures;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblPolSourceSiteMap;
        private System.Windows.Forms.Panel panelPassword;
        private System.Windows.Forms.Panel panelPasswordCenter;
        private System.Windows.Forms.Panel panelAccessCode;
        private System.Windows.Forms.Label lblEnterAccessCodeTxt;
        private System.Windows.Forms.TextBox textBoxAccessCode;
        private System.Windows.Forms.Button butOpenProvinceFile;
        private System.Windows.Forms.Label lblProvinceFileName;
        private System.Windows.Forms.Button butPolSourceSiteEditCancel;
        private System.Windows.Forms.Button butShowMap;
        private System.Windows.Forms.Button butShowPictures;
        private System.Windows.Forms.OpenFileDialog openFileDialogCSSP;
        private System.Windows.Forms.Panel panelPSS;
    }
}

