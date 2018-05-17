using CSSPEnumsDLL.Enums;
using CSSPEnumsDLL.Services;
using CSSPPolSourceSiteInputToolHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CSSPPolSourceSiteInputTool
{
    public partial class CSSPPolSourceSiteInputToolForm : Form
    {
        #region Properties
        private CultureInfo currentCulture { get; set; }
        private CultureInfo currentUICulture { get; set; }
        private PolSourceSiteInputToolHelper polSourceSiteInputToolHelper { get; set; }
        #endregion Properties

        #region Constructors
        public CSSPPolSourceSiteInputToolForm()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-CA");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-CA");

            currentCulture = Thread.CurrentThread.CurrentCulture;
            currentUICulture = Thread.CurrentThread.CurrentUICulture;

            InitializeComponent();
            Setup();

            polSourceSiteInputToolHelper = new PolSourceSiteInputToolHelper(panelViewAndEdit, panelPolSourceSite, LanguageEnum.en);
            polSourceSiteInputToolHelper.UpdateStatus += polSourceSiteInputToolHelper_UpdateStatus;
            polSourceSiteInputToolHelper.subsectorDoc = new SubsectorDoc();
            RefreshComboBoxSubsectorNames();
        }
        #endregion Constructors

        #region Events
        private void button1_Click(object sender, EventArgs e)
        {
            polSourceSiteInputToolHelper.SaveSubsectorTextFile();
        }



        private void butEdit_Click(object sender, EventArgs e)
        {
            textBoxEmpty.Focus();
            panelShowButons.Visible = true;
            polSourceSiteInputToolHelper.IsEditing = true;
            polSourceSiteInputToolHelper.ShowPolSourceSite();
        }
        private void butIssues_Click(object sender, EventArgs e)
        {
            polSourceSiteInputToolHelper.ShowIssues();
        }
        private void butMap_Click(object sender, EventArgs e)
        {
            polSourceSiteInputToolHelper.ShowMap();
        }
        private void butPictures_Click(object sender, EventArgs e)
        {
            polSourceSiteInputToolHelper.ShowPictures();
        }
        private void butRegenerateKMLFile_Click(object sender, EventArgs e)
        {
            polSourceSiteInputToolHelper.RegenerateSubsectorKMLFile();
        }
        private void comboBoxSubsectorNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            RedrawPolSourceSiteList();
            panelViewAndEdit.Controls.Clear();
        }
        private void polSourceSiteInputToolHelper_UpdateStatus(object sender, PolSourceSiteInputToolHelper.StatusEventArgs e)
        {
            lblStatus.Text = e.Status;
            lblStatus.Refresh();
            Application.DoEvents();
        }
        private void showDetailsViaLabel(object sender, EventArgs e)
        {
            textBoxEmpty.Focus();
            panelShowButons.Visible = true;
            polSourceSiteInputToolHelper.PolSourceSiteTVItemID = int.Parse(((Label)sender).Tag.ToString());
            polSourceSiteInputToolHelper.IsEditing = false;
            polSourceSiteInputToolHelper.ShowPolSourceSite();
        }
        private void showDetailsViaPanel(object sender, EventArgs e)
        {
            textBoxEmpty.Focus();
            panelShowButons.Visible = true;
            polSourceSiteInputToolHelper.PolSourceSiteTVItemID = int.Parse(((Panel)sender).Tag.ToString());
            polSourceSiteInputToolHelper.IsEditing = false;
            polSourceSiteInputToolHelper.ShowPolSourceSite();
        }
        private void showEditViaLabel(object sender, EventArgs e)
        {
            textBoxEmpty.Focus();
            panelShowButons.Visible = true;
            polSourceSiteInputToolHelper.PolSourceSiteTVItemID = int.Parse(((Label)sender).Tag.ToString());
            polSourceSiteInputToolHelper.IsEditing = true;
            polSourceSiteInputToolHelper.ShowPolSourceSite();
        }
        private void showEditViaPanel(object sender, EventArgs e)
        {
            textBoxEmpty.Focus();
            panelShowButons.Visible = true;
            polSourceSiteInputToolHelper.PolSourceSiteTVItemID = int.Parse(((Panel)sender).Tag.ToString());
            polSourceSiteInputToolHelper.IsEditing = true;
            polSourceSiteInputToolHelper.ShowPolSourceSite();
        }
        private void splitContainer1_Resize(object sender, EventArgs e)
        {
            textBoxEmpty.Focus();
            panelShowButons.Visible = true;
            if (polSourceSiteInputToolHelper != null)
            {
                if (!polSourceSiteInputToolHelper.IsDirty && polSourceSiteInputToolHelper.PolSourceSiteTVItemID != 0)
                {
                    polSourceSiteInputToolHelper.ShowPolSourceSite();
                }
            }
        }
        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            textBoxEmpty.Focus();
            panelShowButons.Visible = true;
            if (polSourceSiteInputToolHelper != null)
            {
                if (!polSourceSiteInputToolHelper.IsDirty && polSourceSiteInputToolHelper.PolSourceSiteTVItemID != 0)
                {
                    polSourceSiteInputToolHelper.ShowPolSourceSite();
                }
            }
        }
        #endregion Events

        #region Functions private
        private void FillComboBoxSubsectorNames()
        {
            comboBoxSubsectorNames.Items.Clear();

            foreach (string subsector in polSourceSiteInputToolHelper.SubDirectoryList)
            {
                comboBoxSubsectorNames.Items.Add(subsector);
            }

            if (comboBoxSubsectorNames.Items.Count > 0)
            {
                comboBoxSubsectorNames.SelectedIndex = 0;
            }

            checkBoxLanguage.Focus();
        }
        public void RedrawPolSourceSiteList()
        {
            polSourceSiteInputToolHelper.CurrentSubsectorName = (string)comboBoxSubsectorNames.SelectedItem;

            polSourceSiteInputToolHelper.IsReading = true;
            if (!polSourceSiteInputToolHelper.ReadPollutionSourceSitesSubsectorFile())
            {
                return;
            }
            polSourceSiteInputToolHelper.IsReading = false;
            if (!polSourceSiteInputToolHelper.CheckAllReadDataOK())
            {
                return;
            }

            panelPolSourceSite.Controls.Clear();

            //lblSubsectorName.Text = $"{subsectorDoc.Subsector.SubsectorName}";

            int countPSS = 0;
            foreach (PSS pss in polSourceSiteInputToolHelper.subsectorDoc.Subsector.PSSList.OrderByDescending(c => c.SiteNumberText))
            {

                Panel tempPanel = new Panel();

                tempPanel.BorderStyle = BorderStyle.FixedSingle;
                tempPanel.Location = new Point(0, countPSS * 24);
                tempPanel.Size = new Size(panelPolSourceSite.Width, 24);
                tempPanel.TabIndex = 0;
                tempPanel.Tag = pss.PSSTVItemID;
                tempPanel.Dock = DockStyle.Top;
                tempPanel.Click += new System.EventHandler(showDetailsViaPanel);
                tempPanel.DoubleClick += new System.EventHandler(showEditViaPanel);

                Label tempLabel = new Label();

                tempLabel.AutoSize = true;
                tempLabel.Location = new Point(10, 4);
                tempLabel.TabIndex = 0;
                tempLabel.Tag = pss.PSSTVItemID;
                tempLabel.Text = $"{pss.SiteNumber}    {pss.TVText}";
                tempLabel.Click += new System.EventHandler(showDetailsViaLabel);
                tempLabel.DoubleClick += new System.EventHandler(showEditViaLabel);

                tempPanel.Controls.Add(tempLabel);

                panelPolSourceSite.Controls.Add(tempPanel);

                countPSS += 1;
            }
        }

        private void RefreshComboBoxSubsectorNames()
        {
            polSourceSiteInputToolHelper.SubDirectoryList = new List<string>();
            DirectoryInfo di = new DirectoryInfo(polSourceSiteInputToolHelper.BasePath);

            if (!di.Exists)
            {
                try
                {
                    di.Create();
                }
                catch (Exception ex)
                {
                    lblStatus.Text = ex.Message + (ex.InnerException != null ? " InnerException = " + ex.InnerException.Message : "");
                    return;
                }
            }

            List<DirectoryInfo> dirList = di.GetDirectories().ToList();

            foreach (DirectoryInfo directoryInfo in dirList.OrderBy(c => c.Name))
            {
                polSourceSiteInputToolHelper.SubDirectoryList.Add(directoryInfo.Name);
            }

            FillComboBoxSubsectorNames();

        }
        private void Setup()
        {
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.BringToFront();
            splitContainer1.SplitterDistance = 400;
            panelPolSourceSite.Dock = DockStyle.Fill;
            panelPolSourceSite.BringToFront();
            panelViewAndEdit.Dock = DockStyle.Fill;
            panelViewAndEdit.BringToFront();
            panelShowButons.Visible = false;
            textBoxEmpty.Width = 1;
            textBoxEmpty.Height = 1;
            comboBoxSubsectorNames.Focus();
            butMap.Enabled = false;
        }
        #endregion Functions private

    }
}
