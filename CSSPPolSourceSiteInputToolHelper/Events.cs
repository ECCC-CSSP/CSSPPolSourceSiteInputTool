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
        protected virtual void EmitHelpFileName(RTBFileNameEventArgs e)
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
                DrawPanelContactsAndInfrastructures();
                //RedrawSinglePanelInfrastructure();
            }
        }
        private void butChangeToIsActive_Click(object sender, EventArgs e)
        {
            if (IsPolSourceSite)
            {
                CurrentPSS.IsActiveNew = true;
                SavePolSourceSiteInfo();
                RedrawSinglePanelPSS();
                ReDrawPolSourceSite();
            }
            else
            {
                if (ContactTVItemID > 0)
                {
                    CurrentContact.IsActiveNew = true;
                }
                else
                {
                    CurrentInfrastructure.IsActiveNew = true;
                }
                SaveInfrastructureInfo();
                DrawPanelContactsAndInfrastructures();
                ReDrawContactAndInfrastructure();
            }
        }
        private void butChangeToIsNotActive_Click(object sender, EventArgs e)
        {
            if (IsPolSourceSite)
            {
                CurrentPSS.IsActiveNew = false;
                SavePolSourceSiteInfo();
                RedrawSinglePanelPSS();
                ReDrawPolSourceSite();
            }
            else
            {
                if (ContactTVItemID > 0)
                {
                    CurrentContact.IsActiveNew = false;
                }
                else
                {
                    CurrentInfrastructure.IsActiveNew = false;
                }
                SaveInfrastructureInfo();
                DrawPanelContactsAndInfrastructures();
                ReDrawContactAndInfrastructure();
            }
        }
        private void butDeleteContact_Click(object sender, EventArgs e)
        {
            int? ContactTVItemIDToDelete = CurrentContact.ContactTVItemID;

            if (ContactTVItemIDToDelete >= 20000000)
            {
                municipalityDoc.Municipality.ContactList.Remove(CurrentContact);
            }

            ContactTVItemID = 0;
            CurrentContact = null;

            SaveMunicipalityTextFile();

            IsDirty = false;
            PanelShowInputOptions.BackColor = BackColorDefault;
            PanelSubsectorOrMunicipality.Enabled = true;

            PanelViewAndEdit.Controls.Clear();
            DrawPanelContactsAndInfrastructures();
        }

        private void butDeleteEmail_Click(object sender, EventArgs e)
        {
            string Tag = ((Button)sender).Tag.ToString();

            int EmailTVItemIDToDelete = int.Parse(Tag);

            if (EmailTVItemIDToDelete >= 40000000)
            {
                Email email = CurrentContact.EmailList.Where(c => c.EmailTVItemID == EmailTVItemIDToDelete).FirstOrDefault();
                if (email != null)
                {
                    CurrentContact.EmailList.Remove(email);
                }


                IsDirty = true;
                PanelShowInputOptions.BackColor = BackColorEditing;

                ReDrawContactAndInfrastructure();
            }
            else
            {
                MessageBox.Show("Could not find the email to delete", "Error while trying to delete email", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void butDeleteTel_Click(object sender, EventArgs e)
        {
            string Tag = ((Button)sender).Tag.ToString();

            int TelTVItemIDToDelete = int.Parse(Tag);

            if (TelTVItemIDToDelete >= 30000000)
            {
                Telephone tel = CurrentContact.TelephoneList.Where(c => c.TelTVItemID == TelTVItemIDToDelete).FirstOrDefault();
                if (tel != null)
                {
                    CurrentContact.TelephoneList.Remove(tel);
                }

                IsDirty = true;
                PanelShowInputOptions.BackColor = BackColorEditing;

                ReDrawContactAndInfrastructure();
            }
            else
            {
                MessageBox.Show("Could not find the telephone to delete", "Error while trying to delete telephone", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void butChangeToIsPointSource_Click(object sender, EventArgs e)
        {
            CurrentPSS.IsPointSourceNew = true;
            SavePolSourceSiteInfo();
            RedrawSinglePanelPSS();
            ReDrawPolSourceSite();
        }
        private void butChangeToIsNonPointSource_Click(object sender, EventArgs e)
        {
            CurrentPSS.IsPointSourceNew = false;
            SavePolSourceSiteInfo();
            RedrawSinglePanelPSS();
            ReDrawPolSourceSite();
        }
        private void dateTimePickerObsDate_ValueChanged(object sender, EventArgs e)
        {
            DateTime currentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DateTimePicker dateTimePicker = (DateTimePicker)sender;
            if (dateTimePicker != null)
            {
                DateTime selectedDateTime = dateTimePicker.Value;

                if (selectedDateTime > currentDate)
                {
                    MessageBox.Show("Can't accept an observation later than today " + currentDate.ToString("yyyy MMMM dd"), "Error Observation Date", MessageBoxButtons.OK);
                    dateTimePicker.Value = currentDate;
                    return;
                }
            }
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;
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
                        if (muniIDNumber.Municipality == "None")
                        {
                            comboBoxMuni.Items.Add(muniIDNumber.Municipality);
                        }
                    }
                    foreach (MunicipalityIDNumber muniIDNumber in MunicipalityIDNumberList.OrderBy(c => c.Municipality))
                    {
                        if (muniIDNumber.Municipality != "None")
                        {
                            comboBoxMuni.Items.Add(muniIDNumber.Municipality);
                        }
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
        private void lblStreetTypeText_Click(object sender, EventArgs e)
        {
            List<StreetTypeIDNumber> StreetTypeIDNumberList = new List<StreetTypeIDNumber>();

            foreach (Control control in PanelStreetType.Controls)
            {
                if (control.Name == "comboBoxStreetType")
                {
                    ComboBox comboBoxStType = ((ComboBox)control);

                    StreetTypeIDNumberList = GetStreetTypeAndIDNumber();

                    comboBoxStType.Items.Clear();
                    foreach (StreetTypeIDNumber stTypeIDNumber in StreetTypeIDNumberList.OrderBy(c => c.StreetType))
                    {
                        if (stTypeIDNumber.StreetType == "None")
                        {
                            comboBoxStType.Items.Add(stTypeIDNumber.StreetType);

                        }
                    }
                    foreach (StreetTypeIDNumber stTypeIDNumber in StreetTypeIDNumberList.OrderBy(c => c.StreetType))
                    {
                        if (stTypeIDNumber.StreetType != "None")
                        {
                            comboBoxStType.Items.Add(stTypeIDNumber.StreetType);
                        }
                    }

                    if (comboBoxStType.Items.Count > 0)
                    {
                        comboBoxStType.SelectedIndex = 0;
                    }
                }
            }
            PanelStreetType.BringToFront();
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
                foreach (MunicipalityIDNumber muniIDNumber in MunicipalityIDNumberList.Skip(1))
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
                InfrastructureSaveAllToCSSPWebTools();
            }
        }
        private void butRemovePicture_Click(object sender, EventArgs e)
        {
            int PictureTVItemID = int.Parse(((Button)sender).Tag.ToString());
            RemovePicture(PictureTVItemID);
        }
        private void butSuggestFileName_Click(object sender, EventArgs e)
        {
            int PictureTVItemID = int.Parse(((Button)sender).Tag.ToString());
            SuggestFileName(PictureTVItemID);
        }
        private void butUnRemovePicture_Click(object sender, EventArgs e)
        {
            int PictureTVItemID = int.Parse(((Button)sender).Tag.ToString());
            UnRemovePicture(PictureTVItemID);
        }
        private void butCancel_Click(object sender, EventArgs e)
        {
            IsDirty = false;
            PanelShowInputOptions.BackColor = BackColorDefault;
            PanelSubsectorOrMunicipality.Enabled = true;
        }
        private void butSaves_Click(object sender, EventArgs e)
        {
            int AutoScrollPos = PanelViewAndEdit.VerticalScroll.Value;

            if (IsPolSourceSite)
            {
                SavePolSourceSiteInfo();
                RedrawSinglePanelPSS();
                ReDrawPolSourceSite();
            }
            else
            {
                SaveInfrastructureInfo();
                RedrawContactAndInfrastructureList();
                ReDrawContactAndInfrastructure();
            }

            PanelViewAndEdit.VerticalScroll.Value = AutoScrollPos;

            bool DuplicateNameExist = false;
            if (ContactTVItemID > 0)
            {
                string FirstName = CurrentContact.FirstNameNew != null ? CurrentContact.FirstNameNew : CurrentContact.FirstName;
                string Initial = CurrentContact.InitialNew != null ? CurrentContact.InitialNew : CurrentContact.Initial;
                string LastName = CurrentContact.LastNameNew != null ? CurrentContact.LastNameNew : CurrentContact.LastName;

                foreach (Contact contact in municipalityDoc.Municipality.ContactList)
                {
                    if (contact.ContactTVItemID != CurrentContact.ContactTVItemID)
                    {
                        string FirstName2 = contact.FirstNameNew != null ? contact.FirstNameNew : contact.FirstName;
                        string Initial2 = contact.InitialNew != null ? contact.InitialNew : contact.Initial;
                        string LastName2 = contact.LastNameNew != null ? contact.LastNameNew : contact.LastName;

                        if (FirstName == FirstName2 && Initial == Initial2 && LastName == LastName2)
                        {
                            DuplicateNameExist = true;
                            break;
                        }
                    }
                }
            }

            if (DuplicateNameExist)
            {
                IsDirty = true;
                PanelShowInputOptions.BackColor = BackColorEditing;
                MessageBox.Show("At least 2 contacts have the same First Name, Initial and Last Name\r\nPlease change one of them", "Error: Duplicate Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                IsDirty = false;
                PanelShowInputOptions.BackColor = BackColorDefault;
            }
            PanelSubsectorOrMunicipality.Enabled = true;
        }
        private void butCancelPictureInfo_Click(object sender, EventArgs e)
        {
            IsDirty = false;
            PanelShowInputOptions.BackColor = BackColorDefault;
            PanelSubsectorOrMunicipality.Enabled = true;
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
                DrawPanelContactsAndInfrastructures();
                //RedrawSinglePanelInfrastructure();
                ReDrawContactAndInfrastructure();
            }

            IsDirty = false;
            PanelShowInputOptions.BackColor = BackColorDefault;
            PanelSubsectorOrMunicipality.Enabled = true;
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
            SavePolSourceSiteInfo();
            RedrawSinglePanelPSS();
            ReDrawPolSourceSite();
        }
        private void butIssueMoveRight_Click(object sender, EventArgs e)
        {
            IssueID = int.Parse(((Button)sender).Tag.ToString());
            IssueMoveRight(IssueID);
            SavePolSourceSiteInfo();
            RedrawSinglePanelPSS();
            ReDrawPolSourceSite();
        }
        private void butIssueUnDelete_Click(object sender, EventArgs e)
        {
            IssueID = int.Parse(((Button)sender).Tag.ToString());
            UnDeleteIssue(IssueID);
            SaveIssue(IssueID);
        }
        private void butIssueCancel_Click(object sender, EventArgs e)
        {
            IsDirty = false;
            PanelShowInputOptions.BackColor = BackColorDefault;
            PanelSubsectorOrMunicipality.Enabled = true;
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

            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
        }
        private void checkBoxFromWater_CheckedChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;
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
        private void butEmailAdd_Click(object sender, EventArgs e)
        {
            EmailAdd();
            ReDrawContactAndInfrastructure();
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
        }
        private void butTelAdd_Click(object sender, EventArgs e)
        {
            TelAdd();
            ReDrawContactAndInfrastructure();
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
        }
        private void SaveAndRedraw(object sender, EventArgs e)
        {
            if (!IsReading)
            {
                SaveInfrastructureInfo();
                DrawPanelContactsAndInfrastructures();
                ReDrawContactAndInfrastructure();
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
                DrawPanelContactsAndInfrastructures();
                ReDrawContactAndInfrastructure();
            }
        }
        private void ShowHelpDocument(object sender, EventArgs e)
        {
            string FileName = ((Label)sender).Tag.ToString();
            EmitHelpFileName(new RTBFileNameEventArgs(FileName));
        }
        private void ShowPolSourceSite_Click(object sender, EventArgs e)
        {
            if (IsDirty)
            {
                MessageBox.Show("Please save or cancel before changing page.", "Some changes have not been saved yet", MessageBoxButtons.OK);
                return;
            }

            PolSourceSiteTVItemID = int.Parse(((Control)sender).Tag.ToString());
            IssueID = 0;
            CurrentPSS = subsectorDoc.Subsector.PSSList.Where(c => c.PSSTVItemID == PolSourceSiteTVItemID).FirstOrDefault();
            foreach (Control control in PanelShowInputOptions.Controls)
            {
                if (control.Name == "radioButtonDetails")
                {
                    if (((RadioButton)control).Checked)
                    {
                        OnDetailPage = true;
                        OnIssuePage = false;
                        OnMapPage = false;
                        OnPicturePage = false;
                    }
                }

                if (control.Name == "radioButtonIssues")
                {
                    if (((RadioButton)control).Checked)
                    {
                        OnDetailPage = false;
                        OnIssuePage = true;
                        OnMapPage = false;
                        OnPicturePage = false;
                    }
                }

                if (control.Name == "radioButtonShowMap")
                {
                    if (((RadioButton)control).Checked)
                    {
                        OnDetailPage = false;
                        OnIssuePage = false;
                        OnMapPage = true;
                        OnPicturePage = false;
                    }
                }

                if (control.Name == "radioButtonPictures")
                {
                    if (((RadioButton)control).Checked)
                    {
                        OnDetailPage = false;
                        OnIssuePage = false;
                        OnMapPage = false;
                        OnPicturePage = true;
                    }
                }
            }
            ReDrawPolSourceSite();
        }

        private void ShowContact_Click(object sender, EventArgs e)
        {
            if (IsDirty)
            {
                MessageBox.Show("Please save or cancel before changing page.", "Some changes have not been saved yet", MessageBoxButtons.OK);
                return;
            }

            InfrastructureTVItemID = 0;
            ContactTVItemID = int.Parse(((Control)sender).Tag.ToString());
            CurrentContact = municipalityDoc.Municipality.ContactList.Where(c => c.ContactTVItemID == ContactTVItemID).FirstOrDefault();
            foreach (Control control in PanelShowInputOptions.Controls)
            {
                if (control.Name == "radioButtonDetails")
                {
                    if (((RadioButton)control).Checked)
                    {
                        OnDetailPage = true;
                        OnIssuePage = false;
                        OnMapPage = false;
                        OnPicturePage = false;
                    }
                }

                if (control.Name == "radioButtonIssues")
                {
                    if (((RadioButton)control).Checked)
                    {
                        OnDetailPage = false;
                        OnIssuePage = true;
                        OnMapPage = false;
                        OnPicturePage = false;
                    }
                }

                if (control.Name == "radioButtonShowMap")
                {
                    if (((RadioButton)control).Checked)
                    {
                        OnDetailPage = false;
                        OnIssuePage = false;
                        OnMapPage = true;
                        OnPicturePage = false;
                    }
                }

                if (control.Name == "radioButtonPictures")
                {
                    if (((RadioButton)control).Checked)
                    {
                        OnDetailPage = false;
                        OnIssuePage = false;
                        OnMapPage = false;
                        OnPicturePage = true;
                    }
                }
            }
            ReDrawContactAndInfrastructure();
        }
        private void ShowInfrastructure_Click(object sender, EventArgs e)
        {
            if (IsDirty)
            {
                MessageBox.Show("Please save or cancel before changing page.", "Some changes have not been saved yet", MessageBoxButtons.OK);
                return;
            }

            ContactTVItemID = 0;
            InfrastructureTVItemID = int.Parse(((Control)sender).Tag.ToString());
            CurrentInfrastructure = municipalityDoc.Municipality.InfrastructureList.Where(c => c.InfrastructureTVItemID == InfrastructureTVItemID).FirstOrDefault();
            foreach (Control control in PanelShowInputOptions.Controls)
            {
                if (control.Name == "radioButtonDetails")
                {
                    if (((RadioButton)control).Checked)
                    {
                        OnDetailPage = true;
                        OnIssuePage = false;
                        OnMapPage = false;
                        OnPicturePage = false;
                    }
                }

                if (control.Name == "radioButtonIssues")
                {
                    if (((RadioButton)control).Checked)
                    {
                        OnDetailPage = false;
                        OnIssuePage = true;
                        OnMapPage = false;
                        OnPicturePage = false;
                    }
                }

                if (control.Name == "radioButtonShowMap")
                {
                    if (((RadioButton)control).Checked)
                    {
                        OnDetailPage = false;
                        OnIssuePage = false;
                        OnMapPage = true;
                        OnPicturePage = false; 
                    }
                }

                if (control.Name == "radioButtonPictures")
                {
                    if (((RadioButton)control).Checked)
                    {
                        OnDetailPage = false;
                        OnIssuePage = false;
                        OnMapPage = false;
                        OnPicturePage = true;
                    }
                }
            }
            ReDrawContactAndInfrastructure();
        }

        private void textBoxDescription_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;
        }
        private void textBoxFileName_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;
        }

        // DesignFlow_Change
        private void textBoxDesignFlow_m3_day_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;

            TextBox textBox = (TextBox)sender;
            if (textBox.Focused)
            {
                if (!string.IsNullOrWhiteSpace(textBox.Text))
                {
                    if (float.TryParse(textBox.Text, out float tempFloat))
                    {
                        ChangeTextValue("textBoxDesignFlow_CanGal_day", (tempFloat * 219.969248f).ToString("F0"));
                        ChangeTextValue("textBoxDesignFlow_USGal_day", (tempFloat * 264.172f).ToString("F0"));
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }
        private void textBoxDesignFlow_CanGal_day_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;

            TextBox textBox = (TextBox)sender;
            if (textBox.Focused)
            {
                if (!string.IsNullOrWhiteSpace(textBox.Text))
                {
                    if (float.TryParse(textBox.Text, out float tempFloat))
                    {
                        ChangeTextValue("textBoxDesignFlow_m3_day", (tempFloat / 219.969248f).ToString("F0"));
                        ChangeTextValue("textBoxDesignFlow_USGal_day", (tempFloat * (264.172f / 219.969248f)).ToString("F0"));
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }
        private void textBoxDesignFlow_USGal_day_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;

            TextBox textBox = (TextBox)sender;
            if (textBox.Focused)
            {
                if (!string.IsNullOrWhiteSpace(textBox.Text))
                {
                    if (float.TryParse(textBox.Text, out float tempFloat))
                    {
                        ChangeTextValue("textBoxDesignFlow_m3_day", (tempFloat / 264.172f).ToString("F0"));
                        ChangeTextValue("textBoxDesignFlow_CanGal_day", (tempFloat * (219.969248f / 264.172f)).ToString("F0"));
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        // AverageFlow_Change
        private void textBoxAverageFlow_m3_day_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;

            TextBox textBox = (TextBox)sender;
            if (textBox.Focused)
            {
                if (!string.IsNullOrWhiteSpace(textBox.Text))
                {
                    if (float.TryParse(textBox.Text, out float tempFloat))
                    {
                        ChangeTextValue("textBoxAverageFlow_CanGal_day", (tempFloat * 219.969248f).ToString("F0"));
                        ChangeTextValue("textBoxAverageFlow_USGal_day", (tempFloat * 264.172f).ToString("F0"));
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }
        private void textBoxAverageFlow_CanGal_day_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;

            TextBox textBox = (TextBox)sender;
            if (textBox.Focused)
            {
                if (!string.IsNullOrWhiteSpace(textBox.Text))
                {
                    if (float.TryParse(textBox.Text, out float tempFloat))
                    {
                        ChangeTextValue("textBoxAverageFlow_m3_day", (tempFloat / 219.969248f).ToString("F0"));
                        ChangeTextValue("textBoxAverageFlow_USGal_day", (tempFloat * (264.172f / 219.969248f)).ToString("F0"));
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }
        private void textBoxAverageFlow_USGal_day_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;

            TextBox textBox = (TextBox)sender;
            if (textBox.Focused)
            {
                if (!string.IsNullOrWhiteSpace(textBox.Text))
                {
                    if (float.TryParse(textBox.Text, out float tempFloat))
                    {
                        ChangeTextValue("textBoxAverageFlow_m3_day", (tempFloat / 264.172f).ToString("F0"));
                        ChangeTextValue("textBoxAverageFlow_CanGal_day", (tempFloat * (219.969248f / 264.172f)).ToString("F0"));
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        // PeakFlow_Change
        private void textBoxPeakFlow_m3_day_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;

            TextBox textBox = (TextBox)sender;
            if (textBox.Focused)
            {
                if (!string.IsNullOrWhiteSpace(textBox.Text))
                {
                    if (float.TryParse(textBox.Text, out float tempFloat))
                    {
                        ChangeTextValue("textBoxPeakFlow_CanGal_day", (tempFloat * 219.969248f).ToString("F0"));
                        ChangeTextValue("textBoxPeakFlow_USGal_day", (tempFloat * 264.172f).ToString("F0"));
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }
        private void textBoxPeakFlow_CanGal_day_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;

            TextBox textBox = (TextBox)sender;
            if (textBox.Focused)
            {
                if (!string.IsNullOrWhiteSpace(textBox.Text))
                {
                    if (float.TryParse(textBox.Text, out float tempFloat))
                    {
                        ChangeTextValue("textBoxPeakFlow_m3_day", (tempFloat / 219.969248f).ToString("F0"));
                        ChangeTextValue("textBoxPeakFlow_USGal_day", (tempFloat * (264.172f / 219.969248f)).ToString("F0"));
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }
        private void textBoxPeakFlow_USGal_day_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;

            TextBox textBox = (TextBox)sender;
            if (textBox.Focused)
            {
                if (!string.IsNullOrWhiteSpace(textBox.Text))
                {
                    if (float.TryParse(textBox.Text, out float tempFloat))
                    {
                        ChangeTextValue("textBoxPeakFlow_m3_day", (tempFloat / 264.172f).ToString("F0"));
                        ChangeTextValue("textBoxPeakFlow_CanGal_day", (tempFloat * (219.969248f / 264.172f)).ToString("F0"));
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        // Lat_Change
        private void textBoxLat_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;
        }

        // Lng_Change
        private void textBoxLng_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;
        }

        // LatOutfall_Change
        private void textBoxLatOutfall_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;
        }

        // LngOutfall_Change
        private void textBoxLngOutfall_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;
        }

        // SeeOtherMunicipalityTVItemID_Changed
        private void textBoxSeeOtherMunicipalityTVItemID_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;
        }

        // ReceivingWater_MPN_per_100ml_Changed
        private void textBoxReceivingWater_MPN_per_100ml_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;
        }

        // NumberOfPorts_Changed
        private void textBoxNumberOfPorts_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;
        }

        // PumpsToTVItemID_Changed
        private void textBoxPumpsToTVItemID_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;
        }

        // PopServed_Changed
        private void textBoxPopServed_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;
        }

        // NumberOfAeratedCells_Changed
        private void textBoxNumberOfAeratedCells_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;
        }

        // NumberOfCells_Changed
        private void textBoxNumberOfCells_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;
        }

        // StreetNumber_TextChanged
        private void textBoxStreetNumber_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;
        }

        // StreetNumber_TextChanged
        private void textBoxStreetName_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;
        }

        // StreetType_TextChanged
        private void textBoxStreetType_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;
        }

        // Municipality_TextChanged
        private void textBoxMunicipality_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;
        }

        // PostalCode_TextChanged
        private void textBoxPostalCode_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;
        }

        // TVText_TextChanged
        private void textBoxTVText_TextChanged(object sender, EventArgs e)
        {
            TextBox tbTemp = (TextBox)sender;
            if (tbTemp.Text.Contains("#"))
            {
                MessageBox.Show("Illegal character '#'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;
        }

        // FirstName_TextChanged
        private void textBoxFirstName_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;
        }

        // Initial_TextChanged
        private void textBoxInitial_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;
        }

        // LastName_TextChanged
        private void textBoxLastName_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;
        }

        // Email_TextChanged
        private void textBoxEmail_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;
        }

        // TelNumber_TextChanged
        private void textBoxTelNumber_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;
        }

        // EmailAddress_TextChanged
        private void textBoxEmailAddress_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;
        }

        // PercFlowOfTotal_TextChanged
        private void textBoxPercFlowOfTotal_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;
        }

        // PortDiameter_m_TextChanged
        private void textBoxPortDiameter_m_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;
        }

        // PortSpacing_m_TextChanged
        private void textBoxPortSpacing_m_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;
        }

        // PortElevation_m_TextChanged
        private void textBoxPortElevation_m_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;
        }

        // VerticalAngle_deg_TextChanged
        private void textBoxVerticalAngle_deg_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;
        }

        // HorizontalAngle_deg_TextChanged
        private void textBoxHorizontalAngle_deg_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;
        }

        // DistanceFromShore_m_TextChanged
        private void textBoxDistanceFromShore_m_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;
        }

        // AverageDepth_m_TextChanged
        private void textBoxAverageDepth_m_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;
        }

        // DecayRate_per_day_TextChanged
        private void textBoxDecayRate_per_day_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;
        }

        // NearFieldVelocity_m_s_TextChanged
        private void textBoxNearFieldVelocity_m_s_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;
        }

        // FarFieldVelocity_m_s_TextChanged
        private void textBoxFarFieldVelocity_m_s_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;
        }

        // ReceivingWaterSalinity_PSU_TextChanged
        private void textBoxReceivingWaterSalinity_PSU_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;
        }

        // ReceivingWaterTemperature_C_TextChanged
        private void textBoxReceivingWaterTemperature_C_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;
        }

        // ExtraComment_TextChanged
        private void textItemExtraComment_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;
        }

        // CommentEN_TextChanged
        private void textBoxCommentEN_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;
        }

        // CommentFR_TextChanged
        private void textBoxCommentFR_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;
        }

        // CanOverFlow_CheckedChanged
        private void checkBoxCanOverFlow_CheckedChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;
        }

        // HasBackupPower_CheckedChanged
        private void checkBoxHasBackupPower_CheckedChanged(object sender, EventArgs e)
        {
            IsDirty = true;
            PanelShowInputOptions.BackColor = BackColorEditing;
            PanelSubsectorOrMunicipality.Enabled = false;
        }

    }
}
