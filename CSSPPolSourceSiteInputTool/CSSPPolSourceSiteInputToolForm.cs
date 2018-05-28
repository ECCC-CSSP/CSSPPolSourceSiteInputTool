using CSSPEnumsDLL.Enums;
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
        private void butPSSAdd_Click(object sender, EventArgs e)
        {
            polSourceSiteInputToolHelper.PSSAdd();
            polSourceSiteInputToolHelper.SaveSubsectorTextFile();
            polSourceSiteInputToolHelper.RedrawPolSourceSiteList();
        }
        private void butViewKMLFile_Click(object sender, EventArgs e)
        {
            textBoxEmpty.Focus();
            if (polSourceSiteInputToolHelper != null)
            {
                lblStatus.Text = "Regenerating Subsector KML File ...";
                polSourceSiteInputToolHelper.RegenerateSubsectorKMLFile();
                lblStatus.Text = "Subsector KML File was regenerated ...";

                lblStatus.Text = "Opening Google Earth";
                polSourceSiteInputToolHelper.ViewKMLFileInGoogleEarth();
                lblStatus.Text = "";
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
        private void checkBoxInfrastructure_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShowInfrastructure.Checked)
            {
                ShowInfrastructureParts();
            }
            else
            {
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

            polSourceSiteInputToolHelper.ReDraw();

        }
        private void comboBoxSubsectorOrMunicipality_SelectedIndexChanged(object sender, EventArgs e)
        {
            polSourceSiteInputToolHelper.CurrentSubsectorName = (string)comboBoxSubsectorOrMunicipality.SelectedItem;
            polSourceSiteInputToolHelper.CurrentPSS = null;
            polSourceSiteInputToolHelper.PolSourceSiteTVItemID = 0;
            polSourceSiteInputToolHelper.RedrawPolSourceSiteList();
            panelViewAndEdit.Controls.Clear();
            polSourceSiteInputToolHelper.CurrentPSS = null;
            polSourceSiteInputToolHelper.ReDraw();

            if (polSourceSiteInputToolHelper.subsectorDoc.Subsector != null)
            {
                lblSubsectorOrMunicipality.Text = $"{polSourceSiteInputToolHelper.subsectorDoc.Subsector.SubsectorName}";
            }
        }
        private void comboBoxSubsectorOrMunicipalityAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {
            polSourceSiteInputToolHelper.CurrentSubsectorName = ((TVItemModel)comboBoxSubsectorOrMunicipalityAdmin.SelectedItem).TVText;
            if (polSourceSiteInputToolHelper.CurrentSubsectorName.Contains(" "))
            {
                polSourceSiteInputToolHelper.CurrentSubsectorName = polSourceSiteInputToolHelper.CurrentSubsectorName.Substring(0, polSourceSiteInputToolHelper.CurrentSubsectorName.IndexOf(" "));
            }
            polSourceSiteInputToolHelper.CurrentPSS = null;
            polSourceSiteInputToolHelper.PolSourceSiteTVItemID = 0;
            panelViewAndEdit.Controls.Clear();
            panelPolSourceSite.Controls.Clear();
            ComboBoxSubsectorOrMunicipalitySelectedIndexChanged();

            FileInfo fi = new FileInfo($@"{polSourceSiteInputToolHelper.BasePathPollutionSourceSites}{polSourceSiteInputToolHelper.CurrentSubsectorName}\{polSourceSiteInputToolHelper.CurrentSubsectorName}.txt");

            if (!fi.Exists)
            {
                lblStatus.Text = $"{fi.FullName} does not exist.";
                if (polSourceSiteInputToolHelper.IsPolSourceSite)
                {
                    panelCreateSubsectorDirectory.Visible = true;
                }
                else
                {
                    panelCreateMunicipalityDirectory.Visible = true;
                }
            }
            else
            {
                polSourceSiteInputToolHelper.RedrawPolSourceSiteList();
                polSourceSiteInputToolHelper.ReDraw();

                if (polSourceSiteInputToolHelper.subsectorDoc.Subsector != null)
                {
                    lblSubsectorOrMunicipality.Text = $"{polSourceSiteInputToolHelper.subsectorDoc.Subsector.SubsectorName}";
                }
            }
        }
        private void polSourceSiteInputToolHelper_UpdateStatus(object sender, PolSourceSiteInputToolHelper.StatusEventArgs e)
        {
            lblStatus.Text = e.Status;
            lblStatus.Refresh();
            Application.DoEvents();
        }
        private void comboBoxProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxEmpty.Focus();
            ComboBoxProvinceNamesSelectedIndexChanged();
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
        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {
            if (textBoxPassword.Text == "mt")
            {
                ShowAdminParts();
            }
        }
        #endregion Events

        #region Functions private
        private void ComboBoxProvinceNamesSelectedIndexChanged()
        {
            comboBoxSubsectorOrMunicipalityAdmin.Items.Clear();

            TVItemModel tvItemModelProv = (TVItemModel)comboBoxProvince.SelectedItem;
            if (tvItemModelProv != null && tvItemModelProv.TVItemID != 0)
            {
                GetTVItemModelSubsectorList(tvItemModelProv.TVItemID);

                if (polSourceSiteInputToolHelper.tvItemModelSubsectorOrMunicipalityList.Count > 0)
                {
                    foreach (TVItemModel tvItemModel in polSourceSiteInputToolHelper.tvItemModelSubsectorOrMunicipalityList)
                    {
                        comboBoxSubsectorOrMunicipalityAdmin.Items.Add(tvItemModel);
                    }
                    if (comboBoxSubsectorOrMunicipalityAdmin.Items.Count > 0)
                    {
                        comboBoxSubsectorOrMunicipalityAdmin.SelectedIndex = 0;
                    }
                }
            }
        }
        private void ComboBoxSubsectorOrMunicipalitySelectedIndexChanged()
        {
            TVItemModel tvItemModelSS = (TVItemModel)comboBoxSubsectorOrMunicipalityAdmin.SelectedItem;
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
        }
        private void CreateInfrastructureDirectoryWithInfo()
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
            polSourceSiteInputToolHelper.RegenerateSubsectorKMLFile();

            polSourceSiteInputToolHelper.DrawPanelPSS();
            polSourceSiteInputToolHelper.ReDraw();
            butViewKMLFile.Enabled = true;

            lblStatus.Text = $"Done ... {polSourceSiteInputToolHelper.CurrentSubsectorName} directory now exist";
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
            polSourceSiteInputToolHelper.ReDraw();
            butViewKMLFile.Enabled = true;

            lblStatus.Text = $"Done ... {polSourceSiteInputToolHelper.CurrentSubsectorName} directory now exist";
            lblStatus.Refresh();
            Application.DoEvents();

        }
        private void FillComboBoxSubsectorOrMunicipality()
        {
            comboBoxSubsectorOrMunicipality.Items.Clear();

            foreach (string subsector in polSourceSiteInputToolHelper.SubDirectoryOrMunicipalityList)
            {
                comboBoxSubsectorOrMunicipality.Items.Add(subsector);
            }

            if (comboBoxSubsectorOrMunicipality.Items.Count > 0)
            {
                comboBoxSubsectorOrMunicipality.SelectedIndex = 0;
            }

            //checkBoxLanguage.Focus();
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
            //if (MunicipalityText.Contains(" "))
            //{
            //    MunicipalityText = MunicipalityText.Substring(0, MunicipalityText.IndexOf(" "));
            //}

            polSourceSiteInputToolHelper.CurrentSubsectorName = MunicipalityText;
            polSourceSiteInputToolHelper.ReadPollutionSourceSitesSubsectorFile();

            DirectoryInfo di = new DirectoryInfo($@"{polSourceSiteInputToolHelper.BasePathInfrastructures}\{polSourceSiteInputToolHelper.CurrentSubsectorName}\Pictures\");

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
                    FileInfo fi = new FileInfo($@"{polSourceSiteInputToolHelper.BasePathInfrastructures}\{polSourceSiteInputToolHelper.CurrentSubsectorName}\Pictures\{pss.SiteNumberText}_{picture.PictureTVItemID}{fiTemp.Extension}");

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
            polSourceSiteInputToolHelper.ReadPollutionSourceSitesSubsectorFile();

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
            //if (SubsectorText.Contains(" "))
            //{
            //    SubsectorText = SubsectorText.Substring(0, SubsectorText.IndexOf(" "));
            //}


            FileInfo fi = new FileInfo(@"C:\Infrastructures\" + MunicipalityText + @"\" + MunicipalityText + ".txt");

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


            FileInfo fi = new FileInfo(@"C:\PollutionSourceSites\" + SubsectorText + @"\" + SubsectorText + ".txt");

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
        private void GetTVItemModelSubsectorList(int ProvinceTVItemID)
        {
            polSourceSiteInputToolHelper.tvItemModelSubsectorOrMunicipalityList = new List<TVItemModel>();

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
                            polSourceSiteInputToolHelper.tvItemModelSubsectorOrMunicipalityList = JsonConvert.DeserializeObject<List<TVItemModel>>(json_data);
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
        private void RefreshComboBoxSubsectorOrMunicipalityAdmin()
        {
            if (polSourceSiteInputToolHelper.IsPolSourceSite)
            {
                polSourceSiteInputToolHelper.SubDirectoryOrMunicipalityList = new List<string>();
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
                    polSourceSiteInputToolHelper.SubDirectoryOrMunicipalityList.Add(directoryInfo.Name);
                }

                FillComboBoxSubsectorOrMunicipality();
            }
            else
            {
                polSourceSiteInputToolHelper.SubDirectoryOrMunicipalityList = new List<string>();
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
                    polSourceSiteInputToolHelper.SubDirectoryOrMunicipalityList.Add(directoryInfo.Name);
                }

                FillComboBoxSubsectorOrMunicipality();
            }
        }
        private void ShowAdminPasswordParts()
        {
            panelProvinces.Visible = false;
            panelSubsectorOrMunicipality.Visible = false;
            panelSubsectorOrMunicipalityAdmin.Visible = false;
            panelViewKMLFileTop.Enabled = false;
            panelAdminPassword.Visible = true;
            textBoxPassword.Text = "";
            textBoxPassword.Focus();
            panelAddNewPollutionSourceSite.Visible = false;
            panelShowInputOptions.Visible = false;
            panelPolSourceSite.Controls.Clear();
            panelViewAndEdit.Controls.Clear();
            panelCreateSubsectorDirectory.Visible = false;
            panelCreateMunicipalityDirectory.Visible = false;
        }
        private void ShowAdminParts()
        {
            polSourceSiteInputToolHelper.IsAdmin = true;
            panelProvinces.Visible = true;
            panelSubsectorOrMunicipality.Visible = false;
            panelSubsectorOrMunicipalityAdmin.Visible = true;
            panelViewKMLFileTop.Enabled = true;
            panelAdminPassword.Visible = false;
            panelAddNewPollutionSourceSite.Visible = false;
            panelShowInputOptions.Visible = false;
            polSourceSiteInputToolHelper.CurrentPSS = null;
            polSourceSiteInputToolHelper.PolSourceSiteTVItemID = 0;
            polSourceSiteInputToolHelper.RedrawPolSourceSiteList();
            panelCreateSubsectorDirectory.Visible = false;
            panelCreateMunicipalityDirectory.Visible = false;

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

            //RefreshComboBoxSubsectorOrMunicipalityAdmin();

        }
        private void ShowInputParts()
        {
            polSourceSiteInputToolHelper.IsAdmin = false;
            polSourceSiteInputToolHelper.IsEditing = false;
            polSourceSiteInputToolHelper.ShowPolSourceSiteDetails = true;
            panelProvinces.Visible = false;
            panelSubsectorOrMunicipality.Visible = true;
            panelSubsectorOrMunicipalityAdmin.Visible = false;
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
        }
        private void ShowInfrastructureParts()
        {
            polSourceSiteInputToolHelper.IsPolSourceSite = false;
            panelCreateMunicipalityDirectory.Visible = true;
            panelCreateSubsectorDirectory.Visible = false;
            RefreshComboBoxSubsectorOrMunicipalityAdmin();
        }
        private void ShowPollutionSourceSiteParts()
        {
            polSourceSiteInputToolHelper.IsPolSourceSite = true;
            panelCreateMunicipalityDirectory.Visible = false;
            panelCreateSubsectorDirectory.Visible = true;
            RefreshComboBoxSubsectorOrMunicipalityAdmin();
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
            panelShowInput.Visible = false;
            panelAdminPassword.Visible = false;
            panelSubsectorOrMunicipality.Dock = DockStyle.Fill;
            panelSubsectorOrMunicipalityAdmin.Visible = false;
            panelSubsectorOrMunicipalityAdmin.Dock = DockStyle.Fill;
            panelViewAndEdit.Dock = DockStyle.Fill;
            panelPolSourceSite.Dock = DockStyle.Fill;
            textBoxEmpty.Width = 1;
            textBoxEmpty.Height = 1;

            polSourceSiteInputToolHelper = new PolSourceSiteInputToolHelper(panelViewAndEdit, panelPolSourceSite, LanguageEnum.en);
            polSourceSiteInputToolHelper.UpdateStatus += polSourceSiteInputToolHelper_UpdateStatus;
            polSourceSiteInputToolHelper.subsectorDoc = new SubsectorDoc();
            polSourceSiteInputToolHelper.municipalityDoc = new MunicipalityDoc();

            polSourceSiteInputToolHelper.tvItemModelProvinceList = new List<TVItemModel>();
            polSourceSiteInputToolHelper.tvItemModelSubsectorOrMunicipalityList = new List<TVItemModel>();
            comboBoxProvince.ValueMember = "TVItemID";
            comboBoxProvince.DisplayMember = "TVText";
            comboBoxSubsectorOrMunicipalityAdmin.ValueMember = "TVItemID";
            comboBoxSubsectorOrMunicipalityAdmin.DisplayMember = "TVText";

            RefreshComboBoxSubsectorOrMunicipalityAdmin();

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

            di = new DirectoryInfo(@"C:\Infrastructures\");

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

            DirectoryInfo di = new DirectoryInfo(@"C:\Infrastructures\" + SubsectorText + @"\");

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

            DirectoryInfo di = new DirectoryInfo(@"C:\PollutionSourceSites\" + SubsectorText + @"\");

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
