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
            this.panelStatusTop = new System.Windows.Forms.Panel();
            this.checkBoxLanguage = new System.Windows.Forms.CheckBox();
            this.lblSubsectorName = new System.Windows.Forms.Label();
            this.lblSubsector = new System.Windows.Forms.Label();
            this.comboBoxSubsectorNames = new System.Windows.Forms.ComboBox();
            this.panelStatusBar = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblStatusTxt = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panelPolSourceSite = new System.Windows.Forms.Panel();
            this.panelShowButons = new System.Windows.Forms.Panel();
            this.butMap = new System.Windows.Forms.Button();
            this.butPictures = new System.Windows.Forms.Button();
            this.butEdit = new System.Windows.Forms.Button();
            this.panelViewAndEdit = new System.Windows.Forms.Panel();
            this.openFileDialogCSSP = new System.Windows.Forms.OpenFileDialog();
            this.textBoxEmpty = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panelButtonBar.SuspendLayout();
            this.panelStatusTop.SuspendLayout();
            this.panelStatusBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panelShowButons.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelButtonBar
            // 
            this.panelButtonBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelButtonBar.Controls.Add(this.button1);
            this.panelButtonBar.Controls.Add(this.textBoxEmpty);
            this.panelButtonBar.Controls.Add(this.panelStatusTop);
            this.panelButtonBar.Controls.Add(this.lblSubsectorName);
            this.panelButtonBar.Controls.Add(this.lblSubsector);
            this.panelButtonBar.Controls.Add(this.comboBoxSubsectorNames);
            this.panelButtonBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelButtonBar.Location = new System.Drawing.Point(0, 0);
            this.panelButtonBar.Name = "panelButtonBar";
            this.panelButtonBar.Size = new System.Drawing.Size(1140, 36);
            this.panelButtonBar.TabIndex = 9;
            // 
            // panelStatusTop
            // 
            this.panelStatusTop.Controls.Add(this.checkBoxLanguage);
            this.panelStatusTop.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelStatusTop.Location = new System.Drawing.Point(1048, 0);
            this.panelStatusTop.Name = "panelStatusTop";
            this.panelStatusTop.Size = new System.Drawing.Size(90, 34);
            this.panelStatusTop.TabIndex = 10;
            // 
            // checkBoxLanguage
            // 
            this.checkBoxLanguage.AutoSize = true;
            this.checkBoxLanguage.Location = new System.Drawing.Point(10, 10);
            this.checkBoxLanguage.Name = "checkBoxLanguage";
            this.checkBoxLanguage.Size = new System.Drawing.Size(66, 17);
            this.checkBoxLanguage.TabIndex = 3;
            this.checkBoxLanguage.Text = "Français";
            this.checkBoxLanguage.UseVisualStyleBackColor = true;
            // 
            // lblSubsectorName
            // 
            this.lblSubsectorName.AutoSize = true;
            this.lblSubsectorName.Location = new System.Drawing.Point(350, 10);
            this.lblSubsectorName.Name = "lblSubsectorName";
            this.lblSubsectorName.Size = new System.Drawing.Size(88, 13);
            this.lblSubsectorName.TabIndex = 3;
            this.lblSubsectorName.Text = "(subsector name)";
            // 
            // lblSubsector
            // 
            this.lblSubsector.AutoSize = true;
            this.lblSubsector.Location = new System.Drawing.Point(23, 10);
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
            this.comboBoxSubsectorNames.Size = new System.Drawing.Size(237, 21);
            this.comboBoxSubsectorNames.TabIndex = 2;
            this.comboBoxSubsectorNames.SelectedIndexChanged += new System.EventHandler(this.comboBoxSubsectorNames_SelectedIndexChanged);
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
            this.splitContainer1.Location = new System.Drawing.Point(30, 68);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panelPolSourceSite);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panelShowButons);
            this.splitContainer1.Panel2.Controls.Add(this.panelViewAndEdit);
            this.splitContainer1.Size = new System.Drawing.Size(1054, 535);
            this.splitContainer1.SplitterDistance = 351;
            this.splitContainer1.TabIndex = 11;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            this.splitContainer1.Resize += new System.EventHandler(this.splitContainer1_Resize);
            // 
            // panelPolSourceSite
            // 
            this.panelPolSourceSite.AutoScroll = true;
            this.panelPolSourceSite.Location = new System.Drawing.Point(15, 26);
            this.panelPolSourceSite.Name = "panelPolSourceSite";
            this.panelPolSourceSite.Size = new System.Drawing.Size(304, 347);
            this.panelPolSourceSite.TabIndex = 4;
            // 
            // panelShowButons
            // 
            this.panelShowButons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelShowButons.Controls.Add(this.butMap);
            this.panelShowButons.Controls.Add(this.butPictures);
            this.panelShowButons.Controls.Add(this.butEdit);
            this.panelShowButons.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelShowButons.Location = new System.Drawing.Point(0, 0);
            this.panelShowButons.Name = "panelShowButons";
            this.panelShowButons.Size = new System.Drawing.Size(695, 33);
            this.panelShowButons.TabIndex = 0;
            // 
            // butMap
            // 
            this.butMap.Location = new System.Drawing.Point(203, 3);
            this.butMap.Name = "butMap";
            this.butMap.Size = new System.Drawing.Size(59, 23);
            this.butMap.TabIndex = 0;
            this.butMap.Text = "Map";
            this.butMap.UseVisualStyleBackColor = true;
            this.butMap.Click += new System.EventHandler(this.butMap_Click);
            // 
            // butPictures
            // 
            this.butPictures.Location = new System.Drawing.Point(122, 3);
            this.butPictures.Name = "butPictures";
            this.butPictures.Size = new System.Drawing.Size(59, 23);
            this.butPictures.TabIndex = 0;
            this.butPictures.Text = "Pictures";
            this.butPictures.UseVisualStyleBackColor = true;
            this.butPictures.Click += new System.EventHandler(this.butPictures_Click);
            // 
            // butEdit
            // 
            this.butEdit.Location = new System.Drawing.Point(12, 3);
            this.butEdit.Name = "butEdit";
            this.butEdit.Size = new System.Drawing.Size(88, 23);
            this.butEdit.TabIndex = 0;
            this.butEdit.Text = "Edit";
            this.butEdit.UseVisualStyleBackColor = true;
            this.butEdit.Click += new System.EventHandler(this.butEdit_Click);
            // 
            // panelViewAndEdit
            // 
            this.panelViewAndEdit.AutoScroll = true;
            this.panelViewAndEdit.Location = new System.Drawing.Point(44, 60);
            this.panelViewAndEdit.Name = "panelViewAndEdit";
            this.panelViewAndEdit.Size = new System.Drawing.Size(462, 398);
            this.panelViewAndEdit.TabIndex = 1;
            // 
            // openFileDialogCSSP
            // 
            this.openFileDialogCSSP.FileName = "PollutionSourceSiteFromCSSPWebTools_*.txt";
            // 
            // textBoxEmpty
            // 
            this.textBoxEmpty.Location = new System.Drawing.Point(2, 6);
            this.textBoxEmpty.Name = "textBoxEmpty";
            this.textBoxEmpty.Size = new System.Drawing.Size(10, 20);
            this.textBoxEmpty.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(634, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Save in Change";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CSSPPolSourceSiteInputToolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1140, 723);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panelStatusBar);
            this.Controls.Add(this.panelButtonBar);
            this.Name = "CSSPPolSourceSiteInputToolForm";
            this.Text = "CSSP Pollution Source Site Input Tool";
            this.panelButtonBar.ResumeLayout(false);
            this.panelButtonBar.PerformLayout();
            this.panelStatusTop.ResumeLayout(false);
            this.panelStatusTop.PerformLayout();
            this.panelStatusBar.ResumeLayout(false);
            this.panelStatusBar.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panelShowButons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelButtonBar;
        private System.Windows.Forms.Label lblSubsector;
        private System.Windows.Forms.ComboBox comboBoxSubsectorNames;
        private System.Windows.Forms.Panel panelStatusBar;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblStatusTxt;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panelViewAndEdit;
        private System.Windows.Forms.OpenFileDialog openFileDialogCSSP;
        private System.Windows.Forms.Panel panelPolSourceSite;
        private System.Windows.Forms.Label lblSubsectorName;
        private System.Windows.Forms.Panel panelStatusTop;
        private System.Windows.Forms.Panel panelShowButons;
        private System.Windows.Forms.Button butEdit;
        private System.Windows.Forms.Button butMap;
        private System.Windows.Forms.Button butPictures;
        private System.Windows.Forms.CheckBox checkBoxLanguage;
        private System.Windows.Forms.TextBox textBoxEmpty;
        private System.Windows.Forms.Button button1;
    }
}

