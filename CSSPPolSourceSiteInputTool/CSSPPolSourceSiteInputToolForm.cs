using CSSPEnumsDLL.Enums;
using CSSPEnumsDLL.Services;
using CSSPPolSourceSiteReadSubsectorFile;
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
        #region Variables
        private List<string> SubDirectoryList = new List<string>();
        private int PolSourceSiteTVItemID = 0;
        private PSS CurrentPSS = null;
        private bool IsEditing = false;
        private bool IsDirty = false;
        #endregion Variables

        #region Properties
        private CultureInfo currentCulture { get; set; }
        private CultureInfo currentUICulture { get; set; }
        private List<Label> PSSLabelList { get; set; }
        private ReadSubsectorFile readSubsectorFile { get; set; }
        private BaseEnumService _BaseEnumService { get; set; }

        #endregion Properties

        #region Constructors
        public CSSPPolSourceSiteInputToolForm()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-CA");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-CA");

            currentCulture = Thread.CurrentThread.CurrentCulture;
            currentUICulture = Thread.CurrentThread.CurrentUICulture;

            readSubsectorFile = new ReadSubsectorFile();
            readSubsectorFile.UpdateStatus += readSubsectorFile_UpdateStatus;

            PSSLabelList = new List<Label>();
            readSubsectorFile.subsectorDoc = new SubsectorDoc();

            InitializeComponent();
            Setup();

            RefreshComboBoxSubsectorNames();
        }

        #endregion Constructors

        #region Events
        private void butAddPicture_Click(object sender, EventArgs e)
        {
            AddPicture();
        }
        private void butEdit_Click(object sender, EventArgs e)
        {
            ShowEditPolSourceSite();
        }
        private void butMap_Click(object sender, EventArgs e)
        {
            ShowMap();
        }
        private void butPictures_Click(object sender, EventArgs e)
        {
            ShowPictures();
        }
        private void butRemovePicture_Click(object sender, EventArgs e)
        {
            int PictureTVItemID = int.Parse(((Button)sender).Tag.ToString());
            RemovePicture(PictureTVItemID);
        }
        private void butSavePictureFileName_Click(object sender, EventArgs e)
        {
            SavePictureInfo();
        }
        private void comboBoxSubsectorNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            RedrawPolSourceSiteList();
            panelViewAndEdit.Controls.Clear();
        }
        private void readSubsectorFile_UpdateStatus(object sender, ReadSubsectorFile.StatusEventArgs e)
        {
            lblStatus.Text = e.Status;
        }
        private void showDetailsViaLabel(object sender, EventArgs e)
        {
            PolSourceSiteTVItemID = int.Parse(((Label)sender).Tag.ToString());
            ViewPolSourceSiteDetail();
        }
        private void showDetailsViaPanel(object sender, EventArgs e)
        {
            PolSourceSiteTVItemID = int.Parse(((Panel)sender).Tag.ToString());
            ViewPolSourceSiteDetail();
        }
        private void showEditViaLabel(object sender, EventArgs e)
        {
            PolSourceSiteTVItemID = int.Parse(((Label)sender).Tag.ToString());
            ShowEditPolSourceSite();
        }
        private void showEditViaPanel(object sender, EventArgs e)
        {
            PolSourceSiteTVItemID = int.Parse(((Panel)sender).Tag.ToString());
            ShowEditPolSourceSite();
        }
        private void splitContainer1_Resize(object sender, EventArgs e)
        {
            if (!IsDirty && PolSourceSiteTVItemID != 0)
            {
                if (IsEditing)
                {
                    ShowEditPolSourceSite();
                }
                else
                {
                    ViewPolSourceSiteDetail();
                }
            }
        }
        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (!IsDirty && PolSourceSiteTVItemID != 0)
            {
                if (IsEditing)
                {
                    ShowEditPolSourceSite();
                }
                else
                {
                    ViewPolSourceSiteDetail();
                }
            }
        }

        #endregion Events

        #region Functions private
        private void AddPicture()
        {
            OpenFileDialog openFileDialogPictures = new OpenFileDialog();
            openFileDialogPictures.InitialDirectory = $@"C:\PollutionSourceSites\{readSubsectorFile.CurrentSubsectorName}\Pictures\";
            openFileDialogPictures.Title = "Browse Pictures Files";
            openFileDialogPictures.DefaultExt = @"jpg";
            openFileDialogPictures.Filter = @"jpg files (*.jpg)|*.jpg";
            if (openFileDialogPictures.ShowDialog() == DialogResult.OK)
            {
                PSS pss = readSubsectorFile.subsectorDoc.Subsector.PSSList.Where(c => c.PSSTVItemID == PolSourceSiteTVItemID).FirstOrDefault();

                Picture pictureNew = new Picture();

                pictureNew.PictureTVItemID = 10000000;
                pictureNew.FileName = "Change File Name " + pictureNew.PictureTVItemID.ToString();
                pss.PSSPictureList.Add(pictureNew);

                FileInfo fi = new FileInfo(openFileDialogPictures.FileName);

                FileInfo fiCheck = new FileInfo($@"C:\PollutionSourceSites\{readSubsectorFile.CurrentSubsectorName}\Pictures\{pss.SiteNumberText}_{pictureNew.PictureTVItemID}{fi.Extension}");
                pictureNew.Extension = fi.Extension;
                pictureNew.Description = "Insert Required Description";
                while (fiCheck.Exists)
                {
                    pictureNew.PictureTVItemID += 1;
                    pictureNew.FileName = "Change File Name " + pictureNew.PictureTVItemID.ToString();
                    fiCheck = new FileInfo($@"C:\PollutionSourceSites\{readSubsectorFile.CurrentSubsectorName}\Pictures\{pss.SiteNumberText}_{pictureNew.PictureTVItemID}{fi.Extension}");
                }

                File.Copy(openFileDialogPictures.FileName, $@"C:\PollutionSourceSites\{readSubsectorFile.CurrentSubsectorName}\Pictures\{pss.SiteNumberText}_{pictureNew.PictureTVItemID}{fi.Extension}");

                ShowPictures();
            }
        }
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

            checkBoxLanguage.Focus();
        }
        private void RedrawPolSourceSiteList()
        {
            readSubsectorFile.CurrentSubsectorName = (string)comboBoxSubsectorNames.SelectedItem;

            readSubsectorFile.ReadPollutionSourceSitesSubsectorFile();

            panelPolSourceSite.Controls.Clear();

            lblSubsectorName.Text = $"{readSubsectorFile.subsectorDoc.Subsector.SubsectorName}";

            int countPSS = 0;
            foreach (PSS pss in readSubsectorFile.subsectorDoc.Subsector.PSSList.OrderBy(c => c.SiteNumberText))
            {

                Panel tempPanel = new Panel();

                tempPanel.BorderStyle = BorderStyle.FixedSingle;
                tempPanel.Location = new Point(0, countPSS * 24);
                tempPanel.Size = new Size(panelPolSourceSite.Width, 24);
                tempPanel.TabIndex = 0;
                tempPanel.Tag = pss.PSSTVItemID;
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
        private void RemovePicture(int PictureTVItemID)
        {
            PSS pss = readSubsectorFile.subsectorDoc.Subsector.PSSList.Where(c => c.PSSTVItemID == PolSourceSiteTVItemID).FirstOrDefault();

            Picture pictureToRemove = pss.PSSPictureList.Where(c => c.PictureTVItemID == PictureTVItemID).FirstOrDefault();
            if (pictureToRemove != null)
            {
                pss.PSSPictureList.Remove(pictureToRemove);
            }

            //FileInfo fi = new FileInfo($@"C:\PollutionSourceSites\{readSubsectorFile.CurrentSubsectorName}\Pictures\{pss.SiteNumberText}_{pictureToRemove.PictureTVItemID}{pictureToRemove.Extension}");

            //if (fi.Exists)
            //{
            //    try
            //    {
            //        fi.Delete();
            //    }
            //    catch (Exception)
            //    {
            //        lblStatus.Text = "Could not delete file [" + fi.FullName + "]";
            //    }
            //}

            ShowPictures();
        }
        private void SavePictureInfo()
        {
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
        private void ShowEditPolSourceSite()
        {
            IsEditing = true;

            textBoxEmpty.Focus();

            panelShowButons.Visible = true;

            CurrentPSS = readSubsectorFile.subsectorDoc.Subsector.PSSList.Where(c => c.PSSTVItemID == PolSourceSiteTVItemID).FirstOrDefault();

            foreach (var a in panelPolSourceSite.Controls)
            {
                if (a.GetType().Name == "Panel")
                {
                    ((Panel)a).BackColor = Color.White;

                    if (((Panel)a).Tag.ToString() == PolSourceSiteTVItemID.ToString())
                    {
                        ((Panel)a).BackColor = Color.LightGreen;
                    }
                }
            }

            panelViewAndEdit.Controls.Clear();

            if (checkBoxLanguage.Checked)
            {
                _BaseEnumService = new BaseEnumService(LanguageEnum.fr);
            }
            else
            {
                _BaseEnumService = new BaseEnumService(LanguageEnum.en);
            }

            panelViewAndEdit.Controls.Clear();

            PSS pss = readSubsectorFile.subsectorDoc.Subsector.PSSList.Where(c => c.PSSTVItemID == PolSourceSiteTVItemID).FirstOrDefault();


            int pos = 0;
            if (pss != null)
            {
                Label lblTVText = new Label();
                lblTVText.AutoSize = true;
                lblTVText.Location = new Point(10, 4);
                lblTVText.MaximumSize = new Size(panelViewAndEdit.Width * 9 / 10, 0);
                lblTVText.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 14f, FontStyle.Bold);
                lblTVText.Text = $"{pss.SiteNumber} {pss.TVText}";

                panelViewAndEdit.Controls.Add(lblTVText);

                pos = lblTVText.Bottom + 20;

                string ActiveText = pss.IsActive == true ? "Active" : "Inactive";
                string PointSourceText = pss.IsPointSource == true ? "Point Source" : "Non Point Source";

                Label lblLat = new Label();
                lblLat.AutoSize = true;
                lblLat.Location = new Point(20, pos);
                lblLat.Font = new Font(new FontFamily(lblLat.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                lblLat.Text = $@"Lat: ";

                panelViewAndEdit.Controls.Add(lblLat);

                TextBox textBoxLat = new TextBox();
                textBoxLat.Location = new Point(lblLat.Right + 5, pos);
                textBoxLat.Font = new Font(new FontFamily(lblLat.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                textBoxLat.Width = 100;
                textBoxLat.Text = pss.Lat.ToString("F5");

                panelViewAndEdit.Controls.Add(textBoxLat);

                Label lblLng = new Label();
                lblLng.AutoSize = true;
                lblLng.Location = new Point(textBoxLat.Right + 5, pos);
                lblLng.Font = new Font(new FontFamily(lblLng.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                lblLng.Text = $@"Lng: ";

                panelViewAndEdit.Controls.Add(lblLng);

                TextBox textBoxLng = new TextBox();
                textBoxLng.Location = new Point(lblLng.Right + 5, pos);
                textBoxLng.Font = new Font(new FontFamily(lblLng.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                textBoxLng.Width = 100;
                textBoxLng.Text = pss.Lng.ToString("F5");

                panelViewAndEdit.Controls.Add(textBoxLng);

                CheckBox checkBoxIsActive = new CheckBox();
                checkBoxIsActive.Location = new Point(textBoxLng.Right + 25, pos);
                checkBoxIsActive.Font = new Font(new FontFamily(lblLng.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                checkBoxIsActive.Width = 100;
                checkBoxIsActive.Text = "Is Active";
                checkBoxIsActive.Checked = pss.IsActive;

                panelViewAndEdit.Controls.Add(checkBoxIsActive);

                CheckBox checkBoxIsPointSource = new CheckBox();
                checkBoxIsPointSource.Location = new Point(checkBoxIsActive.Right + 5, pos);
                checkBoxIsPointSource.Font = new Font(new FontFamily(lblLng.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                checkBoxIsPointSource.Width = 200;
                checkBoxIsPointSource.Text = "Is Point Source";
                checkBoxIsPointSource.Checked = pss.IsPointSource;

                panelViewAndEdit.Controls.Add(checkBoxIsPointSource);

                pos = lblLat.Bottom + 20;

                if (pss.PSSAddress != null)
                {

                    //Label lblAddress = new Label();
                    //lblAddress.Location = new Point(10, pos);
                    //lblAddress.Font = new Font(new FontFamily(lblAddress.Font.FontFamily.Name).Name, 14f, FontStyle.Bold);
                    //lblAddress.Text = $@"Address";

                    //panelViewAndEdit.Controls.Add(lblAddress);

                    //pos = lblAddress.Bottom + 10;

                    Label lblStreetNumber = new Label();
                    lblStreetNumber.AutoSize = true;
                    lblStreetNumber.Location = new Point(20, pos);
                    lblStreetNumber.Font = new Font(new FontFamily(lblStreetNumber.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                    lblStreetNumber.Text = $@"Street Number";

                    panelViewAndEdit.Controls.Add(lblStreetNumber);

                    TextBox textBoxStreetNumber = new TextBox();
                    textBoxStreetNumber.Location = new Point(lblStreetNumber.Left, lblStreetNumber.Bottom + 4);
                    textBoxStreetNumber.Font = new Font(new FontFamily(lblStreetNumber.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    textBoxStreetNumber.Width = lblStreetNumber.Width;
                    textBoxStreetNumber.Text = pss.PSSAddress.StreetNumber;

                    panelViewAndEdit.Controls.Add(textBoxStreetNumber);

                    Label lblStreetName = new Label();
                    lblStreetName.AutoSize = true;
                    lblStreetName.Location = new Point(lblStreetNumber.Right + 10, pos);
                    lblStreetName.MaximumSize = new Size(panelViewAndEdit.Width * 9 / 10, 0);
                    lblStreetName.Font = new Font(new FontFamily(lblStreetName.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                    lblStreetName.Text = $@"Street Name           ";

                    panelViewAndEdit.Controls.Add(lblStreetName);

                    TextBox textBoxStreetName = new TextBox();
                    textBoxStreetName.Location = new Point(lblStreetName.Left, lblStreetName.Bottom + 4);
                    textBoxStreetName.Font = new Font(new FontFamily(lblStreetName.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    textBoxStreetName.Width = lblStreetName.Width;
                    textBoxStreetName.Text = pss.PSSAddress.StreetName;

                    panelViewAndEdit.Controls.Add(textBoxStreetName);

                    Label lblStreetType = new Label();
                    lblStreetType.AutoSize = true;
                    lblStreetType.Location = new Point(lblStreetName.Right + 10, pos);
                    lblStreetType.MaximumSize = new Size(panelViewAndEdit.Width * 9 / 10, 0);
                    lblStreetType.Font = new Font(new FontFamily(lblStreetType.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                    lblStreetType.Text = $@"Street Type   ";

                    panelViewAndEdit.Controls.Add(lblStreetType);

                    ComboBox comboBoxStreetType = new ComboBox();
                    comboBoxStreetType.Location = new Point(lblStreetType.Left, lblStreetType.Bottom + 4);
                    comboBoxStreetType.Font = new Font(new FontFamily(lblStreetType.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    comboBoxStreetType.Width = lblStreetType.Width;

                    panelViewAndEdit.Controls.Add(comboBoxStreetType);

                    for (int i = 1, count = Enum.GetNames(typeof(StreetTypeEnum)).Count(); i < count; i++)
                    {
                        comboBoxStreetType.Items.Add(((StreetTypeEnum)i).ToString());
                    }

                    if (pss.PSSAddress.StreetType > 0)
                    {
                        comboBoxStreetType.SelectedText = ((StreetTypeEnum)pss.PSSAddress.StreetType).ToString();
                    }

                    Label lblMunicipality = new Label();
                    lblMunicipality.AutoSize = true;
                    lblMunicipality.Location = new Point(lblStreetType.Right + 10, pos);
                    lblMunicipality.MaximumSize = new Size(panelViewAndEdit.Width * 9 / 10, 0);
                    lblMunicipality.Font = new Font(new FontFamily(lblMunicipality.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                    lblMunicipality.Text = $@"Municipality          ";

                    panelViewAndEdit.Controls.Add(lblMunicipality);

                    TextBox textBoxMunicipality = new TextBox();
                    textBoxMunicipality.Location = new Point(lblMunicipality.Left, lblMunicipality.Bottom + 4);
                    textBoxMunicipality.Font = new Font(new FontFamily(lblMunicipality.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    textBoxMunicipality.Width = lblMunicipality.Width;
                    textBoxMunicipality.Text = pss.PSSAddress.Municipality;

                    panelViewAndEdit.Controls.Add(textBoxMunicipality);

                    Label lblPostalCode = new Label();
                    lblPostalCode.AutoSize = true;
                    lblPostalCode.Location = new Point(lblMunicipality.Right + 10, lblMunicipality.Top);
                    lblPostalCode.MaximumSize = new Size(panelViewAndEdit.Width * 9 / 10, 0);
                    lblPostalCode.Font = new Font(new FontFamily(lblPostalCode.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                    lblPostalCode.Text = $@"Postal Code    ";

                    panelViewAndEdit.Controls.Add(lblPostalCode);

                    TextBox textBoxPostalCode = new TextBox();
                    textBoxPostalCode.Location = new Point(lblPostalCode.Left, lblPostalCode.Bottom + 4);
                    textBoxPostalCode.Font = new Font(new FontFamily(lblPostalCode.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    textBoxPostalCode.Width = lblPostalCode.Width;
                    textBoxPostalCode.Text = pss.PSSAddress.PostalCode;

                    panelViewAndEdit.Controls.Add(textBoxPostalCode);

                    pos = textBoxStreetNumber.Bottom + 20;

                }


                if (pss.PSSObsList.Count > 0)
                {
                    foreach (Obs obs in pss.PSSObsList)
                    {
                        Label lblLastObs = new Label();
                        lblLastObs.AutoSize = true;
                        lblLastObs.Location = new Point(10, pos);
                        lblLastObs.MaximumSize = new Size(panelViewAndEdit.Width * 9 / 10, 0);
                        lblLastObs.Font = new Font(new FontFamily(lblLastObs.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                        lblLastObs.Text = $"Observation Date: ";

                        panelViewAndEdit.Controls.Add(lblLastObs);

                        DateTimePicker dateTimePickerObsDate = new DateTimePicker();
                        dateTimePickerObsDate.Location = new Point(lblLastObs.Right + 10, lblLastObs.Top);
                        dateTimePickerObsDate.Format = DateTimePickerFormat.Custom;
                        dateTimePickerObsDate.CustomFormat = "yyyy MMMM dd";
                        dateTimePickerObsDate.Value = obs.ObsDate;

                        panelViewAndEdit.Controls.Add(dateTimePickerObsDate);

                        pos = lblLastObs.Bottom + 20;


                        Label lblWrittenDesc = new Label();
                        lblWrittenDesc.AutoSize = true;
                        lblWrittenDesc.Location = new Point(10, pos);
                        lblWrittenDesc.MaximumSize = new Size(panelViewAndEdit.Width * 9 / 10, 0);
                        lblWrittenDesc.Font = new Font(new FontFamily(lblWrittenDesc.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                        lblWrittenDesc.Text = $"Written Description: ";

                        panelViewAndEdit.Controls.Add(lblWrittenDesc);

                        pos = lblWrittenDesc.Bottom + 4;

                        Label lblWrittenDesc2 = new Label();
                        lblWrittenDesc2.AutoSize = true;
                        lblWrittenDesc2.Location = new Point(20, pos);
                        lblWrittenDesc2.MaximumSize = new Size(panelViewAndEdit.Width * 9 / 10, 0);
                        lblWrittenDesc2.Font = new Font(new FontFamily(lblWrittenDesc2.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        lblWrittenDesc2.Text = $"{obs.Description}";

                        panelViewAndEdit.Controls.Add(lblWrittenDesc2);

                        pos = lblWrittenDesc2.Bottom + 20;

                        if (pss.OldIssueTextList.Count > 0)
                        {
                            Label lblOldIssue = new Label();
                            lblOldIssue.AutoSize = true;
                            lblOldIssue.Location = new Point(10, pos);
                            lblOldIssue.MaximumSize = new Size(panelViewAndEdit.Width * 9 / 10, 0);
                            lblOldIssue.Font = new Font(new FontFamily(lblOldIssue.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                            lblOldIssue.Text = $"Old Issue Text:";

                            panelViewAndEdit.Controls.Add(lblOldIssue);

                            pos = lblOldIssue.Bottom + 6;

                            int OldIssueCount = 0;
                            foreach (string OldIssueText in pss.OldIssueTextList)
                            {
                                OldIssueCount += 1;

                                Label lblOldIssueCount = new Label();
                                lblOldIssueCount.AutoSize = true;
                                lblOldIssueCount.Location = new Point(10, pos);
                                lblOldIssueCount.MaximumSize = new Size(panelViewAndEdit.Width * 9 / 10, 0);
                                lblOldIssueCount.Font = new Font(new FontFamily(lblOldIssueCount.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                                lblOldIssueCount.Text = $"{OldIssueCount}) - ";

                                panelViewAndEdit.Controls.Add(lblOldIssueCount);

                                Label lblOldIssueText = new Label();
                                lblOldIssueText.AutoSize = true;
                                lblOldIssueText.Location = new Point(lblOldIssueCount.Right, pos);
                                lblOldIssueText.MaximumSize = new Size(panelViewAndEdit.Width * 9 / 10, 0);
                                lblOldIssueText.Font = new Font(new FontFamily(lblOldIssueText.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                                lblOldIssueText.Text = $"{OldIssueText}";

                                panelViewAndEdit.Controls.Add(lblOldIssueText);

                                pos = lblOldIssueText.Bottom + 10;
                            }
                        }
                        else
                        {
                            Label lblOldIssue = new Label();
                            lblOldIssue.AutoSize = true;
                            lblOldIssue.Location = new Point(10, pos);
                            lblOldIssue.MaximumSize = new Size(panelViewAndEdit.Width * 9 / 10, 0);
                            lblOldIssue.Font = new Font(new FontFamily(lblOldIssue.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                            lblOldIssue.Text = $"No Old Issue Text:";

                            panelViewAndEdit.Controls.Add(lblOldIssue);

                            pos = lblOldIssue.Bottom + 10;
                        }

                        if (obs.IssueList.Count > 0)
                        {
                            int IssueCount = 0;
                            foreach (Issue issue in obs.IssueList)
                            {
                                IssueCount += 1;

                                Label lblIssue = new Label();
                                lblIssue.AutoSize = true;
                                lblIssue.Location = new Point(10, pos);
                                lblIssue.MaximumSize = new Size(panelViewAndEdit.Width * 9 / 10, 0);
                                lblIssue.Font = new Font(new FontFamily(lblIssue.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                                lblIssue.Text = $"Issue ({IssueCount}) --- Last Update (UTC): ";

                                panelViewAndEdit.Controls.Add(lblIssue);

                                Label lblIssueLastUpdate = new Label();
                                lblIssueLastUpdate.AutoSize = true;
                                lblIssueLastUpdate.Location = new Point(lblIssue.Right, lblIssue.Top);
                                lblIssueLastUpdate.MaximumSize = new Size(panelViewAndEdit.Width * 9 / 10, 0);
                                lblIssueLastUpdate.Font = new Font(new FontFamily(lblIssueLastUpdate.Font.FontFamily.Name).Name, 12f, FontStyle.Regular);
                                lblIssueLastUpdate.Text = $"{issue.LastUpdated_UTC.ToString("yyyy MMMM dd HH:mm:ss")}";

                                panelViewAndEdit.Controls.Add(lblIssueLastUpdate);

                                pos = lblIssue.Bottom + 4;

                                string TVText = "";
                                for (int i = 0, count = issue.PolSourceObsInfoIntList.Count; i < count; i++)
                                {
                                    string Temp = _BaseEnumService.GetEnumText_PolSourceObsInfoReportEnum((PolSourceObsInfoEnum)issue.PolSourceObsInfoIntList[i]);
                                    switch ((issue.PolSourceObsInfoIntList[i].ToString()).Substring(0, 3))
                                    {
                                        case "101":
                                            {
                                                Temp = Temp.Replace("Source", "     Source");
                                            }
                                            break;
                                        case "250":
                                            {
                                                Temp = Temp.Replace("Pathway", "\r\n     Pathway");
                                            }
                                            break;
                                        case "900":
                                            {
                                                Temp = Temp.Replace("Status", "\r\n     Status");
                                            }
                                            break;
                                        case "910":
                                            {
                                                Temp = Temp.Replace("Risk", "\r\n     Risk");
                                            }
                                            break;
                                        case "110":
                                        case "120":
                                        case "122":
                                        case "151":
                                        case "152":
                                        case "153":
                                        case "155":
                                        case "156":
                                        case "157":
                                        case "163":
                                        case "166":
                                        case "167":
                                        case "170":
                                        case "171":
                                        case "172":
                                        case "173":
                                        case "176":
                                        case "178":
                                        case "181":
                                        case "182":
                                        case "183":
                                        case "185":
                                        case "186":
                                        case "187":
                                        case "190":
                                        case "191":
                                        case "192":
                                        case "193":
                                        case "194":
                                        case "196":
                                        case "198":
                                        case "199":
                                        case "220":
                                        case "930":
                                            {
                                                //Temp = Temp;
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                    TVText = TVText + Temp;
                                }

                                Label lblSelected = new Label();
                                lblSelected.AutoSize = true;
                                lblSelected.Location = new Point(20, pos);
                                lblSelected.MaximumSize = new Size(panelViewAndEdit.Width * 9 / 10, 0);
                                lblSelected.Font = new Font(new FontFamily(lblSelected.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                                lblSelected.Text = $"{TVText}";

                                panelViewAndEdit.Controls.Add(lblSelected);

                                pos = lblSelected.Bottom + 10;
                            }
                        }

                    }
                }
            }
        }
        private void ShowMap()
        {
            panelViewAndEdit.Controls.Clear();

            Label tempLabel = new Label();

            tempLabel.AutoSize = true;
            tempLabel.Location = new Point(10, 4);
            tempLabel.TabIndex = 0;
            tempLabel.Text = $"ShowMap is working " + PolSourceSiteTVItemID.ToString();

            panelViewAndEdit.Controls.Add(tempLabel);
        }
        private void ShowPictures()
        {
            panelViewAndEdit.Controls.Clear();

            PSS pss = readSubsectorFile.subsectorDoc.Subsector.PSSList.Where(c => c.PSSTVItemID == PolSourceSiteTVItemID).FirstOrDefault();

            int pos = 4;

            Label lblPictures = new Label();
            lblPictures.AutoSize = true;
            lblPictures.Location = new Point(10, pos);
            lblPictures.MaximumSize = new Size(panelViewAndEdit.Width * 9 / 10, 0);
            lblPictures.Font = new Font(new FontFamily(lblPictures.Font.FontFamily.Name).Name, 14f, FontStyle.Bold);
            lblPictures.Text = $"Pictures";

            panelViewAndEdit.Controls.Add(lblPictures);

            pos = lblPictures.Bottom + 10;

            if (pss.PSSPictureList.Count > 0)
            {
                foreach (Picture picture in pss.PSSPictureList)
                {
                    PictureBox tempPictureBox = new PictureBox();

                    tempPictureBox.BorderStyle = BorderStyle.FixedSingle;
                    tempPictureBox.ImageLocation = $@"C:\PollutionSourceSites\{readSubsectorFile.CurrentSubsectorName}\Pictures\{pss.SiteNumberText}_{picture.PictureTVItemID}{picture.Extension}";
                    tempPictureBox.Location = new Point(10, pos);
                    tempPictureBox.Size = new Size(600, 500);
                    tempPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    tempPictureBox.TabIndex = 0;
                    tempPictureBox.TabStop = false;

                    panelViewAndEdit.Controls.Add(tempPictureBox);

                    pos = tempPictureBox.Bottom + 10;

                    if (IsEditing)
                    {
                        Label lblFileName = new Label();
                        lblFileName.AutoSize = true;
                        lblFileName.Location = new Point(20, pos);
                        lblFileName.Tag = $"{pss.SiteNumberText}_{picture.PictureTVItemID}{picture.Extension}";
                        lblFileName.Font = new Font(new FontFamily(lblFileName.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                        lblFileName.Text = $@"Server File Name: ";

                        panelViewAndEdit.Controls.Add(lblFileName);

                        TextBox textFileName = new TextBox();
                        textFileName.Location = new Point(lblFileName.Right + 5, pos);
                        textFileName.Font = new Font(new FontFamily(lblFileName.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        textFileName.Width = 300;
                        textFileName.Text = picture.FileName;

                        panelViewAndEdit.Controls.Add(textFileName);

                        pos = textFileName.Bottom + 10;

                        Label lblDescription = new Label();
                        lblDescription.AutoSize = true;
                        lblDescription.Location = new Point(20, pos);
                        lblDescription.Tag = $"{pss.SiteNumberText}_{picture.PictureTVItemID}{picture.Extension}";
                        lblDescription.Font = new Font(new FontFamily(lblDescription.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                        lblDescription.Text = $@"Description: ";

                        panelViewAndEdit.Controls.Add(lblDescription);

                        TextBox textDescription = new TextBox();
                        textDescription.Location = new Point(lblDescription.Right + 5, pos);
                        textDescription.Font = new Font(new FontFamily(lblDescription.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        textDescription.Width = 300;
                        textDescription.Height = 100;
                        textDescription.Multiline = true;
                        textDescription.Text = picture.Description;

                        panelViewAndEdit.Controls.Add(textDescription);

                        pos = textDescription.Bottom + 10;

                        Button butSavePictureFileName = new Button();
                        butSavePictureFileName.Location = new Point(120, pos);
                        butSavePictureFileName.Text = "Save File Name";
                        butSavePictureFileName.Tag = $"{picture.PictureTVItemID}";
                        butSavePictureFileName.Click += butSavePictureFileName_Click;

                        panelViewAndEdit.Controls.Add(butSavePictureFileName);

                        Button butRemovePicture = new Button();
                        butRemovePicture.Location = new Point(butSavePictureFileName.Right + 20, pos);
                        butRemovePicture.Text = "Remove";
                        butRemovePicture.Tag = $"{picture.PictureTVItemID}";
                        butRemovePicture.Click += butRemovePicture_Click;

                        panelViewAndEdit.Controls.Add(butRemovePicture);


                        pos = butRemovePicture.Bottom + 50;
                    }
                }
            }
            else
            {
                if (!IsEditing)
                {
                    Label lblEmpty = new Label();
                    lblEmpty.AutoSize = true;
                    lblEmpty.Location = new Point(30, pos);
                    lblEmpty.MaximumSize = new Size(panelViewAndEdit.Width * 9 / 10, 0);
                    lblEmpty.Font = new Font(new FontFamily(lblEmpty.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    lblEmpty.Text = $"No Picture";

                    panelViewAndEdit.Controls.Add(lblEmpty);
                }
            }

            pos += 20;
            if (IsEditing)
            {
                Button butAddPicture = new Button();
                butAddPicture.Location = new Point(20, pos);
                butAddPicture.Text = "Add Picture";
                butAddPicture.Tag = $"{pss.SiteNumberText}";
                butAddPicture.Click += butAddPicture_Click;

                panelViewAndEdit.Controls.Add(butAddPicture);
            }
        }

        private void ViewPolSourceSiteDetail()
        {
            IsEditing = false;

            textBoxEmpty.Focus();

            panelShowButons.Visible = true;

            CurrentPSS = readSubsectorFile.subsectorDoc.Subsector.PSSList.Where(c => c.PSSTVItemID == PolSourceSiteTVItemID).FirstOrDefault();

            foreach (var a in panelPolSourceSite.Controls)
            {
                if (a.GetType().Name == "Panel")
                {
                    ((Panel)a).BackColor = Color.White;

                    if (((Panel)a).Tag.ToString() == PolSourceSiteTVItemID.ToString())
                    {
                        ((Panel)a).BackColor = Color.LightBlue;
                    }
                }
            }

            panelViewAndEdit.Controls.Clear();

            if (checkBoxLanguage.Checked)
            {
                _BaseEnumService = new BaseEnumService(LanguageEnum.fr);
            }
            else
            {
                _BaseEnumService = new BaseEnumService(LanguageEnum.en);
            }

            panelViewAndEdit.Controls.Clear();

            PSS pss = readSubsectorFile.subsectorDoc.Subsector.PSSList.Where(c => c.PSSTVItemID == PolSourceSiteTVItemID).FirstOrDefault();


            int pos = 0;
            if (pss != null)
            {
                Label lblTVText = new Label();
                lblTVText.AutoSize = true;
                lblTVText.Location = new Point(10, 4);
                lblTVText.MaximumSize = new Size(panelViewAndEdit.Width * 9 / 10, 0);
                lblTVText.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 14f, FontStyle.Bold);
                lblTVText.Text = $"{pss.SiteNumber} {pss.TVText}";

                panelViewAndEdit.Controls.Add(lblTVText);

                pos = lblTVText.Bottom + 10;

                if (pss.PSSAddress != null)
                {

                    Label lblAddress = new Label();
                    lblAddress.AutoSize = true;
                    lblAddress.Location = new Point(10, pos);
                    lblAddress.MaximumSize = new Size(panelViewAndEdit.Width * 9 / 10, 0);
                    lblAddress.Font = new Font(new FontFamily(lblAddress.Font.FontFamily.Name).Name, 12f, FontStyle.Bold);
                    lblAddress.Text = $@"Address: ";

                    panelViewAndEdit.Controls.Add(lblAddress);

                    Label lblAddress2 = new Label();
                    lblAddress2.AutoSize = true;
                    lblAddress2.Location = new Point(lblAddress.Right + 4, lblAddress.Top);
                    lblAddress2.MaximumSize = new Size(panelViewAndEdit.Width * 9 / 10, 0);
                    lblAddress2.Font = new Font(new FontFamily(lblAddress2.Font.FontFamily.Name).Name, 12f, FontStyle.Regular);
                    lblAddress2.Text = $@"{pss.PSSAddress.StreetNumber} {pss.PSSAddress.StreetName} {_BaseEnumService.GetEnumText_StreetTypeEnum((StreetTypeEnum)pss.PSSAddress.StreetType)}, {pss.PSSAddress.Municipality}, {pss.PSSAddress.PostalCode}";

                    panelViewAndEdit.Controls.Add(lblAddress2);

                    pos = lblAddress.Bottom + 4;

                }


                string ActiveText = pss.IsActive == true ? "Active" : "Inactive";
                string PointSourceText = pss.IsPointSource == true ? "Point Source" : "Non Point Source";

                if (pss.PSSObsList.Count > 0)
                {
                    foreach (Obs obs in pss.PSSObsList)
                    {
                        Label lblLastObs = new Label();
                        lblLastObs.AutoSize = true;
                        lblLastObs.Location = new Point(10, pos);
                        lblLastObs.MaximumSize = new Size(panelViewAndEdit.Width * 9 / 10, 0);
                        lblLastObs.Font = new Font(new FontFamily(lblLastObs.Font.FontFamily.Name).Name, 12f, FontStyle.Bold);
                        lblLastObs.Text = $"Observation Date: ";

                        panelViewAndEdit.Controls.Add(lblLastObs);

                        Label lblLastObs2 = new Label();
                        lblLastObs2.AutoSize = true;
                        lblLastObs2.Location = new Point(lblLastObs.Right, lblLastObs.Top);
                        lblLastObs2.MaximumSize = new Size(panelViewAndEdit.Width * 9 / 10, 0);
                        lblLastObs2.Font = new Font(new FontFamily(lblLastObs2.Font.FontFamily.Name).Name, 12f, FontStyle.Regular);
                        lblLastObs2.Text = $"{obs.ObsDate.ToString("yyyy MMMM dd")}";

                        panelViewAndEdit.Controls.Add(lblLastObs2);

                        pos = lblLastObs.Bottom + 4;

                        Label lblObsLastDate = new Label();
                        lblObsLastDate.AutoSize = true;
                        lblObsLastDate.Location = new Point(10, pos);
                        lblObsLastDate.MaximumSize = new Size(panelViewAndEdit.Width * 9 / 10, 0);
                        lblObsLastDate.Font = new Font(new FontFamily(lblObsLastDate.Font.FontFamily.Name).Name, 12f, FontStyle.Bold);
                        lblObsLastDate.Text = $"Observation Last Update (UTC): ";

                        panelViewAndEdit.Controls.Add(lblObsLastDate);

                        Label lblObsLastDate2 = new Label();
                        lblObsLastDate2.AutoSize = true;
                        lblObsLastDate2.Location = new Point(lblObsLastDate.Right, lblObsLastDate.Top);
                        lblObsLastDate2.MaximumSize = new Size(panelViewAndEdit.Width * 9 / 10, 0);
                        lblObsLastDate2.Font = new Font(new FontFamily(lblObsLastDate2.Font.FontFamily.Name).Name, 12f, FontStyle.Regular);
                        lblObsLastDate2.Text = $"{obs.LastUpdated_UTC.ToString("yyyy MMMM dd HH:mm:ss")}";

                        panelViewAndEdit.Controls.Add(lblObsLastDate2);

                        pos = lblObsLastDate.Bottom + 4;

                        Label lblWrittenDesc = new Label();
                        lblWrittenDesc.AutoSize = true;
                        lblWrittenDesc.Location = new Point(10, pos);
                        lblWrittenDesc.MaximumSize = new Size(panelViewAndEdit.Width * 9 / 10, 0);
                        lblWrittenDesc.Font = new Font(new FontFamily(lblWrittenDesc.Font.FontFamily.Name).Name, 12f, FontStyle.Bold);
                        lblWrittenDesc.Text = $"Written Description: ";

                        panelViewAndEdit.Controls.Add(lblWrittenDesc);

                        pos = lblWrittenDesc.Bottom + 4;

                        Label lblWrittenDesc2 = new Label();
                        lblWrittenDesc2.AutoSize = true;
                        lblWrittenDesc2.Location = new Point(20, pos);
                        lblWrittenDesc2.MaximumSize = new Size(panelViewAndEdit.Width * 9 / 10, 0);
                        lblWrittenDesc2.Font = new Font(new FontFamily(lblWrittenDesc2.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        lblWrittenDesc2.Text = $"{obs.Description}";

                        panelViewAndEdit.Controls.Add(lblWrittenDesc2);

                        pos = lblWrittenDesc2.Bottom + 10;

                        if (pss.OldIssueTextList.Count > 0)
                        {
                            Label lblOldIssue = new Label();
                            lblOldIssue.AutoSize = true;
                            lblOldIssue.Location = new Point(10, pos);
                            lblOldIssue.MaximumSize = new Size(panelViewAndEdit.Width * 9 / 10, 0);
                            lblOldIssue.Font = new Font(new FontFamily(lblOldIssue.Font.FontFamily.Name).Name, 12f, FontStyle.Bold);
                            lblOldIssue.Text = $"Old Issue Text :";

                            panelViewAndEdit.Controls.Add(lblOldIssue);

                            pos = lblOldIssue.Bottom + 6;

                            int OldIssueCount = 0;
                            foreach (string OldIssueText in pss.OldIssueTextList)
                            {
                                OldIssueCount += 1;

                                Label lblOldIssueCount = new Label();
                                lblOldIssueCount.AutoSize = true;
                                lblOldIssueCount.Location = new Point(10, pos);
                                lblOldIssueCount.MaximumSize = new Size(panelViewAndEdit.Width * 9 / 10, 0);
                                lblOldIssueCount.Font = new Font(new FontFamily(lblOldIssueCount.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                                lblOldIssueCount.Text = $"{OldIssueCount}) - ";

                                panelViewAndEdit.Controls.Add(lblOldIssueCount);

                                Label lblOldIssueText = new Label();
                                lblOldIssueText.AutoSize = true;
                                lblOldIssueText.Location = new Point(lblOldIssueCount.Right, pos);
                                lblOldIssueText.MaximumSize = new Size(panelViewAndEdit.Width * 9 / 10, 0);
                                lblOldIssueText.Font = new Font(new FontFamily(lblOldIssueText.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                                lblOldIssueText.Text = $"{OldIssueText}";

                                panelViewAndEdit.Controls.Add(lblOldIssueText);

                                pos = lblOldIssueText.Bottom + 10;
                            }
                        }

                        if (obs.IssueList.Count > 0)
                        {
                            int IssueCount = 0;
                            foreach (Issue issue in obs.IssueList)
                            {
                                IssueCount += 1;

                                Label lblIssue = new Label();
                                lblIssue.AutoSize = true;
                                lblIssue.Location = new Point(10, pos);
                                lblIssue.MaximumSize = new Size(panelViewAndEdit.Width * 9 / 10, 0);
                                lblIssue.Font = new Font(new FontFamily(lblIssue.Font.FontFamily.Name).Name, 12f, FontStyle.Bold);
                                lblIssue.Text = $"Issue ({IssueCount}) --- Last Update (UTC): ";

                                panelViewAndEdit.Controls.Add(lblIssue);

                                Label lblIssueLastUpdate = new Label();
                                lblIssueLastUpdate.AutoSize = true;
                                lblIssueLastUpdate.Location = new Point(lblIssue.Right, lblIssue.Top);
                                lblIssueLastUpdate.MaximumSize = new Size(panelViewAndEdit.Width * 9 / 10, 0);
                                lblIssueLastUpdate.Font = new Font(new FontFamily(lblIssueLastUpdate.Font.FontFamily.Name).Name, 12f, FontStyle.Regular);
                                lblIssueLastUpdate.Text = $"{issue.LastUpdated_UTC.ToString("yyyy MMMM dd HH:mm:ss")}";

                                panelViewAndEdit.Controls.Add(lblIssueLastUpdate);

                                pos = lblIssue.Bottom + 4;

                                string TVText = "";
                                for (int i = 0, count = issue.PolSourceObsInfoIntList.Count; i < count; i++)
                                {
                                    string Temp = _BaseEnumService.GetEnumText_PolSourceObsInfoReportEnum((PolSourceObsInfoEnum)issue.PolSourceObsInfoIntList[i]);
                                    switch ((issue.PolSourceObsInfoIntList[i].ToString()).Substring(0, 3))
                                    {
                                        case "101":
                                            {
                                                Temp = Temp.Replace("Source", "     Source");
                                            }
                                            break;
                                        case "250":
                                            {
                                                Temp = Temp.Replace("Pathway", "\r\n     Pathway");
                                            }
                                            break;
                                        case "900":
                                            {
                                                Temp = Temp.Replace("Status", "\r\n     Status");
                                            }
                                            break;
                                        case "910":
                                            {
                                                Temp = Temp.Replace("Risk", "\r\n     Risk");
                                            }
                                            break;
                                        case "110":
                                        case "120":
                                        case "122":
                                        case "151":
                                        case "152":
                                        case "153":
                                        case "155":
                                        case "156":
                                        case "157":
                                        case "163":
                                        case "166":
                                        case "167":
                                        case "170":
                                        case "171":
                                        case "172":
                                        case "173":
                                        case "176":
                                        case "178":
                                        case "181":
                                        case "182":
                                        case "183":
                                        case "185":
                                        case "186":
                                        case "187":
                                        case "190":
                                        case "191":
                                        case "192":
                                        case "193":
                                        case "194":
                                        case "196":
                                        case "198":
                                        case "199":
                                        case "220":
                                        case "930":
                                            {
                                                //Temp = Temp;
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                    TVText = TVText + Temp;
                                }

                                Label lblSelected = new Label();
                                lblSelected.AutoSize = true;
                                lblSelected.Location = new Point(20, pos);
                                lblSelected.MaximumSize = new Size(panelViewAndEdit.Width * 9 / 10, 0);
                                lblSelected.Font = new Font(new FontFamily(lblSelected.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                                lblSelected.Text = $"{TVText}";

                                panelViewAndEdit.Controls.Add(lblSelected);

                                pos = lblSelected.Bottom + 10;
                            }
                        }

                    }

                    pos += 10;

                    if (pss.PSSPictureList.Count > 0)
                    {
                        Label lblPictures = new Label();
                        lblPictures.AutoSize = true;
                        lblPictures.Location = new Point(10, pos);
                        lblPictures.MaximumSize = new Size(panelViewAndEdit.Width * 9 / 10, 0);
                        lblPictures.Font = new Font(new FontFamily(lblPictures.Font.FontFamily.Name).Name, 14f, FontStyle.Bold);
                        lblPictures.Text = $"Pictures";

                        panelViewAndEdit.Controls.Add(lblPictures);

                        pos = lblPictures.Bottom + 4;

                        foreach (Picture picture in pss.PSSPictureList)
                        {
                            PictureBox tempPictureBox = new PictureBox();
                            tempPictureBox.BorderStyle = BorderStyle.FixedSingle;
                            tempPictureBox.ImageLocation = $@"C:\PollutionSourceSites\{readSubsectorFile.CurrentSubsectorName}\Pictures\{pss.SiteNumberText}_{picture.PictureTVItemID}{picture.Extension}";
                            tempPictureBox.Location = new Point(10, pos);
                            tempPictureBox.Size = new Size(600, 500);
                            tempPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            tempPictureBox.TabIndex = 0;
                            tempPictureBox.TabStop = false;

                            panelViewAndEdit.Controls.Add(tempPictureBox);

                            pos = tempPictureBox.Bottom + 10;

                            Label lblFileName = new Label();
                            lblFileName.AutoSize = true;
                            lblFileName.Location = new Point(20, pos);
                            lblFileName.MaximumSize = new Size(panelViewAndEdit.Width * 9 / 10, 0);
                            lblFileName.Font = new Font(new FontFamily(lblFileName.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                            lblFileName.Text = $"Server File Name: ";

                            panelViewAndEdit.Controls.Add(lblFileName);


                            Label lblFileName2 = new Label();
                            lblFileName2.AutoSize = true;
                            lblFileName2.Location = new Point(lblFileName.Right + 5, pos);
                            lblFileName2.MaximumSize = new Size(panelViewAndEdit.Width * 9 / 10, 0);
                            lblFileName2.Font = new Font(new FontFamily(lblFileName2.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                            lblFileName2.Text = $"{picture.FileName}";

                            panelViewAndEdit.Controls.Add(lblFileName2);

                            pos = lblFileName2.Bottom + 10;

                            Label lblLocalFileName = new Label();
                            lblLocalFileName.AutoSize = true;
                            lblLocalFileName.Location = new Point(20, pos);
                            lblLocalFileName.MaximumSize = new Size(panelViewAndEdit.Width * 9 / 10, 0);
                            lblLocalFileName.Font = new Font(new FontFamily(lblLocalFileName.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                            lblLocalFileName.Text = $"Local File Name: ";

                            panelViewAndEdit.Controls.Add(lblLocalFileName);

                            Label lblLocalFileName2 = new Label();
                            lblLocalFileName2.AutoSize = true;
                            lblLocalFileName2.Location = new Point(lblLocalFileName.Right + 5, pos);
                            lblLocalFileName2.MaximumSize = new Size(panelViewAndEdit.Width * 9 / 10, 0);
                            lblLocalFileName2.Font = new Font(new FontFamily(lblLocalFileName2.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                            lblLocalFileName2.Text = $@"C:\PollutionSourceSites\{readSubsectorFile.CurrentSubsectorName}\Pictures\{pss.SiteNumberText}_{picture.PictureTVItemID}{picture.Extension}";

                            panelViewAndEdit.Controls.Add(lblLocalFileName2);

                            pos = lblLocalFileName2.Bottom + 10;

                            Label lblDescription = new Label();
                            lblDescription.AutoSize = true;
                            lblDescription.Location = new Point(20, pos);
                            lblDescription.MaximumSize = new Size(panelViewAndEdit.Width * 9 / 10, 0);
                            lblDescription.Font = new Font(new FontFamily(lblDescription.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                            lblDescription.Text = $"Description: ";

                            panelViewAndEdit.Controls.Add(lblDescription);

                            Label lblDescription2 = new Label();
                            lblDescription2.AutoSize = true;
                            lblDescription2.Location = new Point(lblDescription.Right + 5, pos);
                            lblDescription2.MaximumSize = new Size(panelViewAndEdit.Width * 9 / 10, 0);
                            lblDescription2.Font = new Font(new FontFamily(lblDescription2.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                            lblDescription2.Text = $"{picture.Description}";

                            panelViewAndEdit.Controls.Add(lblDescription2);

                            pos = lblDescription2.Bottom + 10;

                            butPictures.Enabled = true;
                        }
                    }
                }


            }

        }


        #endregion Functions private

        #region Functions public
        #endregion Functions public

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            SubsectorDoc doc = readSubsectorFile.subsectorDoc;

            sb.AppendLine($"VERSION\t{doc.Version}\t");
            sb.AppendLine($"DOCDATE\t{doc.DocDate.Year}|{doc.DocDate.Month.ToString("0#")}|{doc.DocDate.Day.ToString("0#")}|{doc.DocDate.Hour.ToString("0#")}|{doc.DocDate.Minute.ToString("0#")}|{doc.DocDate.Second.ToString("0#")}\t");
            sb.AppendLine($"SUBSECTOR\t{doc.Subsector.SubsectorTVItemID}\t{doc.Subsector.SubsectorName}\t");
            foreach (PSS pss in doc.Subsector.PSSList)
            {
                sb.AppendLine($"PSS\t{pss.PSSTVItemID}\t{pss.Lat.ToString("F5")}\t{pss.Lng.ToString("F5")}\t{(pss.IsActive ? "true" : "false")}\t{(pss.IsPointSource ? "true" : "false")}\t");
                sb.AppendLine($"SITENUMB\t{pss.SiteNumber}\t");
                sb.AppendLine($"TVTEXT\t{pss.TVText}\t");

                if (pss.PSSAddress != null)
                {
                    sb.AppendLine($"ADDRESS\t{pss.PSSAddress.AddressTVItemID}\t{pss.PSSAddress.Municipality}\t{((int)pss.PSSAddress.AddressType).ToString()}\t{pss.PSSAddress.StreetNumber}\t{pss.PSSAddress.StreetName}\t{((int)pss.PSSAddress.StreetType).ToString()}\t{pss.PSSAddress.PostalCode}\t");
                }

                foreach (Picture picture in pss.PSSPictureList)
                {
                    sb.AppendLine($"PICTURE\t{picture.PictureTVItemID}\t{picture.FileName}\t{picture.Extension}\t{picture.Description}\t");
                }

                foreach (Obs obs in pss.PSSObsList)
                {
                    sb.AppendLine($"OBS\t{obs.ObsID}\t{obs.LastUpdated_UTC.Year}|{obs.LastUpdated_UTC.Month.ToString("0#")}|{obs.LastUpdated_UTC.Day.ToString("0#")}|{obs.LastUpdated_UTC.Hour.ToString("0#")}|{obs.LastUpdated_UTC.Minute.ToString("0#")}|{obs.LastUpdated_UTC.Second.ToString("0#")}\t{obs.ObsDate.Year}|{obs.ObsDate.Month.ToString("0#")}|{obs.ObsDate.Day.ToString("0#")}\t");

                    if (!string.IsNullOrWhiteSpace(obs.Description))
                    {
                        sb.AppendLine($"DESC\t{obs.Description}\t");
                    }
                }

                foreach (string oldIssueText in pss.OldIssueTextList)
                {
                    sb.AppendLine($"OLDISSUETEXT\t{oldIssueText}\t");
                }

                foreach (Obs obs in pss.PSSObsList)
                {
                    foreach (Issue issue in obs.IssueList)
                    {
                        sb.AppendLine($"ISSUE\t{issue.IssueID}\t{issue.LastUpdated_UTC.Year}|{issue.LastUpdated_UTC.Month.ToString("0#")}|{issue.LastUpdated_UTC.Day.ToString("0#")}|{issue.LastUpdated_UTC.Hour.ToString("0#")}|{issue.LastUpdated_UTC.Minute.ToString("0#")}|{issue.LastUpdated_UTC.Second.ToString("0#")}\t{String.Join(",", issue.PolSourceObsInfoIntList)},\t");
                    }
                }
            }

            DirectoryInfo di = new DirectoryInfo($@"C:\PollutionSourceSites\{readSubsectorFile.CurrentSubsectorName}\Changes\");

            if (!di.Exists)
            {
                try
                {
                    di.Create();
                }
                catch (Exception)
                {
                    lblStatus.Text = "Could not create directory [" + di.FullName + "]";
                }
            }

            FileInfo fi = new FileInfo($@"C:\PollutionSourceSites\{readSubsectorFile.CurrentSubsectorName}\Changes\{readSubsectorFile.CurrentSubsectorName}.txt");

            StreamWriter sw = fi.CreateText();
            sw.Write(sb.ToString());
            sw.Close();
        }
    }
}
