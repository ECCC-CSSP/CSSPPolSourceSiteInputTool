using System;
using System.Collections.Generic;
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
            DrawPanelPSS();
        }
        private void butRemovePicture_Click(object sender, EventArgs e)
        {
            int PictureTVItemID = int.Parse(((Button)sender).Tag.ToString());
            RemovePicture(PictureTVItemID);
        }
        private void butSaveLatLngAndObsAndAddress_Click(object sender, EventArgs e)
        {
            SavePolSourceSiteInfo();
            DrawPanelPSS();
            ReDraw();
        }
        private void butSavePictureFileName_Click(object sender, EventArgs e)
        {
            SavePictureInfo();
            DrawPanelPSS();
            ReDraw();
        }
        private void ShowPolSourceSiteViaLabel(object sender, EventArgs e)
        {
            PolSourceSiteTVItemID = int.Parse(((Label)sender).Tag.ToString());
            CurrentPSS = subsectorDoc.Subsector.PSSList.Where(c => c.PSSTVItemID == PolSourceSiteTVItemID).FirstOrDefault();
            ReDraw();
        }
        private void ShowPolSourceSiteViaPanel(object sender, EventArgs e)
        {
            PolSourceSiteTVItemID = int.Parse(((Panel)sender).Tag.ToString());
            CurrentPSS = subsectorDoc.Subsector.PSSList.Where(c => c.PSSTVItemID == PolSourceSiteTVItemID).FirstOrDefault();
            ReDraw();
        }

    }
}
