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
        protected virtual void OnStatus(StatusEventArgs e)
        {
            UpdateStatus?.Invoke(this, e);
        }

        public event EventHandler<StatusEventArgs> UpdateStatus;

        // -------------------------------------------------------------------------------------------------

        private void butAddPicture_Click(object sender, EventArgs e)
        {
            AddPicture();
            RedrawSinglePanelPSS();
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
            SavePolSourceSiteInfo();
            RedrawSinglePanelPSS();
            ReDraw();
        }
        private void butSavePictureInfo_Click(object sender, EventArgs e)
        {
            int PictureTVItemID = int.Parse(((Button)sender).Tag.ToString());
            SavePictureInfo(PictureTVItemID);
            RedrawSinglePanelPSS();
            ReDraw();
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
            ReDraw();
        }
        private void butIssueMoveRight_Click(object sender, EventArgs e)
        {
            IssueID = int.Parse(((Button)sender).Tag.ToString());
            IssueMoveRight(IssueID);
            ReDraw();
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
            ReDraw();
        }
        private void ShowPolSourceSiteViaLabel(object sender, EventArgs e)
        {
            PolSourceSiteTVItemID = int.Parse(((Label)sender).Tag.ToString());
            IssueID = 0;
            CurrentPSS = subsectorDoc.Subsector.PSSList.Where(c => c.PSSTVItemID == PolSourceSiteTVItemID).FirstOrDefault();
            ReDraw();
        }
        private void ShowPolSourceSiteViaPanel(object sender, EventArgs e)
        {
            PolSourceSiteTVItemID = int.Parse(((Panel)sender).Tag.ToString());
            IssueID = 0;
            CurrentPSS = subsectorDoc.Subsector.PSSList.Where(c => c.PSSTVItemID == PolSourceSiteTVItemID).FirstOrDefault();
            ReDraw();
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
