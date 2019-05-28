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
        #region Variables
        #endregion Variables

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
        private void butFix_Click(object sender, EventArgs e)
        {
            if (polSourceSiteInputToolHelper.IsDirty)
            {
                MessageBox.Show("Please save or cancel before changing page.", "Some changes have not been saved yet", MessageBoxButtons.OK);
                return;
            }

            butFix.Text = "Working ...";
            butFix.Refresh();
            Application.DoEvents();
            polSourceSiteInputToolHelper.Fix();
            butFix.Text = "Fix";
        }
        private void butReduceHelp_Click(object sender, EventArgs e)
        {
            splitContainer2.SplitterDistance = splitContainer2.Height * 9 / 10;
            butReduceHelp.Visible = false;

        }

        private void butUsedMunicipalitySelect_Click(object sender, EventArgs e)
        {
            foreach (Control control in panelViewAndEdit.Controls)
            {
                if (control.Name == "textBoxMunicipality")
                {
                    ((TextBox)control).Text = (string)comboBoxUsedMunicipalities.SelectedItem;
                    if (((TextBox)control).Text == "None")
                    {
                        ((TextBox)control).Text = "";
                    }
                }
            }
            panelMunicipalities.SendToBack();
        }

        private void butStreetTypeSelect_Click(object sender, EventArgs e)
        {
            foreach (Control control in panelViewAndEdit.Controls)
            {
                if (control.Name == "textBoxStreetType")
                {
                    ((TextBox)control).Text = (string)comboBoxStreetType.SelectedItem;
                    if (((TextBox)control).Text == "None")
                    {
                        ((TextBox)control).Text = "";
                    }
                }
            }
            panelStreetType.SendToBack();
        }
        private void butStreetTypeCancel_Click(object sender, EventArgs e)
        {
            panelStreetType.SendToBack();
        }
        private void butProvinceMunicipalitySelect_Click(object sender, EventArgs e)
        {
            foreach (Control control in panelViewAndEdit.Controls)
            {
                if (control.Name == "textBoxMunicipality")
                {
                    ((TextBox)control).Text = (string)comboBoxProvinceMunicipalities.SelectedItem;
                    if (((TextBox)control).Text == "None")
                    {
                        ((TextBox)control).Text = "";
                    }
                }
            }
            panelMunicipalities.SendToBack();
        }

        private void butMunicipalityCancel_Click(object sender, EventArgs e)
        {
            panelMunicipalities.SendToBack();
        }
        private void butPSSAdd_Click(object sender, EventArgs e)
        {
            if (polSourceSiteInputToolHelper.IsDirty)
            {
                MessageBox.Show("Please save or cancel before changing page.", "Some changes have not been saved yet", MessageBoxButtons.OK);
                return;
            }

            polSourceSiteInputToolHelper.PSSAdd();
            polSourceSiteInputToolHelper.SaveSubsectorTextFile();
            polSourceSiteInputToolHelper.RedrawPolSourceSiteList();
        }
        private void butInfrastructureAdd_Click(object sender, EventArgs e)
        {
            if (polSourceSiteInputToolHelper.IsDirty)
            {
                MessageBox.Show("Please save or cancel before changing page.", "Some changes have not been saved yet", MessageBoxButtons.OK);
                return;
            }

            polSourceSiteInputToolHelper.InfrastructureAdd();
            polSourceSiteInputToolHelper.SaveMunicipalityTextFile();
            polSourceSiteInputToolHelper.RedrawInfrastructureList();
        }
        private void butViewKMLFile_Click(object sender, EventArgs e)
        {
            if (polSourceSiteInputToolHelper.IsDirty)
            {
                MessageBox.Show("Please save or cancel before changing page.", "Some changes have not been saved yet", MessageBoxButtons.OK);
                return;
            }

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
            if (polSourceSiteInputToolHelper.IsDirty)
            {
                if (checkBoxShowAdmin.Checked)
                {
                    MessageBox.Show("Please save or cancel before changing page.", "Some changes have not been saved yet", MessageBoxButtons.OK);
                    checkBoxShowAdmin.Checked = false;
                }
                return;
            }

            panelPolSourceSite.Controls.Clear();

            polSourceSiteInputToolHelper.AdminEmail = "";


            if (checkBoxShowAdmin.Checked)
            {
                switch (Environment.UserName.ToLower())
                {
                    case "charles":
                        {
                            polSourceSiteInputToolHelper.AdminEmail = "Charles.LeBlanc2@canada.ca";
                        }
                        break;
                    case "leblancc":
                        {
                            polSourceSiteInputToolHelper.AdminEmail = "Charles.LeBlanc2@canada.ca";
                        }
                        break;
                    case "pomeroyj":
                        {
                            polSourceSiteInputToolHelper.AdminEmail = "Joe.Pomeroy@canada.ca";
                        }
                        break;
                    case "perchardg":
                        {
                            polSourceSiteInputToolHelper.AdminEmail = "Greg.Perchard@canada.ca";
                        }
                        break;
                    case "martellk":
                        {
                            polSourceSiteInputToolHelper.AdminEmail = "Karyne.Martell2@canada.ca";
                        }
                        break;
                    default:
                        break;
                }
                ClearAllPanelAndComboBoxes();
                Connect();
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
        private void checkBoxDeletedIssueAndPicture_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDeletedIssueAndPicture.Checked)
            {
                polSourceSiteInputToolHelper.DeletedIssueAndPicture = true;
            }
            else
            {
                polSourceSiteInputToolHelper.DeletedIssueAndPicture = false;
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
            if (polSourceSiteInputToolHelper.IsDirty)
            {
                if (checkBoxShowInfrastructure.Checked)
                {
                    MessageBox.Show("Please save or cancel before changing page.", "Some changes have not been saved yet", MessageBoxButtons.OK);
                    checkBoxShowInfrastructure.Checked = false;
                }
                return;
            }

            panelPolSourceSite.Controls.Clear();

            if (checkBoxShowInfrastructure.Checked)
            {
                checkBoxMoreInfo.Visible = false;
                if (radioButtonIssues.Checked)
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
        private void checkBoxNewIssue_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxNewIssue.Checked)
            {
                polSourceSiteInputToolHelper.NewIssue = true;
            }
            else
            {
                polSourceSiteInputToolHelper.NewIssue = false;
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
        private void checkBoxOldIssueText_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxOldIssueText.Checked)
            {
                polSourceSiteInputToolHelper.OldIssueText = true;
            }
            else
            {
                polSourceSiteInputToolHelper.OldIssueText = false;
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
        private void checkBoxOldIssue_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxOldIssue.Checked)
            {
                polSourceSiteInputToolHelper.OldIssue = true;
            }
            else
            {
                polSourceSiteInputToolHelper.OldIssue = false;
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
        private void checkBoxWrittenDescription_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxWrittenDescription.Checked)
            {
                polSourceSiteInputToolHelper.WrittenDescription = true;
            }
            else
            {
                polSourceSiteInputToolHelper.WrittenDescription = false;
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
            if (polSourceSiteInputToolHelper.IsDirty)
            {
                MessageBox.Show("Please save or cancel before changing page.", "Some changes have not been saved yet", MessageBoxButtons.OK);
                return;
            }

            textBoxEmpty.Focus();
            ComboBoxProvinceNamesSelectedIndexChanged();
        }
        private void comboBoxSubsectorOrMunicipality_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (polSourceSiteInputToolHelper.IsDirty)
            {
                MessageBox.Show("Please save or cancel before changing page.", "Some changes have not been saved yet", MessageBoxButtons.OK);
                return;
            }

            textBoxEmpty.Focus();
            ComboBoxSubsectorOrMunicipalitySelectedIndexChanged();
        }
        private void polSourceSiteInputToolHelper_UpdateStatus(object sender, PolSourceSiteInputToolHelper.StatusEventArgs e)
        {
            lblStatus.Text = e.Status;
            lblStatus.Refresh();
            Application.DoEvents();
        }
        private void polSourceSiteInputToolHelper_UpdateRTBClear(object sender, PolSourceSiteInputToolHelper.RTBClearEventArgs e)
        {
            richTextBoxStatus.BringToFront();
            richTextBoxStatus.Text = "";
        }
        private void polSourceSiteInputToolHelper_UpdateRTBFileName(object sender, PolSourceSiteInputToolHelper.RTBFileNameEventArgs e)
        {
            lblStatus.Text = $"Loading file [{e.FileName}]";
            lblStatus.Refresh();
            Application.DoEvents();

            richTextBoxStatus.Clear();
            string FileName = e.FileName.Replace("(", "_").Replace(")", "_").Replace("\\", "_").Replace("/", "_").Replace(".", "_").Replace(" ", "_");

            FileInfo fi = new FileInfo(FileName);
            if (fi.Exists)
            {
                webBrowserDocument.BringToFront();
                webBrowserDocument.Navigate($@"C:/PollutionSourceSites/Documentations/" + $"{FileName}.html");
            }
            else
            {
                richTextBoxStatus.BringToFront();
                richTextBoxStatus.Text = "Could not find " + $@"C:/PollutionSourceSites/Documentations/" + $"{FileName}.html";
            }

            if (splitContainer2.Panel2.Height < panelViewAndEdit.Height * 1 / 3)
            {
                splitContainer2.SplitterDistance = panelViewAndEdit.Height * 2 / 3;
                butReduceHelp.Visible = true;
            }
        }
        private void polSourceSiteInputToolHelper_UpdateRTBMessage(object sender, PolSourceSiteInputToolHelper.RTBMessageEventArgs e)
        {
            lblStatus.Text = e.Message;
            lblStatus.Refresh();
            Application.DoEvents();

            richTextBoxStatus.BringToFront();
            richTextBoxStatus.AppendText(e.Message);
        }
        private void radioButtonDetails_CheckedChanged(object sender, EventArgs e)
        {
            if (polSourceSiteInputToolHelper.IsDirty)
            {
                if (radioButtonDetails.Checked)
                {
                    if (polSourceSiteInputToolHelper.OnIssuePage || polSourceSiteInputToolHelper.OnMapPage || polSourceSiteInputToolHelper.OnPicturePage)
                    {
                        if (polSourceSiteInputToolHelper.OnIssuePage)
                        {
                            radioButtonIssues.Checked = true;
                        }
                        if (polSourceSiteInputToolHelper.OnMapPage)
                        {
                            radioButtonShowMap.Checked = true;
                        }
                        if (polSourceSiteInputToolHelper.OnPicturePage)
                        {
                            radioButtonPictures.Checked = true;
                        }
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Please save or cancel before changing page.", "Some changes have not been saved yet", MessageBoxButtons.OK);
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
            polSourceSiteInputToolHelper.OnDetailPage = true;
            polSourceSiteInputToolHelper.OnIssuePage = false;
            polSourceSiteInputToolHelper.OnMapPage = false;
            polSourceSiteInputToolHelper.OnPicturePage = false;

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

        private void radioButtonIssues_CheckedChanged(object sender, EventArgs e)
        {
            if (polSourceSiteInputToolHelper.IsDirty)
            {
                if (radioButtonIssues.Checked)
                {
                    if (polSourceSiteInputToolHelper.OnDetailPage || polSourceSiteInputToolHelper.OnMapPage || polSourceSiteInputToolHelper.OnPicturePage)
                    {
                        if (polSourceSiteInputToolHelper.OnDetailPage)
                        {
                            radioButtonDetails.Checked = true;
                        }
                        if (polSourceSiteInputToolHelper.OnMapPage)
                        {
                            radioButtonShowMap.Checked = true;
                        }
                        if (polSourceSiteInputToolHelper.OnPicturePage)
                        {
                            radioButtonPictures.Checked = true;
                        }
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Please save or cancel before changing page.", "Some changes have not been saved yet", MessageBoxButtons.OK);
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
            polSourceSiteInputToolHelper.OnDetailPage = false;
            polSourceSiteInputToolHelper.OnIssuePage = true;
            polSourceSiteInputToolHelper.OnMapPage = false;
            polSourceSiteInputToolHelper.OnPicturePage = false;

            polSourceSiteInputToolHelper.ShowPolSourceSiteDetails = false;
            polSourceSiteInputToolHelper.ShowOnlyIssues = true;
            polSourceSiteInputToolHelper.ShowOnlyPictures = false;
            polSourceSiteInputToolHelper.ShowOnlyMap = false;

            if (polSourceSiteInputToolHelper.IsEditing && radioButtonIssues.Checked)
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
            if (polSourceSiteInputToolHelper.IsDirty)
            {
                if (radioButtonShowMap.Checked)
                {
                    if (polSourceSiteInputToolHelper.OnDetailPage || polSourceSiteInputToolHelper.OnIssuePage || polSourceSiteInputToolHelper.OnPicturePage)
                    {
                        if (polSourceSiteInputToolHelper.OnDetailPage)
                        {
                            radioButtonDetails.Checked = true;
                        }
                        if (polSourceSiteInputToolHelper.OnIssuePage)
                        {
                            radioButtonIssues.Checked = true;
                        }
                        if (polSourceSiteInputToolHelper.OnPicturePage)
                        {
                            radioButtonPictures.Checked = true;
                        }
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Please save or cancel before changing page.", "Some changes have not been saved yet", MessageBoxButtons.OK);
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
            polSourceSiteInputToolHelper.OnDetailPage = false;
            polSourceSiteInputToolHelper.OnIssuePage = false;
            polSourceSiteInputToolHelper.OnMapPage = true;
            polSourceSiteInputToolHelper.OnPicturePage = false;

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
        private void radioButtonPictures_CheckedChanged(object sender, EventArgs e)
        {
            if (polSourceSiteInputToolHelper.IsDirty)
            {
                if (radioButtonPictures.Checked)
                {
                    if (polSourceSiteInputToolHelper.OnDetailPage || polSourceSiteInputToolHelper.OnIssuePage || polSourceSiteInputToolHelper.OnMapPage)
                    {
                        if (polSourceSiteInputToolHelper.OnDetailPage)
                        {
                            radioButtonDetails.Checked = true;
                        }
                        if (polSourceSiteInputToolHelper.OnIssuePage)
                        {
                            radioButtonIssues.Checked = true;
                        }
                        if (polSourceSiteInputToolHelper.OnMapPage)
                        {
                            radioButtonShowMap.Checked = true;
                        }
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Please save or cancel before changing page.", "Some changes have not been saved yet", MessageBoxButtons.OK);
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
            polSourceSiteInputToolHelper.OnDetailPage = false;
            polSourceSiteInputToolHelper.OnIssuePage = false;
            polSourceSiteInputToolHelper.OnMapPage = false;
            polSourceSiteInputToolHelper.OnPicturePage = true;

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
                    radioButtonIssues.Visible = true;

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
                            butCreateSubsectorDirectory.Enabled = false;
                        }
                        else
                        {
                            butViewKMLFile.Enabled = false;
                            butCreateSubsectorDirectory.Enabled = true;
                        }
                    }
                    polSourceSiteInputToolHelper.RedrawPolSourceSiteList();
                    polSourceSiteInputToolHelper.ReDrawPolSourceSite();
                }
                else
                {
                    panelCreateMunicipalityDirectory.Visible = true;
                    radioButtonIssues.Visible = false;

                    TVItemModel tvItemModelMuni = (TVItemModel)comboBoxSubsectorOrMunicipality.SelectedItem;
                    if (tvItemModelMuni != null && tvItemModelMuni.TVItemID != 0)
                    {
                        polSourceSiteInputToolHelper.CurrentMunicipalityName = tvItemModelMuni.TVText;

                        FileInfo fi = new FileInfo($@"{polSourceSiteInputToolHelper.BasePathInfrastructures}{polSourceSiteInputToolHelper.CurrentMunicipalityName}\{polSourceSiteInputToolHelper.CurrentMunicipalityName}.txt");

                        if (fi.Exists)
                        {
                            butViewKMLFile.Enabled = true;
                            butCreateMunicipalityDirectory.Enabled = false;
                        }
                        else
                        {
                            butViewKMLFile.Enabled = false;
                            butCreateMunicipalityDirectory.Enabled = true;
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
                    radioButtonIssues.Visible = true;

                    polSourceSiteInputToolHelper.CurrentMunicipalityName = null;
                    polSourceSiteInputToolHelper.CurrentSubsectorName = (string)comboBoxSubsectorOrMunicipality.SelectedItem;
                    polSourceSiteInputToolHelper.RedrawPolSourceSiteList();
                    polSourceSiteInputToolHelper.ReDrawPolSourceSite();
                    butViewKMLFile.Enabled = true;
                }
                else
                {
                    panelAddNewInfrastructure.Visible = true;
                    radioButtonIssues.Visible = false;

                    polSourceSiteInputToolHelper.CurrentSubsectorName = null;
                    polSourceSiteInputToolHelper.CurrentMunicipalityName = (string)comboBoxSubsectorOrMunicipality.SelectedItem;
                    polSourceSiteInputToolHelper.RedrawInfrastructureList();
                    polSourceSiteInputToolHelper.ReDrawInfrastructure();
                    butViewKMLFile.Enabled = true;
                }
            }
        }
        public void Connect()
        {
            string ret = polSourceSiteInputToolHelper.UserExistInCSSPWebTools(polSourceSiteInputToolHelper.AdminEmail);
            ret = ret.Replace("\"", "");
            if (ret.StartsWith("ERROR:"))
            {
                MessageBox.Show("Admin users list [pomeroyj, martellk, perchardg]\r\n\r\nPlease contact Joe Pomeroy or Karyne Martell if you think you should have admin rights", "Invalid user for admin rights");
            }
            else
            {
                ShowAdminParts();
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

            polSourceSiteInputToolHelper.InfrastructureTVItemID = 0;
            polSourceSiteInputToolHelper.PolSourceSiteTVItemID = 0;
            polSourceSiteInputToolHelper.CurrentInfrastructure = null;
            polSourceSiteInputToolHelper.CurrentPSS = null;
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
                richTextBoxStatus.BringToFront();
                richTextBoxStatus.Text = "Need to select a municipality first \r\n";
                return false;
            }

            string MunicipalityText = tvItemModelMunicipality.TVText;

            polSourceSiteInputToolHelper.CurrentSubsectorName = MunicipalityText;
            if (!polSourceSiteInputToolHelper.ReadInfrastructuresMunicipalityFile())
            {
                richTextBoxStatus.BringToFront();
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
                    richTextBoxStatus.BringToFront();
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
                        richTextBoxStatus.BringToFront();
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
                richTextBoxStatus.BringToFront();
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
                richTextBoxStatus.BringToFront();
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
                    richTextBoxStatus.BringToFront();
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
                        richTextBoxStatus.BringToFront();
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
                richTextBoxStatus.BringToFront();
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
                richTextBoxStatus.BringToFront();
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
                richTextBoxStatus.BringToFront();
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
                richTextBoxStatus.BringToFront();
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
                richTextBoxStatus.BringToFront();
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
                richTextBoxStatus.BringToFront();
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
                richTextBoxStatus.BringToFront();
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
                        if (checkBoxShowAdmin.Checked)
                        {
                            panelCreateSubsectorDirectory.Visible = true;
                        }
                        else
                        {
                            panelAddNewPollutionSourceSite.Visible = true;
                        }
                        comboBoxSubsectorOrMunicipality.SelectedIndex = 0;
                    }
                    else
                    {
                        panelAddNewPollutionSourceSite.Visible = false;
                        panelCreateSubsectorDirectory.Visible = false;
                        polSourceSiteInputToolHelper.DrawPanelPSS();
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
                        if (checkBoxShowAdmin.Checked)
                        {
                            panelCreateMunicipalityDirectory.Visible = true;
                        }
                        else
                        {
                            panelAddNewInfrastructure.Visible = true;
                        }
                        comboBoxSubsectorOrMunicipality.SelectedIndex = 0;
                    }
                    else
                    {
                        panelCreateMunicipalityDirectory.Visible = false;
                        panelAddNewInfrastructure.Visible = false;
                        polSourceSiteInputToolHelper.DrawPanelInfrastructures();
                    }
                }
            }
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
                richTextBoxStatus.BringToFront();
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

            polSourceSiteInputToolHelper.IsPolSourceSite = false;

            comboBoxProvince.Items.Clear();
            comboBoxProvince.Text = "";
            comboBoxSubsectorOrMunicipality.Items.Clear();
            comboBoxSubsectorOrMunicipality.Text = "";

            panelAddNewPollutionSourceSite.Visible = false;
            panelAddNewInfrastructure.Visible = false;
            panelCreateSubsectorDirectory.Visible = false;
            panelCreateMunicipalityDirectory.Visible = false;

            if (polSourceSiteInputToolHelper.IsAdmin)
            {
                comboBoxSubsectorOrMunicipality.ValueMember = "TVItemID";
                comboBoxSubsectorOrMunicipality.DisplayMember = "TVText";
                if (polSourceSiteInputToolHelper.IsPolSourceSite)
                {
                    panelCreateSubsectorDirectory.Visible = true;
                }
                else
                {
                    panelCreateMunicipalityDirectory.Visible = true;
                }

                //GetTVItemModelProvinceList();
                if (polSourceSiteInputToolHelper.tvItemModelProvinceList.Count == 0)
                {
                    richTextBoxStatus.BringToFront();
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

                RefreshComboBoxSubsectorOrMunicipality();
            }
            else
            {
                panelAddNewInfrastructure.Visible = true;

                comboBoxSubsectorOrMunicipality.ValueMember = null;
                comboBoxSubsectorOrMunicipality.DisplayMember = null;

                if (polSourceSiteInputToolHelper.IsPolSourceSite)
                {
                    panelCreateSubsectorDirectory.Visible = true;
                    panelCreateMunicipalityDirectory.Visible = false;
                }
                else
                {
                    panelCreateSubsectorDirectory.Visible = false;
                    panelCreateMunicipalityDirectory.Visible = true;
                }

                RefreshComboBoxSubsectorOrMunicipality();
            }
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
            panelShowAdmin.Visible = false;
            if (Environment.UserName.ToLower() == "charles" ||
                Environment.UserName.ToLower() == "leblancc" ||
                Environment.UserName.ToLower() == "pomeroyj" ||
                Environment.UserName.ToLower() == "perchardg" ||
                Environment.UserName.ToLower() == "martellk")
            {
                panelShowAdmin.Visible = true;
            }
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
            webBrowserDocument.Dock = DockStyle.Fill;
            webBrowserDocument.BringToFront();
            panelLanguage.Visible = false;
            panelProvinces.Visible = false;
            panelCreateMunicipalityDirectory.Visible = false;
            panelCreateSubsectorDirectory.Visible = false;
            panelSubsectorOrMunicipality.Dock = DockStyle.Fill;
            panelViewAndEdit.Dock = DockStyle.Fill;
            panelPolSourceSite.Dock = DockStyle.Fill;
            textBoxEmpty.Width = 1;
            textBoxEmpty.Height = 1;
            panelAddNewPollutionSourceSite.Visible = false;
            panelShowInputOptions.Visible = false;
            radioButtonShowMap.Visible = false;
            checkBoxMoreInfo.Visible = false;


            polSourceSiteInputToolHelper = new PolSourceSiteInputToolHelper(panelViewAndEdit, panelPolSourceSite, panelMunicipalities, panelStreetType, panelShowInputOptions, panelSubsectorOrMunicipality, LanguageEnum.en);
            polSourceSiteInputToolHelper.UpdateStatus += polSourceSiteInputToolHelper_UpdateStatus;
            polSourceSiteInputToolHelper.UpdateRTBClear += polSourceSiteInputToolHelper_UpdateRTBClear;
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
                    richTextBoxStatus.BringToFront();
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
                    richTextBoxStatus.BringToFront();
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
                    richTextBoxStatus.BringToFront();
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
                    richTextBoxStatus.BringToFront();
                    richTextBoxStatus.Text = ex.Message + (ex.InnerException != null ? " InnerException = " + ex.InnerException.Message : "");
                    return;
                }
            }

            RefreshComboBoxSubsectorOrMunicipality();
        }

        private bool TryToCreateTheInfrastructureDirectory()
        {
            TVItemModel tvItemModelMunicipality = (TVItemModel)comboBoxSubsectorOrMunicipality.SelectedItem;
            if (tvItemModelMunicipality == null || tvItemModelMunicipality.TVItemID == 0)
            {
                richTextBoxStatus.BringToFront();
                richTextBoxStatus.Text = "Need to select a municipality first \r\n";
                return false;
            }

            string MunicipalityText = tvItemModelMunicipality.TVText;

            DirectoryInfo di = new DirectoryInfo(@"C:\PollutionSourceSites\Infrastructures\" + MunicipalityText + @"\");

            if (!di.Exists)
            {
                try
                {
                    di.Create();
                }
                catch (Exception ex)
                {
                    richTextBoxStatus.BringToFront();
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
                richTextBoxStatus.BringToFront();
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
                    richTextBoxStatus.BringToFront();
                    richTextBoxStatus.Text = ex.Message + (ex.InnerException != null ? " InnerException = " + ex.InnerException.Message : "");
                    return false;
                }
            }

            return true;
        }


        #endregion Functions private

        private void lblHelp_Click(object sender, EventArgs e)
        {
            FileInfo fi = new FileInfo($@"C:\PollutionSourceSites\Documentations\help.html");
            if (fi.Exists)
            {
                webBrowserDocument.BringToFront();
                webBrowserDocument.Navigate($"{fi.FullName}");
            }
            else
            {
                richTextBoxStatus.BringToFront();
                richTextBoxStatus.Text = "Could not find " + fi.FullName.Replace(@"\", @"/");
            }

            if (splitContainer2.Panel2.Height < panelViewAndEdit.Height * 1 / 3)
            {
                splitContainer2.SplitterDistance = panelViewAndEdit.Height * 2 / 3;
                butReduceHelp.Visible = true;
            }
        }
    }
}
