using CSSPEnumsDLL.Enums;
using CSSPModelsDLL.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSSPPolSourceSiteInputToolHelper
{
    public partial class PolSourceSiteInputToolHelper
    {
        public class StatusEventArgs : EventArgs
        {
            public StatusEventArgs(string Status)
            {
                this.Status = Status;
            }
            public string Status { get; set; }
        }
        protected virtual void EmitStatus(StatusEventArgs e)
        {
            UpdateStatus?.Invoke(this, e);
        }

        public event EventHandler<StatusEventArgs> UpdateStatus;

        public class RTBClearEventArgs : EventArgs
        {
            public RTBClearEventArgs()
            {
            }
        }
        protected virtual void EmitRTBClear(RTBClearEventArgs e)
        {
            UpdateRTBClear?.Invoke(this, e);
        }

        public event EventHandler<RTBClearEventArgs> UpdateRTBClear;


        public class RTBMessageEventArgs : EventArgs
        {
            public RTBMessageEventArgs(string Message)
            {
                this.Message = Message;
            }
            public string Message { get; set; }
        }
        protected virtual void EmitRTBMessage(RTBMessageEventArgs e)
        {
            UpdateRTBMessage?.Invoke(this, e);
        }

        public event EventHandler<RTBMessageEventArgs> UpdateRTBMessage;

        public class RTBFileNameEventArgs : EventArgs
        {
            public RTBFileNameEventArgs(string FileName)
            {
                this.FileName = FileName;
            }
            public string FileName { get; set; }
        }
        protected virtual void EmitRTBFileName(RTBFileNameEventArgs e)
        {
            UpdateRTBFileName?.Invoke(this, e);
        }

        public event EventHandler<RTBFileNameEventArgs> UpdateRTBFileName;


        // -------------------------------------------------------------------------------------------------

        private void butAddPicture_Click(object sender, EventArgs e)
        {
            if (IsPolSourceSite)
            {
                AddPicture();
                RedrawSinglePanelPSS();
            }
            else
            {
                AddPicture();
                DrawPanelInfrastructures();
                //RedrawSinglePanelInfrastructure();
            }
        }
        private void butChangeToIsActive_Click(object sender, EventArgs e)
        {
            if (IsPolSourceSite)
            {
                CurrentPSS.IsActive = true;
                SavePolSourceSiteInfo();
                RedrawSinglePanelPSS();
                ReDrawPolSourceSite();
            }
            else
            {
                CurrentInfrastructure.IsActive = true;
                SaveInfrastructureInfo();
                DrawPanelInfrastructures();
                //RedrawSinglePanelInfrastructure();
                ReDrawInfrastructure();
            }
        }
        private void butChangeToIsNotActive_Click(object sender, EventArgs e)
        {
            if (IsPolSourceSite)
            {
                CurrentPSS.IsActive = false;
                SavePolSourceSiteInfo();
                RedrawSinglePanelPSS();
                ReDrawPolSourceSite();
            }
            else
            {
                CurrentInfrastructure.IsActive = false;

                SaveInfrastructureInfo();
                DrawPanelInfrastructures();
                //RedrawSinglePanelInfrastructure();
                ReDrawInfrastructure();
            }
        }
        private void butChangeToIsPointSource_Click(object sender, EventArgs e)
        {
            CurrentPSS.IsPointSource = true;
            SavePolSourceSiteInfo();
            RedrawSinglePanelPSS();
            ReDrawPolSourceSite();
        }
        private void butChangeToIsNonPointSource_Click(object sender, EventArgs e)
        {
            CurrentPSS.IsPointSource = false;
            SavePolSourceSiteInfo();
            RedrawSinglePanelPSS();
            ReDrawPolSourceSite();
        }
        private void lblMunicipalityText_Click(object sender, EventArgs e)
        {
            List<MunicipalityIDNumber> MunicipalityIDNumberList = new List<MunicipalityIDNumber>();

            foreach (Control control in PanelMunicipalities.Controls)
            {
                if (control.Name == "comboBoxUsedMunicipalities")
                {
                    ComboBox comboBoxMuni = ((ComboBox)control);
                    MunicipalityIDNumberList = GetMunicipalitiesAndIDNumber();

                    comboBoxMuni.Items.Clear();
                    foreach (MunicipalityIDNumber muniIDNumber in MunicipalityIDNumberList.OrderBy(c => c.Municipality))
                    {
                        comboBoxMuni.Items.Add(muniIDNumber.Municipality);
                    }

                    if (comboBoxMuni.Items.Count > 0)
                    {
                        comboBoxMuni.SelectedIndex = 0;
                    }
                }

                if (control.Name == "comboBoxProvinceMunicipalities")
                {
                    ComboBox comboBoxMuni = ((ComboBox)control);

                    comboBoxMuni.Items.Clear();
                    foreach (MunicipalityIDNumber muniIDNumber in subsectorDoc.MunicipalityIDNumberList.OrderBy(c => c.Municipality))
                    {
                        comboBoxMuni.Items.Add(muniIDNumber.Municipality);
                    }

                    if (comboBoxMuni.Items.Count > 0)
                    {
                        comboBoxMuni.SelectedIndex = 0;
                    }
                }
            }
            PanelMunicipalities.BringToFront();
        }
        private void butSaveToCSSPWebTools_Click(object sender, EventArgs e)
        {
            EmitRTBClear(new RTBClearEventArgs());

            if (IsPolSourceSite)
            {

                string MunicipalitiesText = "";
                if (CurrentPSS.PSSAddressNew != null)
                {
                    if (!string.IsNullOrWhiteSpace(CurrentPSS.PSSAddressNew.Municipality))
                    {
                        MunicipalitiesText = CurrentPSS.PSSAddressNew.Municipality;
                    }
                }
                if (CurrentPSS.PSSAddress != null)
                {
                    if (!string.IsNullOrWhiteSpace(CurrentPSS.PSSAddress.Municipality))
                    {
                        MunicipalitiesText = CurrentPSS.PSSAddress.Municipality;
                    }
                }

                if (DialogResult.OK == MessageBox.Show($"The municipality\r\n\r\n {MunicipalitiesText}\r\n\r\n will be created in CSSPWebTools", "Warning: Will Create municipality", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation))
                {
                    if (CurrentPSS.PSSObs.ObsDateNew == null)
                    {
                        if (DialogResult.OK == MessageBox.Show($"Observation date was not changed.\r\n\r\n { CurrentPSS.PSSObs.ObsDate }\r\n\r\nThis will replace the current observation of [{ CurrentPSS.PSSObs.ObsDate }] with the changes you made.", "Warning: Will Replace Existing Observation", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation))
                        {
                            PSSSaveToCSSPWebTools();
                        }
                    }
                    else
                    {
                        PSSSaveToCSSPWebTools();
                    }
                }
            }
            else
            {
                InfrastructureSaveToCSSPWebTools();
            }
        }
        private void butSaveAllToCSSPWebTools_Click(object sender, EventArgs e)
        {
            EmitRTBClear(new RTBClearEventArgs());

            if (IsPolSourceSite)
            {
                List<MunicipalityIDNumber> MunicipalityIDNumberList = GetMunicipalitiesAndIDNumber();

                string MunicipalitiesText = "";
                foreach (MunicipalityIDNumber muniIDNumber in MunicipalityIDNumberList)
                {
                    EmitStatus(new StatusEventArgs($"Checking if { muniIDNumber.Municipality } already exist in CSSPWebTools"));

                    string ret = MunicipalityExistUnderProvinceInCSSPWebTools((int)subsectorDoc.ProvinceTVItemID, muniIDNumber.Municipality, AdminEmail);
                    ret = ret.Replace("\"", "");
                    if (ret.StartsWith("ERROR"))
                    {
                        MunicipalitiesText = $"{ MunicipalitiesText }{ muniIDNumber.Municipality } -- (P{ muniIDNumber.IDNumber }) \r\n";
                    }
                }

                List<ObsDateIDNumber> ObsDateIDNumberList = GetObsDateAndIDNumber();

                string ObsDateText = "";
                foreach (ObsDateIDNumber obsDateIDNumber in ObsDateIDNumberList)
                {
                    ObsDateText = $"{ ObsDateText }{ obsDateIDNumber.ObsDate.ToString("yyyy MMMM dd") } -- (P{ obsDateIDNumber.IDNumber }) \r\n";
                }

                EmitStatus(new StatusEventArgs($"Please make sure that all municipalities do not have typos"));

                if (DialogResult.OK == MessageBox.Show($"The municipalities\r\n\r\n {MunicipalitiesText}\r\n\r\n will be created in CSSPWebTools", "Warning: Will Create municipalities", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation))
                {
                    if (ObsDateIDNumberList.Count > 0)
                    {
                        EmitStatus(new StatusEventArgs($"Please make sure that all observation date has been changed if you want to create new observations."));

                        if (DialogResult.OK == MessageBox.Show($"Observation date was not changed.\r\n\r\n { CurrentPSS.PSSObs.ObsDate }\r\n\r\nThis will replace the current observation of [{ CurrentPSS.PSSObs.ObsDate }] with the changes you made.", "Warning: Will Replace Existing Observation", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation))
                        {
                            EmitStatus(new StatusEventArgs("Saving all Pollution Source Sites to CSSPWebTools"));

                            PSSSaveAllToCSSPWebTools();
                        }
                    }
                    else
                    {
                        EmitStatus(new StatusEventArgs("Saving all Pollution Source Sites to CSSPWebTools"));

                        PSSSaveAllToCSSPWebTools();
                    }
                }
            }
            else
            {
                //List<MunicipalityIDNumber> MunicipalityIDNumberList = GetMunicipalitiesAndIDNumber();

                //string MunicipalitiesText = "";
                //foreach (MunicipalityIDNumber muniIDNumber in MunicipalityIDNumberList)
                //{
                //    EmitStatus(new StatusEventArgs($"Checking if { muniIDNumber.Municipality } already exist in CSSPWebTools"));

                //    string ret = MunicipalityExistUnderProvinceInCSSPWebTools((int)subsectorDoc.ProvinceTVItemID, muniIDNumber.Municipality, AdminEmail);
                //    ret = ret.Replace("\"", "");
                //    if (ret.StartsWith("ERROR"))
                //    {
                //        MunicipalitiesText = $"{ MunicipalitiesText }{ muniIDNumber.Municipality } -- (P{ muniIDNumber.IDNumber }) \r\n";
                //    }
                //}

                //EmitStatus(new StatusEventArgs($"Please make sure that all municipalities do not have typos"));

                //if (DialogResult.OK == MessageBox.Show($"The municipalities\r\n\r\n {MunicipalitiesText}\r\n\r\n will be created in CSSPWebTools", "Warning: Will Create municipalities", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation))
                //{
                //    EmitStatus(new StatusEventArgs("Saving all Infrastructures to CSSPWebTools"));

                InfrastructureSaveAllToCSSPWebTools();
                //}
            }
        }
        private void butRemovePicture_Click(object sender, EventArgs e)
        {
            int PictureTVItemID = int.Parse(((Button)sender).Tag.ToString());
            RemovePicture(PictureTVItemID);
        }
        private void butUnRemovePicture_Click(object sender, EventArgs e)
        {
            int PictureTVItemID = int.Parse(((Button)sender).Tag.ToString());
            UnRemovePicture(PictureTVItemID);
        }
        private void butSaves_Click(object sender, EventArgs e)
        {
            if (IsPolSourceSite)
            {
                SavePolSourceSiteInfo();
                RedrawSinglePanelPSS();
                ReDrawPolSourceSite();
            }
            else
            {
                bool PumpsToChanged = false;
                foreach (Control control in PanelViewAndEdit.Controls)
                {
                    if (control.Name == "textBoxPumpsToTVItemID")
                    {
                        TextBox tb = (TextBox)control;
                        if (int.TryParse(tb.Text, out int TempInt))
                        {
                            if (CurrentInfrastructure.PumpsToTVItemIDNew != null)
                            {
                                if (TempInt != CurrentInfrastructure.PumpsToTVItemIDNew)
                                {
                                    PumpsToChanged = true;
                                }
                            }
                            else
                            {
                                if (TempInt != CurrentInfrastructure.PumpsToTVItemID)
                                {
                                    PumpsToChanged = true;
                                }
                            }
                            break;
                        }
                    }
                }

                SaveInfrastructureInfo();

                if (PumpsToChanged)
                {
                    CurrentInfrastructure = null;
                    InfrastructureTVItemID = 0;
                }
                RedrawInfrastructureList();
                //DrawPanelInfrastructures();
                ////RedrawSinglePanelInfrastructure();
                ReDrawInfrastructure();
            }
        }
        private void butSavePictureInfo_Click(object sender, EventArgs e)
        {
            if (IsPolSourceSite)
            {
                int PictureTVItemID = int.Parse(((Button)sender).Tag.ToString());
                SavePictureInfo(PictureTVItemID);
                RedrawSinglePanelPSS();
                ReDrawPolSourceSite();
            }
            else
            {
                int PictureTVItemID = int.Parse(((Button)sender).Tag.ToString());
                SavePictureInfo(PictureTVItemID);
                DrawPanelInfrastructures();
                //RedrawSinglePanelInfrastructure();
                ReDrawInfrastructure();
            }
        }
        private void butIssueAdd_Click(object sender, EventArgs e)
        {
            IssueID = int.Parse(((Button)sender).Tag.ToString());
            AddIssue(IssueID);
        }
        private void butIssueDelete_Click(object sender, EventArgs e)
        {
            IssueID = int.Parse(((Button)sender).Tag.ToString());
            DeleteIssue(IssueID);
        }
        private void butIssueMoveLeft_Click(object sender, EventArgs e)
        {
            IssueID = int.Parse(((Button)sender).Tag.ToString());
            IssueMoveLeft(IssueID);
            ReDrawPolSourceSite();
        }
        private void butIssueMoveRight_Click(object sender, EventArgs e)
        {
            IssueID = int.Parse(((Button)sender).Tag.ToString());
            IssueMoveRight(IssueID);
            ReDrawPolSourceSite();
        }
        private void butIssueUnDelete_Click(object sender, EventArgs e)
        {
            IssueID = int.Parse(((Button)sender).Tag.ToString());
            UnDeleteIssue(IssueID);
            SaveIssue(IssueID);
        }
        private void butIssueSave_Click(object sender, EventArgs e)
        {
            IssueID = int.Parse(((Button)sender).Tag.ToString());
            SaveIssue(IssueID);
        }
        private void butIssueSet_Click(object sender, EventArgs e)
        {
            IssueID = int.Parse(((Button)sender).Tag.ToString());
            ReDrawPolSourceSite();
        }
        //private void checkBoxCreateMunicipality_CheckedChanged(object sender, EventArgs e)
        //{
        //    CheckBox checkBoxCreateMun = (CheckBox)sender;
        //    if (checkBoxCreateMun.Checked)
        //    {
        //        CreateMunicipality = true;
        //    }
        //    else
        //    {
        //        CreateMunicipality = false;
        //    }
        //}
        private void SaveAndRedraw(object sender, EventArgs e)
        {
            if (!IsReading)
            {
                SaveInfrastructureInfo();
                DrawPanelInfrastructures();
                //RedrawSinglePanelInfrastructure();
                ReDrawInfrastructure();
            }
        }
        private void SeeOtherMunicipalityChanged(object sender, EventArgs e)
        {
            MunicipalityIDNumber selectedMunicipalityIDNumber = ((MunicipalityIDNumber)((ComboBox)sender).SelectedItem);
            foreach (MunicipalityIDNumber municipalityIDNumber in municipalityDoc.MunicipalityIDNumberList)
            {
                if (municipalityIDNumber.IDNumber == selectedMunicipalityIDNumber.IDNumber)
                {
                    if (int.TryParse(municipalityIDNumber.IDNumber, out int MunicipalityTVItemID))
                    {
                        CurrentInfrastructure.SeeOtherMunicipalityTVItemIDNew = MunicipalityTVItemID;
                        CurrentInfrastructure.SeeOtherMunicipalityTextNew = municipalityIDNumber.Municipality;
                    }

                }
            }
            if (!IsReading)
            {
                SaveInfrastructureInfo();
                DrawPanelInfrastructures();
                //RedrawSinglePanelInfrastructure();
                ReDrawInfrastructure();
            }
        }
        private void ShowRTFDocument(object sender, EventArgs e)
        {
            string FileName = ((Label)sender).Tag.ToString();
            EmitRTBFileName(new RTBFileNameEventArgs(FileName));
        }
        private void ShowPolSourceSite_Click(object sender, EventArgs e)
        {
            PolSourceSiteTVItemID = int.Parse(((Control)sender).Tag.ToString());
            IssueID = 0;
            CurrentPSS = subsectorDoc.Subsector.PSSList.Where(c => c.PSSTVItemID == PolSourceSiteTVItemID).FirstOrDefault();
            //MunicipalityExist = true;
            //CreateMunicipality = false;
            ReDrawPolSourceSite();
        }
        private void ShowMunicipality_Click(object sender, EventArgs e)
        {
            InfrastructureTVItemID = int.Parse(((Control)sender).Tag.ToString());
            CurrentInfrastructure = municipalityDoc.Municipality.InfrastructureList.Where(c => c.InfrastructureTVItemID == InfrastructureTVItemID).FirstOrDefault();
            ReDrawInfrastructure();
        }
        private void lblIssueText_Click(object sender, EventArgs e)
        {
            string DialogText = ((Label)sender).Tag.ToString();
            MessageBox.Show(DialogText, "Description", MessageBoxButtons.OK);
        }
        private void lblIssueText2_Click(object sender, EventArgs e)
        {
            Label labelSelected = ((Label)sender);
            DrawAfterLabelSelectd(labelSelected);
        }
    }
}
