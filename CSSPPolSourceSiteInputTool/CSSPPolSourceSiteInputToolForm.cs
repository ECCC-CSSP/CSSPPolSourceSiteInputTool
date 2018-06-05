﻿using CSSPEnumsDLL.Enums;
using CSSPEnumsDLL.Services;
using CSSPModelsDLL.Models;
using CSSPPolSourceSiteInputToolHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
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
        private BaseEnumService _BaseEnumService { get; set; }
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
        }
        #endregion Constructors

        #region Events
        private void butCreateSubsectorDirectory_Click(object sender, EventArgs e)
        {
            CreateSubsectorDirectoryWithInfo();
        }
        private void butCreateMunicipalityDirectory_Click(object sender, EventArgs e)
        {
            CreateMunicipalityDirectoryWithInfo();
        }
        private void butPSSAdd_Click(object sender, EventArgs e)
        {
            polSourceSiteInputToolHelper.PSSAdd();
            polSourceSiteInputToolHelper.SaveSubsectorTextFile();
            polSourceSiteInputToolHelper.RedrawPolSourceSiteList();
        }
        private void butInfrastructureAdd_Click(object sender, EventArgs e)
        {
            polSourceSiteInputToolHelper.InfrastructureAdd();
            polSourceSiteInputToolHelper.SaveMunicipalityTextFile();
            polSourceSiteInputToolHelper.RedrawInfrastructureList();
        }
        private void butViewKMLFile_Click(object sender, EventArgs e)
        {
            textBoxEmpty.Focus();
            if (polSourceSiteInputToolHelper != null)
            {
                if (polSourceSiteInputToolHelper.IsPolSourceSite)
                {
                    lblStatus.Text = "Regenerating Subsector KML File ...";
                    polSourceSiteInputToolHelper.RegenerateSubsectorKMLFile();
                    lblStatus.Text = "Subsector KML File was regenerated ...";

                    lblStatus.Text = "Opening Google Earth";
                    polSourceSiteInputToolHelper.ViewKMLFileInGoogleEarth();
                    lblStatus.Text = "";
                }
                else
                {
                    lblStatus.Text = "Regenerating Subsector KML File ...";
                    polSourceSiteInputToolHelper.RegenerateMunicipalityKMLFile();
                    lblStatus.Text = "Subsector KML File was regenerated ...";

                    lblStatus.Text = "Opening Google Earth";
                    polSourceSiteInputToolHelper.ViewKMLFileInGoogleEarth();
                    lblStatus.Text = "";
                }
            }
        }
        private void checkBoxAdmin_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShowAdmin.Checked)
            {
                ShowAdminPasswordParts();
            }
            else
            {
                checkBoxMoreInfo.Checked = false;
                checkBoxEditing.Checked = false;
                radioButtonDetails.Checked = true;

                polSourceSiteInputToolHelper.IsEditing = false;
                polSourceSiteInputToolHelper.IsAdmin = false;

                panelShowInputOptions.Visible = true;

                ShowInputParts();
            }
        }
        private void checkBoxEditing_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxEditing.Checked)
            {
                polSourceSiteInputToolHelper.IsEditing = true;
                if (polSourceSiteInputToolHelper.ShowOnlyIssues)
                {
                    checkBoxMoreInfo.Visible = true;
                }
                else
                {
                    checkBoxMoreInfo.Visible = false;
                }
            }
            else
            {
                polSourceSiteInputToolHelper.IsEditing = false;
                checkBoxMoreInfo.Visible = false;
            }

            textBoxEmpty.Focus();

            if (polSourceSiteInputToolHelper.IsPolSourceSite)
            {
                polSourceSiteInputToolHelper.ReDrawPolSourceSite();
            }
            else
            {
                polSourceSiteInputToolHelper.ReDrawInfrastructure();
            }
        }
        private void checkBoxInfrastructure_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShowInfrastructure.Checked)
            {
                checkBoxMoreInfo.Visible = false;
                if (radioButtonOnlyIssues.Checked)
                {
                    radioButtonDetails.Checked = true;
                }
                ShowInfrastructureParts();
            }
            else
            {
                checkBoxMoreInfo.Visible = true;
                ShowPollutionSourceSiteParts();
            }
        }
        private void checkBoxLanguage_CheckedChanged(object sender, EventArgs e)
        {

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

            if (polSourceSiteInputToolHelper.IsPolSourceSite)
            {
                polSourceSiteInputToolHelper.ReDrawPolSourceSite();
            }
            else
            {
                polSourceSiteInputToolHelper.ReDrawInfrastructure();
            }

        }
        private void comboBoxProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxEmpty.Focus();
            ComboBoxProvinceNamesSelectedIndexChanged();
        }
        private void comboBoxSubsectorOrMunicipality_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxSubsectorOrMunicipalitySelectedIndexChanged();
        }
        private void polSourceSiteInputToolHelper_UpdateStatus(object sender, PolSourceSiteInputToolHelper.StatusEventArgs e)
        {
            lblStatus.Text = e.Status;
            lblStatus.Refresh();
            Application.DoEvents();
        }
        private void polSourceSiteInputToolHelper_UpdateRTBFileName(object sender, PolSourceSiteInputToolHelper.RTBFileNameEventArgs e)
        {
            lblStatus.Text = $"Loading file [{e.FileName}]";
            lblStatus.Refresh();
            Application.DoEvents();

            richTextBoxStatus.Clear();
            try
            {
                richTextBoxStatus.LoadFile($@"C:\PollutionSourceSites\Documentations\{e.FileName}.rtf");
            }
            catch (Exception ex)
            {
                richTextBoxStatus.Text = ex.Message;
            }
            if (splitContainer2.Panel2.Height < panelViewAndEdit.Height * 1 / 3)
            {
                splitContainer2.SplitterDistance = panelViewAndEdit.Height * 2 / 3;
            }
        }
        private void polSourceSiteInputToolHelper_UpdateRTBMessage(object sender, PolSourceSiteInputToolHelper.RTBMessageEventArgs e)
        {
            lblStatus.Text = e.Message;
            lblStatus.Refresh();
            Application.DoEvents();

            richTextBoxStatus.AppendText(e.Message);
        }
        private void radioButtonDetails_CheckedChanged(object sender, EventArgs e)
        {
            polSourceSiteInputToolHelper.ShowPolSourceSiteDetails = true;
            polSourceSiteInputToolHelper.ShowOnlyIssues = false;
            polSourceSiteInputToolHelper.ShowOnlyPictures = false;
            polSourceSiteInputToolHelper.ShowOnlyMap = false;

            checkBoxMoreInfo.Visible = false;

            if (polSourceSiteInputToolHelper.IsPolSourceSite)
            {
                polSourceSiteInputToolHelper.ReDrawPolSourceSite();
            }
            else
            {
                polSourceSiteInputToolHelper.ReDrawInfrastructure();
            }
        }

        private void radioButtonOnlyIssues_CheckedChanged(object sender, EventArgs e)
        {
            polSourceSiteInputToolHelper.ShowPolSourceSiteDetails = false;
            polSourceSiteInputToolHelper.ShowOnlyIssues = true;
            polSourceSiteInputToolHelper.ShowOnlyPictures = false;
            polSourceSiteInputToolHelper.ShowOnlyMap = false;

            if (polSourceSiteInputToolHelper.IsEditing && radioButtonOnlyIssues.Checked)
            {
                checkBoxMoreInfo.Visible = true;
            }
            else
            {
                checkBoxMoreInfo.Visible = false;
            }

            if (polSourceSiteInputToolHelper.IsPolSourceSite)
            {
                polSourceSiteInputToolHelper.ReDrawPolSourceSite();
            }
            else
            {
                polSourceSiteInputToolHelper.ReDrawInfrastructure();
            }
        }

        private void radioButtonShowMap_CheckedChanged(object sender, EventArgs e)
        {
            polSourceSiteInputToolHelper.ShowPolSourceSiteDetails = false;
            polSourceSiteInputToolHelper.ShowOnlyIssues = false;
            polSourceSiteInputToolHelper.ShowOnlyPictures = false;
            polSourceSiteInputToolHelper.ShowOnlyMap = true;

            checkBoxMoreInfo.Visible = false;

            if (polSourceSiteInputToolHelper.IsPolSourceSite)
            {
                polSourceSiteInputToolHelper.ReDrawPolSourceSite();
            }
            else
            {
                polSourceSiteInputToolHelper.ReDrawInfrastructure();
            }
        }
        private void radioButtonOnlyPictures_CheckedChanged(object sender, EventArgs e)
        {
            polSourceSiteInputToolHelper.ShowPolSourceSiteDetails = false;
            polSourceSiteInputToolHelper.ShowOnlyIssues = false;
            polSourceSiteInputToolHelper.ShowOnlyPictures = true;
            polSourceSiteInputToolHelper.ShowOnlyMap = false;
            if (polSourceSiteInputToolHelper.IsPolSourceSite)
            {
                polSourceSiteInputToolHelper.ReDrawPolSourceSite();
            }
            else
            {
                polSourceSiteInputToolHelper.ReDrawInfrastructure();
            }
        }
        private void splitContainer1_SizeChanged(object sender, EventArgs e)
        {
            textBoxEmpty.Focus();
            if (polSourceSiteInputToolHelper != null)
            {
                if (polSourceSiteInputToolHelper.IsPolSourceSite)
                {
                    polSourceSiteInputToolHelper.DrawPanelPSS();
                    polSourceSiteInputToolHelper.ReDrawPolSourceSite();
                }
                else
                {
                    polSourceSiteInputToolHelper.DrawPanelInfrastructures();
                    polSourceSiteInputToolHelper.ReDrawInfrastructure();
                }
            }
        }
        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            textBoxEmpty.Focus();
            if (polSourceSiteInputToolHelper != null)
            {
                if (polSourceSiteInputToolHelper.IsPolSourceSite)
                {
                    polSourceSiteInputToolHelper.DrawPanelPSS();
                    polSourceSiteInputToolHelper.ReDrawPolSourceSite();
                }
                else
                {
                    polSourceSiteInputToolHelper.DrawPanelInfrastructures();
                    polSourceSiteInputToolHelper.ReDrawInfrastructure();
                }
            }
        }
        private void textBoxAccessCode_TextChanged(object sender, EventArgs e)
        {
            if (textBoxAccessCode.Text == "mt")
            {
                ShowAdminParts();
            }
        }
        #endregion Events

        #region Functions private
        private void ClearAllPanelAndComboBoxes()
        {
            butViewKMLFile.Enabled = false;
            polSourceSiteInputToolHelper.CurrentPSS = null;
            polSourceSiteInputToolHelper.CurrentInfrastructure = null;
            polSourceSiteInputToolHelper.CurrentSubsectorName = null;
            polSourceSiteInputToolHelper.CurrentMunicipalityName = null;
            polSourceSiteInputToolHelper.PolSourceSiteTVItemID = 0;
            polSourceSiteInputToolHelper.InfrastructureTVItemID = 0;
            comboBoxProvince.Items.Clear();
            comboBoxProvince.Text = "";
            comboBoxSubsectorOrMunicipality.Items.Clear();
            comboBoxSubsectorOrMunicipality.Text = "";
            panelViewAndEdit.Controls.Clear();
            panelPolSourceSite.Controls.Clear();
            panelAddNewPollutionSourceSite.Visible = false;
            panelAddNewInfrastructure.Visible = false;
            panelCreateMunicipalityDirectory.Visible = false;
            panelCreateSubsectorDirectory.Visible = false;
            panelShowInputOptions.Visible = false;
            panelProvinces.Visible = false;
            panelSubsectorOrMunicipality.Visible = false;

            if (polSourceSiteInputToolHelper.IsAdmin)
            {
                comboBoxSubsectorOrMunicipality.ValueMember = "TVItemID";
                comboBoxSubsectorOrMunicipality.DisplayMember = "TVText";
            }
            else
            {
                comboBoxSubsectorOrMunicipality.ValueMember = null;
                comboBoxSubsectorOrMunicipality.DisplayMember = null;
            }
        }
        private void ComboBoxProvinceNamesSelectedIndexChanged()
        {
            comboBoxSubsectorOrMunicipality.Items.Clear();

            TVItemModel tvItemModelProv = (TVItemModel)comboBoxProvince.SelectedItem;
            if (tvItemModelProv != null && tvItemModelProv.TVItemID != 0)
            {
                GetTVItemModelSubsectorList(tvItemModelProv.TVItemID);
                GetTVItemModelMunicipalityList(tvItemModelProv.TVItemID);

                if (polSourceSiteInputToolHelper.IsPolSourceSite)
                {
                    if (polSourceSiteInputToolHelper.tvItemModelSubsectorList.Count > 0)
                    {
                        foreach (TVItemModel tvItemModel in polSourceSiteInputToolHelper.tvItemModelSubsectorList)
                        {
                            comboBoxSubsectorOrMunicipality.Items.Add(tvItemModel);
                        }
                        if (comboBoxSubsectorOrMunicipality.Items.Count > 0)
                        {
                            comboBoxSubsectorOrMunicipality.SelectedIndex = 0;
                        }
                    }
                }
                else
                {
                    if (polSourceSiteInputToolHelper.tvItemModelMunicipalityList.Count > 0)
                    {
                        foreach (TVItemModel tvItemModel in polSourceSiteInputToolHelper.tvItemModelMunicipalityList)
                        {
                            comboBoxSubsectorOrMunicipality.Items.Add(tvItemModel);
                        }
                        if (comboBoxSubsectorOrMunicipality.Items.Count > 0)
                        {
                            comboBoxSubsectorOrMunicipality.SelectedIndex = 0;
                        }
                    }
                }
            }
        }
        private void ComboBoxSubsectorOrMunicipalitySelectedIndexChanged()
        {
            if (comboBoxSubsectorOrMunicipality.SelectedItem == null)
            {
                return;
            }

            panelPolSourceSite.Controls.Clear();
            panelViewAndEdit.Controls.Clear();
            panelShowInputOptions.Visible = false;
            polSourceSiteInputToolHelper.CurrentPSS = null;
            polSourceSiteInputToolHelper.CurrentInfrastructure = null;
            polSourceSiteInputToolHelper.CurrentSubsectorName = null;
            polSourceSiteInputToolHelper.CurrentMunicipalityName = null;
            polSourceSiteInputToolHelper.PolSourceSiteTVItemID = 0;
            polSourceSiteInputToolHelper.InfrastructureTVItemID = 0;
            panelAddNewPollutionSourceSite.Visible = false;
            panelAddNewInfrastructure.Visible = false;
            panelCreateMunicipalityDirectory.Visible = false;
            panelCreateSubsectorDirectory.Visible = false;
            panelShowInputOptions.Visible = !polSourceSiteInputToolHelper.IsAdmin;

            if (polSourceSiteInputToolHelper.IsAdmin)
            {
                if (polSourceSiteInputToolHelper.IsPolSourceSite)
                {
                    panelCreateSubsectorDirectory.Visible = true;
                    radioButtonOnlyIssues.Visible = true;

                    TVItemModel tvItemModelSS = (TVItemModel)comboBoxSubsectorOrMunicipality.SelectedItem;
                    if (tvItemModelSS != null && tvItemModelSS.TVItemID != 0)
                    {
                        if (tvItemModelSS.TVText.Contains(" "))
                        {
                            polSourceSiteInputToolHelper.CurrentSubsectorName = tvItemModelSS.TVText.Substring(0, tvItemModelSS.TVText.IndexOf(" "));
                        }
                        else
                        {
                            polSourceSiteInputToolHelper.CurrentSubsectorName = tvItemModelSS.TVText;
                        }

                        FileInfo fi = new FileInfo($@"{polSourceSiteInputToolHelper.BasePathPollutionSourceSites}{polSourceSiteInputToolHelper.CurrentSubsectorName}\{polSourceSiteInputToolHelper.CurrentSubsectorName}.txt");

                        if (fi.Exists)
                        {
                            butViewKMLFile.Enabled = true;
                        }
                        else
                        {
                            butViewKMLFile.Enabled = false;
                        }
                    }
                    polSourceSiteInputToolHelper.RedrawPolSourceSiteList();
                    polSourceSiteInputToolHelper.ReDrawPolSourceSite();
                }
                else
                {
                    panelCreateMunicipalityDirectory.Visible = true;
                    radioButtonOnlyIssues.Visible = false;

                    TVItemModel tvItemModelMuni = (TVItemModel)comboBoxSubsectorOrMunicipality.SelectedItem;
                    if (tvItemModelMuni != null && tvItemModelMuni.TVItemID != 0)
                    {
                        polSourceSiteInputToolHelper.CurrentMunicipalityName = tvItemModelMuni.TVText;

                        FileInfo fi = new FileInfo($@"{polSourceSiteInputToolHelper.BasePathInfrastructures}{polSourceSiteInputToolHelper.CurrentMunicipalityName}\{polSourceSiteInputToolHelper.CurrentMunicipalityName}.txt");

                        if (fi.Exists)
                        {
                            butViewKMLFile.Enabled = true;
                        }
                        else
                        {
                            butViewKMLFile.Enabled = false;
                        }
                    }
                    polSourceSiteInputToolHelper.RedrawInfrastructureList();
                    polSourceSiteInputToolHelper.ReDrawInfrastructure();
                }
            }
            else
            {
                if (polSourceSiteInputToolHelper.IsPolSourceSite)
                {
                    panelAddNewPollutionSourceSite.Visible = true;
                    radioButtonOnlyIssues.Visible = true;

                    polSourceSiteInputToolHelper.CurrentMunicipalityName = null;
                    polSourceSiteInputToolHelper.CurrentSubsectorName = (string)comboBoxSubsectorOrMunicipality.SelectedItem;
                    polSourceSiteInputToolHelper.RedrawPolSourceSiteList();
                    polSourceSiteInputToolHelper.ReDrawPolSourceSite();
                    butViewKMLFile.Enabled = true;
                }
                else
                {
                    panelAddNewInfrastructure.Visible = true;
                    radioButtonOnlyIssues.Visible = false;

                    polSourceSiteInputToolHelper.CurrentSubsectorName = null;
                    polSourceSiteInputToolHelper.CurrentMunicipalityName = (string)comboBoxSubsectorOrMunicipality.SelectedItem;
                    polSourceSiteInputToolHelper.RedrawInfrastructureList();
                    polSourceSiteInputToolHelper.ReDrawInfrastructure();
                    butViewKMLFile.Enabled = true;
                }
            }
        }
        private void CreateMunicipalityDirectoryWithInfo()
        {
            if (!TryToCreateTheInfrastructureDirectory())
            {
                return;
            }
            if (!GetMunicipalitiesForInputTool())
            {
                return;
            }
            if (!GetInfrastructurePicturesForInputTool())
            {
                return;
            }
            polSourceSiteInputToolHelper.RegenerateMunicipalityKMLFile();

            polSourceSiteInputToolHelper.DrawPanelInfrastructures();
            polSourceSiteInputToolHelper.ReDrawInfrastructure();
            butViewKMLFile.Enabled = true;

            lblStatus.Text = $"Done ... {polSourceSiteInputToolHelper.CurrentMunicipalityName} directory now exist";
            lblStatus.Refresh();
            Application.DoEvents();

        }
        private void CreateSubsectorDirectoryWithInfo()
        {
            if (!TryToCreateThePolSourceSiteDirectory())
            {
                return;
            }
            if (!GetPollutionSourceSitesForInputTool())
            {
                return;
            }
            if (!GetPolSourceSitePicturesForInputTool())
            {
                return;
            }
            polSourceSiteInputToolHelper.RegenerateSubsectorKMLFile();

            polSourceSiteInputToolHelper.DrawPanelPSS();
            polSourceSiteInputToolHelper.ReDrawPolSourceSite();
            butViewKMLFile.Enabled = true;

            lblStatus.Text = $"Done ... {polSourceSiteInputToolHelper.CurrentSubsectorName} directory now exist";
            lblStatus.Refresh();
            Application.DoEvents();

        }
        private void FillComboBoxSubsectorOrMunicipality()
        {
            comboBoxSubsectorOrMunicipality.ValueMember = "TVItemID";
            comboBoxSubsectorOrMunicipality.DisplayMember = "TVText";

            comboBoxSubsectorOrMunicipality.Items.Clear();
            if (polSourceSiteInputToolHelper.IsAdmin)
            {
                if (polSourceSiteInputToolHelper.IsPolSourceSite)
                {
                    foreach (TVItemModel tvItemModelSS in polSourceSiteInputToolHelper.tvItemModelSubsectorList)
                    {
                        comboBoxSubsectorOrMunicipality.Items.Add(tvItemModelSS);
                    }
                }
                else
                {
                    foreach (TVItemModel tvItemModelMuni in polSourceSiteInputToolHelper.tvItemModelMunicipalityList)
                    {
                        comboBoxSubsectorOrMunicipality.Items.Add(tvItemModelMuni);
                    }
                }


                if (comboBoxSubsectorOrMunicipality.Items.Count > 0)
                {
                    comboBoxSubsectorOrMunicipality.SelectedIndex = 0;
                }
                else
                {

                }

            }
            else
            {
                comboBoxSubsectorOrMunicipality.Items.Clear();

                if (polSourceSiteInputToolHelper.IsPolSourceSite)
                {
                    foreach (TVItemModel tvItemModelSS in polSourceSiteInputToolHelper.tvItemModelSubsectorList)
                    {
                        comboBoxSubsectorOrMunicipality.Items.Add(tvItemModelSS);
                    }
                }
                else
                {
                    foreach (TVItemModel tvItemModelMuni in polSourceSiteInputToolHelper.tvItemModelMunicipalityList)
                    {
                        comboBoxSubsectorOrMunicipality.Items.Add(tvItemModelMuni);
                    }
                }


                if (comboBoxSubsectorOrMunicipality.Items.Count > 0)
                {
                    comboBoxSubsectorOrMunicipality.SelectedIndex = 0;
                }

            }
        }
        private bool GetInfrastructurePicturesForInputTool()
        {

            TVItemModel tvItemModelMunicipality = (TVItemModel)comboBoxSubsectorOrMunicipality.SelectedItem;
            if (tvItemModelMunicipality == null || tvItemModelMunicipality.TVItemID == 0)
            {
                richTextBoxStatus.Text = "Need to select a municipality first \r\n";
                return false;
            }

            string MunicipalityText = tvItemModelMunicipality.TVText;

            polSourceSiteInputToolHelper.CurrentSubsectorName = MunicipalityText;
            if (!polSourceSiteInputToolHelper.ReadInfrastructuresMunicipalityFile())
            {
                richTextBoxStatus.Text = $"Error reading {MunicipalityText}";
                return false;
            }

            butViewKMLFile.Enabled = true;

            DirectoryInfo di = new DirectoryInfo($@"{polSourceSiteInputToolHelper.BasePathInfrastructures}\{polSourceSiteInputToolHelper.CurrentMunicipalityName}\Pictures\");

            if (!di.Exists)
            {
                try
                {
                    di.Create();
                }
                catch (Exception ex)
                {
                    richTextBoxStatus.AppendText(ex.Message + (ex.InnerException != null ? " InnerException: " + ex.InnerException.Message : "") + "\r\n");
                    return false;
                }
            }

            foreach (Infrastructure infrastructure in polSourceSiteInputToolHelper.municipalityDoc.Municipality.InfrastructureList)
            {
                foreach (Picture picture in infrastructure.InfrastructurePictureList)
                {
                    FileInfo fiTemp = new FileInfo(picture.FileName);
                    FileInfo fi = new FileInfo($@"{polSourceSiteInputToolHelper.BasePathInfrastructures}\{polSourceSiteInputToolHelper.CurrentMunicipalityName}\Pictures\{infrastructure.InfrastructureTVItemID}_{picture.PictureTVItemID}{fiTemp.Extension}");

                    string url = "";
                    if (checkBoxLanguage.Checked)
                    {
                        url = polSourceSiteInputToolHelper.baseURLFR.Replace(@"/PolSource/", @"/File/FileDownload?TVFileTVItemID=") + picture.PictureTVItemID.ToString();
                    }
                    else
                    {
                        url = polSourceSiteInputToolHelper.baseURLEN.Replace(@"/PolSource/", @"/File/FileDownload?TVFileTVItemID=") + picture.PictureTVItemID.ToString();
                    }

                    try
                    {
                        lblStatus.Text = $"Working ... downloading Image [{picture.FileName}] under the Pictures directory";
                        lblStatus.Refresh();
                        Application.DoEvents();

                        if (!fi.Exists)
                        {

                            using (WebClient webClient = new WebClient())
                            {
                                WebProxy webProxy = new WebProxy();
                                webClient.Proxy = webProxy;


                                var json_data = string.Empty;
                                byte[] responseBytes = webClient.DownloadData(url);

                                FileStream fs = fi.Create();
                                fs.Write(responseBytes, 0, responseBytes.Length);
                                fs.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        richTextBoxStatus.Text = "";
                        richTextBoxStatus.AppendText("Could not load " + url + "\r\n");
                        richTextBoxStatus.AppendText(ex.Message + (ex.InnerException != null ? " InnerException: " + ex.InnerException.Message : "") + "\r\n");
                        return false;
                    }
                }
            }

            return true;
        }
        private bool GetPolSourceSitePicturesForInputTool()
        {

            TVItemModel tvItemModelSS = (TVItemModel)comboBoxSubsectorOrMunicipality.SelectedItem;
            if (tvItemModelSS == null || tvItemModelSS.TVItemID == 0)
            {
                richTextBoxStatus.Text = "Need to select a subsector first \r\n";
                return false;
            }

            string SubsectorText = tvItemModelSS.TVText;
            if (SubsectorText.Contains(" "))
            {
                SubsectorText = SubsectorText.Substring(0, SubsectorText.IndexOf(" "));
            }

            polSourceSiteInputToolHelper.CurrentSubsectorName = SubsectorText;

            if (!polSourceSiteInputToolHelper.ReadPollutionSourceSitesSubsectorFile())
            {
                richTextBoxStatus.Text = $"Error reading {SubsectorText}";
                return false;
            }

            butViewKMLFile.Enabled = true;

            DirectoryInfo di = new DirectoryInfo($@"{polSourceSiteInputToolHelper.BasePathPollutionSourceSites}\{polSourceSiteInputToolHelper.CurrentSubsectorName}\Pictures\");

            if (!di.Exists)
            {
                try
                {
                    di.Create();
                }
                catch (Exception ex)
                {
                    richTextBoxStatus.AppendText(ex.Message + (ex.InnerException != null ? " InnerException: " + ex.InnerException.Message : "") + "\r\n");
                    return false;
                }
            }

            List<int> pictureCSSPIDList = new List<int>();

            foreach (PSS pss in polSourceSiteInputToolHelper.subsectorDoc.Subsector.PSSList)
            {
                foreach (Picture picture in pss.PSSPictureList)
                {
                    FileInfo fiTemp = new FileInfo(picture.FileName);
                    FileInfo fi = new FileInfo($@"{polSourceSiteInputToolHelper.BasePathPollutionSourceSites}\{polSourceSiteInputToolHelper.CurrentSubsectorName}\Pictures\{pss.SiteNumberText}_{picture.PictureTVItemID}{fiTemp.Extension}");

                    string url = "";
                    if (checkBoxLanguage.Checked)
                    {
                        url = polSourceSiteInputToolHelper.baseURLFR.Replace(@"/PolSource/", @"/File/FileDownload?TVFileTVItemID=") + picture.PictureTVItemID.ToString();
                    }
                    else
                    {
                        url = polSourceSiteInputToolHelper.baseURLEN.Replace(@"/PolSource/", @"/File/FileDownload?TVFileTVItemID=") + picture.PictureTVItemID.ToString();
                    }

                    try
                    {
                        lblStatus.Text = $"Working ... downloading Image [{picture.FileName}] under the Pictures directory";
                        lblStatus.Refresh();
                        Application.DoEvents();

                        if (!fi.Exists)
                        {

                            using (WebClient webClient = new WebClient())
                            {
                                WebProxy webProxy = new WebProxy();
                                webClient.Proxy = webProxy;


                                var json_data = string.Empty;
                                byte[] responseBytes = webClient.DownloadData(url);

                                FileStream fs = fi.Create();
                                fs.Write(responseBytes, 0, responseBytes.Length);
                                fs.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        richTextBoxStatus.Text = "";
                        richTextBoxStatus.AppendText("Could not load " + url + "\r\n");
                        richTextBoxStatus.AppendText(ex.Message + (ex.InnerException != null ? " InnerException: " + ex.InnerException.Message : "") + "\r\n");
                        return false;
                    }
                }
            }

            return true;
        }
        private bool GetMunicipalitiesForInputTool()
        {

            TVItemModel tvItemModelMunicipality = (TVItemModel)comboBoxSubsectorOrMunicipality.SelectedItem;
            if (tvItemModelMunicipality == null || tvItemModelMunicipality.TVItemID == 0)
            {
                richTextBoxStatus.Text = "Need to select a subsector first \r\n";
                return false;
            }

            string MunicipalityText = tvItemModelMunicipality.TVText;


            FileInfo fi = new FileInfo(@"C:\PollutionSourceSites\Infrastructures\" + MunicipalityText + @"\" + MunicipalityText + ".txt");

            string url = "";
            if (checkBoxLanguage.Checked)
            {
                url = polSourceSiteInputToolHelper.baseURLFR + "GetInfrastructuresForInputToolJSON?MunicipalityTVItemID=" + tvItemModelMunicipality.TVItemID;
            }
            else
            {
                url = polSourceSiteInputToolHelper.baseURLEN + "GetInfrastructuresForInputToolJSON?MunicipalityTVItemID=" + tvItemModelMunicipality.TVItemID;
            }

            try
            {
                lblStatus.Text = $"Working ... creating and downloading [{MunicipalityText}] Municipality .txt document";
                lblStatus.Refresh();
                Application.DoEvents();

                using (WebClient webClient = new WebClient())
                {
                    //WebProxy webProxy = new WebProxy();
                    //webClient.Proxy = webProxy;


                    var json_data = string.Empty;
                    byte[] responseBytes = webClient.DownloadData(url);
                    json_data = Encoding.UTF8.GetString(responseBytes);

                    StreamWriter sw = fi.CreateText();
                    sw.Write(json_data);
                    sw.Close();
                }

            }
            catch (Exception ex)
            {
                richTextBoxStatus.Text = "";
                richTextBoxStatus.AppendText("Could not load " + url + "\r\n");
                richTextBoxStatus.AppendText(ex.Message + (ex.InnerException != null ? " InnerException: " + ex.InnerException.Message : "") + "\r\n");
            }

            return true;
        }
        private bool GetPollutionSourceSitesForInputTool()
        {
            TVItemModel tvItemModelSS = (TVItemModel)comboBoxSubsectorOrMunicipality.SelectedItem;
            if (tvItemModelSS == null || tvItemModelSS.TVItemID == 0)
            {
                richTextBoxStatus.Text = "Need to select a subsector first \r\n";
                return false;
            }

            string SubsectorText = tvItemModelSS.TVText;
            if (SubsectorText.Contains(" "))
            {
                SubsectorText = SubsectorText.Substring(0, SubsectorText.IndexOf(" "));
            }

            FileInfo fi = new FileInfo(@"C:\PollutionSourceSites\Subsectors\" + SubsectorText + @"\" + SubsectorText + ".txt");

            string url = "";
            if (checkBoxLanguage.Checked)
            {
                url = polSourceSiteInputToolHelper.baseURLFR + "GetPollutionSourceSitesForInputToolJSON?SubsectorTVItemID=" + tvItemModelSS.TVItemID;
            }
            else
            {
                url = polSourceSiteInputToolHelper.baseURLEN + "GetPollutionSourceSitesForInputToolJSON?SubsectorTVItemID=" + tvItemModelSS.TVItemID;
            }

            try
            {
                lblStatus.Text = $"Working ... creating and downloading [{SubsectorText}] Pollution Source Sites .txt document";
                lblStatus.Refresh();
                Application.DoEvents();

                using (WebClient webClient = new WebClient())
                {
                    //WebProxy webProxy = new WebProxy();
                    //webClient.Proxy = webProxy;


                    var json_data = string.Empty;
                    byte[] responseBytes = webClient.DownloadData(url);
                    json_data = Encoding.UTF8.GetString(responseBytes);

                    StreamWriter sw = fi.CreateText();
                    sw.Write(json_data);
                    sw.Close();
                }

            }
            catch (Exception ex)
            {
                richTextBoxStatus.Text = "";
                richTextBoxStatus.AppendText("Could not load " + url + "\r\n");
                richTextBoxStatus.AppendText(ex.Message + (ex.InnerException != null ? " InnerException: " + ex.InnerException.Message : "") + "\r\n");
            }

            return true;
        }
        private void GetTVItemModelProvinceList()
        {
            polSourceSiteInputToolHelper.tvItemModelProvinceList = new List<TVItemModel>();

            string url = "";
            if (checkBoxLanguage.Checked)
            {
                url = polSourceSiteInputToolHelper.baseURLFR + "GetTVItemModelProvinceListJSON";
            }
            else
            {
                url = polSourceSiteInputToolHelper.baseURLEN + "GetTVItemModelProvinceListJSON";
            }

            try
            {
                using (WebClient webClient = new WebClient())
                {
                    WebProxy webProxy = new WebProxy();
                    webClient.Proxy = webProxy;


                    var json_data = string.Empty;
                    byte[] responseBytes = webClient.DownloadData(url);
                    json_data = Encoding.UTF8.GetString(responseBytes);

                    if (json_data.Length > 0)
                    {
                        if (!string.IsNullOrEmpty(json_data))
                        {
                            polSourceSiteInputToolHelper.tvItemModelProvinceList = JsonConvert.DeserializeObject<List<TVItemModel>>(json_data);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                richTextBoxStatus.Text = "";
                richTextBoxStatus.AppendText("Could not load " + url + "\r\n");
                richTextBoxStatus.AppendText(ex.Message + (ex.InnerException != null ? " InnerException: " + ex.InnerException.Message : "") + "\r\n");
            }
        }
        private void GetTVItemModelMunicipalityList(int ProvinceTVItemID)
        {
            polSourceSiteInputToolHelper.tvItemModelMunicipalityList = new List<TVItemModel>();

            string url = "";
            if (checkBoxLanguage.Checked)
            {
                url = polSourceSiteInputToolHelper.baseURLFR + "GetTVItemModelMunicipalityListJSON?ProvinceTVItemID=" + ProvinceTVItemID.ToString();
            }
            else
            {
                url = polSourceSiteInputToolHelper.baseURLEN + "GetTVItemModelMunicipalityListJSON?ProvinceTVItemID=" + ProvinceTVItemID.ToString();
            }

            try
            {
                using (WebClient webClient = new WebClient())
                {
                    WebProxy webProxy = new WebProxy();
                    webClient.Proxy = webProxy;

                    var json_data = string.Empty;
                    byte[] responseBytes = webClient.DownloadData(url);
                    json_data = Encoding.UTF8.GetString(responseBytes);

                    if (json_data.Length > 0)
                    {
                        if (!string.IsNullOrEmpty(json_data))
                        {
                            polSourceSiteInputToolHelper.tvItemModelMunicipalityList = JsonConvert.DeserializeObject<List<TVItemModel>>(json_data);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                richTextBoxStatus.Text = "";
                richTextBoxStatus.AppendText("Could not load " + url + "\r\n");
                richTextBoxStatus.AppendText(ex.Message + (ex.InnerException != null ? " InnerException: " + ex.InnerException.Message : "") + "\r\n");
            }
        }
        private void GetTVItemModelSubsectorList(int ProvinceTVItemID)
        {
            polSourceSiteInputToolHelper.tvItemModelSubsectorList = new List<TVItemModel>();

            string url = "";
            if (checkBoxLanguage.Checked)
            {
                url = polSourceSiteInputToolHelper.baseURLFR + "GetTVItemModelSubsectorListJSON?ProvinceTVItemID=" + ProvinceTVItemID.ToString();
            }
            else
            {
                url = polSourceSiteInputToolHelper.baseURLEN + "GetTVItemModelSubsectorListJSON?ProvinceTVItemID=" + ProvinceTVItemID.ToString();
            }

            try
            {
                using (WebClient webClient = new WebClient())
                {
                    WebProxy webProxy = new WebProxy();
                    webClient.Proxy = webProxy;

                    var json_data = string.Empty;
                    byte[] responseBytes = webClient.DownloadData(url);
                    json_data = Encoding.UTF8.GetString(responseBytes);

                    if (json_data.Length > 0)
                    {
                        if (!string.IsNullOrEmpty(json_data))
                        {
                            polSourceSiteInputToolHelper.tvItemModelSubsectorList = JsonConvert.DeserializeObject<List<TVItemModel>>(json_data);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                richTextBoxStatus.Text = "";
                richTextBoxStatus.AppendText("Could not load " + url + "\r\n");
                richTextBoxStatus.AppendText(ex.Message + (ex.InnerException != null ? " InnerException: " + ex.InnerException.Message : "") + "\r\n");
            }
        }
        private void RefreshComboBoxSubsectorOrMunicipality()
        {
            panelSubsectorOrMunicipality.Visible = true;

            comboBoxSubsectorOrMunicipality.Items.Clear();
            if (polSourceSiteInputToolHelper.IsAdmin)
            {
                comboBoxSubsectorOrMunicipality.ValueMember = "TVItemID";
                comboBoxSubsectorOrMunicipality.DisplayMember = "TVText";
                ComboBoxProvinceNamesSelectedIndexChanged();
            }
            else
            {
                comboBoxSubsectorOrMunicipality.ValueMember = null;
                comboBoxSubsectorOrMunicipality.DisplayMember = null;
                comboBoxSubsectorOrMunicipality.Text = "";

                if (polSourceSiteInputToolHelper.IsPolSourceSite)
                {
                    DirectoryInfo di = new DirectoryInfo(polSourceSiteInputToolHelper.BasePathPollutionSourceSites);

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
                        comboBoxSubsectorOrMunicipality.Items.Add(directoryInfo.Name);
                    }

                    if (comboBoxSubsectorOrMunicipality.Items.Count > 0)
                    {
                        comboBoxSubsectorOrMunicipality.SelectedIndex = 0;
                    }
                }
                else
                {
                    DirectoryInfo di = new DirectoryInfo(polSourceSiteInputToolHelper.BasePathInfrastructures);

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
                        comboBoxSubsectorOrMunicipality.Items.Add(directoryInfo.Name);
                    }

                    if (comboBoxSubsectorOrMunicipality.Items.Count > 0)
                    {
                        comboBoxSubsectorOrMunicipality.SelectedIndex = 0;
                    }
                }
            }
        }
        private void ShowAdminPasswordParts()
        {
            ClearAllPanelAndComboBoxes();

            panelAdminPassword.Visible = true;
            textBoxAccessCode.Text = "";
            textBoxAccessCode.Focus();
        }
        private void ShowAdminParts()
        {
            ClearAllPanelAndComboBoxes();

            comboBoxProvince.ValueMember = "TVItemID";
            comboBoxProvince.DisplayMember = "TVText";
            comboBoxSubsectorOrMunicipality.ValueMember = "TVItemID";
            comboBoxSubsectorOrMunicipality.DisplayMember = "TVText";
            panelProvinces.Visible = true;
            panelSubsectorOrMunicipality.Visible = true;
            panelAdminPassword.Visible = false;
            panelProvinces.Visible = true;
            panelSubsectorOrMunicipality.Visible = true;

            checkBoxMoreInfo.Checked = false;
            checkBoxEditing.Checked = false;
            radioButtonDetails.Checked = true;

            polSourceSiteInputToolHelper.IsEditing = false;
            polSourceSiteInputToolHelper.IsAdmin = true;

            panelShowInputOptions.Visible = false;

            GetTVItemModelProvinceList();
            if (polSourceSiteInputToolHelper.tvItemModelProvinceList.Count == 0)
            {
                richTextBoxStatus.Text = $"You do not have access to {polSourceSiteInputToolHelper.baseURLEN}";
                return;
            }
            else
            {
                comboBoxProvince.Items.Clear();
                foreach (TVItemModel tvItemModel in polSourceSiteInputToolHelper.tvItemModelProvinceList)
                {
                    comboBoxProvince.Items.Add(tvItemModel);
                }
                if (comboBoxProvince.Items.Count > 0)
                {
                    comboBoxProvince.SelectedIndex = 0;
                }
            }

            if (polSourceSiteInputToolHelper.IsPolSourceSite)
            {
                panelCreateSubsectorDirectory.Visible = true;
            }
            else
            {
                panelCreateMunicipalityDirectory.Visible = true;
            }


        }
        private void ShowInputParts()
        {
            polSourceSiteInputToolHelper.IsAdmin = false;
            polSourceSiteInputToolHelper.IsEditing = false;
            polSourceSiteInputToolHelper.ShowPolSourceSiteDetails = true;
            panelProvinces.Visible = false;
            panelSubsectorOrMunicipality.Visible = true;
            panelViewKMLFileTop.Enabled = true;
            panelAdminPassword.Visible = false;
            panelAddNewPollutionSourceSite.Visible = true;
            panelShowInputOptions.Visible = true;
            panelPolSourceSite.Controls.Clear();
            panelViewAndEdit.Controls.Clear();
            polSourceSiteInputToolHelper.CurrentPSS = null;
            polSourceSiteInputToolHelper.PolSourceSiteTVItemID = 0;
            polSourceSiteInputToolHelper.RedrawPolSourceSiteList();
            panelCreateSubsectorDirectory.Visible = false;
            panelCreateMunicipalityDirectory.Visible = false;

            RefreshComboBoxSubsectorOrMunicipality();
        }
        private void ShowInfrastructureParts()
        {
            panelAddNewPollutionSourceSite.Visible = false;
            panelAddNewInfrastructure.Visible = false;
            panelCreateSubsectorDirectory.Visible = false;
            panelCreateMunicipalityDirectory.Visible = false;

            polSourceSiteInputToolHelper.IsPolSourceSite = false;
            panelCreateSubsectorDirectory.Visible = true;
            RefreshComboBoxSubsectorOrMunicipality();

            //polSourceSiteInputToolHelper.IsPolSourceSite = false;

            //comboBoxProvince.Items.Clear();
            //comboBoxProvince.Text = "";
            //comboBoxSubsectorOrMunicipality.Items.Clear();
            //comboBoxSubsectorOrMunicipality.Text = "";

            //panelAddNewPollutionSourceSite.Visible = false;
            //panelAddNewInfrastructure.Visible = false;
            //panelCreateSubsectorDirectory.Visible = false;
            //panelCreateMunicipalityDirectory.Visible = false;

            //if (polSourceSiteInputToolHelper.IsAdmin)
            //{
            //    comboBoxSubsectorOrMunicipality.ValueMember = "TVItemID";
            //    comboBoxSubsectorOrMunicipality.DisplayMember = "TVText";
            //    if (polSourceSiteInputToolHelper.IsPolSourceSite)
            //    {
            //        panelCreateSubsectorDirectory.Visible = true;
            //    }
            //    else
            //    {
            //        panelCreateMunicipalityDirectory.Visible = true;
            //    }

            //    //GetTVItemModelProvinceList();
            //    if (polSourceSiteInputToolHelper.tvItemModelProvinceList.Count == 0)
            //    {
            //        richTextBoxStatus.Text = $"You do not have access to {polSourceSiteInputToolHelper.baseURLEN}";
            //        return;
            //    }
            //    else
            //    {
            //        comboBoxProvince.Items.Clear();
            //        foreach (TVItemModel tvItemModel in polSourceSiteInputToolHelper.tvItemModelProvinceList)
            //        {
            //            comboBoxProvince.Items.Add(tvItemModel);
            //        }
            //        if (comboBoxProvince.Items.Count > 0)
            //        {
            //            comboBoxProvince.SelectedIndex = 0;
            //        }
            //    }

            //    RefreshComboBoxSubsectorOrMunicipality();
            //}
            //else
            //{
            //    panelAddNewInfrastructure.Visible = true;

            //    comboBoxSubsectorOrMunicipality.ValueMember = null;
            //    comboBoxSubsectorOrMunicipality.DisplayMember = null;

            //    if (polSourceSiteInputToolHelper.IsPolSourceSite)
            //    {
            //        panelCreateSubsectorDirectory.Visible = true;
            //        panelCreateMunicipalityDirectory.Visible = false;
            //    }
            //    else
            //    {
            //        panelCreateSubsectorDirectory.Visible = false;
            //        panelCreateMunicipalityDirectory.Visible = true;
            //    }

            //    RefreshComboBoxSubsectorOrMunicipality();
            //}
        }
        private void ShowPollutionSourceSiteParts()
        {
            panelAddNewPollutionSourceSite.Visible = false;
            panelAddNewInfrastructure.Visible = false;
            panelCreateSubsectorDirectory.Visible = false;
            panelCreateMunicipalityDirectory.Visible = false;

            polSourceSiteInputToolHelper.IsPolSourceSite = true;
            panelCreateSubsectorDirectory.Visible = true;
            RefreshComboBoxSubsectorOrMunicipality();
        }
        private void Setup()
        {
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.BringToFront();
            splitContainer1.SplitterDistance = 400;
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.BringToFront();
            splitContainer2.SplitterDistance = splitContainer1.Height - 100;
            panelPolSourceSite.Dock = DockStyle.Fill;
            panelPolSourceSite.BringToFront();
            panelViewAndEdit.Dock = DockStyle.Fill;
            panelViewAndEdit.BringToFront();
            richTextBoxStatus.Dock = DockStyle.Fill;
            panelLanguage.Visible = false;
            panelProvinces.Visible = false;
            panelCreateMunicipalityDirectory.Visible = false;
            panelCreateSubsectorDirectory.Visible = false;
            panelAdminPassword.Visible = false;
            panelSubsectorOrMunicipality.Dock = DockStyle.Fill;
            panelViewAndEdit.Dock = DockStyle.Fill;
            panelPolSourceSite.Dock = DockStyle.Fill;
            textBoxEmpty.Width = 1;
            textBoxEmpty.Height = 1;
            panelAddNewPollutionSourceSite.Visible = false;
            panelShowInputOptions.Visible = false;
            radioButtonShowMap.Visible = false;
            checkBoxMoreInfo.Visible = false;


            polSourceSiteInputToolHelper = new PolSourceSiteInputToolHelper(panelViewAndEdit, panelPolSourceSite, LanguageEnum.en);
            polSourceSiteInputToolHelper.UpdateStatus += polSourceSiteInputToolHelper_UpdateStatus;
            polSourceSiteInputToolHelper.UpdateRTBMessage += polSourceSiteInputToolHelper_UpdateRTBMessage;
            polSourceSiteInputToolHelper.UpdateRTBFileName += polSourceSiteInputToolHelper_UpdateRTBFileName;
            polSourceSiteInputToolHelper.subsectorDoc = new SubsectorDoc();
            polSourceSiteInputToolHelper.municipalityDoc = new MunicipalityDoc();

            polSourceSiteInputToolHelper.tvItemModelProvinceList = new List<TVItemModel>();
            polSourceSiteInputToolHelper.tvItemModelSubsectorList = new List<TVItemModel>();
            polSourceSiteInputToolHelper.tvItemModelMunicipalityList = new List<TVItemModel>();
            comboBoxProvince.ValueMember = "TVItemID";
            comboBoxProvince.DisplayMember = "TVText";
            comboBoxSubsectorOrMunicipality.ValueMember = "TVItemID";
            comboBoxSubsectorOrMunicipality.DisplayMember = "TVText";

            ClearAllPanelAndComboBoxes();

            DirectoryInfo di = new DirectoryInfo(@"C:\PollutionSourceSites\");

            if (!di.Exists)
            {
                try
                {
                    di.Create();
                }
                catch (Exception ex)
                {
                    richTextBoxStatus.Text = ex.Message + (ex.InnerException != null ? " InnerException = " + ex.InnerException.Message : "");
                    return;
                }
            }

            di = new DirectoryInfo(@"C:\PollutionSourceSites\Subsectors\");

            if (!di.Exists)
            {
                try
                {
                    di.Create();
                }
                catch (Exception ex)
                {
                    richTextBoxStatus.Text = ex.Message + (ex.InnerException != null ? " InnerException = " + ex.InnerException.Message : "");
                    return;
                }
            }

            di = new DirectoryInfo(@"C:\PollutionSourceSites\Documentations\");

            if (!di.Exists)
            {
                try
                {
                    di.Create();
                }
                catch (Exception ex)
                {
                    richTextBoxStatus.Text = ex.Message + (ex.InnerException != null ? " InnerException = " + ex.InnerException.Message : "");
                    return;
                }
            }

            di = new DirectoryInfo(@"C:\PollutionSourceSites\Infrastructures\");

            if (!di.Exists)
            {
                try
                {
                    di.Create();
                }
                catch (Exception ex)
                {
                    richTextBoxStatus.Text = ex.Message + (ex.InnerException != null ? " InnerException = " + ex.InnerException.Message : "");
                    return;
                }
            }

            RefreshComboBoxSubsectorOrMunicipality();
        }
        private bool TryToCreateTheInfrastructureDirectory()
        {
            TVItemModel tvItemModelInfrastructure = (TVItemModel)comboBoxSubsectorOrMunicipality.SelectedItem;
            if (tvItemModelInfrastructure == null || tvItemModelInfrastructure.TVItemID == 0)
            {
                richTextBoxStatus.Text = "Need to select a municipality first \r\n";
                return false;
            }

            string SubsectorText = tvItemModelInfrastructure.TVText;

            DirectoryInfo di = new DirectoryInfo(@"C:\PollutionSourceSites\Infrastructures\" + SubsectorText + @"\");

            if (!di.Exists)
            {
                try
                {
                    di.Create();
                }
                catch (Exception ex)
                {
                    richTextBoxStatus.Text = ex.Message + (ex.InnerException != null ? " InnerException = " + ex.InnerException.Message : "");
                    return false;
                }
            }

            return true;
        }
        private bool TryToCreateThePolSourceSiteDirectory()
        {
            TVItemModel tvItemModelSS = (TVItemModel)comboBoxSubsectorOrMunicipality.SelectedItem;
            if (tvItemModelSS == null || tvItemModelSS.TVItemID == 0)
            {
                richTextBoxStatus.Text = "Need to select a subsector first \r\n";
                return false;
            }

            string SubsectorText = tvItemModelSS.TVText;
            if (SubsectorText.Contains(" "))
            {
                SubsectorText = SubsectorText.Substring(0, SubsectorText.IndexOf(" "));
            }

            DirectoryInfo di = new DirectoryInfo(@"C:\PollutionSourceSites\Subsectors\" + SubsectorText + @"\");

            if (!di.Exists)
            {
                try
                {
                    di.Create();
                }
                catch (Exception ex)
                {
                    richTextBoxStatus.Text = ex.Message + (ex.InnerException != null ? " InnerException = " + ex.InnerException.Message : "");
                    return false;
                }
            }

            return true;
        }

        #endregion Functions private
    }
}
