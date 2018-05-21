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
            RefreshComboBoxSubsectorNames();
        }
        #endregion Constructors

        #region Events
        private void butPSSAdd_Click(object sender, EventArgs e)
        {
            polSourceSiteInputToolHelper.PSSAdd();
            polSourceSiteInputToolHelper.SaveSubsectorTextFile();
            polSourceSiteInputToolHelper.RedrawPolSourceSiteList();
        }
        private void butRegenerateKMLFile_Click(object sender, EventArgs e)
        {
            textBoxEmpty.Focus();
            if (polSourceSiteInputToolHelper != null)
            {
                lblStatus.Text = "Regenerating Subsector KML File ...";
                polSourceSiteInputToolHelper.RegenerateSubsectorKMLFile();
                lblStatus.Text = "Subsector KML File was regenerated ...";
            }
        }
        private void butViewKMLFile_Click(object sender, EventArgs e)
        {
            textBoxEmpty.Focus();
            if (polSourceSiteInputToolHelper != null)
            {
                polSourceSiteInputToolHelper.ViewKMLFileInGoogleEarth();
            }
        }
        private void checkBoxEditing_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxEditing.Checked)
            {
                polSourceSiteInputToolHelper.IsEditing = true;
                if (polSourceSiteInputToolHelper.ShowOnlyIssues)
                {
                    checkBoxMoreInfo.Enabled = true;
                }
                else
                {
                    checkBoxMoreInfo.Enabled = false;
                }
            }
            else
            {
                polSourceSiteInputToolHelper.IsEditing = false;
                checkBoxMoreInfo.Enabled = false;
            }

            textBoxEmpty.Focus();

            polSourceSiteInputToolHelper.ReDraw();

        }
        private void checkBoxMoreInfo_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxMoreInfo.Checked)
            {
                polSourceSiteInputToolHelper.MoreInfo = true;
            }
            else
            {
                polSourceSiteInputToolHelper.MoreInfo = false;
            }

            textBoxEmpty.Focus();

            polSourceSiteInputToolHelper.ReDraw();

        }
        private void comboBoxSubsectorNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            polSourceSiteInputToolHelper.CurrentSubsectorName = (string)comboBoxSubsectorNames.SelectedItem;
            polSourceSiteInputToolHelper.CurrentPSS = null;
            polSourceSiteInputToolHelper.PolSourceSiteTVItemID = 0;
            polSourceSiteInputToolHelper.RedrawPolSourceSiteList();
            panelViewAndEdit.Controls.Clear();
            polSourceSiteInputToolHelper.CurrentPSS = null;
            polSourceSiteInputToolHelper.ReDraw();

            lblSubsectorName.Text = $"{polSourceSiteInputToolHelper.subsectorDoc.Subsector.SubsectorName}";
        }
        private void polSourceSiteInputToolHelper_UpdateStatus(object sender, PolSourceSiteInputToolHelper.StatusEventArgs e)
        {
            lblStatus.Text = e.Status;
            lblStatus.Refresh();
            Application.DoEvents();
        }
        private void radioButtonDetails_CheckedChanged(object sender, EventArgs e)
        {
            polSourceSiteInputToolHelper.ShowPolSourceSiteDetails = true;
            polSourceSiteInputToolHelper.ShowOnlyIssues = false;
            polSourceSiteInputToolHelper.ShowOnlyPictures = false;
            polSourceSiteInputToolHelper.ShowOnlyMap = false;

            polSourceSiteInputToolHelper.ReDraw();
        }

        private void radioButtonOnlyIssues_CheckedChanged(object sender, EventArgs e)
        {
            polSourceSiteInputToolHelper.ShowPolSourceSiteDetails = false;
            polSourceSiteInputToolHelper.ShowOnlyIssues = true;
            polSourceSiteInputToolHelper.ShowOnlyPictures = false;
            polSourceSiteInputToolHelper.ShowOnlyMap = false;

            if (polSourceSiteInputToolHelper.IsEditing)
            {
                checkBoxMoreInfo.Enabled = true;
            }
            else
            {
                checkBoxMoreInfo.Enabled = false;
            }


            polSourceSiteInputToolHelper.ReDraw();
        }

        private void radioButtonShowMap_CheckedChanged(object sender, EventArgs e)
        {
            polSourceSiteInputToolHelper.ShowPolSourceSiteDetails = false;
            polSourceSiteInputToolHelper.ShowOnlyIssues = false;
            polSourceSiteInputToolHelper.ShowOnlyPictures = false;
            polSourceSiteInputToolHelper.ShowOnlyMap = true;

            polSourceSiteInputToolHelper.ReDraw();
        }
        private void radioButtonOnlyPictures_CheckedChanged(object sender, EventArgs e)
        {
            polSourceSiteInputToolHelper.ShowPolSourceSiteDetails = false;
            polSourceSiteInputToolHelper.ShowOnlyIssues = false;
            polSourceSiteInputToolHelper.ShowOnlyPictures = true;
            polSourceSiteInputToolHelper.ShowOnlyMap = false;

            polSourceSiteInputToolHelper.ReDraw();
        }
        private void splitContainer1_SizeChanged(object sender, EventArgs e)
        {
            textBoxEmpty.Focus();
            if (polSourceSiteInputToolHelper != null)
            {
                polSourceSiteInputToolHelper.DrawPanelPSS();
                polSourceSiteInputToolHelper.ReDraw();
            }
        }
        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            textBoxEmpty.Focus();
            if (polSourceSiteInputToolHelper != null)
            {
                polSourceSiteInputToolHelper.DrawPanelPSS();
                polSourceSiteInputToolHelper.ReDraw();
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
            textBoxEmpty.Width = 1;
            textBoxEmpty.Height = 1;
            comboBoxSubsectorNames.Focus();
        }
        #endregion Functions private

    }
}
