﻿using CSSPEnumsDLL.Enums;
using CSSPEnumsDLL.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSSPPolSourceSiteReadSubsectorFile
{
    public partial class ReadSubsectorFile
    {

        #region Functions private
        #endregion Functions private

        #region Functions public
        #endregion Functions public

        public void AddPicture()
        {
            OpenFileDialog openFileDialogPictures = new OpenFileDialog();
            openFileDialogPictures.InitialDirectory = $@"C:\PollutionSourceSites\{CurrentSubsectorName}\Pictures\";
            openFileDialogPictures.Title = "Browse Pictures Files";
            openFileDialogPictures.DefaultExt = @"jpg";
            openFileDialogPictures.Filter = @"jpg files (*.jpg)|*.jpg";
            if (openFileDialogPictures.ShowDialog() == DialogResult.OK)
            {
                PSS pss = subsectorDoc.Subsector.PSSList.Where(c => c.PSSTVItemID == PolSourceSiteTVItemID).FirstOrDefault();

                Picture pictureNew = new Picture();

                pictureNew.PictureTVItemID = 10000000;
                pictureNew.FileName = "Change File Name " + pictureNew.PictureTVItemID.ToString();
                pss.PSSPictureList.Add(pictureNew);

                FileInfo fi = new FileInfo(openFileDialogPictures.FileName);

                FileInfo fiCheck = new FileInfo($@"C:\PollutionSourceSites\{CurrentSubsectorName}\Pictures\{pss.SiteNumberText}_{pictureNew.PictureTVItemID}{fi.Extension}");
                pictureNew.Extension = fi.Extension;
                pictureNew.Description = "Insert Required Description";
                while (fiCheck.Exists)
                {
                    pictureNew.PictureTVItemID += 1;
                    pictureNew.FileName = "Change File Name " + pictureNew.PictureTVItemID.ToString();
                    fiCheck = new FileInfo($@"C:\PollutionSourceSites\{CurrentSubsectorName}\Pictures\{pss.SiteNumberText}_{pictureNew.PictureTVItemID}{fi.Extension}");
                }

                File.Copy(openFileDialogPictures.FileName, $@"C:\PollutionSourceSites\{CurrentSubsectorName}\Pictures\{pss.SiteNumberText}_{pictureNew.PictureTVItemID}{fi.Extension}");

                ShowPictures();
            }
        }
        public void RegenerateKMLFile()
        {
            FileInfo fi = new FileInfo($@"{BasePath}\{CurrentSubsectorName}\{CurrentSubsectorName}.kml");

            if (!GenerateKMLFileForInputTool(CurrentSubsectorName, baseURLEN, baseURLFR, Language))
            {
                OnStatus(new StatusEventArgs($@"An error happened during the regeneration of the [{fi.FullName}] file"));
                return;
            }

            if (!fi.Exists)
            {
                OnStatus(new StatusEventArgs($@"An error happened during the regeneration of the [{fi.FullName}] file"));
            }

            OnStatus(new StatusEventArgs($@"The file [{fi.FullName}] has been regenerated with new changes"));
            OnStatus(new StatusEventArgs($"Done ... file [{fi.FullName}] has been regenerated"));
        }
        public void RemovePicture(int PictureTVItemID)
        {
            PSS pss = subsectorDoc.Subsector.PSSList.Where(c => c.PSSTVItemID == PolSourceSiteTVItemID).FirstOrDefault();

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
        public void SaveDocumentSubsectorText()
        {
            StringBuilder sb = new StringBuilder();
            SubsectorDoc doc = subsectorDoc;

            sb.AppendLine($"VERSION\t{doc.Version}\t");
            sb.AppendLine($"DOCDATE\t{((DateTime)doc.DocDate).Year}|{((DateTime)doc.DocDate).Month.ToString("0#")}|{((DateTime)doc.DocDate).Day.ToString("0#")}|{((DateTime)doc.DocDate).Hour.ToString("0#")}|{((DateTime)doc.DocDate).Minute.ToString("0#")}|{((DateTime)doc.DocDate).Second.ToString("0#")}\t");
            sb.AppendLine($"SUBSECTOR\t{doc.Subsector.SubsectorTVItemID}\t{doc.Subsector.SubsectorName}\t");
            foreach (PSS pss in doc.Subsector.PSSList)
            {
                sb.AppendLine($"-----\t-------------------------------------------------\t");
                sb.AppendLine($"PSS\t{pss.PSSTVItemID}\t{((float)pss.Lat).ToString("F5")}\t{((float)pss.Lng).ToString("F5")}\t{(((bool)pss.IsActive) ? "true" : "false")}\t{(((bool)pss.IsPointSource) ? "true" : "false")}\t");
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

                sb.AppendLine($"OBS\t{pss.PSSObs.ObsID}\t{((DateTime)pss.PSSObs.LastUpdated_UTC).Year}|{((DateTime)pss.PSSObs.LastUpdated_UTC).Month.ToString("0#")}|{((DateTime)pss.PSSObs.LastUpdated_UTC).Day.ToString("0#")}|{((DateTime)pss.PSSObs.LastUpdated_UTC).Hour.ToString("0#")}|{((DateTime)pss.PSSObs.LastUpdated_UTC).Minute.ToString("0#")}|{((DateTime)pss.PSSObs.LastUpdated_UTC).Second.ToString("0#")}\t{((DateTime)pss.PSSObs.LastUpdated_UTC).Year}|{((DateTime)pss.PSSObs.LastUpdated_UTC).Month.ToString("0#")}|{((DateTime)pss.PSSObs.LastUpdated_UTC).Day.ToString("0#")}\t");

                if (!string.IsNullOrWhiteSpace(pss.PSSObs.OldWrittenDescription))
                {
                    sb.AppendLine($"DESC\t{pss.PSSObs.OldWrittenDescription}\t");
                }

                foreach (string oldIssueText in pss.OldIssueTextList)
                {
                    sb.AppendLine($"OLDISSUETEXT\t{oldIssueText}\t");
                }

                foreach (Issue issue in pss.PSSObs.IssueList)
                {
                    sb.AppendLine($"ISSUE\t{issue.IssueID}\t{issue.Ordinal}\t{((DateTime)issue.LastUpdated_UTC).Year}|{((DateTime)issue.LastUpdated_UTC).Month.ToString("0#")}|{((DateTime)issue.LastUpdated_UTC).Day.ToString("0#")}|{((DateTime)issue.LastUpdated_UTC).Hour.ToString("0#")}|{((DateTime)issue.LastUpdated_UTC).Minute.ToString("0#")}|{((DateTime)issue.LastUpdated_UTC).Second.ToString("0#")}\t{String.Join(",", issue.PolSourceObsInfoIntList)},\t");
                }
            }

            DirectoryInfo di = new DirectoryInfo($@"C:\PollutionSourceSites\{CurrentSubsectorName}\{CurrentSubsectorName}\");

            if (!di.Exists)
            {
                try
                {
                    di.Create();
                }
                catch (Exception)
                {
                    OnStatus(new StatusEventArgs("Could not create directory [" + di.FullName + "]"));
                }
            }

            FileInfo fi = new FileInfo($@"C:\PollutionSourceSites\{CurrentSubsectorName}\{CurrentSubsectorName}.txt");

            StreamWriter sw = fi.CreateText();
            sw.Write(sb.ToString());
            sw.Close();
        }
        public void ShowPolSourceSite()
        {
            Color ChangedColor = Color.Green;
            Color NoChangedColor = Color.Black;

            IsReading = true;

            CurrentPSS = subsectorDoc.Subsector.PSSList.Where(c => c.PSSTVItemID == PolSourceSiteTVItemID).FirstOrDefault();

            foreach (var a in PanelPolSourceSite.Controls)
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

            PanelViewAndEdit.Controls.Clear();

            if (Language == LanguageEnum.fr)
            {
                _BaseEnumService = new BaseEnumService(LanguageEnum.fr);
            }
            else
            {
                _BaseEnumService = new BaseEnumService(LanguageEnum.en);
            }

            PanelViewAndEdit.Controls.Clear();

            int pos = 0;
            int CurrentRight = 0;
            if (CurrentPSS != null)
            {
                Label lblTVText = new Label();
                lblTVText.AutoSize = true;
                lblTVText.Location = new Point(10, 4);
                lblTVText.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                lblTVText.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 14f, FontStyle.Bold);
                lblTVText.Text = $"{CurrentPSS.SiteNumber} {(CurrentPSS.TVTextNew != null ? CurrentPSS.TVTextNew : CurrentPSS.TVText)}";

                PanelViewAndEdit.Controls.Add(lblTVText);

                pos = lblTVText.Bottom + 20;

                Label lblLat = new Label();
                lblLat.AutoSize = true;
                lblLat.Location = new Point(20, pos);
                lblLat.Font = new Font(new FontFamily(lblLat.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                lblLat.ForeColor = CurrentPSS.LatNew != null ? ChangedColor : NoChangedColor;
                lblLat.Text = $@"Lat: " + $@"{(CurrentPSS.LatNew != null ? $" ({((float)CurrentPSS.Lat).ToString("F5")})" : "")}";

                PanelViewAndEdit.Controls.Add(lblLat);

                CurrentRight = lblLat.Right + 5;
                if (IsEditing)
                {
                    TextBox textBoxLat = new TextBox();
                    textBoxLat.Location = new Point(CurrentRight, pos);
                    textBoxLat.Name = "textBoxLat";
                    textBoxLat.Font = new Font(new FontFamily(lblLat.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    textBoxLat.Width = 100;
                    textBoxLat.Text = (CurrentPSS.LatNew != null ? (float)CurrentPSS.LatNew : (float)CurrentPSS.Lat).ToString("F5");

                    PanelViewAndEdit.Controls.Add(textBoxLat);

                    CurrentRight = textBoxLat.Right + 5;

                }
                else
                {
                    Label lblLatValue = new Label();
                    lblLatValue.AutoSize = true;
                    lblLatValue.Location = new Point(CurrentRight, pos);
                    lblLatValue.Font = new Font(new FontFamily(lblLatValue.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    lblLatValue.ForeColor = CurrentPSS.LatNew != null ? ChangedColor : NoChangedColor;
                    lblLatValue.Text = (CurrentPSS.LatNew != null ? (float)CurrentPSS.LatNew : (float)CurrentPSS.Lat).ToString("F5");

                    PanelViewAndEdit.Controls.Add(lblLatValue);

                    CurrentRight = lblLatValue.Right + 5;
                }

                Label lblLng = new Label();
                lblLng.AutoSize = true;
                lblLng.Location = new Point(CurrentRight, pos);
                lblLng.Font = new Font(new FontFamily(lblLng.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                lblLng.ForeColor = CurrentPSS.LngNew != null ? ChangedColor : NoChangedColor;
                lblLng.Text = $@"Lng: " + $@"{(CurrentPSS.LngNew != null ? $" ({((float)CurrentPSS.Lng).ToString("F5")})" : "")}";

                PanelViewAndEdit.Controls.Add(lblLng);

                CurrentRight = lblLng.Right + 5;

                if (IsEditing)
                {
                    TextBox textBoxLng = new TextBox();
                    textBoxLng.Location = new Point(CurrentRight, pos);
                    textBoxLng.Name = "textBoxLng";
                    textBoxLng.Font = new Font(new FontFamily(lblLng.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    textBoxLng.Width = 100;
                    textBoxLng.Text = CurrentPSS.LngNew != null ? ((float)CurrentPSS.LngNew).ToString("F5") : ((float)CurrentPSS.Lng).ToString("F5");

                    PanelViewAndEdit.Controls.Add(textBoxLng);

                    pos = textBoxLng.Bottom + 20;
                }
                else
                {
                    Label lblLngValue = new Label();
                    lblLngValue.AutoSize = true;
                    lblLngValue.Location = new Point(CurrentRight, pos);
                    lblLngValue.Font = new Font(new FontFamily(lblLngValue.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    lblLngValue.ForeColor = CurrentPSS.LngNew != null ? ChangedColor : NoChangedColor;
                    lblLngValue.Text = CurrentPSS.LngNew != null ? ((float)CurrentPSS.LngNew).ToString() : ((float)CurrentPSS.Lng).ToString("F5");

                    PanelViewAndEdit.Controls.Add(lblLngValue);

                    pos = lblLngValue.Bottom + 20;
                }

                if (IsEditing)
                {
                    CheckBox checkBoxIsActive = new CheckBox();
                    checkBoxIsActive.Location = new Point(20, pos);
                    checkBoxIsActive.Name = "checkBoxIsActive";
                    checkBoxIsActive.AutoSize = true;
                    checkBoxIsActive.Font = new Font(new FontFamily(lblLng.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    checkBoxIsActive.ForeColor = CurrentPSS.IsActiveNew != null ? ChangedColor : NoChangedColor;
                    checkBoxIsActive.Text = (CurrentPSS.IsActiveNew != null ? ((bool)CurrentPSS.IsActiveNew ? "Is Active" : "Not Active") : ((bool)CurrentPSS.IsActive ? "Is Active" : "Not Active"));
                    checkBoxIsActive.Checked = (CurrentPSS.IsActiveNew != null ? (bool)CurrentPSS.IsActiveNew : (bool)CurrentPSS.IsActive);

                    PanelViewAndEdit.Controls.Add(checkBoxIsActive);

                    CurrentRight = checkBoxIsActive.Right + 5;
                }
                else
                {
                    Label lblIsActive = new Label();
                    lblIsActive.AutoSize = true;
                    lblIsActive.Location = new Point(20, pos);
                    lblIsActive.Font = new Font(new FontFamily(lblIsActive.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    lblIsActive.ForeColor = CurrentPSS.IsActiveNew != null ? ChangedColor : NoChangedColor;
                    lblIsActive.Text = (CurrentPSS.IsActiveNew != null ? ((bool)CurrentPSS.IsActiveNew ? "Is Active" : "Not Active") : ((bool)CurrentPSS.IsActive ? "Is Active" : "Not Active"));

                    PanelViewAndEdit.Controls.Add(lblIsActive);

                    CurrentRight = lblIsActive.Right + 5;
                }

                if (IsEditing)
                {
                    CheckBox checkBoxIsPointSource = new CheckBox();
                    checkBoxIsPointSource.Location = new Point(CurrentRight, pos);
                    checkBoxIsPointSource.Name = "checkBoxIsPointSource";
                    checkBoxIsPointSource.AutoSize = true;
                    checkBoxIsPointSource.Font = new Font(new FontFamily(lblLng.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    checkBoxIsPointSource.ForeColor = CurrentPSS.IsPointSourceNew != null ? ChangedColor : NoChangedColor;
                    checkBoxIsPointSource.Text = (CurrentPSS.IsPointSourceNew != null ? ((bool)CurrentPSS.IsPointSourceNew ? "Is Point Source" : "Not Point Source") : ((bool)CurrentPSS.IsPointSource ? "Is Point Source" : "Not Point Source"));
                    checkBoxIsPointSource.Checked = (CurrentPSS.IsPointSourceNew != null ? (bool)CurrentPSS.IsPointSourceNew : (bool)CurrentPSS.IsPointSource);

                    PanelViewAndEdit.Controls.Add(checkBoxIsPointSource);

                    pos = checkBoxIsPointSource.Bottom + 20;
                }
                else
                {
                    Label lblIsPointSource = new Label();
                    lblIsPointSource.AutoSize = true;
                    lblIsPointSource.Location = new Point(CurrentRight, pos);
                    lblIsPointSource.Font = new Font(new FontFamily(lblIsPointSource.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    lblIsPointSource.ForeColor = CurrentPSS.IsPointSourceNew != null ? ChangedColor : NoChangedColor;
                    lblIsPointSource.Text = (CurrentPSS.IsPointSourceNew != null ? ((bool)CurrentPSS.IsPointSourceNew ? "Is Point Source" : "Not Point Source") : ((bool)CurrentPSS.IsPointSource ? "Is Point Source" : "Not Point Source"));

                    PanelViewAndEdit.Controls.Add(lblIsPointSource);


                    pos = lblIsPointSource.Bottom + 20;
                }


                // doing address

                CurrentRight = 10;

                int AddressPos = 0;
                #region StreetNumber
                // ---------------------------------------------------------------------------------------------------------------
                // start of StreetNumber
                // ---------------------------------------------------------------------------------------------------------------
                Label lblStreetNumberText = new Label();
                lblStreetNumberText.AutoSize = true;
                lblStreetNumberText.Location = new Point(CurrentRight, pos);
                lblStreetNumberText.Font = new Font(new FontFamily(lblStreetNumberText.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                lblStreetNumberText.ForeColor = CurrentPSS.PSSAddressNew.StreetNumber != null ? ChangedColor : NoChangedColor;
                lblStreetNumberText.Text = $@"Street Number";

                PanelViewAndEdit.Controls.Add(lblStreetNumberText);

                if (IsEditing)
                {
                    int currentTop = lblStreetNumberText.Bottom + 4;

                    if (CurrentPSS.PSSAddressNew.StreetNumber != null)
                    {
                        Label lblStreetNumberOld = new Label();
                        lblStreetNumberOld.AutoSize = true;
                        lblStreetNumberOld.Location = new Point(CurrentRight, currentTop);
                        lblStreetNumberOld.Font = new Font(new FontFamily(lblStreetNumberOld.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        lblStreetNumberOld.ForeColor = CurrentPSS.PSSAddressNew.StreetNumber != null ? ChangedColor : NoChangedColor;
                        lblStreetNumberOld.Text = string.IsNullOrWhiteSpace(CurrentPSS.PSSAddress.StreetNumber) ? "(empty)" : $"({CurrentPSS.PSSAddress.StreetNumber})";

                        PanelViewAndEdit.Controls.Add(lblStreetNumberOld);

                        currentTop = lblStreetNumberOld.Bottom + 4;

                    }

                    TextBox textBoxStreetNumber = new TextBox();
                    textBoxStreetNumber.Location = new Point(CurrentRight, currentTop);
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
                    lblStreetNumber.Location = new Point(CurrentRight, currentTop);
                    lblStreetNumber.Font = new Font(new FontFamily(lblStreetNumber.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    lblStreetNumber.ForeColor = CurrentPSS.PSSAddressNew.StreetNumber != null ? ChangedColor : NoChangedColor;
                    lblStreetNumber.Text = string.IsNullOrWhiteSpace(CurrentPSS.PSSAddress.StreetNumber) ? "(empty)" : $"({CurrentPSS.PSSAddress.StreetNumber})";

                    PanelViewAndEdit.Controls.Add(lblStreetNumber);

                    currentTop = lblStreetNumber.Bottom + 4;

                    AddressPos = lblStreetNumber.Bottom + 10;

                    if (CurrentPSS.PSSAddressNew.StreetNumber != null)
                    {
                        Label lblStreetNumberNew = new Label();
                        lblStreetNumberNew.AutoSize = true;
                        lblStreetNumberNew.Location = new Point(CurrentRight, currentTop);
                        lblStreetNumberNew.Font = new Font(new FontFamily(lblStreetNumberNew.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        lblStreetNumberNew.ForeColor = CurrentPSS.PSSAddressNew.StreetNumber != null ? ChangedColor : NoChangedColor;
                        lblStreetNumberNew.Text = string.IsNullOrWhiteSpace(CurrentPSS.PSSAddressNew.StreetNumber) ? "empty" : CurrentPSS.PSSAddressNew.StreetNumber;

                        PanelViewAndEdit.Controls.Add(lblStreetNumberNew);

                        currentTop = lblStreetNumberNew.Bottom + 4;

                        AddressPos = lblStreetNumberNew.Bottom + 10;

                    }

                }

                CurrentRight = lblStreetNumberText.Right + 10;

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
                lblStreetNameText.Location = new Point(CurrentRight, lblStreetNumberText.Top);
                lblStreetNameText.Font = new Font(new FontFamily(lblStreetNameText.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                lblStreetNameText.ForeColor = CurrentPSS.PSSAddressNew.StreetName != null ? ChangedColor : NoChangedColor;
                lblStreetNameText.Text = $@"Street Name    ";

                PanelViewAndEdit.Controls.Add(lblStreetNameText);

                if (IsEditing)
                {
                    int currentTop = lblStreetNameText.Bottom + 4;

                    if (CurrentPSS.PSSAddressNew.StreetName != null)
                    {
                        Label lblStreetNameOld = new Label();
                        lblStreetNameOld.AutoSize = true;
                        lblStreetNameOld.Location = new Point(CurrentRight, currentTop);
                        lblStreetNameOld.Font = new Font(new FontFamily(lblStreetNameOld.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        lblStreetNameOld.ForeColor = CurrentPSS.PSSAddressNew.StreetName != null ? ChangedColor : NoChangedColor;
                        lblStreetNameOld.Text = string.IsNullOrWhiteSpace(CurrentPSS.PSSAddress.StreetName) ? "(empty)" : $"({CurrentPSS.PSSAddress.StreetName})";

                        PanelViewAndEdit.Controls.Add(lblStreetNameOld);

                        currentTop = lblStreetNameOld.Bottom + 4;

                    }

                    TextBox textBoxStreetName = new TextBox();
                    textBoxStreetName.Location = new Point(CurrentRight, currentTop);
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

                    pos = textBoxStreetName.Bottom + 10;
                }
                else
                {
                    Label lblStreetName = new Label();
                    lblStreetName.AutoSize = true;
                    lblStreetName.Location = new Point(CurrentRight, lblStreetNameText.Bottom + 4);
                    lblStreetName.Font = new Font(new FontFamily(lblStreetName.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    lblStreetName.ForeColor = CurrentPSS.PSSAddressNew.StreetName != null ? ChangedColor : NoChangedColor;
                    lblStreetName.Text = string.IsNullOrWhiteSpace(CurrentPSS.PSSAddress.StreetName) ? "(empty)" : $"({CurrentPSS.PSSAddress.StreetName})";

                    PanelViewAndEdit.Controls.Add(lblStreetName);

                    pos = lblStreetName.Bottom + 4;

                    if (CurrentPSS.PSSAddressNew.StreetName != null)
                    {
                        Label lblStreetNameNew = new Label();
                        lblStreetNameNew.AutoSize = true;
                        lblStreetNameNew.Location = new Point(CurrentRight, lblStreetName.Bottom + 4);
                        lblStreetNameNew.Font = new Font(new FontFamily(lblStreetNameNew.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        lblStreetNameNew.ForeColor = CurrentPSS.PSSAddressNew.StreetName != null ? ChangedColor : NoChangedColor;
                        lblStreetNameNew.Text = string.IsNullOrWhiteSpace(CurrentPSS.PSSAddressNew.StreetName) ? "empty" : CurrentPSS.PSSAddressNew.StreetName;

                        PanelViewAndEdit.Controls.Add(lblStreetNameNew);

                        pos = lblStreetNameNew.Bottom + 4;
                    }

                }

                CurrentRight = lblStreetNameText.Right + 10;

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
                lblStreetTypeText.Location = new Point(CurrentRight, lblStreetNumberText.Top);
                lblStreetTypeText.Font = new Font(new FontFamily(lblStreetTypeText.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                lblStreetTypeText.ForeColor = CurrentPSS.PSSAddressNew.StreetType != null ? ChangedColor : NoChangedColor;
                lblStreetTypeText.Text = $@"Street Type   ";

                PanelViewAndEdit.Controls.Add(lblStreetTypeText);

                if (IsEditing)
                {
                    int currentTop = lblStreetTypeText.Bottom + 4;

                    if (CurrentPSS.PSSAddressNew.StreetType != null)
                    {
                        Label lblStreetTypeOld = new Label();
                        lblStreetTypeOld.AutoSize = true;
                        lblStreetTypeOld.Location = new Point(CurrentRight, currentTop);
                        lblStreetTypeOld.Font = new Font(new FontFamily(lblStreetTypeOld.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        lblStreetTypeOld.ForeColor = CurrentPSS.PSSAddressNew.StreetType != null ? ChangedColor : NoChangedColor;
                        lblStreetTypeOld.Text = CurrentPSS.PSSAddress.StreetType == null ? "(empty)" : $"({((StreetTypeEnum)CurrentPSS.PSSAddress.StreetType).ToString()})";

                        PanelViewAndEdit.Controls.Add(lblStreetTypeOld);

                        currentTop = lblStreetTypeOld.Bottom + 4;

                    }

                    ComboBox comboBoxStreetType = new ComboBox();
                    comboBoxStreetType.Location = new Point(CurrentRight, currentTop);
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

                    pos = comboBoxStreetType.Bottom + 10;
                }
                else
                {
                    Label lblStreetType = new Label();
                    lblStreetType.AutoSize = true;
                    lblStreetType.Location = new Point(CurrentRight, lblStreetTypeText.Bottom + 4);
                    lblStreetType.Font = new Font(new FontFamily(lblStreetType.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    lblStreetType.ForeColor = CurrentPSS.PSSAddressNew.StreetType != null ? ChangedColor : NoChangedColor;
                    lblStreetType.Text = CurrentPSS.PSSAddress.StreetType == null ? "(empty)" : $"({((StreetTypeEnum)CurrentPSS.PSSAddress.StreetType).ToString()})";

                    PanelViewAndEdit.Controls.Add(lblStreetType);

                    pos = lblStreetType.Bottom + 4;

                    if (CurrentPSS.PSSAddressNew.StreetType != null)
                    {
                        Label lblStreetTypeNew = new Label();
                        lblStreetTypeNew.AutoSize = true;
                        lblStreetTypeNew.Location = new Point(CurrentRight, lblStreetType.Bottom + 4);
                        lblStreetTypeNew.Font = new Font(new FontFamily(lblStreetTypeNew.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        lblStreetTypeNew.ForeColor = CurrentPSS.PSSAddressNew.StreetType != null ? ChangedColor : NoChangedColor;
                        lblStreetTypeNew.Text = CurrentPSS.PSSAddressNew.StreetType == null ? "empty" : ((StreetTypeEnum)CurrentPSS.PSSAddressNew.StreetType).ToString();

                        PanelViewAndEdit.Controls.Add(lblStreetTypeNew);

                        pos = lblStreetTypeNew.Bottom + 4;
                    }

                }

                CurrentRight = lblStreetTypeText.Right + 10;

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
                lblMunicipalityText.Location = new Point(CurrentRight, lblStreetNumberText.Top);
                lblMunicipalityText.Font = new Font(new FontFamily(lblMunicipalityText.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                lblMunicipalityText.ForeColor = CurrentPSS.PSSAddressNew.Municipality != null ? ChangedColor : NoChangedColor;
                lblMunicipalityText.Text = $@"Municipality        ";

                PanelViewAndEdit.Controls.Add(lblMunicipalityText);

                if (IsEditing)
                {
                    int currentTop = lblMunicipalityText.Bottom + 4;

                    if (CurrentPSS.PSSAddressNew.Municipality != null)
                    {
                        Label lblMunicipalityOld = new Label();
                        lblMunicipalityOld.AutoSize = true;
                        lblMunicipalityOld.Location = new Point(CurrentRight, currentTop);
                        lblMunicipalityOld.Font = new Font(new FontFamily(lblMunicipalityOld.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        lblMunicipalityOld.ForeColor = CurrentPSS.PSSAddressNew.Municipality != null ? ChangedColor : NoChangedColor;
                        lblMunicipalityOld.Text = string.IsNullOrWhiteSpace(CurrentPSS.PSSAddress.Municipality) ? "(empty)" : $"({CurrentPSS.PSSAddress.Municipality})";

                        PanelViewAndEdit.Controls.Add(lblMunicipalityOld);

                        currentTop = lblMunicipalityOld.Bottom + 4;

                    }

                    TextBox textBoxMunicipality = new TextBox();
                    textBoxMunicipality.Location = new Point(CurrentRight, currentTop);
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

                    pos = textBoxMunicipality.Bottom + 10;
                }
                else
                {
                    Label lblMunicipality = new Label();
                    lblMunicipality.AutoSize = true;
                    lblMunicipality.Location = new Point(CurrentRight, lblMunicipalityText.Bottom + 4);
                    lblMunicipality.Font = new Font(new FontFamily(lblMunicipality.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    lblMunicipality.ForeColor = CurrentPSS.PSSAddressNew.Municipality != null ? ChangedColor : NoChangedColor;
                    lblMunicipality.Text = string.IsNullOrWhiteSpace(CurrentPSS.PSSAddress.Municipality) ? "(empty)" : $"({CurrentPSS.PSSAddress.Municipality})";

                    PanelViewAndEdit.Controls.Add(lblMunicipality);

                    pos = lblMunicipality.Bottom + 4;

                    if (CurrentPSS.PSSAddressNew.Municipality != null)
                    {
                        Label lblMunicipalityNew = new Label();
                        lblMunicipalityNew.AutoSize = true;
                        lblMunicipalityNew.Location = new Point(CurrentRight, lblMunicipality.Bottom + 4);
                        lblMunicipalityNew.Font = new Font(new FontFamily(lblMunicipalityNew.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        lblMunicipalityNew.ForeColor = CurrentPSS.PSSAddressNew.Municipality != null ? ChangedColor : NoChangedColor;
                        lblMunicipalityNew.Text = string.IsNullOrWhiteSpace(CurrentPSS.PSSAddressNew.Municipality) ? "empty" : CurrentPSS.PSSAddressNew.Municipality;

                        PanelViewAndEdit.Controls.Add(lblMunicipalityNew);

                        pos = lblMunicipalityNew.Bottom + 4;
                    }

                }

                CurrentRight = lblMunicipalityText.Right + 10;

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
                lblPostalCodeText.Location = new Point(CurrentRight, lblStreetNumberText.Top);
                lblPostalCodeText.Font = new Font(new FontFamily(lblPostalCodeText.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                lblPostalCodeText.ForeColor = CurrentPSS.PSSAddressNew.PostalCode != null ? ChangedColor : NoChangedColor;
                lblPostalCodeText.Text = $@"Postal Code";

                PanelViewAndEdit.Controls.Add(lblPostalCodeText);

                if (IsEditing)
                {
                    int currentTop = lblPostalCodeText.Bottom + 4;

                    if (CurrentPSS.PSSAddressNew.PostalCode != null)
                    {
                        Label lblPostalCodeOld = new Label();
                        lblPostalCodeOld.AutoSize = true;
                        lblPostalCodeOld.Location = new Point(CurrentRight, currentTop);
                        lblPostalCodeOld.Font = new Font(new FontFamily(lblPostalCodeOld.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        lblPostalCodeOld.ForeColor = CurrentPSS.PSSAddressNew.PostalCode != null ? ChangedColor : NoChangedColor;
                        lblPostalCodeOld.Text = string.IsNullOrWhiteSpace(CurrentPSS.PSSAddress.PostalCode) ? "(empty)" : $"({CurrentPSS.PSSAddress.PostalCode})";

                        PanelViewAndEdit.Controls.Add(lblPostalCodeOld);

                        currentTop = lblPostalCodeOld.Bottom + 4;

                    }

                    TextBox textBoxPostalCode = new TextBox();
                    textBoxPostalCode.Location = new Point(CurrentRight, currentTop);
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

                    pos = textBoxPostalCode.Bottom + 10;
                }
                else
                {
                    Label lblPostalCode = new Label();
                    lblPostalCode.AutoSize = true;
                    lblPostalCode.Location = new Point(CurrentRight, lblPostalCodeText.Bottom + 4);
                    lblPostalCode.Font = new Font(new FontFamily(lblPostalCode.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    lblPostalCode.ForeColor = CurrentPSS.PSSAddressNew.PostalCode != null ? ChangedColor : NoChangedColor;
                    lblPostalCode.Text = string.IsNullOrWhiteSpace(CurrentPSS.PSSAddress.PostalCode) ? "(empty)" : $"({CurrentPSS.PSSAddress.PostalCode})";

                    PanelViewAndEdit.Controls.Add(lblPostalCode);

                    pos = lblPostalCode.Bottom + 4;

                    if (CurrentPSS.PSSAddressNew.PostalCode != null)
                    {
                        Label lblPostalCodeNew = new Label();
                        lblPostalCodeNew.AutoSize = true;
                        lblPostalCodeNew.Location = new Point(CurrentRight, lblPostalCode.Bottom + 4);
                        lblPostalCodeNew.Font = new Font(new FontFamily(lblPostalCodeNew.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        lblPostalCodeNew.ForeColor = CurrentPSS.PSSAddressNew.PostalCode != null ? ChangedColor : NoChangedColor;
                        lblPostalCodeNew.Text = string.IsNullOrWhiteSpace(CurrentPSS.PSSAddressNew.PostalCode) ? "empty" : CurrentPSS.PSSAddressNew.PostalCode;

                        PanelViewAndEdit.Controls.Add(lblPostalCodeNew);

                        pos = lblPostalCodeNew.Bottom + 4;
                    }

                }

                CurrentRight = lblPostalCodeText.Right + 10;

                // ---------------------------------------------------------------------------------------------------------------
                // end of PostalCode
                // ---------------------------------------------------------------------------------------------------------------

                #endregion PostalCode


                pos = lblStreetNumberText.Bottom + 80;

                #region Observation
                Label lblLastObsText = new Label();
                lblLastObsText.AutoSize = true;
                lblLastObsText.Location = new Point(10, pos);
                lblLastObsText.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                lblLastObsText.Font = new Font(new FontFamily(lblLastObsText.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                lblLastObsText.ForeColor = CurrentPSS.PSSObs.ObsDateNew != null ? ChangedColor : NoChangedColor;
                lblLastObsText.Text = $"Observation Date: ";

                PanelViewAndEdit.Controls.Add(lblLastObsText);

                CurrentRight = lblLastObsText.Right + 10;

                if (IsEditing)
                {
                    if (CurrentPSS.PSSObs.ObsDateNew != null)
                    {
                        Label lblLastObsOld = new Label();
                        lblLastObsOld.AutoSize = true;
                        lblLastObsOld.Location = new Point(CurrentRight, lblLastObsText.Top);
                        lblLastObsOld.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                        lblLastObsOld.Font = new Font(new FontFamily(lblLastObsOld.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                        lblLastObsOld.ForeColor = CurrentPSS.PSSObs.ObsDateNew != null ? ChangedColor : NoChangedColor;
                        if (CurrentPSS.PSSObs.ObsDate != null)
                        {
                            lblLastObsOld.Text = $" ({CurrentPSS.PSSObs.ObsDate})";
                        }
                        else
                        {
                            lblLastObsOld.Text = $" (empty)";
                        }

                        PanelViewAndEdit.Controls.Add(lblLastObsOld);

                        CurrentRight = lblLastObsOld.Right + 10;
                    }

                    DateTimePicker dateTimePickerObsDate = new DateTimePicker();
                    dateTimePickerObsDate.Location = new Point(CurrentRight, lblLastObsText.Top);
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
                    lblLastObsOld.Location = new Point(CurrentRight, lblLastObsText.Top);
                    lblLastObsOld.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                    lblLastObsOld.Font = new Font(new FontFamily(lblLastObsOld.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                    lblLastObsOld.ForeColor = CurrentPSS.PSSObs.ObsDateNew != null ? ChangedColor : NoChangedColor;
                    if (CurrentPSS.PSSObs.ObsDateNew != null)
                    {
                        lblLastObsOld.Text = $" ({CurrentPSS.PSSObs.ObsDate})";
                    }
                    else
                    {
                        lblLastObsOld.Text = $" {CurrentPSS.PSSObs.ObsDate}";
                    }

                    PanelViewAndEdit.Controls.Add(lblLastObsOld);

                    CurrentRight = lblLastObsOld.Right + 10;

                    if (CurrentPSS.PSSObs.ObsDateNew != null)
                    {
                        Label lblLastObsNew = new Label();
                        lblLastObsNew.AutoSize = true;
                        lblLastObsNew.Location = new Point(CurrentRight, lblLastObsText.Top);
                        lblLastObsNew.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                        lblLastObsNew.Font = new Font(new FontFamily(lblLastObsNew.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                        lblLastObsNew.ForeColor = CurrentPSS.PSSObs.ObsDateNew != null ? ChangedColor : NoChangedColor;
                        lblLastObsNew.Text = $" {CurrentPSS.PSSObs.ObsDateNew}";

                        PanelViewAndEdit.Controls.Add(lblLastObsNew);
                    }
                }

                pos = lblLastObsText.Bottom + 20;

                #endregion Observation

                pos = lblLastObsText.Bottom + 80;

                if (IsEditing)
                {
                    Button butSaveLatLngObsAndAddress = new Button();
                    butSaveLatLngObsAndAddress.AutoSize = true;
                    butSaveLatLngObsAndAddress.Location = new Point(200, pos);
                    butSaveLatLngObsAndAddress.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                    butSaveLatLngObsAndAddress.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    butSaveLatLngObsAndAddress.Text = $"Save";
                    butSaveLatLngObsAndAddress.Click += butSaveLatLngAndObsAndAddress_Click;

                    PanelViewAndEdit.Controls.Add(butSaveLatLngObsAndAddress);
                }
            }
            IsReading = false;
        }
        public void SaveLatLngAndObsAndAddress()
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
                                if (CurrentPSS.PSSAddress.StreetType == null)
                                {
                                    if (cb.SelectedItem == null)
                                    {
                                        CurrentPSS.PSSAddressNew.StreetType = null;
                                    }
                                    else
                                    {
                                        for (int i = 1, count = Enum.GetNames(typeof(StreetTypeEnum)).Count(); i < count; i++)
                                        {
                                            if (cb.SelectedItem == ((StreetTypeEnum)i).ToString())
                                            {
                                                CurrentPSS.PSSAddressNew.StreetType = i;
                                                IsDirty = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (cb.SelectedItem == ((StreetTypeEnum)CurrentPSS.PSSAddress.StreetType).ToString())
                                    {
                                        CurrentPSS.PSSAddressNew.StreetType = null;
                                    }
                                    else
                                    {
                                        for (int i = 1, count = Enum.GetNames(typeof(StreetTypeEnum)).Count(); i < count; i++)
                                        {
                                            if (cb.SelectedItem == ((StreetTypeEnum)i).ToString())
                                            {
                                                CurrentPSS.PSSAddressNew.StreetType = i;
                                                IsDirty = true;
                                                break;
                                            }
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
                                if (CurrentPSS.PSSAddress.PostalCode == null)
                                {
                                    if (string.IsNullOrWhiteSpace(tb.Text))
                                    {
                                        CurrentPSS.PSSAddressNew.PostalCode = null;
                                    }
                                    else
                                    {
                                        CurrentPSS.PSSAddressNew.PostalCode = tb.Text;
                                        IsDirty = true;
                                    }
                                }
                                else
                                {
                                    if (tb.Text == CurrentPSS.PSSAddress.PostalCode)
                                    {
                                        CurrentPSS.PSSAddressNew.PostalCode = null;
                                    }
                                    else
                                    {
                                        CurrentPSS.PSSAddressNew.PostalCode = tb.Text;
                                        IsDirty = true;
                                    }
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
                                if (CurrentPSS.PSSAddress.Municipality == null)
                                {
                                    if (string.IsNullOrWhiteSpace(tb.Text))
                                    {
                                        CurrentPSS.PSSAddressNew.Municipality = null;
                                    }
                                    else
                                    {
                                        CurrentPSS.PSSAddressNew.Municipality = tb.Text;
                                        IsDirty = true;
                                    }
                                }
                                else
                                {
                                    if (tb.Text == CurrentPSS.PSSAddress.Municipality)
                                    {
                                        CurrentPSS.PSSAddressNew.Municipality = null;
                                    }
                                    else
                                    {
                                        CurrentPSS.PSSAddressNew.Municipality = tb.Text;
                                        IsDirty = true;
                                    }
                                }
                            }
                        }
                        break;
                    case "textBoxStreetName":
                        {
                            TextBox tb = (TextBox)control;
                            if (tb != null)
                            {
                                if (CurrentPSS.PSSAddress.StreetName == null)
                                {
                                    if (string.IsNullOrWhiteSpace(tb.Text))
                                    {
                                        CurrentPSS.PSSAddressNew.StreetName = null;
                                    }
                                    else
                                    {
                                        CurrentPSS.PSSAddressNew.StreetName = tb.Text;
                                        IsDirty = true;
                                    }
                                }
                                else
                                {
                                    if (tb.Text == CurrentPSS.PSSAddress.StreetName)
                                    {
                                        CurrentPSS.PSSAddressNew.StreetName = null;
                                    }
                                    else
                                    {
                                        CurrentPSS.PSSAddressNew.StreetName = tb.Text;
                                        IsDirty = true;
                                    }
                                }
                            }
                        }
                        break;
                    case "textBoxStreetNumber":
                        {
                            TextBox tb = (TextBox)control;
                            if (tb != null)
                            {
                                if (CurrentPSS.PSSAddress.StreetNumber == null)
                                {
                                    if (string.IsNullOrWhiteSpace(tb.Text))
                                    {
                                        CurrentPSS.PSSAddressNew.StreetNumber = null;
                                    }
                                    else
                                    {
                                        CurrentPSS.PSSAddressNew.StreetNumber = tb.Text;
                                        IsDirty = true;
                                    }
                                }
                                else
                                {
                                    if (tb.Text == CurrentPSS.PSSAddress.StreetNumber)
                                    {
                                        CurrentPSS.PSSAddressNew.StreetNumber = null;
                                    }
                                    else
                                    {
                                        CurrentPSS.PSSAddressNew.StreetNumber = tb.Text;
                                        IsDirty = true;
                                    }
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        public void SavePictureInfo()
        {
        }
        public void ShowIssues()
        {
            PanelViewAndEdit.Controls.Clear();

            PSS pss = subsectorDoc.Subsector.PSSList.Where(c => c.PSSTVItemID == PolSourceSiteTVItemID).FirstOrDefault();

            int pos = 4;

            Label lblIssues = new Label();
            lblIssues.AutoSize = true;
            lblIssues.Location = new Point(10, pos);
            lblIssues.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
            lblIssues.Font = new Font(new FontFamily(lblIssues.Font.FontFamily.Name).Name, 14f, FontStyle.Bold);
            lblIssues.Text = $"Issues";

            PanelViewAndEdit.Controls.Add(lblIssues);

            pos = lblIssues.Bottom + 20;

            Label lblWrittenDesc = new Label();
            lblWrittenDesc.AutoSize = true;
            lblWrittenDesc.Location = new Point(10, pos);
            lblWrittenDesc.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
            lblWrittenDesc.Font = new Font(new FontFamily(lblWrittenDesc.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
            lblWrittenDesc.Text = $"Written Description: ";

            PanelViewAndEdit.Controls.Add(lblWrittenDesc);

            pos = lblWrittenDesc.Bottom + 8;

            Label lblWrittenDesc2 = new Label();
            lblWrittenDesc2.AutoSize = true;
            lblWrittenDesc2.Location = new Point(30, pos);
            lblWrittenDesc2.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
            lblWrittenDesc2.Font = new Font(new FontFamily(lblWrittenDesc2.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
            if (string.IsNullOrWhiteSpace(CurrentPSS.PSSObs.OldWrittenDescription))
            {
                lblWrittenDesc2.Text = $"No written description";
            }
            else
            {
                lblWrittenDesc2.Text = $"{CurrentPSS.PSSObs.OldWrittenDescription}";
            }

            PanelViewAndEdit.Controls.Add(lblWrittenDesc2);

            pos = lblWrittenDesc2.Bottom + 10;

            if (CurrentPSS.OldIssueTextList.Count > 0)
            {
                Label lblOldIssue = new Label();
                lblOldIssue.AutoSize = true;
                lblOldIssue.Location = new Point(10, pos);
                lblOldIssue.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                lblOldIssue.Font = new Font(new FontFamily(lblOldIssue.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                lblOldIssue.Text = $"Old Issue Text:";

                PanelViewAndEdit.Controls.Add(lblOldIssue);

                pos = lblOldIssue.Bottom + 6;

                int OldIssueCount = 0;
                foreach (string OldIssueText in CurrentPSS.OldIssueTextList)
                {
                    OldIssueCount += 1;

                    Label lblOldIssueCount = new Label();
                    lblOldIssueCount.AutoSize = true;
                    lblOldIssueCount.Location = new Point(30, pos);
                    lblOldIssueCount.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                    lblOldIssueCount.Font = new Font(new FontFamily(lblOldIssueCount.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                    lblOldIssueCount.Text = $"{OldIssueCount}) - ";

                    PanelViewAndEdit.Controls.Add(lblOldIssueCount);

                    Label lblOldIssueText = new Label();
                    lblOldIssueText.AutoSize = true;
                    lblOldIssueText.Location = new Point(lblOldIssueCount.Right, pos);
                    lblOldIssueText.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                    lblOldIssueText.Font = new Font(new FontFamily(lblOldIssueText.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    lblOldIssueText.Text = $"{OldIssueText}";

                    PanelViewAndEdit.Controls.Add(lblOldIssueText);

                    pos = lblOldIssueText.Bottom + 10;
                }
            }
            else
            {
                Label lblOldIssue = new Label();
                lblOldIssue.AutoSize = true;
                lblOldIssue.Location = new Point(30, pos);
                lblOldIssue.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                lblOldIssue.Font = new Font(new FontFamily(lblOldIssue.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                lblOldIssue.Text = $"No Old Issue Text:";

                PanelViewAndEdit.Controls.Add(lblOldIssue);

                pos = lblOldIssue.Bottom + 10;
            }

            if (pss.PSSObs.IssueList.Count > 0)
            {
                int IssueCount = 0;
                foreach (Issue issue in pss.PSSObs.IssueList)
                {
                    IssueCount += 1;

                    Label lblIssue = new Label();
                    lblIssue.AutoSize = true;
                    lblIssue.Location = new Point(10, pos);
                    lblIssue.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                    lblIssue.Font = new Font(new FontFamily(lblIssue.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                    lblIssue.Text = $"Issue ({IssueCount}) --- Last Update (UTC): ";

                    PanelViewAndEdit.Controls.Add(lblIssue);

                    Label lblIssueLastUpdate = new Label();
                    lblIssueLastUpdate.AutoSize = true;
                    lblIssueLastUpdate.Location = new Point(lblIssue.Right, lblIssue.Top);
                    lblIssueLastUpdate.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                    lblIssueLastUpdate.Font = new Font(new FontFamily(lblIssueLastUpdate.Font.FontFamily.Name).Name, 12f, FontStyle.Regular);
                    lblIssueLastUpdate.Text = $"{((DateTime)issue.LastUpdated_UTC).ToString("yyyy MMMM dd HH:mm:ss")}";

                    PanelViewAndEdit.Controls.Add(lblIssueLastUpdate);

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
                                    Temp = Temp;
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
                    lblSelected.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                    lblSelected.Font = new Font(new FontFamily(lblSelected.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    lblSelected.Text = $"{TVText}";

                    PanelViewAndEdit.Controls.Add(lblSelected);

                    pos = lblSelected.Bottom + 10;
                }
            }

        }
        public void ShowMap()
        {
            PanelViewAndEdit.Controls.Clear();

            Label tempLabel = new Label();

            tempLabel.AutoSize = true;
            tempLabel.Location = new Point(10, 4);
            tempLabel.TabIndex = 0;
            tempLabel.Text = $"ShowMap is working " + PolSourceSiteTVItemID.ToString();

            PanelViewAndEdit.Controls.Add(tempLabel);
        }
        public void ShowPictures()
        {
            PanelViewAndEdit.Controls.Clear();

            PSS pss = subsectorDoc.Subsector.PSSList.Where(c => c.PSSTVItemID == PolSourceSiteTVItemID).FirstOrDefault();

            int pos = 4;

            Label lblPictures = new Label();
            lblPictures.AutoSize = true;
            lblPictures.Location = new Point(10, pos);
            lblPictures.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
            lblPictures.Font = new Font(new FontFamily(lblPictures.Font.FontFamily.Name).Name, 14f, FontStyle.Bold);
            lblPictures.Text = $"Pictures";

            PanelViewAndEdit.Controls.Add(lblPictures);

            pos = lblPictures.Bottom + 10;

            if (pss.PSSPictureList.Count > 0)
            {
                foreach (Picture picture in pss.PSSPictureList)
                {
                    PictureBox tempPictureBox = new PictureBox();

                    tempPictureBox.BorderStyle = BorderStyle.FixedSingle;
                    tempPictureBox.ImageLocation = $@"C:\PollutionSourceSites\{CurrentSubsectorName}\Pictures\{pss.SiteNumberText}_{picture.PictureTVItemID}{picture.Extension}";
                    tempPictureBox.Location = new Point(10, pos);
                    tempPictureBox.Size = new Size(600, 500);
                    tempPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    tempPictureBox.TabIndex = 0;
                    tempPictureBox.TabStop = false;

                    PanelViewAndEdit.Controls.Add(tempPictureBox);

                    pos = tempPictureBox.Bottom + 10;

                    if (IsEditing)
                    {
                        Label lblFileName = new Label();
                        lblFileName.AutoSize = true;
                        lblFileName.Location = new Point(20, pos);
                        lblFileName.Tag = $"{pss.SiteNumberText}_{picture.PictureTVItemID}{picture.Extension}";
                        lblFileName.Font = new Font(new FontFamily(lblFileName.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                        lblFileName.Text = $@"Server File Name: ";

                        PanelViewAndEdit.Controls.Add(lblFileName);

                        TextBox textFileName = new TextBox();
                        textFileName.Location = new Point(lblFileName.Right + 5, pos);
                        textFileName.Font = new Font(new FontFamily(lblFileName.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        textFileName.Width = 300;
                        textFileName.Text = picture.FileName;

                        PanelViewAndEdit.Controls.Add(textFileName);

                        pos = textFileName.Bottom + 10;

                        Label lblDescription = new Label();
                        lblDescription.AutoSize = true;
                        lblDescription.Location = new Point(20, pos);
                        lblDescription.Tag = $"{pss.SiteNumberText}_{picture.PictureTVItemID}{picture.Extension}";
                        lblDescription.Font = new Font(new FontFamily(lblDescription.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                        lblDescription.Text = $@"Description: ";

                        PanelViewAndEdit.Controls.Add(lblDescription);

                        TextBox textDescription = new TextBox();
                        textDescription.Location = new Point(lblDescription.Right + 5, pos);
                        textDescription.Font = new Font(new FontFamily(lblDescription.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        textDescription.Width = 300;
                        textDescription.Height = 100;
                        textDescription.Multiline = true;
                        textDescription.Text = picture.Description;

                        PanelViewAndEdit.Controls.Add(textDescription);

                        pos = textDescription.Bottom + 10;

                        Button butSavePictureFileName = new Button();
                        butSavePictureFileName.Location = new Point(120, pos);
                        butSavePictureFileName.Text = "Save File Name";
                        butSavePictureFileName.Tag = $"{picture.PictureTVItemID}";
                        butSavePictureFileName.Click += butSavePictureFileName_Click;

                        PanelViewAndEdit.Controls.Add(butSavePictureFileName);

                        Button butRemovePicture = new Button();
                        butRemovePicture.Location = new Point(butSavePictureFileName.Right + 20, pos);
                        butRemovePicture.Text = "Remove";
                        butRemovePicture.Tag = $"{picture.PictureTVItemID}";
                        butRemovePicture.Click += butRemovePicture_Click;

                        PanelViewAndEdit.Controls.Add(butRemovePicture);


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
                    lblEmpty.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                    lblEmpty.Font = new Font(new FontFamily(lblEmpty.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    lblEmpty.Text = $"No Picture";

                    PanelViewAndEdit.Controls.Add(lblEmpty);
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

                PanelViewAndEdit.Controls.Add(butAddPicture);
            }
        }
    }
}
