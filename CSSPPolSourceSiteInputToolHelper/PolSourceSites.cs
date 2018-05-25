using CSSPEnumsDLL.Enums;
using CSSPEnumsDLL.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSSPPolSourceSiteInputToolHelper
{
    public partial class PolSourceSiteInputToolHelper
    {
        public void DrawPanelPSS()
        {
            PanelPolSourceSite.Controls.Clear();

            if (subsectorDoc.Subsector == null)
            {
                Label lblTVText = new Label();

                lblTVText.AutoSize = true;
                lblTVText.Location = new Point(10, 4);
                lblTVText.TabIndex = 0;
                lblTVText.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                lblTVText.Text = $"Selected subsector has no local pollution source sites";

                PanelPolSourceSite.Controls.Add(lblTVText);
            }
            else
            {
                int countPSS = 0;
                foreach (PSS pss in subsectorDoc.Subsector.PSSList.OrderBy(c => c.SiteNumber))
                {
                    Panel panelpss = new Panel();

                    panelpss.BorderStyle = BorderStyle.FixedSingle;
                    panelpss.Location = new Point(0, countPSS * 44);
                    panelpss.Size = new Size(PanelPolSourceSite.Width, 44);
                    panelpss.TabIndex = 0;
                    panelpss.Tag = pss.PSSTVItemID;
                    panelpss.Click += ShowPolSourceSiteViaPanel;

                    Label lblTVText = new Label();

                    lblTVText.AutoSize = true;
                    lblTVText.Location = new Point(10, 4);
                    lblTVText.TabIndex = 0;
                    lblTVText.Tag = pss.PSSTVItemID;
                    if (pss.IsActive == false)
                    {
                        lblTVText.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Strikeout, GraphicsUnit.Point, ((byte)(0)));
                    }
                    else
                    {
                        lblTVText.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                    }
                    if (!string.IsNullOrWhiteSpace(pss.TVTextNew))
                    {
                        lblTVText.Text = $"{pss.SiteNumber}    {pss.TVTextNew}";
                    }
                    else
                    {
                        lblTVText.Text = $"{pss.SiteNumber}    {pss.TVText}";
                    }
                    lblTVText.Click += ShowPolSourceSiteViaLabel;

                    panelpss.Controls.Add(lblTVText);


                    Label lblPSSStatus = new Label();

                    bool NeedDetailsUpdate = false;
                    bool NeedIssuesUpdate = false;
                    bool NeedPicturesUpdate = false;
                    if (IsAdmin)
                    {
                        if (pss.LatNew != null
                           || pss.LngNew != null
                           || pss.IsActiveNew != null
                           || pss.IsPointSourceNew != null
                           || pss.PSSAddressNew.AddressTVItemID != null
                           || pss.PSSAddressNew.AddressType != null
                           || pss.PSSAddressNew.Municipality != null
                           || pss.PSSAddressNew.PostalCode != null
                           || pss.PSSAddressNew.StreetName != null
                           || pss.PSSAddressNew.StreetNumber != null
                           || pss.PSSAddressNew.StreetType != null
                           || pss.PSSObs.ObsDateNew != null)
                        {
                            NeedDetailsUpdate = true;
                        }

                        foreach (Issue issue in pss.PSSObs.IssueList)
                        {
                            if (issue.PolSourceObsInfoIntListNew.Count > 0 || issue.ToRemove == true)
                            {
                                NeedIssuesUpdate = true;
                                break;
                            }
                        }
                        foreach (Picture picture in pss.PSSPictureList)
                        {
                            if (picture.DescriptionNew != null
                                || picture.ExtensionNew != null
                                || picture.FileNameNew != null
                                || picture.ToRemove != null)
                            {
                                NeedPicturesUpdate = true;
                                break;
                            }
                        }

                        lblPSSStatus.AutoSize = true;
                        lblPSSStatus.Location = new Point(40, lblTVText.Bottom + 4);
                        lblPSSStatus.TabIndex = 0;
                        lblPSSStatus.Tag = pss.PSSTVItemID;
                        if (pss.IsActive == false)
                        {
                            lblPSSStatus.Font = new Font(new FontFamily(lblPSSStatus.Font.FontFamily.Name).Name, 10f, FontStyle.Strikeout, GraphicsUnit.Point, ((byte)(0)));
                        }
                        else
                        {
                            lblPSSStatus.Font = new Font(new FontFamily(lblPSSStatus.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        }
                        string NeedDetailsUpdateText = NeedDetailsUpdate ? "Details" : "";
                        string NeedIssuesUpdateText = NeedIssuesUpdate ? "Issues" : "";
                        string NeedPictuesUpdateText = NeedPicturesUpdate ? "Pictures" : "";
                        if (NeedDetailsUpdate || NeedIssuesUpdate || NeedPicturesUpdate)
                        {
                            lblPSSStatus.Text = $"Good --- Needs update for {NeedDetailsUpdateText} {NeedIssuesUpdateText} {NeedPictuesUpdateText}";
                        }
                        else
                        {
                            lblPSSStatus.Text = $"Good";
                        }
                        lblPSSStatus.Click += ShowPolSourceSiteViaLabel;


                        panelpss.Controls.Add(lblPSSStatus);

                    }
                    else
                    {

                        lblPSSStatus.AutoSize = true;
                        lblPSSStatus.Location = new Point(40, lblTVText.Bottom + 4);
                        lblPSSStatus.TabIndex = 0;
                        lblPSSStatus.Tag = pss.PSSTVItemID;
                        if (pss.IsActive == false)
                        {
                            lblPSSStatus.Font = new Font(new FontFamily(lblPSSStatus.Font.FontFamily.Name).Name, 10f, FontStyle.Strikeout, GraphicsUnit.Point, ((byte)(0)));
                        }
                        else
                        {
                            lblPSSStatus.Font = new Font(new FontFamily(lblPSSStatus.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        }
                        lblPSSStatus.Text = $"Good";
                        lblPSSStatus.Click += ShowPolSourceSiteViaLabel;


                        panelpss.Controls.Add(lblPSSStatus);
                    }


                    foreach (Issue issue in pss.PSSObs.IssueList)
                    {
                        if (issue.PolSourceObsInfoIntListNew.Count > 0)
                        {
                            issue.IsWellFormed = IssueWellFormed(issue, true);
                            issue.IsCompleted = IssueCompleted(issue, true);
                        }
                        else
                        {
                            issue.IsWellFormed = IssueWellFormed(issue, false);
                            issue.IsCompleted = IssueCompleted(issue, false);
                        }

                        if (issue.IsWellFormed == false)
                        {
                            panelpss.BackColor = BackColorNotWellFormed;
                            if (IsAdmin)
                            {
                                string NeedDetailsUpdateText = NeedDetailsUpdate ? "Details" : "";
                                string NeedIssuesUpdateText = NeedIssuesUpdate ? "Issues" : "";
                                string NeedPictuesUpdateText = NeedPicturesUpdate ? "Pictures" : "";
                                if (NeedDetailsUpdate || NeedIssuesUpdate || NeedPicturesUpdate)
                                {
                                    lblPSSStatus.Text = $"Not Well Formed --- Needs update for {NeedDetailsUpdateText} {NeedIssuesUpdateText} {NeedPictuesUpdateText}";
                                }
                                else
                                {
                                    lblPSSStatus.Text = $"Not Well Formed";
                                }
                            }
                            else
                            {
                                lblPSSStatus.Text = $"Not Well Formed";
                            }
                            break;
                        }
                        if (issue.IsCompleted == false)
                        {
                            panelpss.BackColor = BackColorNotCompleted;
                            if (IsAdmin)
                            {
                                string NeedDetailsUpdateText = NeedDetailsUpdate ? "Details" : "";
                                string NeedIssuesUpdateText = NeedIssuesUpdate ? "Issues" : "";
                                string NeedPictuesUpdateText = NeedPicturesUpdate ? "Pictures" : "";
                                if (NeedDetailsUpdate || NeedIssuesUpdate || NeedPicturesUpdate)
                                {
                                    lblPSSStatus.Text = $"Not Completed --- Needs update for {NeedDetailsUpdateText} {NeedIssuesUpdateText} {NeedPictuesUpdateText}";
                                }
                                else
                                {
                                    lblPSSStatus.Text = $"Not Completed";
                                }
                            }
                            else
                            {
                                lblPSSStatus.Text = $"Not Completed";
                            }
                            break;
                        }
                    }

                    PanelPolSourceSite.Controls.Add(panelpss);

                    countPSS += 1;
                }
            }
        }
        public void PSSAdd()
        {
            if (subsectorDoc.Subsector.SubsectorName != null)
            {
                PSS pss = new PSS();
                float LastLat = 0.0f;
                float LastLng = 0.0f;
                int MaxPSSTVItemID = 10000000;
                if (subsectorDoc.Subsector.PSSList.Count > 0)
                {
                    int Max = subsectorDoc.Subsector.PSSList.Max(c => c.PSSTVItemID).Value;
                    if (Max >= MaxPSSTVItemID)
                    {
                        MaxPSSTVItemID = Max + 1;
                    }
                    LastLat = ((float)subsectorDoc.Subsector.PSSList[subsectorDoc.Subsector.PSSList.Count - 1].Lat);
                    LastLng = ((float)subsectorDoc.Subsector.PSSList[subsectorDoc.Subsector.PSSList.Count - 1].Lng);
                }
                pss.PSSTVItemID = MaxPSSTVItemID;
                pss.SiteNumber = subsectorDoc.Subsector.PSSList.Max(c => c.SiteNumber).Value + 1;
                pss.SiteNumberText = "00000".Substring(0, "00000".Length - pss.SiteNumber.ToString().Length) + pss.SiteNumber.ToString();
                pss.Lat = LastLat + 0.1f;
                pss.LatNew = null;
                pss.Lng = LastLng + 0.1f;
                pss.LngNew = null;
                pss.IsActive = true;
                pss.IsActiveNew = false;
                pss.IsPointSource = false;
                pss.IsPointSourceNew = false;
                pss.TVText = "New PSS";

                subsectorDoc.Subsector.PSSList.Add(pss);

                Obs obs = new Obs();
                obs.ObsID = 10000000;
                obs.OldWrittenDescription = "";
                obs.LastUpdated_UTC = DateTime.Now;
                obs.ObsDate = DateTime.Now;

                pss.PSSObs = obs;

                Issue issue = new Issue();
                issue.IssueID = 10000000;
                issue.Ordinal = 0;
                issue.LastUpdated_UTC = DateTime.Now;

                pss.PSSObs.IssueList.Add(issue);
            }
        }
        public void PSSSaveToCSSPWebTools()
        {
            //MessageBox.Show("PSSSaveToCSSPWebTools " + CurrentPSS.PSSTVItemID.ToString(), PolSourceSiteTVItemID.ToString());
            MessageBox.Show("This functionality has not been implemented yet.");
        }
        public void RedrawSinglePanelPSS()
        {
            Panel panel = new Panel();
            PSS pss = new PSS();

            if (PolSourceSiteTVItemID > 0)
            {
                foreach (Panel panel2 in PanelPolSourceSite.Controls)
                {
                    int PSSTVItemID = int.Parse(panel2.Tag.ToString());
                    if (PolSourceSiteTVItemID == PSSTVItemID)
                    {
                        panel = panel2;
                        pss = subsectorDoc.Subsector.PSSList.Where(c => c.PSSTVItemID == PolSourceSiteTVItemID).FirstOrDefault();
                        break;
                    }
                }
            }
            panel.Controls.Clear();

            if (pss != null)
            {
                Label lblTVText = new Label();

                lblTVText.AutoSize = true;
                lblTVText.Location = new Point(10, 4);
                lblTVText.TabIndex = 0;
                lblTVText.Tag = pss.PSSTVItemID;
                if (pss.IsActive == false)
                {
                    lblTVText.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Strikeout, GraphicsUnit.Point, ((byte)(0)));
                }
                else
                {
                    lblTVText.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                }
                if (!string.IsNullOrWhiteSpace(pss.TVTextNew))
                {
                    lblTVText.Text = $"{pss.SiteNumber}    {pss.TVTextNew}";
                }
                else
                {
                    lblTVText.Text = $"{pss.SiteNumber}    {pss.TVText}";
                }
                lblTVText.Click += ShowPolSourceSiteViaLabel;

                panel.Controls.Add(lblTVText);

                Label lblPSSStatus = new Label();

                bool NeedDetailsUpdate = false;
                bool NeedIssuesUpdate = false;
                bool NeedPicturesUpdate = false;
                if (IsAdmin)
                {
                    if (pss.LatNew != null
                       || pss.LngNew != null
                       || pss.IsActiveNew != null
                       || pss.IsPointSourceNew != null
                       || pss.PSSAddressNew.AddressTVItemID != null
                       || pss.PSSAddressNew.AddressType != null
                       || pss.PSSAddressNew.Municipality != null
                       || pss.PSSAddressNew.PostalCode != null
                       || pss.PSSAddressNew.StreetName != null
                       || pss.PSSAddressNew.StreetNumber != null
                       || pss.PSSAddressNew.StreetType != null
                       || pss.PSSObs.ObsDateNew != null)
                    {
                        NeedDetailsUpdate = true;
                    }

                    foreach (Issue issue in pss.PSSObs.IssueList)
                    {
                        if (issue.PolSourceObsInfoIntListNew.Count > 0 || issue.ToRemove == true)
                        {
                            NeedIssuesUpdate = true;
                            break;
                        }
                    }
                    foreach (Picture picture in pss.PSSPictureList)
                    {
                        if (picture.DescriptionNew != null
                            || picture.ExtensionNew != null
                            || picture.FileNameNew != null
                            || picture.ToRemove != null)
                        {
                            NeedPicturesUpdate = true;
                            break;
                        }
                    }

                    lblPSSStatus.AutoSize = true;
                    lblPSSStatus.Location = new Point(40, lblTVText.Bottom + 4);
                    lblPSSStatus.TabIndex = 0;
                    lblPSSStatus.Tag = pss.PSSTVItemID;
                    if (pss.IsActive == false)
                    {
                        lblPSSStatus.Font = new Font(new FontFamily(lblPSSStatus.Font.FontFamily.Name).Name, 10f, System.Drawing.FontStyle.Strikeout, GraphicsUnit.Point, ((byte)(0)));
                    }
                    else
                    {
                        lblPSSStatus.Font = new Font(new FontFamily(lblPSSStatus.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    }
                    string NeedDetailsUpdateText = NeedDetailsUpdate ? "Details" : "";
                    string NeedIssuesUpdateText = NeedIssuesUpdate ? "Issues" : "";
                    string NeedPictuesUpdateText = NeedPicturesUpdate ? "Pictures" : "";
                    if (NeedDetailsUpdate || NeedIssuesUpdate || NeedPicturesUpdate)
                    {
                        lblPSSStatus.Text = $"Good --- Needs update for {NeedDetailsUpdateText} {NeedIssuesUpdateText} {NeedPictuesUpdateText}";
                    }
                    else
                    {
                        lblPSSStatus.Text = $"Good";
                    }
                    lblPSSStatus.Click += ShowPolSourceSiteViaLabel;

                    panel.Controls.Add(lblPSSStatus);
                }
                else
                {
                    lblPSSStatus.AutoSize = true;
                    lblPSSStatus.Location = new Point(40, lblTVText.Bottom + 4);
                    lblPSSStatus.TabIndex = 0;
                    lblPSSStatus.Tag = pss.PSSTVItemID;
                    if (pss.IsActive == false)
                    {
                        lblPSSStatus.Font = new Font(new FontFamily(lblPSSStatus.Font.FontFamily.Name).Name, 10f, System.Drawing.FontStyle.Strikeout, GraphicsUnit.Point, ((byte)(0)));
                    }
                    else
                    {
                        lblPSSStatus.Font = new Font(new FontFamily(lblPSSStatus.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    }
                    lblPSSStatus.Text = $"Good";
                    lblPSSStatus.Click += ShowPolSourceSiteViaLabel;

                    panel.Controls.Add(lblPSSStatus);
                }



                foreach (Issue issue in pss.PSSObs.IssueList)
                {
                    if (issue.PolSourceObsInfoIntListNew.Count > 0)
                    {
                        issue.IsWellFormed = IssueWellFormed(issue, true);
                        issue.IsCompleted = IssueCompleted(issue, true);
                    }
                    else
                    {
                        issue.IsWellFormed = IssueWellFormed(issue, false);
                        issue.IsCompleted = IssueCompleted(issue, false);
                    }
                    if (issue.IsWellFormed == false)
                    {
                        panel.BackColor = BackColorNotWellFormed;
                        if (IsAdmin)
                        {
                            string NeedDetailsUpdateText = NeedDetailsUpdate ? "Details" : "";
                            string NeedIssuesUpdateText = NeedIssuesUpdate ? "Issues" : "";
                            string NeedPictuesUpdateText = NeedPicturesUpdate ? "Pictures" : "";
                            if (NeedDetailsUpdate || NeedIssuesUpdate || NeedPicturesUpdate)
                            {
                                lblPSSStatus.Text = $"Not Well Formed --- Needs update for {NeedDetailsUpdateText} {NeedIssuesUpdateText} {NeedPictuesUpdateText}";
                            }
                            else
                            {
                                lblPSSStatus.Text = $"Not Well Formed";
                            }
                        }
                        else
                        {
                            lblPSSStatus.Text = $"Not Well Formed";
                        }
                        break;
                    }
                    if (issue.IsCompleted == false)
                    {
                        panel.BackColor = BackColorNotCompleted;
                        if (IsAdmin)
                        {
                            string NeedDetailsUpdateText = NeedDetailsUpdate ? "Details" : "";
                            string NeedIssuesUpdateText = NeedIssuesUpdate ? "Issues" : "";
                            string NeedPictuesUpdateText = NeedPicturesUpdate ? "Pictures" : "";
                            if (NeedDetailsUpdate || NeedIssuesUpdate || NeedPicturesUpdate)
                            {
                                lblPSSStatus.Text = $"Not Completed --- Needs update for {NeedDetailsUpdateText} {NeedIssuesUpdateText} {NeedPictuesUpdateText}";
                            }
                            else
                            {
                                lblPSSStatus.Text = $"Not Completed";
                            }
                        }
                        else
                        {
                            lblPSSStatus.Text = $"Not Completed";
                        }
                        break;
                    }
                }

            }
        }
        public void RedrawPolSourceSiteList()
        {
            //polSourceSiteInputToolHelper.CurrentSubsectorName = (string)comboBoxSubsectorNames.SelectedItem;

            IsReading = true;
            if (!ReadPollutionSourceSitesSubsectorFile())
            {
                return;
            }
            IsReading = false;
            if (!CheckAllReadDataOK())
            {
                return;
            }

            DrawPanelPSS();
        }
        public void ShowPolSourceSite()
        {
            PanelViewAndEdit.Controls.Clear();

            if (CurrentPSS == null)
            {
                Label lblMessage = new Label();
                lblMessage.AutoSize = true;
                lblMessage.Location = new Point(30, 30);
                lblMessage.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                lblMessage.Font = new Font(new FontFamily(lblMessage.Font.FontFamily.Name).Name, 14f, FontStyle.Bold);
                lblMessage.Text = $"Please select a pollution source site items for {(IsEditing ? "editing" : "viewing")} {(ShowOnlyIssues ? "issues" : (ShowOnlyPictures ? "pictures" : "pollution source site"))}";

                PanelViewAndEdit.Controls.Add(lblMessage);

                return;
            }

            if (Language == LanguageEnum.fr)
            {
                _BaseEnumService = new BaseEnumService(LanguageEnum.fr);
            }
            else
            {
                _BaseEnumService = new BaseEnumService(LanguageEnum.en);
            }

            int Y = 0;
            int X = 10;
            if (CurrentPSS != null)
            {
                Label lblTVText = new Label();
                lblTVText.AutoSize = true;
                lblTVText.Location = new Point(10, 4);
                lblTVText.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                if (CurrentPSS.IsActive == false)
                {
                    lblTVText.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Strikeout, GraphicsUnit.Point, ((byte)(0)));
                }
                else
                {
                    lblTVText.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                }
                //lblTVText.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 14f, FontStyle.Bold);
                lblTVText.Text = $"{CurrentPSS.SiteNumber} {(CurrentPSS.TVTextNew != null ? CurrentPSS.TVTextNew : CurrentPSS.TVText)}";

                PanelViewAndEdit.Controls.Add(lblTVText);

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;

                Label lblLat = new Label();
                lblLat.AutoSize = true;
                lblLat.Location = new Point(20, Y);
                lblLat.Font = new Font(new FontFamily(lblLat.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                lblLat.ForeColor = CurrentPSS.LatNew != null ? ForeColorChangedOrNew : ForeColorNormal;
                lblLat.Text = $@"Lat: " + $@"{(CurrentPSS.LatNew != null ? $" ({((float)CurrentPSS.Lat).ToString("F5")})" : "")}";

                PanelViewAndEdit.Controls.Add(lblLat);

                X = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 5;
                if (IsEditing)
                {
                    TextBox textBoxLat = new TextBox();
                    textBoxLat.Location = new Point(X, Y);
                    textBoxLat.Name = "textBoxLat";
                    textBoxLat.Font = new Font(new FontFamily(lblLat.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    textBoxLat.Width = 100;
                    textBoxLat.Text = (CurrentPSS.LatNew != null ? (float)CurrentPSS.LatNew : (float)CurrentPSS.Lat).ToString("F5");

                    PanelViewAndEdit.Controls.Add(textBoxLat);

                    X = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 5;

                }
                else
                {
                    Label lblLatValue = new Label();
                    lblLatValue.AutoSize = true;
                    lblLatValue.Location = new Point(X, Y);
                    lblLatValue.Font = new Font(new FontFamily(lblLatValue.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    lblLatValue.ForeColor = CurrentPSS.LatNew != null ? ForeColorChangedOrNew : ForeColorNormal;
                    lblLatValue.Text = (CurrentPSS.LatNew != null ? (float)CurrentPSS.LatNew : (float)CurrentPSS.Lat).ToString("F5");

                    PanelViewAndEdit.Controls.Add(lblLatValue);

                    X = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 5;
                }

                Label lblLng = new Label();
                lblLng.AutoSize = true;
                lblLng.Location = new Point(X, Y);
                lblLng.Font = new Font(new FontFamily(lblLng.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                lblLng.ForeColor = CurrentPSS.LngNew != null ? ForeColorChangedOrNew : ForeColorNormal;
                lblLng.Text = $@"Lng: " + $@"{(CurrentPSS.LngNew != null ? $" ({((float)CurrentPSS.Lng).ToString("F5")})" : "")}";

                PanelViewAndEdit.Controls.Add(lblLng);

                X = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 5;

                if (IsEditing)
                {
                    TextBox textBoxLng = new TextBox();
                    textBoxLng.Location = new Point(X, Y);
                    textBoxLng.Name = "textBoxLng";
                    textBoxLng.Font = new Font(new FontFamily(lblLng.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    textBoxLng.Width = 100;
                    textBoxLng.Text = CurrentPSS.LngNew != null ? ((float)CurrentPSS.LngNew).ToString("F5") : ((float)CurrentPSS.Lng).ToString("F5");

                    PanelViewAndEdit.Controls.Add(textBoxLng);

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;

                    X = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 20;
                }
                else
                {
                    Label lblLngValue = new Label();
                    lblLngValue.AutoSize = true;
                    lblLngValue.Location = new Point(X, Y);
                    lblLngValue.Font = new Font(new FontFamily(lblLngValue.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    lblLngValue.ForeColor = CurrentPSS.LngNew != null ? ForeColorChangedOrNew : ForeColorNormal;
                    lblLngValue.Text = CurrentPSS.LngNew != null ? ((float)CurrentPSS.LngNew).ToString() : ((float)CurrentPSS.Lng).ToString("F5");

                    PanelViewAndEdit.Controls.Add(lblLngValue);

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;

                    X = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 20;
                }

                if (IsEditing)
                {
                    if ((bool)CurrentPSS.IsActive)
                    {
                        Button butChangeToIsNotActive = new Button();
                        butChangeToIsNotActive.AutoSize = true;
                        butChangeToIsNotActive.Location = new Point(X, lblLat.Top);
                        butChangeToIsNotActive.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                        butChangeToIsNotActive.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        butChangeToIsNotActive.Padding = new Padding(5);
                        butChangeToIsNotActive.Tag = CurrentPSS.PSSTVItemID.ToString();
                        butChangeToIsNotActive.Text = $"Set as non active";
                        butChangeToIsNotActive.Click += butChangeToIsNotActive_Click;

                        PanelViewAndEdit.Controls.Add(butChangeToIsNotActive);
                    }
                    else
                    {
                        Button butChangeToIsActive = new Button();
                        butChangeToIsActive.AutoSize = true;
                        butChangeToIsActive.Location = new Point(X, lblLat.Top);
                        butChangeToIsActive.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                        butChangeToIsActive.Font = new Font(new FontFamily(butChangeToIsActive.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        butChangeToIsActive.Padding = new Padding(5);
                        butChangeToIsActive.Tag = CurrentPSS.PSSTVItemID.ToString();
                        butChangeToIsActive.Text = $"Set as active";
                        butChangeToIsActive.Click += butChangeToIsActive_Click;

                        PanelViewAndEdit.Controls.Add(butChangeToIsActive);
                    }

                    X = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 5;
                }
                else
                {
                    Label lblIsActive = new Label();
                    lblIsActive.AutoSize = true;
                    lblIsActive.Location = new Point(X, lblLat.Top);
                    lblIsActive.Font = new Font(new FontFamily(lblIsActive.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    lblIsActive.ForeColor = CurrentPSS.IsActiveNew != null ? ForeColorChangedOrNew : ForeColorNormal;
                    lblIsActive.Text = (CurrentPSS.IsActiveNew != null ? ((bool)CurrentPSS.IsActiveNew ? "Is Active" : "Not Active") : ((bool)CurrentPSS.IsActive ? "Is Active" : "Not Active"));

                    PanelViewAndEdit.Controls.Add(lblIsActive);

                    X = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 5;
                }

                if (IsEditing)
                {
                    if ((bool)CurrentPSS.IsPointSource)
                    {
                        Button butChangeToIsNonPointSource = new Button();
                        butChangeToIsNonPointSource.AutoSize = true;
                        butChangeToIsNonPointSource.Location = new Point(X, lblLat.Top);
                        butChangeToIsNonPointSource.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                        butChangeToIsNonPointSource.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        butChangeToIsNonPointSource.Padding = new Padding(5);
                        butChangeToIsNonPointSource.Tag = CurrentPSS.PSSTVItemID.ToString();
                        butChangeToIsNonPointSource.Text = $"Set as non point source";
                        butChangeToIsNonPointSource.Click += butChangeToIsNonPointSource_Click;

                        PanelViewAndEdit.Controls.Add(butChangeToIsNonPointSource);
                    }
                    else
                    {
                        Button butChangeToIsPointSource = new Button();
                        butChangeToIsPointSource.AutoSize = true;
                        butChangeToIsPointSource.Location = new Point(X, lblLat.Top);
                        butChangeToIsPointSource.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                        butChangeToIsPointSource.Font = new Font(new FontFamily(butChangeToIsPointSource.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        butChangeToIsPointSource.Padding = new Padding(5);
                        butChangeToIsPointSource.Tag = CurrentPSS.PSSTVItemID.ToString();
                        butChangeToIsPointSource.Text = $"Set as point source";
                        butChangeToIsPointSource.Click += butChangeToIsPointSource_Click;

                        PanelViewAndEdit.Controls.Add(butChangeToIsPointSource);
                    }

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;
                }
                else
                {
                    Label lblIsPointSource = new Label();
                    lblIsPointSource.AutoSize = true;
                    lblIsPointSource.Location = new Point(X, lblLat.Top);
                    lblIsPointSource.Font = new Font(new FontFamily(lblIsPointSource.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    lblIsPointSource.ForeColor = CurrentPSS.IsPointSourceNew != null ? ForeColorChangedOrNew : ForeColorNormal;
                    lblIsPointSource.Text = (CurrentPSS.IsPointSourceNew != null ? ((bool)CurrentPSS.IsPointSourceNew ? "Is Point Source" : "Not Point Source") : ((bool)CurrentPSS.IsPointSource ? "Is Point Source" : "Not Point Source"));

                    PanelViewAndEdit.Controls.Add(lblIsPointSource);


                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;
                }


                // doing address

                X = 10;

                int AddressPos = 0;
                #region StreetNumber
                // ---------------------------------------------------------------------------------------------------------------
                // start of StreetNumber
                // ---------------------------------------------------------------------------------------------------------------
                Label lblStreetNumberText = new Label();
                lblStreetNumberText.AutoSize = true;
                lblStreetNumberText.Location = new Point(X, Y);
                lblStreetNumberText.Font = new Font(new FontFamily(lblStreetNumberText.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                lblStreetNumberText.ForeColor = CurrentPSS.PSSAddressNew.StreetNumber != null ? ForeColorChangedOrNew : ForeColorNormal;
                lblStreetNumberText.Text = $@"Street Number";

                PanelViewAndEdit.Controls.Add(lblStreetNumberText);

                X = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                if (IsEditing)
                {
                    int currentTop = lblStreetNumberText.Bottom + 4;

                    if (CurrentPSS.PSSAddressNew.StreetNumber != null)
                    {
                        Label lblStreetNumberOld = new Label();
                        lblStreetNumberOld.AutoSize = true;
                        lblStreetNumberOld.Location = new Point(lblStreetNumberText.Left, currentTop);
                        lblStreetNumberOld.Font = new Font(new FontFamily(lblStreetNumberOld.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        lblStreetNumberOld.ForeColor = CurrentPSS.PSSAddressNew.StreetNumber != null ? ForeColorChangedOrNew : ForeColorNormal;
                        lblStreetNumberOld.Text = string.IsNullOrWhiteSpace(CurrentPSS.PSSAddress.StreetNumber) ? "(empty)" : $"({CurrentPSS.PSSAddress.StreetNumber})";

                        PanelViewAndEdit.Controls.Add(lblStreetNumberOld);

                        currentTop = lblStreetNumberOld.Bottom + 4;

                    }

                    TextBox textBoxStreetNumber = new TextBox();
                    textBoxStreetNumber.Location = new Point(lblStreetNumberText.Left, currentTop);
                    textBoxStreetNumber.Name = "textBoxStreetNumber";
                    textBoxStreetNumber.Font = new Font(new FontFamily(lblStreetNumberText.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    textBoxStreetNumber.Width = lblStreetNumberText.Width;
                    if (CurrentPSS.PSSAddressNew.StreetNumber != null)
                    {
                        textBoxStreetNumber.Text = CurrentPSS.PSSAddressNew.StreetNumber;
                    }
                    else
                    {
                        textBoxStreetNumber.Text = CurrentPSS.PSSAddress.StreetNumber;
                    }

                    PanelViewAndEdit.Controls.Add(textBoxStreetNumber);

                    AddressPos = textBoxStreetNumber.Bottom + 10;
                }
                else
                {
                    int currentTop = lblStreetNumberText.Bottom + 4;

                    Label lblStreetNumber = new Label();
                    lblStreetNumber.AutoSize = true;
                    lblStreetNumber.Location = new Point(lblStreetNumberText.Left, currentTop);
                    lblStreetNumber.Font = new Font(new FontFamily(lblStreetNumber.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    lblStreetNumber.ForeColor = CurrentPSS.PSSAddressNew.StreetNumber != null ? ForeColorChangedOrNew : ForeColorNormal;
                    lblStreetNumber.Text = string.IsNullOrWhiteSpace(CurrentPSS.PSSAddress.StreetNumber) ? "(empty)" : $"({CurrentPSS.PSSAddress.StreetNumber})";

                    PanelViewAndEdit.Controls.Add(lblStreetNumber);

                    currentTop = lblStreetNumber.Bottom + 4;

                    AddressPos = lblStreetNumber.Bottom + 10;

                    if (CurrentPSS.PSSAddressNew.StreetNumber != null)
                    {
                        Label lblStreetNumberNew = new Label();
                        lblStreetNumberNew.AutoSize = true;
                        lblStreetNumberNew.Location = new Point(lblStreetNumberText.Left, currentTop);
                        lblStreetNumberNew.Font = new Font(new FontFamily(lblStreetNumberNew.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        lblStreetNumberNew.ForeColor = CurrentPSS.PSSAddressNew.StreetNumber != null ? ForeColorChangedOrNew : ForeColorNormal;
                        lblStreetNumberNew.Text = string.IsNullOrWhiteSpace(CurrentPSS.PSSAddressNew.StreetNumber) ? "empty" : CurrentPSS.PSSAddressNew.StreetNumber;

                        PanelViewAndEdit.Controls.Add(lblStreetNumberNew);

                        currentTop = lblStreetNumberNew.Bottom + 4;

                        AddressPos = lblStreetNumberNew.Bottom + 10;

                    }

                }


                // ---------------------------------------------------------------------------------------------------------------
                // end of StreetNumber
                // ---------------------------------------------------------------------------------------------------------------

                #endregion StreetNumber

                #region StreetName
                // ---------------------------------------------------------------------------------------------------------------
                // start of StreetName
                // ---------------------------------------------------------------------------------------------------------------
                Label lblStreetNameText = new Label();
                lblStreetNameText.AutoSize = true;
                lblStreetNameText.Location = new Point(X, lblStreetNumberText.Top);
                lblStreetNameText.Font = new Font(new FontFamily(lblStreetNameText.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                lblStreetNameText.ForeColor = CurrentPSS.PSSAddressNew.StreetName != null ? ForeColorChangedOrNew : ForeColorNormal;
                lblStreetNameText.Text = $@"Street Name    ";

                PanelViewAndEdit.Controls.Add(lblStreetNameText);

                X = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                if (IsEditing)
                {
                    int currentTop = lblStreetNameText.Bottom + 4;

                    if (CurrentPSS.PSSAddressNew.StreetName != null)
                    {
                        Label lblStreetNameOld = new Label();
                        lblStreetNameOld.AutoSize = true;
                        lblStreetNameOld.Location = new Point(lblStreetNameText.Left, currentTop);
                        lblStreetNameOld.Font = new Font(new FontFamily(lblStreetNameOld.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        lblStreetNameOld.ForeColor = CurrentPSS.PSSAddressNew.StreetName != null ? ForeColorChangedOrNew : ForeColorNormal;
                        lblStreetNameOld.Text = string.IsNullOrWhiteSpace(CurrentPSS.PSSAddress.StreetName) ? "(empty)" : $"({CurrentPSS.PSSAddress.StreetName})";

                        PanelViewAndEdit.Controls.Add(lblStreetNameOld);

                        currentTop = lblStreetNameOld.Bottom + 4;

                    }

                    TextBox textBoxStreetName = new TextBox();
                    textBoxStreetName.Location = new Point(lblStreetNameText.Left, currentTop);
                    textBoxStreetName.Name = "textBoxStreetName";
                    textBoxStreetName.Font = new Font(new FontFamily(lblStreetNameText.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    textBoxStreetName.Width = lblStreetNameText.Width;
                    if (CurrentPSS.PSSAddressNew.StreetName != null)
                    {
                        textBoxStreetName.Text = CurrentPSS.PSSAddressNew.StreetName;
                    }
                    else
                    {
                        textBoxStreetName.Text = CurrentPSS.PSSAddress.StreetName;
                    }

                    PanelViewAndEdit.Controls.Add(textBoxStreetName);

                }
                else
                {
                    int currentTop = lblStreetNameText.Bottom + 4;

                    Label lblStreetName = new Label();
                    lblStreetName.AutoSize = true;
                    lblStreetName.Location = new Point(lblStreetNameText.Left, currentTop);
                    lblStreetName.Font = new Font(new FontFamily(lblStreetName.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    lblStreetName.ForeColor = CurrentPSS.PSSAddressNew.StreetName != null ? ForeColorChangedOrNew : ForeColorNormal;
                    lblStreetName.Text = string.IsNullOrWhiteSpace(CurrentPSS.PSSAddress.StreetName) ? "(empty)" : $"({CurrentPSS.PSSAddress.StreetName})";

                    PanelViewAndEdit.Controls.Add(lblStreetName);

                    //Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 4;

                    if (CurrentPSS.PSSAddressNew.StreetName != null)
                    {
                        currentTop = lblStreetName.Bottom + 4;

                        Label lblStreetNameNew = new Label();
                        lblStreetNameNew.AutoSize = true;
                        lblStreetNameNew.Location = new Point(lblStreetNameText.Left, lblStreetName.Bottom + 4);
                        lblStreetNameNew.Font = new Font(new FontFamily(lblStreetNameNew.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        lblStreetNameNew.ForeColor = CurrentPSS.PSSAddressNew.StreetName != null ? ForeColorChangedOrNew : ForeColorNormal;
                        lblStreetNameNew.Text = string.IsNullOrWhiteSpace(CurrentPSS.PSSAddressNew.StreetName) ? "empty" : CurrentPSS.PSSAddressNew.StreetName;

                        PanelViewAndEdit.Controls.Add(lblStreetNameNew);

                        //Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 4;
                    }

                }


                // ---------------------------------------------------------------------------------------------------------------
                // end of StreetName
                // ---------------------------------------------------------------------------------------------------------------

                #endregion StreetName

                #region StreetType
                // ---------------------------------------------------------------------------------------------------------------
                // start of StreetType
                // ---------------------------------------------------------------------------------------------------------------
                Label lblStreetTypeText = new Label();
                lblStreetTypeText.AutoSize = true;
                lblStreetTypeText.Location = new Point(X, lblStreetNumberText.Top);
                lblStreetTypeText.Font = new Font(new FontFamily(lblStreetTypeText.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                lblStreetTypeText.ForeColor = CurrentPSS.PSSAddressNew.StreetType != null ? ForeColorChangedOrNew : ForeColorNormal;
                lblStreetTypeText.Text = $@"Street Type   ";

                PanelViewAndEdit.Controls.Add(lblStreetTypeText);

                X = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                if (IsEditing)
                {
                    int currentTop = lblStreetTypeText.Bottom + 4;

                    if (CurrentPSS.PSSAddressNew.StreetType != null)
                    {
                        Label lblStreetTypeOld = new Label();
                        lblStreetTypeOld.AutoSize = true;
                        lblStreetTypeOld.Location = new Point(lblStreetTypeText.Left, currentTop);
                        lblStreetTypeOld.Font = new Font(new FontFamily(lblStreetTypeOld.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        lblStreetTypeOld.ForeColor = CurrentPSS.PSSAddressNew.StreetType != null ? ForeColorChangedOrNew : ForeColorNormal;
                        lblStreetTypeOld.Text = CurrentPSS.PSSAddress.StreetType == null ? "(empty)" : $"({((StreetTypeEnum)CurrentPSS.PSSAddress.StreetType).ToString()})";

                        PanelViewAndEdit.Controls.Add(lblStreetTypeOld);

                        currentTop = lblStreetTypeOld.Bottom + 4;

                    }

                    ComboBox comboBoxStreetType = new ComboBox();
                    comboBoxStreetType.Location = new Point(lblStreetTypeText.Left, currentTop);
                    comboBoxStreetType.Name = "comboBoxStreetType";
                    comboBoxStreetType.Font = new Font(new FontFamily(lblStreetNameText.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    comboBoxStreetType.Width = lblStreetTypeText.Width;

                    PanelViewAndEdit.Controls.Add(comboBoxStreetType);

                    for (int i = 1, count = Enum.GetNames(typeof(StreetTypeEnum)).Count(); i < count; i++)
                    {
                        comboBoxStreetType.Items.Add(((StreetTypeEnum)i).ToString());
                    }

                    if (CurrentPSS.PSSAddressNew.StreetType != null)
                    {
                        comboBoxStreetType.SelectedItem = ((StreetTypeEnum)CurrentPSS.PSSAddressNew.StreetType).ToString();
                    }
                    else
                    {
                        if (CurrentPSS.PSSAddress.StreetType != null)
                        {
                            comboBoxStreetType.SelectedItem = ((StreetTypeEnum)CurrentPSS.PSSAddress.StreetType).ToString();
                        }
                    }

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                }
                else
                {
                    int currentTop = lblStreetTypeText.Bottom + 4;

                    Label lblStreetType = new Label();
                    lblStreetType.AutoSize = true;
                    lblStreetType.Location = new Point(lblStreetTypeText.Left, currentTop);
                    lblStreetType.Font = new Font(new FontFamily(lblStreetType.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    lblStreetType.ForeColor = CurrentPSS.PSSAddressNew.StreetType != null ? ForeColorChangedOrNew : ForeColorNormal;
                    lblStreetType.Text = CurrentPSS.PSSAddress.StreetType == null ? "(empty)" : $"({((StreetTypeEnum)CurrentPSS.PSSAddress.StreetType).ToString()})";

                    PanelViewAndEdit.Controls.Add(lblStreetType);

                    //Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 4;

                    if (CurrentPSS.PSSAddressNew.StreetType != null)
                    {
                        currentTop = lblStreetType.Bottom + 4;

                        Label lblStreetTypeNew = new Label();
                        lblStreetTypeNew.AutoSize = true;
                        lblStreetTypeNew.Location = new Point(lblStreetTypeText.Left, lblStreetType.Bottom + 4);
                        lblStreetTypeNew.Font = new Font(new FontFamily(lblStreetTypeNew.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        lblStreetTypeNew.ForeColor = CurrentPSS.PSSAddressNew.StreetType != null ? ForeColorChangedOrNew : ForeColorNormal;
                        lblStreetTypeNew.Text = CurrentPSS.PSSAddressNew.StreetType == null ? "empty" : ((StreetTypeEnum)CurrentPSS.PSSAddressNew.StreetType).ToString();

                        PanelViewAndEdit.Controls.Add(lblStreetTypeNew);

                        //Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 4;
                    }

                }

                //X = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                // ---------------------------------------------------------------------------------------------------------------
                // end of StreetType
                // ---------------------------------------------------------------------------------------------------------------

                #endregion StreetType

                #region Municipality
                // ---------------------------------------------------------------------------------------------------------------
                // start of Municipality
                // ---------------------------------------------------------------------------------------------------------------
                Label lblMunicipalityText = new Label();
                lblMunicipalityText.AutoSize = true;
                lblMunicipalityText.Location = new Point(X, lblStreetNumberText.Top);
                lblMunicipalityText.Font = new Font(new FontFamily(lblMunicipalityText.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                lblMunicipalityText.ForeColor = CurrentPSS.PSSAddressNew.Municipality != null ? ForeColorChangedOrNew : ForeColorNormal;
                lblMunicipalityText.Text = $@"Municipality        ";

                PanelViewAndEdit.Controls.Add(lblMunicipalityText);

                X = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                if (IsEditing)
                {
                    int currentTop = lblMunicipalityText.Bottom + 4;

                    if (CurrentPSS.PSSAddressNew.Municipality != null)
                    {
                        Label lblMunicipalityOld = new Label();
                        lblMunicipalityOld.AutoSize = true;
                        lblMunicipalityOld.Location = new Point(lblMunicipalityText.Left, currentTop);
                        lblMunicipalityOld.Font = new Font(new FontFamily(lblMunicipalityOld.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        lblMunicipalityOld.ForeColor = CurrentPSS.PSSAddressNew.Municipality != null ? ForeColorChangedOrNew : ForeColorNormal;
                        lblMunicipalityOld.Text = string.IsNullOrWhiteSpace(CurrentPSS.PSSAddress.Municipality) ? "(empty)" : $"({CurrentPSS.PSSAddress.Municipality})";

                        PanelViewAndEdit.Controls.Add(lblMunicipalityOld);

                        currentTop = lblMunicipalityOld.Bottom + 4;

                    }

                    TextBox textBoxMunicipality = new TextBox();
                    textBoxMunicipality.Location = new Point(lblMunicipalityText.Left, currentTop);
                    textBoxMunicipality.Name = "textBoxMunicipality";
                    textBoxMunicipality.Font = new Font(new FontFamily(lblMunicipalityText.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    textBoxMunicipality.Width = lblMunicipalityText.Width;
                    if (CurrentPSS.PSSAddressNew.Municipality != null)
                    {
                        textBoxMunicipality.Text = CurrentPSS.PSSAddressNew.Municipality;
                    }
                    else
                    {
                        textBoxMunicipality.Text = CurrentPSS.PSSAddress.Municipality;
                    }

                    PanelViewAndEdit.Controls.Add(textBoxMunicipality);

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                }
                else
                {
                    int currentTop = lblMunicipalityText.Bottom + 4;

                    Label lblMunicipality = new Label();
                    lblMunicipality.AutoSize = true;
                    lblMunicipality.Location = new Point(lblMunicipalityText.Left, lblMunicipalityText.Bottom + 4);
                    lblMunicipality.Font = new Font(new FontFamily(lblMunicipality.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    lblMunicipality.ForeColor = CurrentPSS.PSSAddressNew.Municipality != null ? ForeColorChangedOrNew : ForeColorNormal;
                    lblMunicipality.Text = string.IsNullOrWhiteSpace(CurrentPSS.PSSAddress.Municipality) ? "(empty)" : $"({CurrentPSS.PSSAddress.Municipality})";

                    PanelViewAndEdit.Controls.Add(lblMunicipality);

                    //Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 4;

                    if (CurrentPSS.PSSAddressNew.Municipality != null)
                    {
                        currentTop = lblMunicipality.Bottom + 4;

                        Label lblMunicipalityNew = new Label();
                        lblMunicipalityNew.AutoSize = true;
                        lblMunicipalityNew.Location = new Point(lblMunicipalityText.Left, lblMunicipality.Bottom + 4);
                        lblMunicipalityNew.Font = new Font(new FontFamily(lblMunicipalityNew.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        lblMunicipalityNew.ForeColor = CurrentPSS.PSSAddressNew.Municipality != null ? ForeColorChangedOrNew : ForeColorNormal;
                        lblMunicipalityNew.Text = string.IsNullOrWhiteSpace(CurrentPSS.PSSAddressNew.Municipality) ? "empty" : CurrentPSS.PSSAddressNew.Municipality;

                        PanelViewAndEdit.Controls.Add(lblMunicipalityNew);

                        //Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 4;
                    }

                }

                //X = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                // ---------------------------------------------------------------------------------------------------------------
                // end of Municipality
                // ---------------------------------------------------------------------------------------------------------------

                #endregion Municipality

                #region PostalCode
                // ---------------------------------------------------------------------------------------------------------------
                // start of PostalCode
                // ---------------------------------------------------------------------------------------------------------------
                Label lblPostalCodeText = new Label();
                lblPostalCodeText.AutoSize = true;
                lblPostalCodeText.Location = new Point(X, lblStreetNumberText.Top);
                lblPostalCodeText.Font = new Font(new FontFamily(lblPostalCodeText.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                lblPostalCodeText.ForeColor = CurrentPSS.PSSAddressNew.PostalCode != null ? ForeColorChangedOrNew : ForeColorNormal;
                lblPostalCodeText.Text = $@"Postal Code";

                PanelViewAndEdit.Controls.Add(lblPostalCodeText);

                X = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                if (IsEditing)
                {
                    int currentTop = lblPostalCodeText.Bottom + 4;

                    if (CurrentPSS.PSSAddressNew.PostalCode != null)
                    {
                        Label lblPostalCodeOld = new Label();
                        lblPostalCodeOld.AutoSize = true;
                        lblPostalCodeOld.Location = new Point(lblPostalCodeText.Left, currentTop);
                        lblPostalCodeOld.Font = new Font(new FontFamily(lblPostalCodeOld.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        lblPostalCodeOld.ForeColor = CurrentPSS.PSSAddressNew.PostalCode != null ? ForeColorChangedOrNew : ForeColorNormal;
                        lblPostalCodeOld.Text = string.IsNullOrWhiteSpace(CurrentPSS.PSSAddress.PostalCode) ? "(empty)" : $"({CurrentPSS.PSSAddress.PostalCode})";

                        PanelViewAndEdit.Controls.Add(lblPostalCodeOld);

                        currentTop = lblPostalCodeOld.Bottom + 4;

                    }

                    TextBox textBoxPostalCode = new TextBox();
                    textBoxPostalCode.Location = new Point(lblPostalCodeText.Left, currentTop);
                    textBoxPostalCode.Name = "textBoxPostalCode";
                    textBoxPostalCode.Font = new Font(new FontFamily(lblPostalCodeText.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    textBoxPostalCode.Width = lblPostalCodeText.Width;
                    if (CurrentPSS.PSSAddressNew.PostalCode != null)
                    {
                        textBoxPostalCode.Text = CurrentPSS.PSSAddressNew.PostalCode;
                    }
                    else
                    {
                        textBoxPostalCode.Text = CurrentPSS.PSSAddress.PostalCode;
                    }

                    PanelViewAndEdit.Controls.Add(textBoxPostalCode);

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                }
                else
                {
                    int currentTop = lblPostalCodeText.Bottom + 4;

                    Label lblPostalCode = new Label();
                    lblPostalCode.AutoSize = true;
                    lblPostalCode.Location = new Point(lblPostalCodeText.Left, lblPostalCodeText.Bottom + 4);
                    lblPostalCode.Font = new Font(new FontFamily(lblPostalCode.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    lblPostalCode.ForeColor = CurrentPSS.PSSAddressNew.PostalCode != null ? ForeColorChangedOrNew : ForeColorNormal;
                    lblPostalCode.Text = string.IsNullOrWhiteSpace(CurrentPSS.PSSAddress.PostalCode) ? "(empty)" : $"({CurrentPSS.PSSAddress.PostalCode})";

                    PanelViewAndEdit.Controls.Add(lblPostalCode);

                    //Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 4;

                    if (CurrentPSS.PSSAddressNew.PostalCode != null)
                    {
                        currentTop = lblPostalCode.Bottom + 4;

                        Label lblPostalCodeNew = new Label();
                        lblPostalCodeNew.AutoSize = true;
                        lblPostalCodeNew.Location = new Point(lblPostalCodeText.Left, lblPostalCode.Bottom + 4);
                        lblPostalCodeNew.Font = new Font(new FontFamily(lblPostalCodeNew.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        lblPostalCodeNew.ForeColor = CurrentPSS.PSSAddressNew.PostalCode != null ? ForeColorChangedOrNew : ForeColorNormal;
                        lblPostalCodeNew.Text = string.IsNullOrWhiteSpace(CurrentPSS.PSSAddressNew.PostalCode) ? "empty" : CurrentPSS.PSSAddressNew.PostalCode;

                        PanelViewAndEdit.Controls.Add(lblPostalCodeNew);

                        //Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 4;
                    }

                }

                //X = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                // ---------------------------------------------------------------------------------------------------------------
                // end of PostalCode
                // ---------------------------------------------------------------------------------------------------------------

                #endregion PostalCode


                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;

                #region Observation
                Label lblLastObsText = new Label();
                lblLastObsText.AutoSize = true;
                lblLastObsText.Location = new Point(10, Y);
                lblLastObsText.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                lblLastObsText.Font = new Font(new FontFamily(lblLastObsText.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                lblLastObsText.ForeColor = CurrentPSS.PSSObs.ObsDateNew != null ? ForeColorChangedOrNew : ForeColorNormal;
                lblLastObsText.Text = $"Observation Date: ";

                PanelViewAndEdit.Controls.Add(lblLastObsText);

                X = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                if (IsEditing)
                {
                    if (CurrentPSS.PSSObs.ObsDateNew != null)
                    {
                        Label lblLastObsOld = new Label();
                        lblLastObsOld.AutoSize = true;
                        lblLastObsOld.Location = new Point(X, lblLastObsText.Top);
                        lblLastObsOld.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                        lblLastObsOld.Font = new Font(new FontFamily(lblLastObsOld.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                        lblLastObsOld.ForeColor = CurrentPSS.PSSObs.ObsDateNew != null ? ForeColorChangedOrNew : ForeColorNormal;
                        if (CurrentPSS.PSSObs.ObsDate != null)
                        {
                            lblLastObsOld.Text = $" ({CurrentPSS.PSSObs.ObsDate})";
                        }
                        else
                        {
                            lblLastObsOld.Text = $" (empty)";
                        }

                        PanelViewAndEdit.Controls.Add(lblLastObsOld);

                        X = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;
                    }

                    DateTimePicker dateTimePickerObsDate = new DateTimePicker();
                    dateTimePickerObsDate.Location = new Point(X, lblLastObsText.Top);
                    dateTimePickerObsDate.Name = "dateTimePickerObsDate";
                    dateTimePickerObsDate.Format = DateTimePickerFormat.Custom;
                    dateTimePickerObsDate.CustomFormat = "yyyy MMMM dd";
                    if (CurrentPSS.PSSObs.ObsDateNew != null)
                    {
                        dateTimePickerObsDate.Value = ((DateTime)CurrentPSS.PSSObs.ObsDateNew);
                    }
                    else
                    {
                        dateTimePickerObsDate.Value = ((DateTime)CurrentPSS.PSSObs.ObsDate);
                    }
                    dateTimePickerObsDate.Tag = CurrentPSS.PSSObs.ObsID.ToString();

                    PanelViewAndEdit.Controls.Add(dateTimePickerObsDate);
                }
                else
                {
                    Label lblLastObsOld = new Label();
                    lblLastObsOld.AutoSize = true;
                    lblLastObsOld.Location = new Point(X, lblLastObsText.Top);
                    lblLastObsOld.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                    lblLastObsOld.Font = new Font(new FontFamily(lblLastObsOld.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                    lblLastObsOld.ForeColor = CurrentPSS.PSSObs.ObsDateNew != null ? ForeColorChangedOrNew : ForeColorNormal;
                    if (CurrentPSS.PSSObs.ObsDateNew != null)
                    {
                        lblLastObsOld.Text = $" ({((DateTime)CurrentPSS.PSSObs.ObsDate).ToString("yyyy MMMM dd")})";
                    }
                    else
                    {
                        lblLastObsOld.Text = $" {((DateTime)CurrentPSS.PSSObs.ObsDate).ToString("yyyy MMMM dd")}";
                    }

                    PanelViewAndEdit.Controls.Add(lblLastObsOld);

                    X = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                    if (CurrentPSS.PSSObs.ObsDateNew != null)
                    {
                        Label lblLastObsNew = new Label();
                        lblLastObsNew.AutoSize = true;
                        lblLastObsNew.Location = new Point(X, lblLastObsText.Top);
                        lblLastObsNew.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                        lblLastObsNew.Font = new Font(new FontFamily(lblLastObsNew.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                        lblLastObsNew.ForeColor = CurrentPSS.PSSObs.ObsDateNew != null ? ForeColorChangedOrNew : ForeColorNormal;
                        lblLastObsNew.Text = $" {((DateTime)CurrentPSS.PSSObs.ObsDateNew).ToString("yyyy MMMM dd")}";

                        PanelViewAndEdit.Controls.Add(lblLastObsNew);
                    }
                }

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;

                #endregion Observation

                if (IsEditing)
                {
                    Button butSaveLatLngObsAndAddress = new Button();
                    butSaveLatLngObsAndAddress.AutoSize = true;
                    butSaveLatLngObsAndAddress.Location = new Point(200, Y);
                    butSaveLatLngObsAndAddress.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                    butSaveLatLngObsAndAddress.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    butSaveLatLngObsAndAddress.Padding = new Padding(5);
                    butSaveLatLngObsAndAddress.Text = $"Save";
                    butSaveLatLngObsAndAddress.Click += butSaveLatLngAndObsAndAddress_Click;

                    PanelViewAndEdit.Controls.Add(butSaveLatLngObsAndAddress);

                }

                if (!IsEditing)
                {
                    DrawIssuesForViewing();
                    ShowPictures();
                }

                if (IsAdmin)
                {
                    bool NeedDetailsUpdate = false;
                    bool NeedIssuesUpdate = false;
                    bool NeedPicturesUpdate = false;
                    if (CurrentPSS.LatNew != null
                       || CurrentPSS.LngNew != null
                       || CurrentPSS.IsActiveNew != null
                       || CurrentPSS.IsPointSourceNew != null
                       || CurrentPSS.PSSAddressNew.AddressTVItemID != null
                       || CurrentPSS.PSSAddressNew.AddressType != null
                       || CurrentPSS.PSSAddressNew.Municipality != null
                       || CurrentPSS.PSSAddressNew.PostalCode != null
                       || CurrentPSS.PSSAddressNew.StreetName != null
                       || CurrentPSS.PSSAddressNew.StreetNumber != null
                       || CurrentPSS.PSSAddressNew.StreetType != null
                       || CurrentPSS.PSSObs.ObsDateNew != null)
                    {
                        NeedDetailsUpdate = true;
                    }

                    foreach (Issue issue in CurrentPSS.PSSObs.IssueList)
                    {
                        if (issue.PolSourceObsInfoIntListNew.Count > 0 || issue.ToRemove == true)
                        {
                            NeedIssuesUpdate = true;
                            break;
                        }
                    }
                    foreach (Picture picture in CurrentPSS.PSSPictureList)
                    {
                        if (picture.DescriptionNew != null
                            || picture.ExtensionNew != null
                            || picture.FileNameNew != null
                            || picture.ToRemove != null)
                        {
                            NeedPicturesUpdate = true;
                            break;
                        }
                    }

                    string NeedDetailsUpdateText = NeedDetailsUpdate ? "Details" : "";
                    string NeedIssuesUpdateText = NeedIssuesUpdate ? "Issues" : "";
                    string NeedPictuesUpdateText = NeedPicturesUpdate ? "Pictures" : "";
                    if (NeedDetailsUpdate || NeedIssuesUpdate || NeedPicturesUpdate)
                    {
                        Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;
                        X = 20;

                        Label lblUndersandWhatWillBeSentToDB = new Label();
                        lblUndersandWhatWillBeSentToDB.AutoSize = true;
                        lblUndersandWhatWillBeSentToDB.Location = new Point(30, Y);
                        lblUndersandWhatWillBeSentToDB.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                        lblUndersandWhatWillBeSentToDB.Font = new Font(new FontFamily(lblUndersandWhatWillBeSentToDB.Font.FontFamily.Name).Name, 12f, FontStyle.Bold);

                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine($"Here are all the thing that will be sent to the CSSPWebTools and stored in it's DB");
                        sb.AppendLine("");
                        sb.AppendLine($"{NeedDetailsUpdateText}");
                        sb.AppendLine("");
                        if (CurrentPSS.LatNew != null)
                        {
                            sb.AppendLine("     Lat       (changed)");
                        }
                        if (CurrentPSS.LngNew != null)
                        {
                            sb.AppendLine("     Lng       (changed)");
                        }
                        if (CurrentPSS.IsActiveNew != null)
                        {
                            sb.AppendLine("     IsActive       (changed)");
                        }
                        if (CurrentPSS.IsPointSourceNew != null)
                        {
                            sb.AppendLine("     IsPointSource       (changed)");
                        }
                        if (CurrentPSS.PSSAddressNew.AddressType != null
                            || CurrentPSS.PSSAddressNew.Municipality != null
                            || CurrentPSS.PSSAddressNew.PostalCode != null
                            || CurrentPSS.PSSAddressNew.StreetName != null
                            || CurrentPSS.PSSAddressNew.StreetNumber != null
                            || CurrentPSS.PSSAddressNew.StreetType != null)
                        {
                            NeedDetailsUpdate = true;
                        }
                        if (CurrentPSS.PSSAddressNew.AddressTVItemID != null)
                        {
                            sb.AppendLine("     Address       (changed)");
                        }
                        if (CurrentPSS.PSSObs.ObsDateNew != null)
                        {
                            sb.AppendLine("     ObsDate       (changed)");
                        }

                        sb.AppendLine("");
                        sb.AppendLine($"{NeedIssuesUpdateText}");
                        sb.AppendLine("");

                        int count = 0;
                        foreach (Issue issue in CurrentPSS.PSSObs.IssueList.OrderBy(c => c.Ordinal))
                        {
                            count += 1;
                            if (issue.PolSourceObsInfoIntListNew.Count > 0 || issue.ToRemove == true)
                            {
                                if (issue.ToRemove == true)
                                {
                                    sb.AppendLine($"     Issue {count}          (to be removed)");
                                }
                                else
                                {
                                    sb.AppendLine($"     Issue {count}          (changed)");
                                }
                            }
                        }

                        sb.AppendLine("");
                        sb.AppendLine($"{NeedPictuesUpdateText}");
                        sb.AppendLine("");

                        count = 0;
                        foreach (Picture picture in CurrentPSS.PSSPictureList)
                        {
                            count += 1;
                            if (picture.DescriptionNew != null
                                || picture.ExtensionNew != null
                                || picture.FileNameNew != null
                                || picture.ToRemove != null)
                            {
                                if (picture.ToRemove != null && picture.ToRemove == true)
                                {
                                    sb.AppendLine($"     Picture {count}          (to be removed)");
                                }
                                else
                                {
                                    if (!string.IsNullOrWhiteSpace(picture.FileNameNew) 
                                        || !string.IsNullOrWhiteSpace(picture.ExtensionNew)
                                        || !string.IsNullOrWhiteSpace(picture.DescriptionNew))
                                    {
                                        sb.AppendLine($"     Picture {count}          (filename and/or description changed)");
                                    }
                                }
                            }
                        }

                        lblUndersandWhatWillBeSentToDB.Text = sb.ToString();

                        PanelViewAndEdit.Controls.Add(lblUndersandWhatWillBeSentToDB);

                        Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;
                        X = 20;

                        Button butPSSSaveToCSSPWebTools = new Button();
                        butPSSSaveToCSSPWebTools.AutoSize = true;
                        butPSSSaveToCSSPWebTools.Location = new Point(20, Y);
                        butPSSSaveToCSSPWebTools.Text = "Update All Pollution Source Site Related Information To CSSPWebTools";
                        butPSSSaveToCSSPWebTools.Tag = $"{CurrentPSS.PSSTVItemID}";
                        butPSSSaveToCSSPWebTools.Font = new Font(new FontFamily(butPSSSaveToCSSPWebTools.Font.FontFamily.Name).Name, 12f, FontStyle.Bold);
                        butPSSSaveToCSSPWebTools.Padding = new Padding(5);
                        butPSSSaveToCSSPWebTools.Click += butPSSSaveToCSSPWebTools_Click;

                        PanelViewAndEdit.Controls.Add(butPSSSaveToCSSPWebTools);

                        Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;
                        X = 20;

                        Label lblReturns = new Label();
                        lblReturns.AutoSize = true;
                        lblReturns.Location = new Point(30, Y);
                        lblReturns.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                        lblReturns.Font = new Font(new FontFamily(lblUndersandWhatWillBeSentToDB.Font.FontFamily.Name).Name, 12f, FontStyle.Bold);
                        lblReturns.Text = "\r\n\r\n\r\n\r\n";

                        PanelViewAndEdit.Controls.Add(lblReturns);
                    }
                }

            }
            IsReading = false;
        }
        public void SavePolSourceSiteInfo()
        {
            foreach (Control control in PanelViewAndEdit.Controls)
            {
                switch (control.Name)
                {
                    case "checkBoxIsActive":
                        {
                            CheckBox cbIsActive = (CheckBox)control;
                            if (cbIsActive != null)
                            {
                                if (cbIsActive.Checked == CurrentPSS.IsActive)
                                {
                                    CurrentPSS.IsActiveNew = null;
                                }
                                else
                                {
                                    CurrentPSS.IsActiveNew = cbIsActive.Checked;
                                    IsDirty = true;
                                }
                            }
                        }
                        break;
                    case "checkBoxIsPointSource":
                        {
                            CheckBox checkBoxIsPointSource = (CheckBox)control;
                            if (checkBoxIsPointSource != null)
                            {
                                if (checkBoxIsPointSource.Checked == CurrentPSS.IsPointSource)
                                {
                                    CurrentPSS.IsPointSourceNew = null;
                                }
                                else
                                {
                                    CurrentPSS.IsPointSourceNew = checkBoxIsPointSource.Checked;
                                    IsDirty = true;
                                }
                            }
                        }
                        break;
                    case "comboBoxStreetType":
                        {
                            ComboBox cb = (ComboBox)control;
                            if (cb != null)
                            {
                                if (cb.SelectedItem == null)
                                {
                                    CurrentPSS.PSSAddressNew.StreetType = null;
                                }
                                else
                                {
                                    for (int i = 1, count = Enum.GetNames(typeof(StreetTypeEnum)).Count(); i < count; i++)
                                    {
                                        if ((string)cb.SelectedItem == ((StreetTypeEnum)i).ToString())
                                        {
                                            CurrentPSS.PSSAddressNew.AddressTVItemID = 10000000;
                                            CurrentPSS.PSSAddressNew.StreetType = i;
                                            IsDirty = true;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case "dateTimePickerObsDate":
                        {
                            DateTimePicker dtp = (DateTimePicker)control;
                            if (dtp != null)
                            {
                                int ObsID = int.Parse(dtp.Tag.ToString());
                                if (dtp.Value == CurrentPSS.PSSObs.ObsDate)
                                {
                                    CurrentPSS.PSSObs.ObsDateNew = null;
                                    OnStatus(new StatusEventArgs("Please enter a valid date for ObsDate"));
                                }
                                else
                                {
                                    CurrentPSS.PSSObs.ObsDateNew = dtp.Value;
                                    IsDirty = true;
                                }
                            }
                        }
                        break;
                    case "textBoxPostalCode":
                        {
                            TextBox tb = (TextBox)control;
                            if (tb != null)
                            {
                                if (string.IsNullOrWhiteSpace(tb.Text))
                                {
                                    CurrentPSS.PSSAddressNew.PostalCode = null;
                                }
                                else
                                {
                                    CurrentPSS.PSSAddressNew.AddressTVItemID = 10000000;
                                    CurrentPSS.PSSAddressNew.PostalCode = tb.Text;
                                    IsDirty = true;
                                }
                            }
                        }
                        break;
                    case "textBoxLat":
                        {
                            TextBox tb = (TextBox)control;

                            if (float.TryParse(tb.Text, out float TempFloat))
                            {
                                if (TempFloat == CurrentPSS.Lat)
                                {
                                    CurrentPSS.LatNew = null;
                                }
                                else
                                {
                                    CurrentPSS.LatNew = TempFloat;
                                    IsDirty = true;
                                }
                            }
                            else
                            {
                                tb.Text = CurrentPSS.Lat.ToString();
                                OnStatus(new StatusEventArgs("Please enter a valid number for Lat"));
                            }
                        }
                        break;
                    case "textBoxLng":
                        {
                            TextBox tb = (TextBox)control;

                            if (float.TryParse(tb.Text, out float TempFloat))
                            {
                                if (TempFloat == CurrentPSS.Lng)
                                {
                                    CurrentPSS.LngNew = null;
                                }
                                else
                                {
                                    CurrentPSS.LngNew = TempFloat;
                                    IsDirty = true;
                                }
                            }
                            else
                            {
                                tb.Text = CurrentPSS.Lng.ToString();
                                OnStatus(new StatusEventArgs("Please enter a valid number for Lat"));
                            }
                        }
                        break;
                    case "textBoxMunicipality":
                        {
                            TextBox tb = (TextBox)control;
                            if (tb != null)
                            {
                                if (string.IsNullOrWhiteSpace(tb.Text))
                                {
                                    CurrentPSS.PSSAddressNew.Municipality = null;
                                }
                                else
                                {
                                    CurrentPSS.PSSAddressNew.AddressTVItemID = 10000000;
                                    CurrentPSS.PSSAddressNew.Municipality = tb.Text;
                                    IsDirty = true;
                                }
                            }
                        }
                        break;
                    case "textBoxStreetName":
                        {
                            TextBox tb = (TextBox)control;
                            if (tb != null)
                            {
                                if (string.IsNullOrWhiteSpace(tb.Text))
                                {
                                    CurrentPSS.PSSAddressNew.StreetName = null;
                                }
                                else
                                {
                                    CurrentPSS.PSSAddressNew.AddressTVItemID = 10000000;
                                    CurrentPSS.PSSAddressNew.StreetName = tb.Text;
                                    IsDirty = true;
                                }
                            }
                        }
                        break;
                    case "textBoxStreetNumber":
                        {
                            TextBox tb = (TextBox)control;
                            if (tb != null)
                            {
                                if (string.IsNullOrWhiteSpace(tb.Text))
                                {
                                    CurrentPSS.PSSAddressNew.StreetNumber = null;
                                }
                                else
                                {
                                    CurrentPSS.PSSAddressNew.AddressTVItemID = 10000000;
                                    CurrentPSS.PSSAddressNew.StreetNumber = tb.Text;
                                    IsDirty = true;
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }

            SaveSubsectorTextFile();
        }
        private void UpdatePolSourceSitePanelColor()
        {
            foreach (var a in PanelPolSourceSite.Controls)
            {
                if (a.GetType().Name == "Panel")
                {
                    ((Panel)a).BackColor = BackColorNormal;

                    int PSSTVItemID = int.Parse(((Panel)a).Tag.ToString());
                    PSS pss = subsectorDoc.Subsector.PSSList.Where(c => c.PSSTVItemID == PSSTVItemID).FirstOrDefault();
                    if (pss != null)
                    {
                        foreach (Issue issue in pss.PSSObs.IssueList)
                        {
                            if (issue.IsWellFormed == false)
                            {
                                ((Panel)a).BackColor = BackColorNotWellFormed;
                                break;
                            }
                            if (issue.IsCompleted == false)
                            {
                                ((Panel)a).BackColor = BackColorNotCompleted;
                                break;
                            }
                        }
                    }

                    if (((Panel)a).Tag.ToString() == PolSourceSiteTVItemID.ToString())
                    {
                        ((Panel)a).BackColor = BackColorEditing;
                    }
                }
            }

        }
    }
}
