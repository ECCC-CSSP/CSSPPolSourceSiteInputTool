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
                RedrawSinglePanelInfrastructure();
            }
        }
        private void butChangeToIsActive_Click(object sender, EventArgs e)
        {
            CurrentPSS.IsActive = true;
            SavePolSourceSiteInfo();
            RedrawSinglePanelPSS();
            ReDrawPolSourceSite();
        }
        private void butChangeToIsNotActive_Click(object sender, EventArgs e)
        {
            CurrentPSS.IsActive = false;
            SavePolSourceSiteInfo();
            RedrawSinglePanelPSS();
            ReDrawPolSourceSite();
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
        private void butPSSSaveToCSSPWebTools_Click(object sender, EventArgs e)
        {
            PSSSaveToCSSPWebTools();
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
                SaveInfrastructureInfo();
                RedrawSinglePanelInfrastructure();
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
                RedrawSinglePanelInfrastructure();
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
        private void ShowPolSourceSite_Click(object sender, EventArgs e)
        {
            PolSourceSiteTVItemID = int.Parse(((Control)sender).Tag.ToString());
            IssueID = 0;
            CurrentPSS = subsectorDoc.Subsector.PSSList.Where(c => c.PSSTVItemID == PolSourceSiteTVItemID).FirstOrDefault();
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
