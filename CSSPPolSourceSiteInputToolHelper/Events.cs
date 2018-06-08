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
        private void butSaveToCSSPWebTools_Click(object sender, EventArgs e)
        {
            EmitRTBClear(new RTBClearEventArgs());

            if (IsPolSourceSite)
            {
                PSSSaveToCSSPWebTools();
            }
            else
            {
                InfrastructureSaveToCSSPWebTools();
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
        private void butSaveLatLngAndObsAndAddress_Click(object sender, EventArgs e)
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
        private void checkBoxCreateMunicipality_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBoxCreateMun = (CheckBox)sender;
            if (checkBoxCreateMun.Checked)
            {
                CreateMunicipality = true;
            }
            else
            {
                CreateMunicipality = false;
            }
        }
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
            MunicipalityExist = true;
            CreateMunicipality = false;
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
