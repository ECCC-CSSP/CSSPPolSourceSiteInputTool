using CSSPPolSourceSiteReadSubsectorFile;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSSPPolSourceSiteInputTool
{
    public partial class CSSPPolSourceSiteInputToolForm : Form
    {
        #region Variables
        private List<string> SubDirectoryList = new List<string>();
        #endregion Variables

        #region Properties
        private CultureInfo currentCulture { get; set; }
        private CultureInfo currentUICulture { get; set; }
        private List<Label> PSSLabelList { get; set; }
        private ReadSubsectorFile readSubsectorFile { get; set; }
        #endregion Properties

        #region Constructors
        public CSSPPolSourceSiteInputToolForm()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-CA");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-CA");

            currentCulture = Thread.CurrentThread.CurrentCulture;
            currentUICulture = Thread.CurrentThread.CurrentUICulture;

            readSubsectorFile = new ReadSubsectorFile();
            readSubsectorFile.UpdateStatus += ReadSubsectorFile_UpdateStatus;

            PSSLabelList = new List<Label>();
            readSubsectorFile.subsectorDoc = new SubsectorDoc();

            InitializeComponent();
            Setup();

            RefreshComboBoxSubsectorNames();
        }

        #endregion Constructors

        #region Events
        private void butRefresh_Click(object sender, EventArgs e)
        {
            RefreshComboBoxSubsectorNames();
        }
        private void comboBoxSubsectorNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            readSubsectorFile.CurrentSubsectorName = (string)comboBoxSubsectorNames.SelectedItem;

            readSubsectorFile.ReadPollutionSourceSitesSubsectorFile();

            panelPSS.Controls.Clear();

            lblSubsectorName.Text = $"{readSubsectorFile.subsectorDoc.Subsector.SubsectorName}";

            int countPSS = 0;
            foreach (PSS pss in readSubsectorFile.subsectorDoc.Subsector.PSSList)
            {
                countPSS += 1;

                Label tempLabel = new Label();

                tempLabel.AutoSize = true;
                tempLabel.Location = new Point(60, countPSS * 38);
                //tempLabel.Name = "label" + countPSS.ToString();
                tempLabel.Size = new Size(35, 13);
                tempLabel.TabIndex = 0;
                tempLabel.Text = $"Lat: {pss.Lat} --- Lng: {pss.Lng}";
                panelPSS.Controls.Add(tempLabel);
            }

        }

        private void ReadSubsectorFile_UpdateStatus(object sender, ReadSubsectorFile.StatusEventArgs e)
        {
            lblStatus.Text = e.Status;
        }

        #endregion Events

        #region Functions private
        private void FillComboBoxSubsectorNames()
        {
            comboBoxSubsectorNames.Items.Clear();

            foreach (string subsector in SubDirectoryList)
            {
                comboBoxSubsectorNames.Items.Add(subsector);
            }

            if (comboBoxSubsectorNames.Items.Count > 0)
            {
                comboBoxSubsectorNames.SelectedIndex = 0;
            }
        }
        private void RefreshComboBoxSubsectorNames()
        {
            SubDirectoryList = new List<string>();
            DirectoryInfo di = new DirectoryInfo(readSubsectorFile.BasePath);

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
                SubDirectoryList.Add(directoryInfo.Name);
            }

            FillComboBoxSubsectorNames();
        }
        private void Setup()
        {
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.BringToFront();
            splitContainer1.SplitterDistance = splitContainer1.Width - 2;
            panelPollutionSiteEdit.Dock = DockStyle.Fill;
            panelPollutionSitesList.Dock = DockStyle.Fill;
            panelPollutionSitesList.BringToFront();
            panelMap.Dock = DockStyle.Fill;
            panelPicture.Dock = DockStyle.Fill;
            panelMap.BringToFront();
        }
        #endregion Functions private

        #region Functions public
        #endregion Functions public

       }
}
